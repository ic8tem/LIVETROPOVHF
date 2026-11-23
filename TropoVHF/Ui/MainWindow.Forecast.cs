using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using TropoVHF.Core;
using TropoVHF.Forecast;

namespace TropoVHF.Ui;

/// <summary>
/// Linguetta Previsioni (sperimentale): indice tropo proprio calcolato dai profili GFS, quadretti dove manca la
/// previsione propria ma Hepburn o F5LEN sono alti, copertura in ottica dal terreno DEM 30 m e stima del segnale sul
/// percorso verso un corrispondente con antenne yagi DL6WU.
/// </summary>
public sealed partial class MainWindow
{
    readonly OwnForecast _own = new();
    // client dedicato: i riquadri DEM 30 m sono di 15–30 MB e su linee lente superano i 40 s del client generale
    static readonly HttpClient FcHttp = new() { Timeout = TimeSpan.FromMinutes(10) };
    TextBlock _fcProgress;
    TextBlock _fcCorr;
    ForecastPainter _fcPaint;
    Srtm _srtm;
    MapView _fcMap;
    TextBlock _fcStatus, _fcTime, _fcGtx, _fcGrx;
    StackPanel _fcResults;
    ProfileView _fcProfile;
    TextBox _fcTxLoc, _fcRxLoc;
    readonly Dictionary<string, TextBox> _fcBox = new();
    readonly List<ToggleButton> _fcBandBtns = new();
    CancellationTokenSource _fcCts;
    LinkResult _fcLast;
    (double[] az, double[] first, double[] far, LatLon origin)? _fcLos;
    List<LatLon> _fcPath;

    bool _fcSyncing;

    /// <summary>La riga in alto ha cambiato il locatore: lo si riporta nella linguetta Previsioni.</summary>
    void FcSyncLoc()
    {
        if (_fcTxLoc == null || _fcSyncing) return;
        var v = _st.Loc ?? "";
        if ((_fcTxLoc.Text ?? "").Trim().ToUpperInvariant() == v) return;
        _fcSyncing = true;
        try { _fcTxLoc.Text = v; } finally { _fcSyncing = false; }
        _fcMap?.InvalidateVisual();
    }

    static string L(string it, string en, params object[] a) => Lang.Tr(it, en, a);

    double FcFreq => _st.S.Get("fc_band", "144") switch { "432" => 432.2, "1296" => 1296.2, _ => 144.3 };
    int FcHours { get => _st.S.GetInt("fc_hours", 0); set => _st.S.SetInt("fc_hours", value); }
    DateTime FcTime { get { var n = Clock.UtcNow; var b = _own?.Hist != null ? _own.Hist.StartUtc : new DateTime(n.Year, n.Month, n.Day, n.Hour, 0, 0, DateTimeKind.Utc); return b.AddHours(FcHours); } }

