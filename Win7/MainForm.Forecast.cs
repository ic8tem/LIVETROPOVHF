using System.Drawing;
using System.Windows.Forms;
using TropoVHF.Core;
using TropoVHF.Forecast;
using TropoVHF.Ui;
using AM = Avalonia.Media;
using AC = Avalonia.Controls;

namespace TropoVHF.Win7;

/// <summary>
/// Linguetta Previsioni (sperimentale) nella versione per Windows 7: stesso motore, stessi testi, stessi fumetti e
/// stesso disegno della mappa della versione normale (file comuni), finestra in WinForms.
/// </summary>
public sealed partial class MainForm
{
    readonly OwnForecast _own = new();
    // client dedicato: i riquadri DEM 30 m sono di 15–30 MB e su linee lente superano i tempi del client generale
    static readonly HttpClient FcHttp = new HttpClient { Timeout = TimeSpan.FromMinutes(10) };
    Srtm _srtm;
    MapView _fcMap;
    AC.ControlHost _fcMapHost, _fcProfileHost;
    ProfileView _fcProfile;
    ForecastPainter _fcPaint;
    Label _fcStatus, _fcTime, _fcGtx, _fcGrx, _fcNoise, _fcCorr;
    TableLayoutPanel _fcResults;
    TextBox _fcTxLoc, _fcRxLoc, _fcLogBox;
    readonly Dictionary<string, TextBox> _fcBox = new();
    readonly Dictionary<string, Button> _fcBandBtns = new();
    CancellationTokenSource _fcCts;
    LinkResult _fcLast;
    (double[] az, double[] first, double[] far, LatLon origin)? _fcLos;
    bool _fcSyncing, _fcLogDirty;
    System.Windows.Forms.Timer _fcLogTimer;
    ToolTip _fcTips;

    static string L(string it, string en, params object[] a) => Lang.Tr(it, en, a);

    double FcFreq => _st.S.Get("fc_band", "144") switch { "432" => 432.2, "1296" => 1296.2, _ => 144.3 };
    int FcHours { get => _st.S.GetInt("fc_hours", 0); set => _st.S.SetInt("fc_hours", value); }
    DateTime FcTime { get { var n = Clock.UtcNow; var b = _own?.Hist != null ? _own.Hist.StartUtc : new DateTime(n.Year, n.Month, n.Day, n.Hour, 0, 0, DateTimeKind.Utc); return b.AddHours(FcHours); } }

    double Num(string key, double def)
    {
        if (!_fcBox.TryGetValue(key, out var tb)) return def;
        var s = (tb.Text ?? "").Trim().Replace(',', '.');
        return double.TryParse(s, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var v) ? v : def;
    }

    /// <summary>La riga in alto ha cambiato il locatore: lo si riporta nella linguetta Previsioni.</summary>
    void FcSyncLoc()
    {
        if (_fcTxLoc == null || _fcSyncing || _fcTxLoc.IsDisposed) return;
        var v = _st.Loc ?? "";
        if ((_fcTxLoc.Text ?? "").Trim().ToUpperInvariant() == v) return;
        _fcSyncing = true;
        try { _fcTxLoc.Text = v; } finally { _fcSyncing = false; }
        _fcMap?.InvalidateVisual();
    }

    void FcSetStatus(string s) => Ui(() => { if (_fcStatus != null && !_fcStatus.IsDisposed) _fcStatus.Text = s; });