    double Num(string key, double def)
    {
        if (!_fcBox.TryGetValue(key, out var tb)) return def;
        var s = (tb.Text ?? "").Trim().Replace(',', '.');
        return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var v) ? v : def;
    }

    Control BuildForecastTab()
    {
        _srtm ??= new Srtm(FcHttp);
        // pannello a sinistra largo a piacere: si trascina il bordo (larghezza salvata)
        double panelW = Math.Clamp(_st.S.GetInt("fc_panel_w", 430), 320, 1100);
        var g = new Grid { ColumnDefinitions = new ColumnDefinitions($"{panelW.ToString(CultureInfo.InvariantCulture)},6,*,300") };
        var col = new StackPanel { Spacing = 8, Margin = new Thickness(14) };

        col.Children.Add(new TextBlock { Text = L("Previsione propria", "Own forecast"), FontSize = (18) * UiScale.F, FontWeight = FontWeight.Bold, Foreground = B(_th.Accent) });
        var exp = TB(L("SPERIMENTALE: i calcoli sono in fase di verifica. Usare i risultati per confrontare situazioni, non come garanzia di un collegamento. " +
                       "Il manuale delle previsioni spiega ogni dato; i rapporti dei collegamenti reali servono a validare il modello.",
                       "EXPERIMENTAL: the calculations are being validated. Use the results to compare situations, not as a guarantee of a contact. " +
                       "The forecast manual explains every value; reports of real contacts help validate the model."), 12, _th.Bad, true);
        exp.TextWrapping = TextWrapping.Wrap;
        col.Children.Add(new Border { BorderBrush = B(_th.Bad), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(6), Padding = new Thickness(8, 6), Child = exp });
        if (!P453.Available)
        {
            var p4 = TB(L("Calcolo B dei condotti spento: mancano i file ITU-R P.453 (mappe dei condotti) in {0}. Vedi il manuale delle previsioni, § 6.",
                          "Duct calculation B off: ITU-R P.453 duct map files missing in {0}. See the forecast manual, § 6.", P453.Folder), 11, _th.Muted);
            p4.TextWrapping = TextWrapping.Wrap;
            var box = new StackPanel { Spacing = 6 };
            box.Children.Add(p4);
            Button dl = null;
            dl = OutlineButton(L("Scarica le mappe P.453 dall'ITU (uso personale)", "Download P.453 maps from ITU (personal use)"), async () =>
            {
                bool ok = await AskYesNo(L("Mappe ITU-R P.453", "ITU-R P.453 maps"),
                    L("Il programma scarica dal sito dell'ITU l'archivio dei condotti (elevated_and_surface_duct.zip) e ne estrae i sette file in {0}.\n\n" +
                      "I dati sono © ITU. Le condizioni d'uso del sito ITU permettono di scaricarli, copiarli e usarli per scopi personali, educativi o non commerciali, citando la fonte; " +
                      "non permettono di ridistribuirli, modificarli o usarli a fini commerciali senza permesso scritto dell'ITU. Scaricandoli accetti queste condizioni.\n\n" +
                      "Fonte: ITU-R P.453, prodotti digitali della Study Group 3 ({1}).",
                      "The program downloads the duct archive (elevated_and_surface_duct.zip) from the ITU website and extracts the seven files into {0}.\n\n" +
                      "The data are © ITU. The ITU website terms allow downloading, copying and using them for personal, educational or non-commercial purposes with credit to the source; " +
                      "they do not allow redistribution, modification or commercial use without ITU's written permission. By downloading you accept these terms.\n\n" +
                      "Source: ITU-R P.453, Study Group 3 digital products ({1}).", P453.Folder, P453.ItuPage),
                    L("Scarica", "Download"), L("Annulla", "Cancel"));
                if (!ok) return;
                dl.IsEnabled = false;
                try
                {
                    var (done, msg) = await Task.Run(() => P453.DownloadFromItu(FcHttp, FcSetStatus, CancellationToken.None));
                    p4.Text = msg; p4.Foreground = B(done ? _th.Ok : _th.Bad);
                    FcSetStatus(msg);
                    if (done) dl.IsVisible = false; else dl.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    var why = CalcLog.NetError(ex, "www.itu.int");
                    CalcLog.W("P.453: scaricamento non riuscito: " + why);
                    p4.Text = L("Scaricamento dall'ITU non riuscito: ", "ITU download failed: ") + why + L(". Si può scaricare a mano dalla pagina dell'ITU (vedi il manuale, § 6).", ". You can download it manually from the ITU page (see the manual, § 6).");
                    p4.Foreground = B(_th.Bad); dl.IsEnabled = true;
                }
            });
            ToolTip.SetTip(dl, P453.ItuZip);
            box.Children.Add(dl);
            col.Children.Add(box);
        }
        var help = TB(L("Indice calcolato dai profili verticali GFS (soglie di Bean e Dutton, condotti con la formula di Kerr). " +
                        "Quadretti: qui la previsione propria non c'è ma Hepburn o F5LEN danno 5 o più. Clic sulla mappa per scegliere il corrispondente.",
                        "Index from GFS vertical profiles (Bean & Dutton thresholds, ducts with Kerr's formula). " +
                        "Checkered squares: no own forecast here but Hepburn or F5LEN show 5 or more. Click the map to pick the other station."), 12, _th.Muted);
        help.TextWrapping = TextWrapping.Wrap;
        col.Children.Add(help);

        // banda
        var bands = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 0 };
        _fcBandBtns.Clear();
        foreach (var b in new[] { "144", "432", "1296" })
        {
            var tb = new ToggleButton { Content = b + " MHz", IsChecked = _st.S.Get("fc_band", "144") == b, Padding = new Thickness(14, 4), CornerRadius = new CornerRadius(0) };
            ToolTip.SetTip(tb, L("Banda della previsione e del calcolo: cambia i colori della mappa (i condotti intrappolano meglio le frequenze alte), il guadagno delle yagi e il rumore.", "Band for the forecast and the computation: changes the map colours (ducts trap higher frequencies better), the yagi gain and the noise."));
            tb.Click += (_, _) => { _st.S.Set("fc_band", b); foreach (var x in _fcBandBtns) x.IsChecked = (string)x.Content == b + " MHz"; FcRefreshGains(); _fcMap?.InvalidateVisual(); };
            _fcBandBtns.Add(tb); bands.Children.Add(tb);
        }
        col.Children.Add(bands);

        // ora
        _fcTime = TB("", 13, _th.Text, true, true);
        var sl = new Slider { Minimum = 0, Maximum = OwnForecast.Hours - 6, TickFrequency = 3, IsSnapToTickEnabled = true, Value = FcHours };
        _fcSlider = sl; _fcHourSeen = FcNowHour();
        ToolTip.SetTip(sl, L("Ora della previsione: da adesso fino a circa 3 giorni avanti, a passi di 3 ore. L'inizio segue l'orologio: allo scoccare dell'ora la mappa resta sull'ora che stavi guardando e il cursore si sposta.", "Forecast hour: from now up to about 3 days ahead, in 3-hour steps. The start follows the clock: when the hour turns, the map stays on the hour you were viewing and the slider moves."));
        sl.PropertyChanged += (_, e) => { if (e.Property == RangeBase.ValueProperty && !_fcHourSync) { FcHours = (int)Math.Round(sl.Value); FcTimeLabel(); FcSyncOverlay(); _fcMap?.InvalidateVisual(); } };
        FcTimeLabel();
        col.Children.Add(_fcTime);
        col.Children.Add(sl);

        col.Children.Add(FcSourceBox());
        _fcCorr = TB("", 11, _th.Muted); _fcCorr.TextWrapping = TextWrapping.Wrap;
        ToolTip.SetTip(_fcCorr, L("Coefficiente di correlazione di Pearson fra il guadagno stimato e la pressione al livello del mare, e fra il guadagno e l'inversione termica, sui punti della zona visibile all'ora scelta. 1 = vanno insieme, 0 = nessun legame, −1 = opposti. Manuale delle previsioni, § 5.",
                                  "Pearson correlation between estimated gain and sea-level pressure, and between gain and temperature inversion, over the visible points at the chosen hour. 1 = together, 0 = unrelated, −1 = opposite. Forecast manual, § 5."));
        _fcCorr.IsVisible = _st.S.GetBool("fc_show_corr", true);
        col.Children.Add(_fcCorr);

        _fcStatus = TB(_own.Status == "" ? L("Previsione propria non ancora scaricata.", "Own forecast not downloaded yet.") : _own.Status, 12, _th.Sub);
        _fcStatus.TextWrapping = TextWrapping.Wrap;
        col.Children.Add(new WrapPanel
        {
            Children =
            {
                Tip(PrimaryButton(L("Aggiorna previsione (zona visibile)", "Update forecast (visible area)"), FcUpdateGrid), L("Scarica i profili verticali del modello scelto per la zona visibile della mappa e ricalcola i colori. Ci vuole da qualche secondo a un minuto.", "Downloads the vertical profiles of the chosen model for the visible map area and recomputes the colours. Takes from a few seconds to a minute.")),
                new Border { Width = 8 },
                Tip(OutlineButton(L("Stop", "Stop"), () => _fcCts?.Cancel()), L("Interrompe lo scaricamento o il calcolo in corso.", "Stops the download or computation in progress.")),
                new Border { Width = 12 },
                FcAutoBox()
            }
        });
        col.Children.Add(_fcStatus);

        // stazioni
        TextBox Field(string key, string def, double w = 90)
        {
            var t = Pill(_st.S.Get(key, def), w);
            t.TextChanged += (_, _) => { _st.S.Set(key, (t.Text ?? "").Trim()); _st.Dirty = true; if (key.Contains("boom")) FcRefreshGains(); else if (key.StartsWith("fc_rx_") || key == "fc_bw_ssb") FcRefreshNoise(); };
            _fcBox[key] = t;
            return t;
        }
        Control Row(string label, Control c, Control extra = null)
        {
            var sp = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };
            sp.Children.Add(new Border { Width = 190, Child = new TextBlock { Text = label, FontSize = (12) * UiScale.F, Foreground = B(_th.Muted), TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center } });
            sp.Children.Add(c);
            if (extra != null) sp.Children.Add(extra);
            return sp;
        }

        var me = new StackPanel { Spacing = 6 };
        me.Children.Add(TB(L("La mia stazione (trasmette)", "My station (transmitting)"), 14, _th.Text, true));
        // il locatore della mia stazione è lo stesso della riga in alto (4–10 caratteri): cambiarlo qui lo cambia anche là
        _fcTxLoc = Pill(_st.Loc, 130, "JN70CN83CL"); _fcTxLoc.MaxLength = 10;
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
        me.Children.Add(Row(L("Locatore (4–10 caratteri; 8–10 = punto preciso su DEM 30 m)", "Locator (4–10 chars; 8–10 = exact point on DEM 30 m)"), _fcTxLoc));
        me.Children.Add(Row(L("Antenna dal suolo, m (locatore 8–10)", "Antenna above ground, m (8–10 locator)"), Field("fc_tx_agl", "10")));
        me.Children.Add(Row(L("Antenna sul mare, m (locatore 4–6: fondamentale)", "Antenna above sea, m (4–6 locator: required)"), Field("fc_tx_asl", "0")));
        me.Children.Add(Row(L("Potenza, W", "Power, W"), Field("fc_tx_w", "100")));
        me.Children.Add(Row(L("Perdita del cavo, dB", "Cable loss, dB"), Field("fc_tx_cable", "1")));
        _fcGtx = TB("", 12, _th.Accent, true);
        me.Children.Add(Row(L("Yagi DL6WU: boom, m", "DL6WU Yagi: boom, m"), Field("fc_tx_boom", "2"), _fcGtx));
        col.Children.Add(Card(me, new Thickness(0)));

        var rx = new StackPanel { Spacing = 6 };
        rx.Children.Add(TB(L("Corrispondente (riceve)", "Other station (receiving)"), 14, _th.Text, true));
        _fcRxLoc = Field("fc_rx_loc", "", 130); _fcRxLoc.MaxLength = 10;
        rx.Children.Add(Row(L("Locatore (o clic sulla mappa)", "Locator (or click the map)"), _fcRxLoc));
        rx.Children.Add(Row(L("Antenna dal suolo, m", "Antenna above ground, m"), Field("fc_rx_agl", "3")));
        rx.Children.Add(Row(L("Antenna sul mare, m (locatore 4–6; 0 = suolo SRTM + altezza)", "Antenna above sea, m (4–6 locator; 0 = SRTM ground + height)"), Field("fc_rx_asl", "0")));
        _fcGrx = TB("", 12, _th.Accent, true);
        rx.Children.Add(Row(L("Yagi DL6WU: boom, m", "DL6WU Yagi: boom, m"), Field("fc_rx_boom", "2"), _fcGrx));
        rx.Children.Add(Row(L("Perdita del cavo, dB", "Cable loss, dB"), Field("fc_rx_cable", "1")));
        rx.Children.Add(Row(L("Cifra di rumore del ricevitore, dB", "Receiver noise figure, dB"), Field("fc_rx_nf", "2")));
        {
            var envs = new[] { ("quiet", L("rurale tranquillo", "quiet rural")), ("rural", L("rurale", "rural")), ("residential", L("residenziale", "residential")), ("city", L("città", "city")), ("manual", L("temperatura d'antenna a mano", "manual antenna temperature")) };
            var cb = new ComboBox { Width = 200, ItemsSource = envs.Select(e => e.Item2).ToList() };
            var cur = _st.S.Get("fc_rx_env", "quiet");
            cb.SelectedIndex = Math.Max(0, Array.FindIndex(envs, e => e.Item1 == cur));
            cb.SelectionChanged += (_, _) => { if (cb.SelectedIndex >= 0) { _st.S.Set("fc_rx_env", envs[cb.SelectedIndex].Item1); _st.Dirty = true; FcRefreshNoise(); } };
            rx.Children.Add(Row(L("Ambiente di rumore del ricevitore (ITU-R P.372)", "Receiver noise environment (ITU-R P.372)"), cb));
            rx.Children.Add(Row(L("Temperatura d'antenna a mano, K", "Manual antenna temperature, K"), Field("fc_rx_ta", "300")));
            _fcNoise = TB("", 11, _th.Muted); _fcNoise.TextWrapping = TextWrapping.Wrap;
            rx.Children.Add(_fcNoise);
        }
        rx.Children.Add(Row(L("SSB: banda Hz / S/N minimo dB", "SSB: bandwidth Hz / min S/N dB"), Field("fc_bw_ssb", "2500", 70), Field("fc_req_ssb", "6", 50)));
        rx.Children.Add(Row(L("CW: banda Hz / S/N minimo dB", "CW: bandwidth Hz / min S/N dB"), Field("fc_bw_cw", "500", 70), Field("fc_req_cw", "0", 50)));
        rx.Children.Add(Row(L("FT8: banda Hz / S/N minimo dB", "FT8: bandwidth Hz / min S/N dB"), Field("fc_bw_ft8", "2500", 70), Field("fc_req_ft8", "-21", 50)));
        col.Children.Add(Card(rx, new Thickness(0)));
        FcRefreshGains();

        col.Children.Add(new WrapPanel
        {
            Children =
            {
                Tip(PrimaryButton(L("Calcola il segnale", "Compute signal"), FcComputeLink), L("Calcola il segnale fra la tua stazione e il corrispondente: profilo del terreno, strati lungo la tratta, perdita prevista, S/N per modo e puntamento consigliato. Manuale delle previsioni, § 7–12.", "Computes the signal between your station and the other one: terrain profile, layers along the path, predicted loss, S/N per mode and recommended heading. Forecast manual, § 7–12.")),
                new Border { Width = 8 },
                Tip(OutlineButton(L("Copertura in ottica (DEM 30 m)", "Line-of-sight coverage (DEM 30 m)"), FcComputeLos), L("Disegna in azzurro fino a dove la tua antenna si vede in ottica (prima ombra) e con la linea tratteggiata il punto visibile più lontano, sul terreno DEM 30 m.", "Draws in light blue how far your antenna is in line of sight (first shadow) and with a dashed line the farthest visible point, on 30 m DEM terrain."))
            }
        });
        // cosa mostrare: i calcoli restano tutti, si sceglie solo cosa vedere
        ProfileView.Flat = _st.S.GetBool("fc_flat", true);
        _fcProfile = new ProfileView { Height = 190, Th = _th };
        col.Children.Add(new Border { BorderBrush = B(_th.Line), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(8), ClipToBounds = true, Child = _fcProfile });
        _fcResults = new StackPanel { Spacing = 3, Margin = new Thickness(0, 0, 16, 10) };   // spazio per la barra di scorrimento
        col.Children.Add(_fcResults);
        if (_fcLast != null) FcShowResult(_fcLast); else _fcResults.Children.Add(TB(L("Nessun calcolo ancora.", "No computation yet."), 12, _th.Muted));
        col.Children.Add(FcLegend());
        _fcLogPanel = FcLogPanel();
        _fcLogPanel.IsVisible = _st.S.GetBool("fc_logshow", false);
        col.Children.Add(_fcLogPanel);

        g.Children.Add(new Border { Background = B(_th.Panel), BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 0, 1, 0), Child = new ScrollViewer { Content = col } });

        _fcMap = new MapView(_st, _th) { PreviewSource = "own" };
        _fcPaint = new ForecastPainter
        {
            St = _st, Own = _own, Map = _fcMap, Th = _th, Time = () => FcTime, Freq = () => FcFreq,
            TxLoc = () => _fcTxLoc?.Text, RxLoc = () => _fcRxLoc?.Text,
            ReqSsb = () => Num("fc_req_ssb", 6), ReqCw = () => Num("fc_req_cw", 0), ReqFt8 = () => Num("fc_req_ft8", -21)
        };
        _fcPaint.CorrText += t => Dispatcher.UIThread.Post(() => { if (_fcCorr != null) _fcCorr.Text = t; });
        _fcMap.OwnLayer = ctx => { _fcPaint.Last = _fcLast; _fcPaint.Los = _st.S.GetBool("fc_show_los", true) ? _fcLos : null; _fcPaint.DrawLayer(ctx); };
        _fcMap.OwnOverlay = ctx => _fcPaint.DrawOverlay(ctx);
        FcSyncOverlay();
        if (LocatorX.TryParse(_st.S.Get("fc_rx_loc", ""), out var rp)) _fcMap.Probe = rp;
        _fcMap.SelectionChanged += () =>
        {
            if (_fcMap.Probe is LatLon p)
            {
                _fcRxLoc.Text = LocatorX.From(p, 10);
                _fcPath = null;
            }
        };
        var split = new GridSplitter { ResizeDirection = GridResizeDirection.Columns, Background = B(_th.Line), Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.SizeWestEast) };
        ToolTip.SetTip(split, L("Trascina per allargare o stringere il pannello", "Drag to widen or narrow the panel"));
        split.DragCompleted += (_, _) =>
        {
            g.ColumnDefinitions[0].Width = new GridLength(Math.Clamp(g.ColumnDefinitions[0].ActualWidth, 320, 1100));
            _st.S.SetInt("fc_panel_w", (int)g.ColumnDefinitions[0].ActualWidth); _st.Dirty = true;
        };
        Grid.SetColumn(split, 1);
        g.Children.Add(split);
        Grid.SetColumn(_fcMap, 2);
        g.Children.Add(_fcMap);
        var det = new Border { Background = B(_th.Panel), BorderBrush = B(_th.Line), BorderThickness = new Thickness(1, 0, 0, 0), Child = new ScrollViewer { Content = FcDetailsPanel() } };
        Grid.SetColumn(det, 3);
        g.Children.Add(det);
        return g;
    }

    void FcTimeLabel()
    {
        if (_fcTime == null) return;
        var t = FcTime;
        _fcTime.Text = $"{Lang.DayNames()[(int)t.DayOfWeek]} {(_own.Hist != null ? t.ToString("dd/MM/yyyy HH") : t.ToString("dd/MM HH"))}:00 UTC (+{FcHours} h)";
    }

    TextBlock _fcNoise;

    /// <summary>Mostra subito la temperatura d'antenna e il rumore in SSB per la banda e l'ambiente scelti.</summary>
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

    TextBox _fcLogBox;
    bool _fcLogDirty;
    DispatcherTimer _fcLogTimer;

    /// <summary>Registro dettagliato: ogni passo dei calcoli, anche nel file Dati/previsioni-calcoli.log.</summary>
    Control FcLogPanel()
    {
        var sp = new StackPanel { Spacing = 6, Margin = new Thickness(0, 10, 0, 0) };
        sp.Children.Add(TB(L("Registro dei calcoli", "Computation log"), 13, _th.Text, true));
        CalcLog.Enabled = _st.S.GetBool("fc_log", true);
        var open = OutlineButton(L("Apri il file", "Open file"), () =>
        {
            try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(CalcLog.FilePath) { UseShellExecute = true }); }
            catch (Exception ex) { FcSetStatus(ex.Message); }
        });
        ToolTip.SetTip(open, L("Apre il file del registro dei calcoli con il programma predefinito.", "Opens the computation log file with the default program."));
        var clear = Tip(OutlineButton(L("Svuota la finestra", "Clear window"), () => { CalcLog.Clear(); _fcLogDirty = true; }), L("Svuota la finestra del registro; il file resta com'è.", "Clears the log window; the file is kept."));
        sp.Children.Add(new WrapPanel { Children = { open, new Border { Width = 8 }, clear } });
        var path = TB(CalcLog.FilePath, 11, _th.Muted); path.TextWrapping = TextWrapping.Wrap;
        sp.Children.Add(path);
        _fcLogBox = new TextBox
        {
            IsReadOnly = true, AcceptsReturn = true, TextWrapping = TextWrapping.Wrap, FontSize = (11) * UiScale.F,
            FontFamily = new FontFamily("Consolas, Menlo, DejaVu Sans Mono, monospace"), Text = CalcLog.RecentText()
        };
        ScrollViewer.SetHorizontalScrollBarVisibility(_fcLogBox, ScrollBarVisibility.Disabled);   // a capo automatico
        // altezza della finestra del registro a piacere: si trascina la barra sotto (altezza salvata)
        double logH = Math.Clamp(_st.S.GetInt("fc_log_h", 320), 120, 2000);
        var lg = new Grid { RowDefinitions = new RowDefinitions($"{logH.ToString(CultureInfo.InvariantCulture)},8,Auto") };
        lg.Children.Add(_fcLogBox);
        var hs = new GridSplitter { ResizeDirection = GridResizeDirection.Rows, Background = B(_th.Line), Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.SizeNorthSouth) };
        ToolTip.SetTip(hs, L("Trascina per alzare o abbassare il registro", "Drag to make the log taller or shorter"));
        hs.DragCompleted += (_, _) =>
        {
            lg.RowDefinitions[0].Height = new GridLength(Math.Clamp(lg.RowDefinitions[0].ActualHeight, 120, 2000));
            _st.S.SetInt("fc_log_h", (int)lg.RowDefinitions[0].ActualHeight); _st.Dirty = true;
        };
        Grid.SetRow(hs, 1);
        lg.Children.Add(hs);
        sp.Children.Add(lg);
        CalcLog.Line -= FcOnLogLine; CalcLog.Line += FcOnLogLine;
        if (_fcLogTimer == null)
        {
            _fcLogTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(700) };
            _fcLogTimer.Tick += (_, _) =>
            {
                if (!_fcLogDirty || _fcLogBox == null) return;
                _fcLogDirty = false;
                _fcLogBox.Text = CalcLog.RecentText();
                _fcLogBox.CaretIndex = _fcLogBox.Text?.Length ?? 0;
            };
            _fcLogTimer.Start();
        }
        return sp;
    }

    void FcOnLogLine(string _) => _fcLogDirty = true;

    Control FcLegend()
    {
        var sp = new StackPanel { Spacing = 4, Margin = new Thickness(0, 6, 0, 0) };
        sp.Children.Add(TB(L("Legenda", "Legend"), 13, _th.Text, true));
        var row = new WrapPanel();
        row.Children.Add(TB(L("sotto 7 dB nessun colore", "below 7 dB no colour"), 11, _th.Muted));
        row.Children.Add(new Border { Width = 12 });
        foreach (var bnd in Palette.GainBands)
        {
            row.Children.Add(new Border { Width = 18, Height = 12, Background = B(Color.FromArgb((byte)(255 * Palette.GainAlpha), bnd.C.R, bnd.C.G, bnd.C.B)), Margin = new Thickness(0, 0, 4, 0), VerticalAlignment = VerticalAlignment.Center });
            row.Children.Add(TB(bnd.Hi > 100 ? $"≥ {bnd.Lo:0.#} dB" : $"{bnd.Lo:0.#}–{bnd.Hi:0.#} dB", 11, _th.Muted));
            row.Children.Add(new Border { Width = 10 });
        }
        ToolTip.SetTip(row, L("Scala fissa del guadagno stimato rispetto all'atmosfera normale, uguale con tutti i temi. Guadagno dagli strati: Bean e Dutton, Radio Meteorology (NBS Monograph 92, 1966), tab. 6.5; condotti: Kerr, Propagation of Short Radio Waves (1951), eq. 17–18. Manuale delle previsioni, § 4.",
                              "Fixed scale of estimated gain over the normal atmosphere, the same in every theme. Layer gain: Bean & Dutton, Radio Meteorology (NBS Monograph 92, 1966), table 6.5; ducts: Kerr, Propagation of Short Radio Waves (1951), eq. 17–18. Forecast manual, § 4."));
        sp.Children.Add(row);
        var t2 = TB(L("Colori: guadagno stimato in dB (base della mappa, sempre visibile). Quadretti: nessuna previsione propria, Hepburn/F5LEN ≥ 5. " +
                      "Area azzurra: copertura in ottica fino alla prima ombra; linea tratteggiata: punto visibile più lontano. " +
                      "Condotti potenziali: doppio calcolo (A dal GFS, B con lo spessore climatologico ITU-R P.453, © ITU) e si usa il peggiore. " +
                      "Strato fondante: corridoio continuo con strati di almeno 40 m; se c'è terra conta la parte di terra coperta (CSIRO 2026). " +
                      "Confronto P.452: metodo ITU con il ΔN del GFS e p = 20%, solo indicativo. " +
                      "I valori del segnale sono stime: servono a confrontare situazioni, non garantiscono il collegamento.",
                      "Colours: estimated gain in dB (map base, always shown). Checkered: no own forecast, Hepburn/F5LEN ≥ 5. " +
                      "Light-blue area: line of sight up to the first shadow; dashed line: farthest visible point. " +
                      "Potential ducts: two calculations (A from GFS, B with ITU-R P.453 climatological thickness, © ITU), the worst is used. " +
                      "Governing layer: continuous corridor of layers at least 40 m thick; over land the covered land share counts (CSIRO 2026). " +
                      "P.452 check: ITU method with GFS ΔN and p = 20%, indicative only. " +
                      "Signal values are estimates for comparing situations, not a guarantee of a contact."), 11, _th.Muted);
        t2.TextWrapping = TextWrapping.Wrap;
        sp.Children.Add(t2);
        return sp;
    }


    // ------------------------------------------------------------------ linee opzionali

    /// <summary>Riquadro «Dettagli da mostrare» a destra della mappa (bozzetto approvato alla lettera, 22/11/2026).</summary>
    Control FcDetailsPanel()
    {
        var tips = new Dictionary<string, string>
        {
        ["fc_ov_curves"] = L("Curve a 7, 10, 13, 16, 18,5 e 21 dB, i bordi della scala dei colori, sul campo levigato. Tracciate con i quadrati marcianti. Manuale delle previsioni, § 5.", "Contours at 7, 10, 13, 16, 18.5 and 21 dB, the edges of the colour scale, on the smoothed field (marching squares). Forecast manual, § 5."),
        ["fc_ov_low"] = L("Disegna con un tratteggio grigio leggero le zone fra 3 e 7 dB, dove qualcosa si sta formando ma resta sotto la scala dei colori. I colori non cambiano. Manuale delle previsioni, § 4.", "Draws a light grey hatch where the gain is 3–7 dB, where something is forming but stays below the colour scale. Colours are unchanged. Forecast manual, § 4."),
        ["fc_ov_iso"] = L("Pressione al livello del mare ogni 4 hPa dal modello GFS (NOAA/NCEP) tramite Open-Meteo, alla stessa ora della previsione. Manuale delle previsioni, § 5.", "Mean sea level pressure every 4 hPa from GFS (NOAA/NCEP) via Open-Meteo, at the forecast hour. Forecast manual, § 5."),
        ["fc_ov_inv"] = L("Curve tratteggiate di quanto l'aria a 925 o 850 hPa è più calda di quella a 2 m (1, 3, 5, 8 °C), dal GFS via Open-Meteo. Un'inversione senza calo di umidità piega poco: Bean e Dutton § 4.4. Manuale delle previsioni, § 5.", "Dashed contours of how much warmer air at 925 or 850 hPa is than at 2 m (1, 3, 5, 8 °C), GFS via Open-Meteo. An inversion without a humidity drop bends little: Bean & Dutton § 4.4. Forecast manual, § 5."),
        ["fc_ov_wx"] = L("Su ogni cella della griglia: ↓ verde = subsidenza, FAVOREVOLE (aria che scende con inversione, +1,5 dB, Moler e Holden 1960); ↑ rosso = aria in salita, SFAVOREVOLE (rompe gli strati, −2 dB); S arancio = scirocco, indizio favorevole senza correzione (vento da SE–S a 850 hPa con aria calda, ARRL Handbook § 19.4). Passa con il mouse su un segno per il fumetto. Visibili quando le celle sono abbastanza larghe. Manuale delle previsioni, § 9 bis.", "On each grid cell: green ↓ = subsidence, GOOD (sinking air with an inversion, +1.5 dB, Moler & Holden 1960); red ↑ = rising air, BAD (breaks the layers, −2 dB); orange S = scirocco, good sign with no correction (SE–S wind at 850 hPa with warm air, ARRL Handbook § 19.4). Hover a mark for the bubble. Shown when cells are wide enough. Forecast manual, § 9 bis."),
        ["fc_show_corr"] = L("Coefficiente di correlazione di Pearson fra il guadagno stimato e la pressione al livello del mare, e fra il guadagno e l'inversione termica, sui punti della zona visibile all'ora scelta. 1 = vanno insieme, 0 = nessun legame, −1 = opposti. Manuale delle previsioni, § 5.", "Pearson correlation between estimated gain and sea-level pressure, and between gain and temperature inversion, over the visible points at the chosen hour. 1 = together, 0 = unrelated, −1 = opposite. Forecast manual, § 5."),
        ["fc_flat"] = L("Acceso: profilo a terra piatta, le montagne risaltano e il raggio diretto curva verso il basso. Spento: disegno classico, il terreno sale con la curvatura della terra e il raggio è dritto. Il calcolo è lo stesso.", "On: flat-earth profile, mountains stand out and the direct ray curves down. Off: classic drawing, terrain rises with the earth's curvature and the ray is straight. The computation is the same."),
        ["fc_full"] = L("Spento: solo la sintesi (distanza, strato, guadagno, perdita, segnale, S/N, modi, probabilità, puntamento). Acceso: tutte le righe del calcolo fra i due locatori. I calcoli sono sempre completi.", "Off: summary only (distance, layer, gain, loss, signal, S/N, modes, probability, heading). On: every line of the computation between the two locators. The computations are always complete."),
        ["fc_logshow"] = L("Mostra o nasconde la finestra del registro dei calcoli. Il registro continua a essere scritto nel file anche quando è nascosto.", "Shows or hides the computation log window. The log keeps being written to the file even when hidden."),
        ["fc_log"] = L("Scrive ogni passo dei calcoli nel registro e nel file. Spento, i calcoli sono identici ma non lasciano traccia.", "Writes every step of the computations to the log and the file. Off, the computations are identical but leave no trace."),
        ["fc_corr"] = L("Aggiunge al guadagno il termine di stabilità di Misme (solo senza condotto, da −4 a +6 dB, taratura provvisoria) e la correzione per il moto verticale: +1,5 dB con subsidenza, −2 dB con aria in salita. Toglila per vedere il modello della 1.2. Manuale delle previsioni, § 9 bis.", "Adds Misme's stability term (only without a duct, −4 to +6 dB, provisional calibration) and the vertical-motion correction: +1.5 dB with subsidence, −2 dB with rising air. Untick to see the 1.2 model. Forecast manual, § 9 bis."),
        ["fc_via_ideal"] = L("Quando il corrispondente è chiuso da un rilievo vicino, il giro d'orizzonte cerca il punto migliore da cui rilanciare il segnale. Con questa casella la perdita fra la tua stazione e il punto è considerata nulla: è come portare la stazione sulla cima (antenna a 10 m, stessa potenza e antenna) e conta solo la tratta dal punto al corrispondente. Il punto è dato con il locatore a 10 caratteri. Limite ottimistico. Manuale delle previsioni, § 10 bis.", "When the other station is blocked by nearby terrain, the horizon sweep looks for the best point to relaunch the signal. With this box the loss between your station and the point is taken as zero: as if you moved the station to the summit (antenna 10 m, same power and antenna); only the leg from the point to the other station counts. The point is given with a 10-character locator. Optimistic bound. Forecast manual, § 10 bis."),
        ["fc_seaair"] = L("Sul mare confronta l'aria con la temperatura della superficie del mare (SST). Aria a 925 hPa più calda del mare (temperatura potenziale oltre la SST di 4 K: +1 dB; di 7 K: +2 dB; −0,5 dB con aria umida): inversione d'avvezione, condotto probabile. Mare raffreddato di 1,5 °C o più nelle 72 ore prima (dalle analisi Open-Meteo): +1 dB, tipico dei mari bassi come l'alto Adriatico dopo la bora. Mare più caldo dell'aria a 2 m di 2 °C: −1,5 dB; di 4 °C: −3 dB, perché l'aria si rimescola. Soglie provvisorie, da tarare con gli spot. Manuale delle previsioni, § 9 quater.", "Over the sea it compares the air with the sea surface temperature (SST). Air at 925 hPa warmer than the sea (potential temperature 4 K above SST: +1 dB; 7 K: +2 dB; −0.5 dB with humid air): advection inversion, duct likely. Sea cooled by 1.5 °C or more in the previous 72 hours (Open-Meteo analyses): +1 dB, typical of shallow seas such as the northern Adriatic after a bora. Sea warmer than the 2 m air by 2 °C: −1.5 dB; by 4 °C: −3 dB, because the air mixes. Provisional thresholds, to be tuned with spots. Forecast manual, § 9 quater."),
        ["fc_nowcast"] = L("Corridoi d'apertura: almeno 3 coppie di stazioni indipendenti su tratte parallele (±15°, entro 150 km dall'asse) di almeno 300 km negli ultimi 30 minuti, ripetute a distanza di 10 minuti (un collegamento isolato può essere un aereo). Fonti: cluster, PSKReporter, QSO e decodifiche, salti APRS in 2 m. Tratte oltre 1500 km solo se confermate da due fonti; non contano le stazioni che in un'ora sentono stazioni lontane da tutte le direzioni (ricevitori configurati male). Vicino alla parte centrale delle tratte (±60 km) la previsione dell'ora attuale non scende sotto un minimo da 3 a 16 dB, secondo lunghezza (sotto i 600 km è quasi sempre troposcatter normale), numero di tratte e S/N, e le curve non fanno gocce. Manuale delle previsioni, § 4 bis.", "Opening corridors: at least 3 independent station pairs on parallel paths (±15°, within 150 km of the axis) of at least 300 km in the last 30 minutes, repeated 10 minutes apart (a single contact may be an aircraft). Sources: clusters, PSKReporter, QSOs and decodes, APRS 2 m hops. Paths over 1500 km only if confirmed by two sources; stations hearing distant stations from all directions within an hour (misconfigured receivers) are ignored. Near the middle part of the paths (±60 km) the current-hour forecast does not drop below a floor of 3 to 16 dB, by length (below 600 km it is almost always normal troposcatter), number of paths and S/N, and the contours make no drops. Forecast manual, § 4 bis."),
        ["aprs_on"] = L("APRS-IS in sola lettura (rotate.aprs2.net, filtro sull'Europa): salti radio di almeno 300 km fra stazioni di posizione nota, in 2 m, LoRa 433 MHz (banda 432) e LoRa 868 MHz (conta per i 1296), sempre fra stazioni della stessa banda. Ogni salto è fra due trasmettitori veri: si scartano i salti attraverso digipeater che non mettono il nominativo, i pacchetti con il percorso consumato rispetto al solito (ritrasmessi di nascosto), i salti con un digipeater attivo a metà strada, i pacchetti rigirati da internet, HF e stazioni in quota. Un salto conta solo se si ripete. Idea dalla mappa VHF di DXView e da APRS PropView (RF-YVY).", "Read-only APRS-IS (rotate.aprs2.net, Europe filter): radio hops of at least 300 km between stations of known position, on 2 m, LoRa 433 MHz (432 band) and LoRa 868 MHz (counts for 1296), always between stations on the same band. Each hop is between two real transmitters: hops through digipeaters that don't insert their call, packets whose path was consumed compared to usual (hidden retransmission), hops with an active digipeater halfway, packets gated from the internet, HF and airborne stations are discarded. A hop counts only if it repeats. Idea from the DXView VHF map and APRS PropView (RF-YVY)."),
            ["fc_show_los"] = FcDetails.LosTip,
        };
        var boxes = new Dictionary<string, CheckBox>();
        var col = new StackPanel { Spacing = 10, Margin = new Thickness(12) };
        var lv = new StackPanel { Spacing = 6 };
        lv.Children.Add(TB(FcDetails.Title, 13, _th.Text, true));
        var seg = new Grid { ColumnDefinitions = new ColumnDefinitions("*,*,*") };
        var btns = new ToggleButton[3];
        int cur = Math.Clamp(_st.S.GetInt("fc_detail_level", 1), 0, 2);
        for (int i = 0; i < 3; i++)
        {
            int k = i;
            var b = new ToggleButton { Content = FcDetails.Levels[i], IsChecked = i == cur, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center, FontSize = (12) * UiScale.F, Margin = new Thickness(i == 0 ? 0 : 2, 0, 0, 0) };
            b.Click += (_, _) =>
            {
                for (int j = 0; j < 3; j++) btns[j].IsChecked = j == k;
                _st.S.SetInt("fc_detail_level", k); _st.Dirty = true;
                foreach (var it in FcDetails.All) boxes[it.Key].IsChecked = k >= it.Level;
            };
            btns[i] = b; Grid.SetColumn(b, i); seg.Children.Add(b);
        }
        lv.Children.Add(seg);
        var help = TB(FcDetails.Help, 11, _th.Muted); help.TextWrapping = TextWrapping.Wrap;
        lv.Children.Add(help);
        col.Children.Add(Card(lv, new Thickness(0)));
        for (int gi = 0; gi < 3; gi++)
        {
            var sp = new StackPanel { Spacing = 2 };
            sp.Children.Add(TB(FcDetails.Groups[gi], 13, _th.Text, true));
            foreach (var it in FcDetails.All)
            {
                if (it.Group != gi) continue;
                var key = it.Key;
                var cb = new CheckBox { Content = L(it.It, it.En), IsChecked = _st.S.GetBool(key, FcDetails.Default(key)), FontSize = (12) * UiScale.F };
                cb.IsCheckedChanged += (_, _) => FcApplyDetail(key, cb.IsChecked == true);
                if (tips.TryGetValue(key, out var tip)) ToolTip.SetTip(cb, tip);
                boxes[key] = cb;
                var row = new DockPanel { LastChildFill = true };
                var sh = TB(FcDetails.Short[it.Level], 11, _th.Muted); sh.VerticalAlignment = VerticalAlignment.Center;
                DockPanel.SetDock(sh, Dock.Right); row.Children.Add(sh); row.Children.Add(cb);
                sp.Children.Add(row);
            }
            col.Children.Add(Card(sp, new Thickness(0)));
        }
        return col;
    }

    void FcApplyDetail(string key, bool v)
    {
        _st.S.SetBool(key, v); _st.Dirty = true;
        switch (key)
        {
            case "fc_show_corr": if (_fcCorr != null) _fcCorr.IsVisible = v; break;
            case "fc_full": if (_fcLast != null) FcShowResult(_fcLast); break;
            case "fc_flat": ProfileView.Flat = v; _fcProfile?.InvalidateVisual(); break;
            case "fc_logshow": if (_fcLogPanel != null) _fcLogPanel.IsVisible = v; break;
            case "fc_log": CalcLog.Enabled = v; break;
            case "fc_corr": Refractivity.UseCorrections = v; break;
            case "fc_seaair": Refractivity.UseSeaAir = v; break;
            case "fc_via_ideal": Refractivity.ViaIdeal = v; break;
            case "fc_nowcast": _st.NowcastOn = v; break;
            case "aprs_on": _st.AprsOn = v; if (v) _st.Aprs.Start(); else _st.Aprs.Stop(); break;
        }
        FcSyncOverlay(); _fcMap?.InvalidateVisual();
    }

    void FcSyncOverlay()
    {
        if (_fcMap == null) return;
        _fcMap.OwnIso = _st.S.GetBool("fc_ov_iso", false);
        _fcMap.OwnTime = FcTime;
    }





    // ------------------------------------------------------------------ mappa


    // ------------------------------------------------------------------ azioni

    void FcSetStatus(string s) => Dispatcher.UIThread.Post(() =>
    {
        if (_fcStatus != null) _fcStatus.Text = s;
        if (_fcProgress != null) _fcProgress.Text = s;     // durante il calcolo l'avanzamento si vede anche sotto il profilo
        CalcLog.W("stato: " + s);
    });

    void FcResultMessage(string s, Color c) => Dispatcher.UIThread.Post(() =>
    {
        _fcProgress = null;
        _fcResults.Children.Clear();
        var t = TB(s, 12, c); t.TextWrapping = TextWrapping.Wrap;
        _fcResults.Children.Add(t);
    });

    bool FcAuto { get => _st.S.GetBool("fc_auto", true); set => _st.S.SetBool("fc_auto", value); }

    Control FcAutoBox()
    {
        var c = new CheckBox { Content = L("Aggiorna da sola", "Update by itself"), IsChecked = FcAuto, Foreground = B(_th.Text), VerticalAlignment = VerticalAlignment.Center };
        c.Click += (_, _) => { FcAuto = c.IsChecked == true; _st.Save(); };
        ToolTip.SetTip(c, L("Scarica la previsione senza clic: all'apertura, a ogni nuova corsa GFS su NOMADS (ogni 6 ore, circa 5 ore dopo l'ora della corsa) e quando la zona visibile esce dalla griglia. Mai con un caso storico.",
                            "Downloads the forecast without clicking: at start, at each new GFS run on NOMADS (every 6 hours, about 5 hours after the run time) and when the visible area leaves the grid. Never with a historical case."));
        return c;
    }

    int _fcAutoTick;
    /// <summary>Chiamata ogni secondo dal timer della finestra: ogni 30 s controlla se serve aggiornare la previsione.</summary>
    Slider _fcSlider; Control _fcLogPanel;
    DateTime _fcHourSeen; bool _fcHourSync;
    static T Tip<T>(T c, string tip) where T : Control { ToolTip.SetTip(c, tip); return c; }
    static DateTime FcNowHour() { var n = Clock.UtcNow; return new DateTime(n.Year, n.Month, n.Day, n.Hour, 0, 0, DateTimeKind.Utc); }

    /// <summary>
    /// L'inizio del cursore dell'ora è l'ora attuale: quando l'ora scatta, la mappa resta sull'ora che si stava guardando
    /// (lo scostamento cala di quanto è passato) e l'etichetta si aggiorna; a «+0 h» segue l'ora attuale.
    /// </summary>
    void FcClockTick()
    {
        if (_own?.Hist != null || _fcSlider == null) return;
        var h = FcNowHour();
        if (_fcHourSeen == default) { _fcHourSeen = h; return; }
        if (h == _fcHourSeen) return;
        int passed = (int)Math.Round((h - _fcHourSeen).TotalHours);
        _fcHourSeen = h;
        FcHours = Math.Max(0, FcHours - Math.Max(0, passed));
        _fcHourSync = true;
        try { _fcSlider.Value = FcHours; } finally { _fcHourSync = false; }
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
        // un nuovo clic durante lo scaricamento non lo interrompe (per fermare c'è Stop)
        if (_own.Busy) { FcSetStatus(L("Scaricamento già in corso: ", "Download already running: ") + _own.Status + L(" (per fermarlo: Stop)", " (to stop it: Stop)")); return; }
        _fcCts?.Cancel(); _fcCts = new CancellationTokenSource();
        var ct = _fcCts.Token;
        var (la1, la2, lo1, lo2) = _fcMap.Box;
        if (_own.Hist != null) { la1 = _own.Hist.LatMin; la2 = _own.Hist.LatMax; lo1 = _own.Hist.LonMin; lo2 = _own.Hist.LonMax; }
        CalcLog.Section("Aggiornamento della previsione propria");
        CalcLog.W($"zona visibile {la1:0.##}–{la2:0.##} N, {lo1:0.##}–{lo2:0.##} E");
        Task.Run(async () =>
        {
            await _own.UpdateAsync(FcHttp, la1, la2, lo1, lo2, () => { FcSetStatus(_own.Status); Dispatcher.UIThread.Post(() => _fcMap?.InvalidateVisual()); }, ct);
        });
    }

    bool FcStations(out Station tx, out Station rx, out string err)
    {
        tx = rx = null; err = "";
        var tl = (_fcTxLoc.Text ?? "").Trim(); var rl = (_fcRxLoc.Text ?? "").Trim();
        if (!LocatorX.TryParse(tl, out var tp)) { err = L("Locatore della mia stazione non valido (4, 6, 8 o 10 caratteri).", "Invalid own locator (4, 6, 8 or 10 chars)."); return false; }
        if (!LocatorX.TryParse(rl, out var rp)) { err = L("Locatore del corrispondente non valido: scrivilo o fai clic sulla mappa.", "Invalid other locator: type it or click the map."); return false; }
        tx = new Station { Loc = tl, Pos = tp, Precise = tl.Length >= 8, AglM = Num("fc_tx_agl", 10), AslM = Num("fc_tx_asl", 0), BoomM = Num("fc_tx_boom", 2), CableDb = Num("fc_tx_cable", 1), PowerW = Num("fc_tx_w", 100) };
        rx = new Station { Loc = rl, Pos = rp, Precise = rl.Length >= 8, AglM = Num("fc_rx_agl", 3), AslM = Num("fc_rx_asl", 0), BoomM = Num("fc_rx_boom", 2), CableDb = Num("fc_rx_cable", 1), NfDb = Num("fc_rx_nf", 2) };
        // locatore a 4–6 caratteri senza quota sul mare: si stima con il suolo SRTM al centro del quadrato più l'altezza dal suolo
        return true;
    }

    void FcComputeLink()
    {
        if (!FcStations(out var tx, out var rx, out var err)) { _fcResults.Children.Clear(); _fcResults.Children.Add(TB(err, 12, _th.Bad)); return; }
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
        _fcResults.Children.Clear();
        _fcResults.Children.Add(TB(L("Calcolo in corso…", "Computing…"), 12, _th.Muted, true));
        _fcProgress = TB(L("preparo il percorso", "preparing the path"), 12, _th.Sub); _fcProgress.TextWrapping = TextWrapping.Wrap;
        _fcResults.Children.Add(_fcProgress);
        var hint = TB(L("La prima volta si scaricano i riquadri DEM 30 m del percorso (15–30 MB l'uno) e i profili GFS: può volerci qualche minuto. L'avanzamento è anche nel registro qui sotto.",
                        "The first time the path's DEM 30 m tiles (15–30 MB each) and GFS profiles are downloaded: it can take a few minutes. Progress is also in the log below."), 11, _th.Muted);
        hint.TextWrapping = TextWrapping.Wrap;
        _fcResults.Children.Add(hint);
        Task.Run(async () =>
        {
            try
            {
                var tm = new Timing();
                FetchStats fs = null;
                // profili della previsione lungo il percorso: dalla griglia se la copre, altrimenti si scaricano
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
                CalcLog.W($"TEMPI dal clic al risultato: {CalcLog.Dur(tm.TotalMs)} (profili {CalcLog.Dur(tm.Laps[0].Ms)}" + (fs != null ? $": {fs.Summary()}" : "") + $", percorso {CalcLog.Dur(tm.Laps[1].Ms)})");
                _fcLast = r;
                FcSetStatus(L("Calcolo completato.", "Done.") + (r.TilesMissing > 0 ? " " + L("Riquadri DEM 30 m mancanti: ", "Missing DEM 30 m tiles: ") + r.TilesMissing : ""));
                Dispatcher.UIThread.Post(() =>
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
        _fcProgress = null;
        _fcResults.Children.Clear();
        _fcResults.Children.Add(TB(ForecastText.Header(r, FcTime), 14, _th.Accent, true));
        bool full = _st.S.GetBool("fc_full", false);
        foreach (var (label, value, tone, bold) in ForecastText.Lines(r, Num("fc_req_ssb", 6), Num("fc_req_cw", 0), Num("fc_req_ft8", -21), _own))
        {
            if (!full && !ForecastText.IsSummary(label)) continue;
            var col = tone switch
            {
                FcTone.Strong => _th.Text, FcTone.Accent => _th.Accent, FcTone.Ok => _th.Ok, FcTone.Bad => _th.Bad,
                FcTone.Warn => Color.Parse("#D08A10"), FcTone.Muted => _th.Muted, _ => _th.Text
            };
            if (label == "") { var t = TB(value, 11, col); t.TextWrapping = TextWrapping.Wrap; _fcResults.Children.Add(t); continue; }
            // fumetto con fonte e richiamo per ogni dato sensibile
            var sp = new DockPanel { Background = Brushes.Transparent };
            var a = label;
            if (ForecastText.Tip(label) is string tip) { ToolTip.SetTip(sp, tip); ToolTip.SetShowDelay(sp, 250); a += " ⓘ"; }
            var ta = TB(a, 12, _th.Muted); DockPanel.SetDock(ta, Dock.Left); ta.Width = 210; ta.VerticalAlignment = VerticalAlignment.Top;
            sp.Children.Add(ta);
            var tv = TB(value, 12, col, bold, true); tv.TextWrapping = TextWrapping.Wrap;   // a capo invece di tagliare il testo
            sp.Children.Add(tv);
            _fcResults.Children.Add(sp);
        }
    }

    /// <summary>Sorgente dei dati, caso storico e correzioni meteo della 1.3.0.</summary>
    Control FcSourceBox()
    {
        OwnForecast.Source = WxSources.Parse(_st.S.Get("fc_source", "Auto"));
        Refractivity.UseCorrections = _st.S.GetBool("fc_corr", true);
        Refractivity.ViaIdeal = _st.S.GetBool("fc_via_ideal", false);
        Refractivity.UseSeaAir = _st.S.GetBool("fc_seaair", true);
        var box = new StackPanel { Spacing = 6 };
        var src = new ComboBox { MinWidth = 0, FontSize = (10.5) * UiScale.F, MinHeight = 24, Padding = new Thickness(6, 1, 4, 1), HorizontalAlignment = HorizontalAlignment.Stretch, ItemsSource = WxSources.All.Select(WxSources.Name).ToList(), SelectedIndex = Array.IndexOf(WxSources.All, OwnForecast.Source) };
        src.SelectionChanged += (_, _) =>
        {
            if (src.SelectedIndex < 0) return;
            OwnForecast.Source = WxSources.All[src.SelectedIndex];
            _st.S.Set("fc_source", OwnForecast.Source.ToString()); _st.Dirty = true;
            FcSetStatus(L("Sorgente scelta: ", "Source: ") + WxSources.Name(OwnForecast.Source) + L(". Premi «Aggiorna previsione» per scaricare la griglia.", ". Press «Update forecast» to download the grid."));
        };
        ToolTip.SetTip(src, L("Automatica (consigliata): GFS prima da NOMADS (NOAA, file GRIB2 della zona, nessun limite giornaliero), poi da Open-Meteo per i punti che mancano: è lo stesso modello e la stessa corsa. ICON-EU (solo Europa) ed ECMWF valgono per tutta la griglia: modelli diversi non si mescolano. Manuale delle previsioni, § 3 bis.",
                              "Automatic (recommended): GFS first from NOMADS (NOAA, GRIB2 files of the area, no daily limit), then from Open-Meteo for any missing points: same model and run. ICON-EU (Europe only) and ECMWF apply to the whole grid: different models are not mixed. Forecast manual, § 3 bis."));
        var sp1 = new Grid { ColumnDefinitions = new ColumnDefinitions("Auto,*") };
        sp1.Children.Add(new TextBlock { Text = L("Sorgente dei dati", "Data source"), Width = 108, FontSize = (11) * UiScale.F, Foreground = B(_th.Muted), VerticalAlignment = VerticalAlignment.Center });
        Grid.SetColumn(src, 1); sp1.Children.Add(src);
        box.Children.Add(sp1);

        var cases = new List<string> { L("previsione attuale", "current forecast") };
        cases.AddRange(HistCase.Known.Select(h => h.Name));
        var hc = new ComboBox { MinWidth = 0, FontSize = (10.5) * UiScale.F, MinHeight = 24, Padding = new Thickness(6, 1, 4, 1), HorizontalAlignment = HorizontalAlignment.Stretch, ItemsSource = cases, SelectedIndex = _own.Hist == null ? 0 : Array.IndexOf(HistCase.Known, _own.Hist) + 1 };
        hc.SelectionChanged += (_, _) =>
        {
            if (hc.SelectedIndex < 0) return;
            _own.Hist = hc.SelectedIndex == 0 ? null : HistCase.Known[hc.SelectedIndex - 1];
            if (_own.Hist != null)
            {
                var h = _own.Hist;
                _fcMap?.ShowBox(h.LatMin, h.LatMax, h.LonMin, h.LonMax);
                _st.S.Set("fc_band", h.FreqMHz >= 1000 ? "1296" : h.FreqMHz >= 400 ? "432" : "144");
                foreach (var x in _fcBandBtns) x.IsChecked = (string)x.Content == _st.S.Get("fc_band", "144") + " MHz"; FcRefreshGains();
                FcSetStatus(L("Caso storico: ", "Historical case: ") + h.Name + " — " + h.Note + L(". Premi «Aggiorna previsione» per scaricare i profili d'archivio.", ". Press «Update forecast» to download the archive profiles."));
            }
            else FcSetStatus(L("Previsione attuale: premi «Aggiorna previsione».", "Current forecast: press «Update forecast»."));
            FcTimeLabel(); _fcMap?.InvalidateVisual();
        };
        ToolTip.SetTip(hc, L("Rivede un'apertura documentata (QST, «The World Above 50 MHz») con le previsioni GFS d'archivio di Open-Meteo, per confrontare il modello con quello che è successo. L'ora parte dal primo giorno del caso. Manuale delle previsioni, § 3 ter.",
                             "Replays a documented opening (QST, «The World Above 50 MHz») with Open-Meteo archived GFS forecasts, to compare the model with what happened. Time starts at the first day of the case. Forecast manual, § 3 ter."));
        var sp2 = new Grid { ColumnDefinitions = new ColumnDefinitions("Auto,*") };
        sp2.Children.Add(new TextBlock { Text = L("Caso storico", "Historical case"), Width = 108, FontSize = (11) * UiScale.F, Foreground = B(_th.Muted), VerticalAlignment = VerticalAlignment.Center });
        Grid.SetColumn(hc, 1); sp2.Children.Add(hc);
        box.Children.Add(sp2);

        return box;
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
        CalcLog.W(p0 == null ? "nessuna previsione sulla stazione: k = 4/3" : $"k dal ΔN del primo km = {k:0.000}");
        Task.Run(async () =>
        {
            try
            {
                FcSetStatus(L("Copertura in ottica: leggo il terreno…", "Line of sight: reading terrain…"));
                // raggio massimo: orizzonte radio delle due antenne con k, più margine per i rilievi
                double ae = k * LinkCalc.Re;
                double hGuess = tx.Precise ? tx.AglM + 300 : tx.AslM;
                double maxKm = Math.Min(200, Math.Max(40, Math.Sqrt(2 * ae * Math.Max(10, hGuess) / 1000) + Math.Sqrt(2 * ae * hrx / 1000) + 40));
                var swLos = System.Diagnostics.Stopwatch.StartNew();
                var (az, first, far) = await LinkCalc.LineOfSight(tx, hrx, k, maxKm, _srtm, ct);
                CalcLog.W($"TEMPI copertura in ottica: {CalcLog.Dur(swLos.Elapsed.TotalMilliseconds)}");
                _fcLos = (az, first, far, tp);
                FcSetStatus(L("Copertura in ottica", "Line of sight") + $": k = {k:0.00}, " + L("antenna a ", "antenna at ") + $"{tx.AntennaAsl:0} m s.l.m., " +
                            L("prima ombra media ", "mean first shadow ") + $"{first.Average():0} km, " + L("massimo visibile ", "farthest visible ") + $"{far.Max():0} km · " +
                            L("in ", "in ") + CalcLog.Dur(swLos.Elapsed.TotalMilliseconds));
                Dispatcher.UIThread.Post(() => _fcMap?.InvalidateVisual());
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { FcSetStatus(L("Interrotto.", "Stopped.")); }
            catch (Exception ex) { CalcLog.W("ERRORE: " + ex); if (!(ex is OperationCanceledException)) ErrorLog.Write(ex, "copertura in ottica"); FcSetStatus(L("Errore: ", "Error: ") + (ex is OperationCanceledException ? L("tempo scaduto nello scaricamento, riprova", "download timed out, try again") : ex.Message)); }
        });
    }
}