    Control BuildForecastTab()
    {
        _srtm ??= new Srtm(FcHttp);
        _fcBox.Clear(); _fcBandBtns.Clear();
        _fcTips = new ToolTip { AutoPopDelay = 30000, InitialDelay = 250, ReshowDelay = 100 };
        var root = new Panel { BackColor = C(_th.Page) };
        var col = Stack(new Padding(12, 10, 12, 10));

        Add(col, Lbl(L("Previsione propria", "Own forecast"), 14f, _th.Accent, true));
        var exp = WrapLbl(L("SPERIMENTALE: i calcoli sono in fase di verifica. Usare i risultati per confrontare situazioni, non come garanzia di un collegamento. Il manuale delle previsioni spiega ogni dato.",
                            "EXPERIMENTAL: the calculations are being validated. Use the results to compare situations, not as a guarantee of a contact. The forecast manual explains every value."), 9f, _th.Bad, true);
        Add(col, exp);
        if (!P453.Available)
        {
            var p4 = WrapLbl(L("Calcolo B dei condotti spento: mancano i file ITU-R P.453 in {0} (manuale delle previsioni, § 6).",
                               "Duct calculation B off: ITU-R P.453 files missing in {0} (forecast manual, § 6).", P453.Folder), 8.5f, _th.Muted);
            Add(col, p4);
            Button dl = null;
            dl = Btn(L("Scarica le mappe P.453 dall'ITU (uso personale)", "Download P.453 maps from ITU (personal use)"), async () =>
            {
                var ok = MessageBox.Show(this,
                    L("Il programma scarica dal sito dell'ITU l'archivio dei condotti e ne estrae i file in {0}.\r\n\r\nI dati sono © ITU: le condizioni d'uso del sito permettono di scaricarli e usarli per scopi personali, educativi o non commerciali citando la fonte, non di ridistribuirli o usarli a fini commerciali senza permesso scritto. Scaricandoli accetti queste condizioni.\r\n\r\nFonte: {1}",
                      "The program downloads the duct archive from the ITU website and extracts the files into {0}.\r\n\r\nThe data are © ITU: the website terms allow personal, educational or non-commercial use with credit, not redistribution or commercial use without written permission. By downloading you accept these terms.\r\n\r\nSource: {1}", P453.Folder, P453.ItuPage),
                    "ITU-R P.453", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (ok != DialogResult.OK) return;
                dl.Enabled = false;
                try
                {
                    var (done, msg) = await Task.Run(() => P453.DownloadFromItu(FcHttp, FcSetStatus, CancellationToken.None));
                    p4.Text = msg; p4.ForeColor = C(done ? _th.Ok : _th.Bad);
                    if (done) dl.Visible = false; else dl.Enabled = true;
                }
                catch (Exception ex)
                {
                    ErrorLog.Write(ex, "scaricamento P.453");
                    var why = CalcLog.NetError(ex, "www.itu.int");
                    CalcLog.W("P.453: scaricamento non riuscito: " + why);
                    p4.Text = L("Scaricamento dall'ITU non riuscito: ", "ITU download failed: ") + why + L(". Si può scaricare a mano dalla pagina dell'ITU (manuale delle previsioni, § 6).", ". You can download it manually from the ITU page (forecast manual, § 6)."); p4.ForeColor = C(_th.Bad); dl.Enabled = true;
                }
            }, false);
            Add(col, dl);
        }

        // banda e ora
        var bands = Row();
        foreach (var b in new[] { "144", "432", "1296" })
        {
            var k = b;
            var bt = Btn(b + " MHz", () => { _st.S.Set("fc_band", k); FcBandLook(); FcRefreshGains(); _fcMap?.InvalidateVisual(); }, false);
            _fcTips.SetToolTip(bt, L("Banda della previsione e del calcolo: cambia i colori della mappa (i condotti intrappolano meglio le frequenze alte), il guadagno delle yagi e il rumore.", "Band for the forecast and the computation: changes the map colours (ducts trap higher frequencies better), the yagi gain and the noise."));
            _fcBandBtns[b] = bt; bands.Controls.Add(bt);
        }
        Add(col, bands);
        FcBandLook();
        _fcTime = Lbl("", 10f, _th.Text, true, true);
        Add(col, _fcTime);
        var sl = new TrackBar { Minimum = 0, Maximum = (OwnForecast.Hours - 6) / 3, Value = Math.Clamp(FcHours / 3, 0, (OwnForecast.Hours - 6) / 3), TickFrequency = 4, Width = 360, BackColor = C(_th.Page) };
        _fcSlider = sl; _fcHourSeen = FcNowHour();
        _fcTips.SetToolTip(sl, L("Ora della previsione: da adesso fino a circa 3 giorni avanti, a passi di 3 ore. L'inizio segue l'orologio: allo scoccare dell'ora la mappa resta sull'ora che stavi guardando e il cursore si sposta.", "Forecast hour: from now up to about 3 days ahead, in 3-hour steps. The start follows the clock: when the hour turns, the map stays on the hour you were viewing and the slider moves."));
        sl.ValueChanged += (_, _) => { if (_fcHourSync) return; FcHours = sl.Value * 3; FcTimeLabel(); FcSyncOverlay(); _fcMap?.InvalidateVisual(); };
        Add(col, sl);
        FcTimeLabel();

        FcSourceBox(col);
        _fcCorr = WrapLbl("", 8.5f, _th.Muted);
        _fcTips.SetToolTip(_fcCorr, L("Correlazione di Pearson fra guadagno stimato e pressione, e fra guadagno e inversione, sulla zona visibile. Manuale delle previsioni, § 5.", "Pearson correlation between gain and pressure, and gain and inversion, over the visible area. Forecast manual, § 5."));
        _fcCorr.Visible = _st.S.GetBool("fc_show_corr", true);
        Add(col, _fcCorr);

        var upd = Row();
        upd.Controls.Add(TipW(Btn(L("Aggiorna previsione (zona visibile)", "Update forecast (visible area)"), FcUpdateGrid, true), L("Scarica i profili verticali del modello scelto per la zona visibile della mappa e ricalcola i colori. Ci vuole da qualche secondo a un minuto.", "Downloads the vertical profiles of the chosen model for the visible map area and recomputes the colours. Takes from a few seconds to a minute.")));
        upd.Controls.Add(TipW(Btn("Stop", () => _fcCts?.Cancel(), false), L("Interrompe lo scaricamento o il calcolo in corso.", "Stops the download or computation in progress.")));
        var auto = Chk(L("Aggiorna da sola", "Update by itself"), FcAuto, v => FcAuto = v);
        _fcTips.SetToolTip(auto, L("Scarica la previsione senza clic: all'apertura, a ogni nuova corsa GFS su NOMADS e quando la zona visibile esce dalla griglia. Mai con un caso storico.", "Downloads the forecast without clicking: at start, at each new GFS run on NOMADS and when the visible area leaves the grid. Never with a historical case."));
        upd.Controls.Add(auto);
        Add(col, upd);
        _fcStatus = WrapLbl(_own.Status == "" ? L("Previsione propria non ancora scaricata.", "Own forecast not downloaded yet.") : _own.Status, 9f, _th.Sub);
        Add(col, _fcStatus);

        // stazioni
        TextBox Field(string key, string def, int w = 80)
        {
            var t = Box(_st.S.Get(key, def), w);
            t.TextChanged += (_, _) => { _st.S.Set(key, (t.Text ?? "").Trim()); _st.Dirty = true; if (key.Contains("boom")) FcRefreshGains(); else if (key.StartsWith("fc_rx_") || key == "fc_bw_ssb") FcRefreshNoise(); };
            _fcBox[key] = t;
            return t;
        }
        Control FRow(string label, Control c, Control extra = null)
        {
            var r = Row(); r.WrapContents = false;
            var l = Lbl(label, 8.5f, _th.Muted); l.AutoSize = false; l.Width = 230; l.Height = 34; l.TextAlign = ContentAlignment.MiddleLeft;
            r.Controls.Add(l); r.Controls.Add(c);
            if (extra != null) r.Controls.Add(extra);
            return r;
        }
        var (meSec, me) = Section(L("La mia stazione (trasmette)", "My station (transmitting)"), 0);
        _fcTxLoc = Box(_st.Loc, 120); _fcTxLoc.MaxLength = 10; _fcTxLoc.CharacterCasing = CharacterCasing.Upper;
        _fcTxLoc.TextChanged += (_, _) =>
        {
            if (_fcSyncing) return;
            var v = (_fcTxLoc.Text ?? "").Trim().ToUpperInvariant();
            if (v == (_st.Loc ?? "")) return;
            _st.Loc = v; _st.Dirty = true;
            _fcSyncing = true;
            try { if (_locBox != null && (_locBox.Text ?? "").Trim().ToUpperInvariant() != v) _locBox.Text = v; } finally { _fcSyncing = false; }
            _fcMap?.InvalidateVisual(); _map.InvalidateVisual();
        };
        Add(me, FRow(L("Locatore (4–10 caratteri; 8–10 = punto preciso su DEM 30 m)", "Locator (4–10 chars; 8–10 = exact point on DEM 30 m)"), _fcTxLoc));
        Add(me, FRow(L("Antenna dal suolo, m (locatore 8–10)", "Antenna above ground, m (8–10 locator)"), Field("fc_tx_agl", "10")));
        Add(me, FRow(L("Antenna sul mare, m (locatore 4–6: fondamentale)", "Antenna above sea, m (4–6 locator: required)"), Field("fc_tx_asl", "0")));
        Add(me, FRow(L("Potenza, W", "Power, W"), Field("fc_tx_w", "100")));
        Add(me, FRow(L("Perdita del cavo, dB", "Cable loss, dB"), Field("fc_tx_cable", "1")));
        _fcGtx = Lbl("", 9f, _th.Accent, true);
        Add(me, FRow(L("Yagi DL6WU: boom, m", "DL6WU Yagi: boom, m"), Field("fc_tx_boom", "2"), _fcGtx));
        Add(col, meSec);

        var (rxSec, rx) = Section(L("Corrispondente (riceve)", "Other station (receiving)"), 1);
        _fcRxLoc = Field("fc_rx_loc", "", 120); _fcRxLoc.MaxLength = 10; _fcRxLoc.CharacterCasing = CharacterCasing.Upper;
        Add(rx, FRow(L("Locatore (o clic sulla mappa)", "Locator (or click the map)"), _fcRxLoc));
        Add(rx, FRow(L("Antenna dal suolo, m", "Antenna above ground, m"), Field("fc_rx_agl", "3")));
        Add(rx, FRow(L("Antenna sul mare, m (locatore 4–6; 0 = suolo SRTM + altezza)", "Antenna above sea, m (4–6 locator; 0 = SRTM ground + height)"), Field("fc_rx_asl", "0")));
        _fcGrx = Lbl("", 9f, _th.Accent, true);
        Add(rx, FRow(L("Yagi DL6WU: boom, m", "DL6WU Yagi: boom, m"), Field("fc_rx_boom", "2"), _fcGrx));
        Add(rx, FRow(L("Perdita del cavo, dB", "Cable loss, dB"), Field("fc_rx_cable", "1")));
        Add(rx, FRow(L("Cifra di rumore del ricevitore, dB", "Receiver noise figure, dB"), Field("fc_rx_nf", "2")));
        var envs = new[] { ("quiet", L("rurale tranquillo", "quiet rural")), ("rural", L("rurale", "rural")), ("residential", L("residenziale", "residential")), ("city", L("città", "city")), ("manual", L("temperatura d'antenna a mano", "manual antenna temperature")) };
        var cur = _st.S.Get("fc_rx_env", "quiet");
        var envBox = Combo(envs.Select(e => e.Item2), Math.Max(0, Array.FindIndex(envs, e => e.Item1 == cur)), 200, i => { _st.S.Set("fc_rx_env", envs[i].Item1); _st.Dirty = true; FcRefreshNoise(); });
        Add(rx, FRow(L("Ambiente di rumore (ITU-R P.372)", "Noise environment (ITU-R P.372)"), envBox));
        Add(rx, FRow(L("Temperatura d'antenna a mano, K", "Manual antenna temperature, K"), Field("fc_rx_ta", "300")));
        _fcNoise = WrapLbl("", 8.5f, _th.Muted);
        Add(rx, _fcNoise);
        Add(rx, FRow(L("SSB: banda Hz / S/N minimo dB", "SSB: bandwidth Hz / min S/N dB"), Field("fc_bw_ssb", "2500", 70), Field("fc_req_ssb", "6", 50)));
        Add(rx, FRow(L("CW: banda Hz / S/N minimo dB", "CW: bandwidth Hz / min S/N dB"), Field("fc_bw_cw", "500", 70), Field("fc_req_cw", "0", 50)));
        Add(rx, FRow(L("FT8: banda Hz / S/N minimo dB", "FT8: bandwidth Hz / min S/N dB"), Field("fc_bw_ft8", "2500", 70), Field("fc_req_ft8", "-21", 50)));
        Add(col, rxSec);
        FcRefreshGains();

        var act = Row();
        act.Controls.Add(TipW(Btn(L("Calcola il segnale", "Compute signal"), FcComputeLink, true), L("Calcola il segnale fra la tua stazione e il corrispondente: profilo del terreno, strati lungo la tratta, perdita prevista, S/N per modo e puntamento consigliato. Manuale delle previsioni, § 7–12.", "Computes the signal between your station and the other one: terrain profile, layers along the path, predicted loss, S/N per mode and recommended heading. Forecast manual, § 7–12.")));
        act.Controls.Add(TipW(Btn(L("Copertura in ottica (DEM 30 m)", "Line-of-sight coverage (DEM 30 m)"), FcComputeLos, false), L("Disegna in azzurro fino a dove la tua antenna si vede in ottica (prima ombra) e con la linea tratteggiata il punto visibile più lontano, sul terreno DEM 30 m.", "Draws in light blue how far your antenna is in line of sight (first shadow) and with a dashed line the farthest visible point, on 30 m DEM terrain.")));
        Add(col, act);
        // cosa mostrare: i calcoli restano tutti, si sceglie solo cosa vedere
        ProfileView.Flat = _st.S.GetBool("fc_flat", true);
        _fcProfile = new ProfileView { Th = _th };
        _fcProfileHost = new AC.ControlHost(_fcProfile) { Height = 190, BackColor = C(_th.MapSea), Margin = new Padding(3, 6, 3, 6) };
        Add(col, _fcProfileHost);
        _fcResults = Stack(new Padding(0));
        Add(col, _fcResults);
        if (_fcLast != null) FcShowResult(_fcLast); else Add(_fcResults, Lbl(L("Nessun calcolo ancora.", "No computation yet."), 9f, _th.Muted));

        // legenda
        Add(col, Lbl(L("Legenda", "Legend"), 10f, _th.Text, true));
        var leg = Row();
        leg.Controls.Add(Lbl(L("sotto 7 dB nessun colore", "below 7 dB no colour"), 8.5f, _th.Muted));
        foreach (var bnd in Palette.GainBands)
        {
            leg.Controls.Add(new Panel { Width = 18, Height = 12, BackColor = Color.FromArgb((int)(255 * Palette.GainAlpha), bnd.C.R, bnd.C.G, bnd.C.B), Margin = new Padding(6, 7, 2, 3) });
            leg.Controls.Add(Lbl(bnd.Hi > 100 ? $"≥ {bnd.Lo:0.#} dB" : $"{bnd.Lo:0.#}–{bnd.Hi:0.#} dB", 8.5f, _th.Muted));
        }
        Add(col, leg);
        Add(col, WrapLbl(L("Colori: guadagno stimato in dB (base della mappa, sempre visibile). Quadretti: nessuna previsione propria, Hepburn/F5LEN ≥ 5. Area azzurra: copertura in ottica fino alla prima ombra. " +
                           "Condotti potenziali: doppio calcolo (A dal GFS, B con lo spessore climatologico ITU-R P.453, © ITU) e si usa il peggiore. I valori del segnale sono stime.",
                           "Colours: estimated gain in dB (map base, always shown). Checkered: no own forecast, Hepburn/F5LEN ≥ 5. Light-blue area: line of sight. " +
                           "Potential ducts: two calculations (A from GFS, B with ITU-R P.453 thickness, © ITU), the worst is used. Signal values are estimates."), 8.5f, _th.Muted));

        // registro dei calcoli (a capo automatico)
        _fcLogCtl.Clear();
        var logHead = Lbl(L("Registro dei calcoli", "Computation log"), 10f, _th.Text, true);
        Add(col, logHead); _fcLogCtl.Add(logHead);
        CalcLog.Enabled = _st.S.GetBool("fc_log", true);
        var lr = Row();
        lr.Controls.Add(TipW(Btn(L("Apri il file", "Open file"), () => { try { System.Diagnostics.Process.Start(CalcLog.FilePath); } catch (Exception ex) { FcSetStatus(ex.Message); } }, false), L("Apre il file del registro dei calcoli con il programma predefinito.", "Opens the computation log file with the default program.")));
        lr.Controls.Add(TipW(Btn(L("Svuota la finestra", "Clear window"), () => { CalcLog.Clear(); _fcLogDirty = true; }, false), L("Svuota la finestra del registro; il file resta com'è.", "Clears the log window; the file is kept.")));
        Add(col, lr); _fcLogCtl.Add(lr);
        var logPath = WrapLbl(CalcLog.FilePath, 8f, _th.Muted);
        Add(col, logPath); _fcLogCtl.Add(logPath);
        _fcLogBox = new TextBox
        {
            Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, WordWrap = true, Height = Math.Max(120, _st.S.GetInt("fc_log_h", 320)),
            Font = new Font(MonoFamily, 8.5f), BackColor = C(_th.CtlBg), ForeColor = C(_th.Text), BorderStyle = BorderStyle.FixedSingle, Text = CalcLog.RecentText()
        };
        Add(col, _fcLogBox); _fcLogCtl.Add(_fcLogBox);
        foreach (var c in _fcLogCtl) c.Visible = _st.S.GetBool("fc_logshow", false);
        CalcLog.Line -= FcOnLogLine; CalcLog.Line += FcOnLogLine;
        if (_fcLogTimer == null)
        {
            _fcLogTimer = new System.Windows.Forms.Timer { Interval = 700 };
            _fcLogTimer.Tick += (_, _) =>
            {
                if (!_fcLogDirty || _fcLogBox == null || _fcLogBox.IsDisposed || !_fcLogBox.Visible) return;
                _fcLogDirty = false;
                _fcLogBox.Text = CalcLog.RecentText();
                _fcLogBox.SelectionStart = _fcLogBox.TextLength; _fcLogBox.ScrollToCaret();
            };
            _fcLogTimer.Start();
        }

        var left = ScrollBox(col, C(_th.Panel));
        left.Dock = DockStyle.Left; left.Width = Math.Clamp(_st.S.GetInt("fc_panel_w", 460), 320, 1100);
        // pannello largo a piacere: si trascina il bordo
        var split = new Splitter { Dock = DockStyle.Left, Width = 6, BackColor = C(_th.Line), MinSize = 320 };
        split.SplitterMoved += (_, _) => { _st.S.SetInt("fc_panel_w", left.Width); _st.Dirty = true; };

        _fcMap = new MapView(_st, _th) { PreviewSource = "own" };
        _fcPaint = new ForecastPainter
        {
            St = _st, Own = _own, Map = _fcMap, Th = _th, Time = () => FcTime, Freq = () => FcFreq,
            TxLoc = () => _fcTxLoc?.Text, RxLoc = () => _fcRxLoc?.Text,
            ReqSsb = () => Num("fc_req_ssb", 6), ReqCw = () => Num("fc_req_cw", 0), ReqFt8 = () => Num("fc_req_ft8", -21)
        };
        _fcPaint.CorrText += t => Ui(() => { if (_fcCorr != null && !_fcCorr.IsDisposed) _fcCorr.Text = t; });
        _fcMap.OwnLayer = ctx => { _fcPaint.Last = _fcLast; _fcPaint.Los = _st.S.GetBool("fc_show_los", true) ? _fcLos : null; _fcPaint.DrawLayer(ctx); };
        _fcMap.OwnOverlay = ctx => _fcPaint.DrawOverlay(ctx);
        FcSyncOverlay();
        if (LocatorX.TryParse(_st.S.Get("fc_rx_loc", ""), out var rp)) _fcMap.Probe = rp;
        _fcMap.SelectionChanged += () => { if (_fcMap.Probe is LatLon p) Ui(() => _fcRxLoc.Text = LocatorX.From(p, 10)); };
        _fcMapHost = new AC.ControlHost(_fcMap) { Dock = DockStyle.Fill, BackColor = C(_th.MapSea) };
        _fcMapHost.RenderError += ex => ErrorLog.Write(ex, "disegno della mappa Previsioni");

        root.Controls.Add(_fcMapHost);
        var right = ScrollBox(FcDetailsPanel(), C(_th.Panel));
        right.Dock = DockStyle.Right; right.Width = 300;
        root.Controls.Add(right);
        root.Controls.Add(split);
        root.Controls.Add(left);
        return root;
    }

    /// <summary>Riquadro «Dettagli da mostrare» a destra della mappa (bozzetto approvato alla lettera, 22/11/2026).</summary>
    TableLayoutPanel FcDetailsPanel()
    {
        var tips = new Dictionary<string, string>
        {
            { "fc_ov_curves", L("Curve a 7, 10, 13, 16, 18,5 e 21 dB sul campo levigato. Manuale delle previsioni, § 5.", "Contours at 7, 10, 13, 16, 18.5 and 21 dB on the smoothed field. Forecast manual, § 5.") },
            { "fc_ov_low", L("Tratteggio grigio leggero fra 3 e 7 dB; i colori non cambiano. Manuale delle previsioni, § 4.", "Light grey hatch for 3–7 dB; colours unchanged. Forecast manual, § 4.") },
            { "fc_ov_iso", L("Pressione al livello del mare ogni 4 hPa dal GFS (Open-Meteo). Manuale delle previsioni, § 5.", "Sea-level pressure every 4 hPa from GFS (Open-Meteo). Forecast manual, § 5.") },
            { "fc_ov_inv", L("Quanto l'aria a 925 o 850 hPa è più calda di quella a 2 m (1, 3, 5, 8 °C). Manuale delle previsioni, § 5.", "How much warmer air at 925 or 850 hPa is than at 2 m (1, 3, 5, 8 °C). Forecast manual, § 5.") },
            { "fc_ov_wx", L("↓ verde = subsidenza, FAVOREVOLE (+1,5 dB); ↑ rosso = aria in salita, SFAVOREVOLE (−2 dB); S arancio = scirocco, indizio favorevole. Passa con il mouse su un segno per il fumetto. Manuale delle previsioni, § 9 bis.", "green ↓ = subsidence, GOOD (+1.5 dB); red ↑ = rising air, BAD (−2 dB); orange S = scirocco, good sign. Hover a mark for the bubble. Forecast manual, § 9 bis.") },
            { "fc_show_corr", L("Correlazione di Pearson fra guadagno stimato e pressione, e fra guadagno e inversione, sulla zona visibile. Manuale delle previsioni, § 5.", "Pearson correlation between gain and pressure, and gain and inversion, over the visible area. Forecast manual, § 5.") },
            { "fc_flat", L("Acceso: profilo a terra piatta, le montagne risaltano e il raggio diretto curva verso il basso. Spento: disegno classico, il terreno sale con la curvatura della terra e il raggio è dritto. Il calcolo è lo stesso.", "On: flat-earth profile, mountains stand out and the direct ray curves down. Off: classic drawing, terrain rises with the earth's curvature and the ray is straight. The computation is the same.") },
            { "fc_full", L("Spento: solo la sintesi (distanza, strato, guadagno, perdita, segnale, S/N, modi, probabilità, puntamento). Acceso: tutte le righe del calcolo fra i due locatori. I calcoli sono sempre completi.", "Off: summary only (distance, layer, gain, loss, signal, S/N, modes, probability, heading). On: every line of the computation between the two locators. The computations are always complete.") },
            { "fc_logshow", L("Mostra o nasconde la finestra del registro dei calcoli. Il registro continua a essere scritto nel file anche quando è nascosto.", "Shows or hides the computation log window. The log keeps being written to the file even when hidden.") },
            { "fc_log", L("Scrive ogni passo dei calcoli nel registro e nel file. Spento, i calcoli sono identici ma non lasciano traccia.", "Writes every step of the computations to the log and the file. Off, the computations are identical but leave no trace.") },
            { "fc_corr", L("Termine di stabilità di Misme (solo senza condotto, da −4 a +6 dB, taratura provvisoria) e moto verticale: +1,5 dB con subsidenza, −2 dB con aria in salita. Manuale delle previsioni, § 9 bis.", "Misme stability term (only without a duct, −4 to +6 dB, provisional) and vertical motion: +1.5 dB with subsidence, −2 dB with rising air. Forecast manual, § 9 bis.") },
            { "fc_via_ideal", L("Quando il corrispondente è chiuso da un rilievo vicino, il giro d'orizzonte cerca il punto migliore da cui rilanciare il segnale. Con questa casella la perdita fra la tua stazione e il punto è considerata nulla: è come portare la stazione sulla cima (antenna a 10 m, stessa potenza e antenna) e conta solo la tratta dal punto al corrispondente. Il punto è dato con il locatore a 10 caratteri. Limite ottimistico. Manuale delle previsioni, § 10 bis.", "When the other station is blocked by nearby terrain, the horizon sweep looks for the best point to relaunch the signal. With this box the loss between your station and the point is taken as zero: as if you moved the station to the summit (antenna 10 m, same power and antenna); only the leg from the point to the other station counts. The point is given with a 10-character locator. Optimistic bound. Forecast manual, § 10 bis.") },
            { "fc_seaair", L("Sul mare confronta l'aria con la temperatura della superficie del mare (SST). Aria a 925 hPa più calda del mare (temperatura potenziale oltre la SST di 4 K: +1 dB; di 7 K: +2 dB; −0,5 dB con aria umida): inversione d'avvezione, condotto probabile. Mare raffreddato di 1,5 °C o più nelle 72 ore prima (dalle analisi Open-Meteo): +1 dB, tipico dei mari bassi come l'alto Adriatico dopo la bora. Mare più caldo dell'aria a 2 m di 2 °C: −1,5 dB; di 4 °C: −3 dB, perché l'aria si rimescola. Soglie provvisorie, da tarare con gli spot. Manuale delle previsioni, § 9 quater.", "Over the sea it compares the air with the sea surface temperature (SST). Air at 925 hPa warmer than the sea (potential temperature 4 K above SST: +1 dB; 7 K: +2 dB; −0.5 dB with humid air): advection inversion, duct likely. Sea cooled by 1.5 °C or more in the previous 72 hours (Open-Meteo analyses): +1 dB, typical of shallow seas such as the northern Adriatic after a bora. Sea warmer than the 2 m air by 2 °C: −1.5 dB; by 4 °C: −3 dB, because the air mixes. Provisional thresholds, to be tuned with spots. Forecast manual, § 9 quater.") },
            { "fc_nowcast", L("Corridoi d'apertura: almeno 3 coppie di stazioni indipendenti su tratte parallele (±15°, entro 150 km dall'asse) di almeno 300 km negli ultimi 30 minuti, ripetute a distanza di 10 minuti (un collegamento isolato può essere un aereo). Fonti: cluster, PSKReporter, QSO e decodifiche, salti APRS in 2 m. Tratte oltre 1500 km solo se confermate da due fonti; non contano le stazioni che in un'ora sentono stazioni lontane da tutte le direzioni (ricevitori configurati male). Vicino alla parte centrale delle tratte (±60 km) la previsione dell'ora attuale non scende sotto un minimo da 3 a 16 dB, secondo lunghezza (sotto i 600 km è quasi sempre troposcatter normale), numero di tratte e S/N, e le curve non fanno gocce. Manuale delle previsioni, § 4 bis.", "Opening corridors: at least 3 independent station pairs on parallel paths (±15°, within 150 km of the axis) of at least 300 km in the last 30 minutes, repeated 10 minutes apart (a single contact may be an aircraft). Sources: clusters, PSKReporter, QSOs and decodes, APRS 2 m hops. Paths over 1500 km only if confirmed by two sources; stations hearing distant stations from all directions within an hour (misconfigured receivers) are ignored. Near the middle part of the paths (±60 km) the current-hour forecast does not drop below a floor of 3 to 16 dB, by length (below 600 km it is almost always normal troposcatter), number of paths and S/N, and the contours make no drops. Forecast manual, § 4 bis.") },
            { "aprs_on", L("APRS-IS in sola lettura (rotate.aprs2.net, filtro sull'Europa): salti radio di almeno 300 km fra stazioni di posizione nota, in 2 m, LoRa 433 MHz (banda 432) e LoRa 868 MHz (conta per i 1296), sempre fra stazioni della stessa banda. Ogni salto è fra due trasmettitori veri: si scartano i salti attraverso digipeater che non mettono il nominativo, i pacchetti con il percorso consumato rispetto al solito (ritrasmessi di nascosto), i salti con un digipeater attivo a metà strada, i pacchetti rigirati da internet, HF e stazioni in quota. Un salto conta solo se si ripete. Idea dalla mappa VHF di DXView e da APRS PropView (RF-YVY).", "Read-only APRS-IS (rotate.aprs2.net, Europe filter): radio hops of at least 300 km between stations of known position, on 2 m, LoRa 433 MHz (432 band) and LoRa 868 MHz (counts for 1296), always between stations on the same band. Each hop is between two real transmitters: hops through digipeaters that don't insert their call, packets whose path was consumed compared to usual (hidden retransmission), hops with an active digipeater halfway, packets gated from the internet, HF and airborne stations are discarded. A hop counts only if it repeats. Idea from the DXView VHF map and APRS PropView (RF-YVY).") },
            { "fc_show_los", FcDetails.LosTip },
        };
        var boxes = new Dictionary<string, CheckBox>();
        var col = Stack(new Padding(10));
        Add(col, Lbl(FcDetails.Title, 10f, _th.Text, true));
        var seg = Row();
        var btns = new Button[3];
        int cur = Math.Clamp(_st.S.GetInt("fc_detail_level", 1), 0, 2);
        void Look(int k)
        {
            for (int j = 0; j < 3; j++)
            {
                btns[j].BackColor = j == k ? C(_th.Accent) : C(_th.CtlBg);
                btns[j].ForeColor = j == k ? C(_th.AccentFg) : C(_th.Text2);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            int k = i;
            btns[i] = Btn(FcDetails.Levels[i], () =>
            {
                _st.S.SetInt("fc_detail_level", k); _st.Dirty = true; Look(k);
                foreach (var it in FcDetails.All) boxes[it.Key].Checked = k >= it.Level;
            }, false);
            seg.Controls.Add(btns[i]);
        }
        Look(cur);
        Add(col, seg);
        Add(col, WrapLbl(FcDetails.Help, 8.5f, _th.Muted));
        for (int gi = 0; gi < 3; gi++)
        {
            Add(col, Lbl(FcDetails.Groups[gi], 10f, _th.Text, true));
            foreach (var it in FcDetails.All)
            {
                if (it.Group != gi) continue;
                var key = it.Key;
                var cb = Chk(L(it.It, it.En), _st.S.GetBool(key, FcDetails.Default(key)), v => FcApplyDetail(key, v));
                if (tips.TryGetValue(key, out var tip)) _fcTips.SetToolTip(cb, tip);
                boxes[key] = cb;
                var r = Row();
                r.Controls.Add(cb);
                r.Controls.Add(Lbl(FcDetails.Short[it.Level], 8f, _th.Muted));
                Add(col, r);
            }
        }
        return col;
    }

    void FcApplyDetail(string key, bool v)
    {
        _st.S.SetBool(key, v); _st.Dirty = true;
        switch (key)
        {
            case "fc_show_corr": if (_fcCorr != null && !_fcCorr.IsDisposed) _fcCorr.Visible = v; break;
            case "fc_full": if (_fcLast != null) FcShowResult(_fcLast); break;
            case "fc_flat": ProfileView.Flat = v; _fcProfile?.InvalidateVisual(); _fcProfileHost?.Invalidate(); break;
            case "fc_logshow": foreach (var c in _fcLogCtl) c.Visible = v; break;
            case "fc_log": CalcLog.Enabled = v; break;
            case "fc_corr": Refractivity.UseCorrections = v; break;
            case "fc_seaair": Refractivity.UseSeaAir = v; break;
            case "fc_via_ideal": Refractivity.ViaIdeal = v; break;
            case "fc_nowcast": _st.NowcastOn = v; break;
            case "aprs_on": _st.AprsOn = v; if (v) _st.Aprs.Start(); else _st.Aprs.Stop(); break;
        }
        FcSyncOverlay(); _fcMap?.InvalidateVisual();
    }

    void FcOnLogLine(string _) => _fcLogDirty = true;

    void FcBandLook()
    {
        var cur = _st.S.Get("fc_band", "144");
        foreach (var kv in _fcBandBtns)
        {
            bool on = kv.Key == cur;
            kv.Value.BackColor = on ? C(_th.Accent) : C(_th.CtlBg);
            kv.Value.ForeColor = on ? C(_th.AccentFg) : C(_th.Text2);
        }
    }

    void FcSyncOverlay()
    {
        if (_fcMap == null) return;
        _fcMap.OwnIso = _st.S.GetBool("fc_ov_iso", false);
        _fcMap.OwnTime = FcTime;
    }

    void FcTimeLabel()
    {
        if (_fcTime == null) return;
        var t = FcTime;
        _fcTime.Text = $"{Lang.DayNames()[(int)t.DayOfWeek]} {(_own.Hist != null ? t.ToString("dd/MM/yyyy HH") : t.ToString("dd/MM HH"))}:00 UTC (+{FcHours} h)";
    }

    void FcRefreshNoise()
    {
        if (_fcNoise == null) return;
        var (tg, tm, ta) = LinkCalc.AntennaTemp(FcFreq, _st.S.Get("fc_rx_env", "quiet"), Num("fc_rx_ta", 300));
        double tsys = LinkCalc.SystemTempK(ta, Num("fc_rx_cable", 1), Num("fc_rx_nf", 2));
        _fcNoise.Text = (_st.S.Get("fc_rx_env", "quiet") == "manual" ? $"Ta {ta:0} K" : L("galattico", "galactic") + $" {tg:0} K + " + L("artificiale", "man-made") + $" {tm:0} K = Ta {ta:0} K") +
                        $" · Tsys {tsys:0} K · " + L("rumore SSB", "SSB noise") + $" {LinkCalc.NoiseDbm(tsys, Num("fc_bw_ssb", 2500)):0.0} dBm";
    }

    void FcRefreshGains()
    {
        FcRefreshNoise();
        if (_fcGtx == null) return;
        double f = FcFreq;
        _fcGtx.Text = $"{Dl6wu.GainDbi(Num("fc_tx_boom", 2), f):0.0} dBi ({299.792458 / f:0.00} m λ)";
        _fcGrx.Text = $"{Dl6wu.GainDbi(Num("fc_rx_boom", 2), f):0.0} dBi";
    }

    /// <summary>Sorgente dei dati, caso storico e correzioni meteo della 1.3.0.</summary>
    void FcSourceBox(TableLayoutPanel col)
    {
        OwnForecast.Source = WxSources.Parse(_st.S.Get("fc_source", "Auto"));
        Refractivity.UseCorrections = _st.S.GetBool("fc_corr", true);
        Refractivity.ViaIdeal = _st.S.GetBool("fc_via_ideal", false);
        Refractivity.UseSeaAir = _st.S.GetBool("fc_seaair", true);
        var r1 = Row();
        var l1 = Lbl(L("Sorgente dei dati", "Data source"), 8.5f, _th.Muted); l1.AutoSize = false; l1.Width = 120; l1.Height = 28; l1.TextAlign = ContentAlignment.MiddleLeft;
        var src = Combo(WxSources.All.Select(WxSources.Name), Math.Max(0, Array.IndexOf(WxSources.All, OwnForecast.Source)), 320, i =>
        {
            OwnForecast.Source = WxSources.All[i];
            _st.S.Set("fc_source", OwnForecast.Source.ToString()); _st.Dirty = true;
            FcSetStatus(L("Sorgente scelta: ", "Source: ") + WxSources.Name(OwnForecast.Source) + L(". Premi «Aggiorna previsione» per scaricare la griglia.", ". Press «Update forecast» to download the grid."));
        });
        _fcTips.SetToolTip(src, L("Automatica (consigliata): GFS prima da NOMADS (NOAA, file GRIB2 della zona, nessun limite giornaliero), poi da Open-Meteo per i punti che mancano: è lo stesso modello e la stessa corsa. ICON-EU (solo Europa) ed ECMWF valgono per tutta la griglia: modelli diversi non si mescolano. Manuale delle previsioni, § 3 bis.",
                                  "Automatic (recommended): GFS first from NOMADS (NOAA, GRIB2 files of the area, no daily limit), then from Open-Meteo for any missing points: same model and run. ICON-EU (Europe only) and ECMWF apply to the whole grid: different models are not mixed. Forecast manual, § 3 bis."));
        r1.Controls.Add(l1); r1.Controls.Add(src);
        Add(col, r1);

        var r2 = Row();
        var l2 = Lbl(L("Caso storico", "Historical case"), 8.5f, _th.Muted); l2.AutoSize = false; l2.Width = 120; l2.Height = 28; l2.TextAlign = ContentAlignment.MiddleLeft;
        var cases = new List<string> { L("previsione attuale", "current forecast") };
        cases.AddRange(HistCase.Known.Select(h => h.Name));
        var hc = Combo(cases, _own.Hist == null ? 0 : Array.IndexOf(HistCase.Known, _own.Hist) + 1, 320, i =>
        {
            _own.Hist = i == 0 ? null : HistCase.Known[i - 1];
            if (_own.Hist != null)
            {
                var h = _own.Hist;
                _fcMap?.ShowBox(h.LatMin, h.LatMax, h.LonMin, h.LonMax);
                _st.S.Set("fc_band", h.FreqMHz >= 1000 ? "1296" : h.FreqMHz >= 400 ? "432" : "144");
                FcBandLook(); FcRefreshGains();
                FcSetStatus(L("Caso storico: ", "Historical case: ") + h.Name + " — " + h.Note + L(". Premi «Aggiorna previsione» per scaricare i profili d'archivio.", ". Press «Update forecast» to download the archive profiles."));
            }
            else FcSetStatus(L("Previsione attuale: premi «Aggiorna previsione».", "Current forecast: press «Update forecast»."));
            FcTimeLabel(); _fcMap?.InvalidateVisual();
        });
        _fcTips.SetToolTip(hc, L("Rivede un'apertura documentata (QST) con le previsioni GFS d'archivio di Open-Meteo. Manuale delle previsioni, § 3 ter.", "Replays a documented opening (QST) with Open-Meteo archived GFS forecasts. Forecast manual, § 3 ter."));
        r2.Controls.Add(l2); r2.Controls.Add(hc);
        Add(col, r2);

    }

    bool FcAuto { get => _st.S.GetBool("fc_auto", true); set => _st.S.SetBool("fc_auto", value); }
    int _fcAutoTick;
    /// <summary>Chiamata ogni secondo dal timer: ogni 30 s controlla se serve aggiornare la previsione (1.3.2).</summary>
    TrackBar _fcSlider;
    readonly List<Control> _fcLogCtl = new List<Control>();
    DateTime _fcHourSeen; bool _fcHourSync;
    T TipW<T>(T c, string tip) where T : Control { _fcTips.SetToolTip(c, tip); return c; }
    static DateTime FcNowHour() { var n = Clock.UtcNow; return new DateTime(n.Year, n.Month, n.Day, n.Hour, 0, 0, DateTimeKind.Utc); }

    /// <summary>L'inizio del cursore dell'ora è l'ora attuale: allo scoccare dell'ora la mappa resta sull'ora che si stava guardando.</summary>
    void FcClockTick()
    {
        if (_own?.Hist != null || _fcSlider == null || _fcSlider.IsDisposed) return;
        var h = FcNowHour();
        if (_fcHourSeen == default(DateTime)) { _fcHourSeen = h; return; }
        if (h == _fcHourSeen) return;
        int passed = (int)Math.Round((h - _fcHourSeen).TotalHours);
        _fcHourSeen = h;
        FcHours = Math.Max(0, FcHours - Math.Max(0, passed));
        _fcHourSync = true;
        try { _fcSlider.Value = Math.Clamp(FcHours / 3, _fcSlider.Minimum, _fcSlider.Maximum); } finally { _fcHourSync = false; }
        FcTimeLabel(); FcSyncOverlay(); _fcMap?.InvalidateVisual();
    }

    void FcAutoCheck()
    {
        FcClockTick();
        if (++_fcAutoTick % 30 != 0 || !FcAuto || _fcMap == null) return;
        var (la1, la2, lo1, lo2) = _fcMap.Box;
        if (la2 - la1 < 0.5 || lo2 - lo1 < 0.5) return;      // mappa non ancora disposta
        if (_own.NeedsAutoUpdate(la1, la2, lo1, lo2)) { CalcLog.W("aggiornamento automatico della previsione"); FcUpdateGrid(); }
    }

    void FcUpdateGrid()
    {
        if (_fcMap == null) return;
        if (_own.Busy) { FcSetStatus(L("Scaricamento già in corso: ", "Download already running: ") + _own.Status + L(" (per fermarlo: Stop)", " (to stop it: Stop)")); return; }
        _fcCts?.Cancel(); _fcCts = new CancellationTokenSource();
        var ct = _fcCts.Token;
        var (la1, la2, lo1, lo2) = _fcMap.Box;
        if (_own.Hist != null) { la1 = _own.Hist.LatMin; la2 = _own.Hist.LatMax; lo1 = _own.Hist.LonMin; lo2 = _own.Hist.LonMax; }
        CalcLog.Section("Aggiornamento della previsione propria");
        CalcLog.W($"zona visibile {la1:0.##}–{la2:0.##} N, {lo1:0.##}–{lo2:0.##} E");
        Task.Run(async () =>
        {
            try { await _own.UpdateAsync(FcHttp, la1, la2, lo1, lo2, () => { FcSetStatus(_own.Status); Ui(() => _fcMap?.InvalidateVisual()); }, ct); }
            catch (Exception ex) { ErrorLog.Write(ex, "previsione propria (griglia)"); FcSetStatus(ex.Message); }
        });
    }

    bool FcStations(out Station tx, out Station rx, out string err)
    {
        tx = rx = null; err = "";
        var tl = (_fcTxLoc.Text ?? "").Trim(); var rl = (_fcRxLoc.Text ?? "").Trim();
        if (!LocatorX.TryParse(tl, out var tp)) { err = L("Locatore della mia stazione non valido (4, 6, 8 o 10 caratteri).", "Invalid own locator (4, 6, 8 or 10 chars)."); return false; }
        if (!LocatorX.TryParse(rl, out var rpos)) { err = L("Locatore del corrispondente non valido: scrivilo o fai clic sulla mappa.", "Invalid other locator: type it or click the map."); return false; }
        tx = new Station { Loc = tl, Pos = tp, Precise = tl.Length >= 8, AglM = Num("fc_tx_agl", 10), AslM = Num("fc_tx_asl", 0), BoomM = Num("fc_tx_boom", 2), CableDb = Num("fc_tx_cable", 1), PowerW = Num("fc_tx_w", 100) };
        rx = new Station { Loc = rl, Pos = rpos, Precise = rl.Length >= 8, AglM = Num("fc_rx_agl", 3), AslM = Num("fc_rx_asl", 0), BoomM = Num("fc_rx_boom", 2), CableDb = Num("fc_rx_cable", 1), NfDb = Num("fc_rx_nf", 2) };
        // locatore a 4–6 caratteri senza quota sul mare: si stima con il suolo SRTM al centro del quadrato più l'altezza dal suolo
        return true;
    }

    void FcResultMessage(string s, AM.Color c) => Ui(() =>
    {
        if (_fcResults == null || _fcResults.IsDisposed) return;
        _fcResults.Controls.Clear(); _fcResults.RowStyles.Clear(); _fcResults.RowCount = 0;
        Add(_fcResults, WrapLbl(s, 9f, c));
    });

    void FcComputeLink()
    {
        if (!FcStations(out var tx, out var rx, out var err)) { FcResultMessage(err, _th.Bad); return; }
        _fcCts?.Cancel(); _fcCts = new CancellationTokenSource();
        var ct = _fcCts.Token;
        var opt = new LinkCalc.Options
        {
            FreqMHz = FcFreq, Utc = FcTime,
            BwSsb = Num("fc_bw_ssb", 2500), BwCw = Num("fc_bw_cw", 500), BwFt8 = Num("fc_bw_ft8", 2500),
            ReqSsb = Num("fc_req_ssb", 6), ReqCw = Num("fc_req_cw", 0), ReqFt8 = Num("fc_req_ft8", -21),
            NoiseEnv = _st.S.Get("fc_rx_env", "quiet"), ManualTaK = Num("fc_rx_ta", 300)
        };
        _srtm.Progress = FcSetStatus;
        FcResultMessage(L("Calcolo in corso… La prima volta si scaricano i riquadri DEM 30 m del percorso (15–30 MB l'uno) e i profili GFS: può volerci qualche minuto. L'avanzamento è nel registro qui sotto.",
                          "Computing… The first time the path's DEM 30 m tiles (15–30 MB each) and GFS profiles are downloaded: it can take a few minutes. Progress is in the log below."), _th.Muted);
        Task.Run(async () =>
        {
            try
            {
                var tm = new Timing();
                FetchStats fs = null;
                double d = Geo.DistKm(tx.Pos, rx.Pos);
                int n = Math.Max(2, Math.Min(40, (int)Math.Ceiling(d / 25) + 1));
                var pp = Geo.GreatCircle(tx.Pos, rx.Pos, n - 1);
                var prof = pp.Select(p => _own.At(p.Lat, p.Lon, opt.Utc)).ToList();
                CalcLog.W($"percorso {d:0.0} km: {n} punti di previsione; dalla griglia già scaricata {prof.Count(p => p != null)}");
                if (prof.Count(p => p != null) < prof.Count * 0.8 || (_own.HasData && _own.Step > 1.0 && _own.Hist == null))
                {
                    if (_own.Step > 1.0) CalcLog.W($"la griglia ha passo {_own.Step:0.##}°, troppo largo per il percorso: scarico i profili dei {n} punti della tratta");
                    FcSetStatus(L("GFS: scarico i profili lungo il percorso…", "GFS: downloading profiles along the path…"));
                    fs = new FetchStats();
                    var ser = await OwnForecast.FetchPoints(FcHttp, pp, FcSetStatus, ct, fs, _own.RunUtc, null, _own.Hist, null, 0.25, opt.Utc);
                    prof = ser.Select(s =>
                    {
                        int best = -1; double bd = double.MaxValue;
                        for (int i = 0; i < s.Times.Length; i++) { double dd = Math.Abs((s.Times[i] - opt.Utc).TotalMinutes); if (dd < bd) { bd = dd; best = i; } }
                        return best >= 0 && bd <= 90 ? s.Profiles[best] : null;
                    }).ToList();
                }
                tm.Lap(fs != null ? "profili GFS scaricati" : "profili dalla griglia");
                if (_own.HasData) opt.PersistFrac = pp.Average(p => _own.PersistAt(p.Lat, p.Lon, opt.Utc, opt.FreqMHz));
                var r = await LinkCalc.Compute(tx, rx, opt, _srtm, prof, ct);
                tm.Lap("calcolo sul percorso");
                r.Times.Laps.Insert(0, tm.Laps[0]);
                r.Times.Add("TOTALE", tm.TotalMs);
                if (!tx.Precise && tx.AslM <= 0) r.Notes.Add(L("quota della mia antenna stimata: suolo SRTM al centro del quadrato {0} + {1:0} m; per la precisione usa un locatore a 8–10 caratteri o scrivi la quota sul mare",
                                                             "my antenna height estimated: SRTM ground at the centre of {0} + {1:0} m; for precision use an 8–10 character locator or enter the height above sea", tx.Loc, tx.AglM));
                if (!rx.Precise && rx.AslM <= 0) r.Notes.Add(L("quota dell'antenna del corrispondente stimata: suolo SRTM al centro del quadrato {0} + {1:0} m",
                                                             "other antenna height estimated: SRTM ground at the centre of {0} + {1:0} m", rx.Loc, rx.AglM));
                CalcLog.W($"TEMPI dal clic al risultato: {CalcLog.Dur(tm.TotalMs)}");
                _fcLast = r;
                FcSetStatus(L("Calcolo completato.", "Done.") + (r.TilesMissing > 0 ? " " + L("Riquadri DEM 30 m mancanti: ", "Missing DEM 30 m tiles: ") + r.TilesMissing : ""));
                Ui(() =>
                {
                    FcShowResult(r);
                    _fcProfile.Set(r, prof.Where(p => p != null).ToList());
                    _fcMap?.InvalidateVisual();
                });
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { FcSetStatus(L("Interrotto.", "Stopped.")); FcResultMessage(L("Calcolo interrotto.", "Computation stopped."), _th.Muted); }
            catch (Exception ex)
            {
                var msg = ex is OperationCanceledException ? L("tempo scaduto nello scaricamento dei dati (rete lenta o server occupato): riprova", "download timed out (slow network or busy server): try again") : ex.Message;
                CalcLog.W("ERRORE: " + ex);
                if (!(ex is OperationCanceledException) && !(ex is OwnForecast.RateLimitException)) ErrorLog.Write(ex, "calcolo del segnale");
                FcSetStatus(L("Errore: ", "Error: ") + msg);
                FcResultMessage(L("Calcolo non riuscito: ", "Computation failed: ") + msg, _th.Bad);
            }
        });
    }

    void FcShowResult(LinkResult r)
    {
        if (_fcResults == null || _fcResults.IsDisposed) return;
        _fcResults.SuspendLayout();
        _fcResults.Controls.Clear(); _fcResults.RowStyles.Clear(); _fcResults.RowCount = 0;
        Add(_fcResults, Lbl(ForecastText.Header(r, FcTime), 11f, _th.Accent, true));
        bool full = _st.S.GetBool("fc_full", false);
        foreach (var (label, value, tone, bold) in ForecastText.Lines(r, Num("fc_req_ssb", 6), Num("fc_req_cw", 0), Num("fc_req_ft8", -21), _own))
        {
            if (!full && !ForecastText.IsSummary(label)) continue;
            var col = tone switch
            {
                FcTone.Strong => _th.Text, FcTone.Accent => _th.Accent, FcTone.Ok => _th.Ok, FcTone.Bad => _th.Bad,
                FcTone.Warn => AM.Color.Parse("#D08A10"), FcTone.Muted => _th.Muted, _ => _th.Text
            };
            if (label == "") { Add(_fcResults, WrapLbl(value, 8.5f, col)); continue; }
            var tip = ForecastText.Tip(label);
            var row = Row(); row.WrapContents = false;
            var a = Lbl(label + (tip != null ? " ⓘ" : ""), 9f, _th.Muted); a.AutoSize = false; a.Width = 210; a.Height = 20;
            var b = Lbl(value, 9f, col, bold, true);
            b.AutoSize = true; b.MaximumSize = new System.Drawing.Size(Math.Max(220, _fcResults.ClientSize.Width - 240), 0);   // a capo invece di tagliare
            row.Controls.Add(a); row.Controls.Add(b);
            if (tip != null) { _fcTips.SetToolTip(a, tip); _fcTips.SetToolTip(b, tip); }
            Add(_fcResults, row);
        }
        _fcResults.ResumeLayout(true);
    }

    void FcComputeLos()
    {
        if (!LocatorX.TryParse((_fcTxLoc.Text ?? "").Trim(), out var tp)) { FcSetStatus(L("Locatore della mia stazione non valido.", "Invalid own locator.")); return; }
        var tl = (_fcTxLoc.Text ?? "").Trim();
        var tx = new Station { Loc = tl, Pos = tp, Precise = tl.Length >= 8, AglM = Num("fc_tx_agl", 10), AslM = Num("fc_tx_asl", 0) };

        _fcCts?.Cancel(); _fcCts = new CancellationTokenSource();
        var ct = _fcCts.Token;
        _srtm.Progress = FcSetStatus;
        double hrx = Num("fc_rx_agl", 3);
        var p0 = _own.At(tp.Lat, tp.Lon, FcTime);
        double k = p0 != null ? LinkCalc.KFromDn(p0.DN1km) : 4.0 / 3;
        CalcLog.Section("Copertura in ottica: dati di partenza");
        CalcLog.Profile("profilo GFS sulla mia stazione", p0);
        Task.Run(async () =>
        {
            try
            {
                FcSetStatus(L("Copertura in ottica: leggo il terreno…", "Line of sight: reading terrain…"));
                double ae = k * LinkCalc.Re;
                double hGuess = tx.Precise ? tx.AglM + 300 : tx.AslM;
                double maxKm = Math.Min(200, Math.Max(40, Math.Sqrt(2 * ae * Math.Max(10, hGuess) / 1000) + Math.Sqrt(2 * ae * hrx / 1000) + 40));
                var sw = System.Diagnostics.Stopwatch.StartNew();
                var (az, first, far) = await LinkCalc.LineOfSight(tx, hrx, k, maxKm, _srtm, ct);
                _fcLos = (az, first, far, tp);
                FcSetStatus(L("Copertura in ottica", "Line of sight") + $": k = {k:0.00}, " + L("prima ombra media ", "mean first shadow ") + $"{first.Average():0} km, " +
                            L("massimo visibile ", "farthest visible ") + $"{far.Max():0} km · " + CalcLog.Dur(sw.Elapsed.TotalMilliseconds));
                Ui(() => _fcMap?.InvalidateVisual());
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { FcSetStatus(L("Interrotto.", "Stopped.")); }
            catch (Exception ex) { CalcLog.W("ERRORE: " + ex); if (!(ex is OperationCanceledException)) ErrorLog.Write(ex, "copertura in ottica"); FcSetStatus(L("Errore: ", "Error: ") + ex.Message); }
        });
    }
}
