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
/// Linguetta Previsioni (versione privata): indice tropo proprio calcolato dai profili GFS, quadretti dove manca la
/// previsione propria ma Hepburn o F5LEN sono alti, copertura in ottica dal terreno SRTM1 e stima del segnale sul
/// percorso verso un corrispondente con antenne yagi DL6WU.
/// </summary>
public sealed partial class MainWindow
{
    readonly OwnForecast _own = new();
    // client dedicato: i riquadri SRTM1 sono di 6–7 MB e su linee lente superano i 40 s del client generale
    static readonly HttpClient FcHttp = new() { Timeout = TimeSpan.FromMinutes(10) };
    TextBlock _fcProgress;
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

    static string L(string it, string en) => Lang.Current == "it" ? it : en;

    double FcFreq => _st.S.Get("fc_band", "144") switch { "432" => 432.2, "1296" => 1296.2, _ => 144.3 };
    int FcHours { get => _st.S.GetInt("fc_hours", 0); set => _st.S.SetInt("fc_hours", value); }
    DateTime FcTime { get { var n = Clock.UtcNow; return new DateTime(n.Year, n.Month, n.Day, n.Hour, 0, 0, DateTimeKind.Utc).AddHours(FcHours); } }

    double Num(string key, double def)
    {
        if (!_fcBox.TryGetValue(key, out var tb)) return def;
        var s = (tb.Text ?? "").Trim().Replace(',', '.');
        return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var v) ? v : def;
    }

    Control BuildForecastTab()
    {
        _srtm ??= new Srtm(FcHttp);
        var g = new Grid { ColumnDefinitions = new ColumnDefinitions("430,*") };
        var col = new StackPanel { Spacing = 8, Margin = new Thickness(14) };

        col.Children.Add(new TextBlock { Text = L("Previsione propria", "Own forecast"), FontSize = 18, FontWeight = FontWeight.Bold, Foreground = B(_th.Accent) });
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
            tb.Click += (_, _) => { _st.S.Set("fc_band", b); foreach (var x in _fcBandBtns) x.IsChecked = (string)x.Content == b + " MHz"; FcRefreshGains(); _fcMap?.InvalidateVisual(); };
            _fcBandBtns.Add(tb); bands.Children.Add(tb);
        }
        col.Children.Add(bands);

        // ora
        _fcTime = TB("", 13, _th.Text, true, true);
        var sl = new Slider { Minimum = 0, Maximum = OwnForecast.Hours - 6, TickFrequency = 3, IsSnapToTickEnabled = true, Value = FcHours };
        sl.PropertyChanged += (_, e) => { if (e.Property == RangeBase.ValueProperty) { FcHours = (int)Math.Round(sl.Value); FcTimeLabel(); _fcMap?.InvalidateVisual(); } };
        FcTimeLabel();
        col.Children.Add(_fcTime);
        col.Children.Add(sl);

        _fcStatus = TB(_own.Status == "" ? L("Previsione propria non ancora scaricata.", "Own forecast not downloaded yet.") : _own.Status, 12, _th.Sub);
        _fcStatus.TextWrapping = TextWrapping.Wrap;
        col.Children.Add(new WrapPanel
        {
            Children =
            {
                PrimaryButton(L("Aggiorna previsione (zona visibile)", "Update forecast (visible area)"), FcUpdateGrid),
                new Border { Width = 8 },
                OutlineButton(L("Stop", "Stop"), () => _fcCts?.Cancel())
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
            sp.Children.Add(new Border { Width = 190, Child = new TextBlock { Text = label, FontSize = 12, Foreground = B(_th.Muted), TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center } });
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
        me.Children.Add(Row(L("Locatore (4–10 caratteri; 8–10 = punto preciso su SRTM1)", "Locator (4–10 chars; 8–10 = exact point on SRTM1)"), _fcTxLoc));
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
                PrimaryButton(L("Calcola il segnale", "Compute signal"), FcComputeLink),
                new Border { Width = 8 },
                OutlineButton(L("Copertura in ottica (SRTM1)", "Line-of-sight coverage (SRTM1)"), FcComputeLos)
            }
        });
        _fcProfile = new ProfileView { Height = 190, Th = _th };
        col.Children.Add(new Border { BorderBrush = B(_th.Line), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(8), ClipToBounds = true, Child = _fcProfile });
        _fcResults = new StackPanel { Spacing = 3 };
        col.Children.Add(_fcResults);
        if (_fcLast != null) FcShowResult(_fcLast); else _fcResults.Children.Add(TB(L("Nessun calcolo ancora.", "No computation yet."), 12, _th.Muted));
        col.Children.Add(FcLegend());
        col.Children.Add(FcLogPanel());

        g.Children.Add(new Border { Background = B(_th.Panel), BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 0, 1, 0), Child = new ScrollViewer { Content = col } });

        _fcMap = new MapView(_st, _th) { PreviewSource = "own" };
        _fcMap.OwnLayer = FcDrawLayer;
        if (LocatorX.TryParse(_st.S.Get("fc_rx_loc", ""), out var rp)) _fcMap.Probe = rp;
        _fcMap.SelectionChanged += () =>
        {
            if (_fcMap.Probe is LatLon p)
            {
                _fcRxLoc.Text = LocatorX.From(p, 10);
                _fcPath = null;
            }
        };
        Grid.SetColumn(_fcMap, 1);
        g.Children.Add(_fcMap);
        return g;
    }

    void FcTimeLabel()
    {
        if (_fcTime == null) return;
        var t = FcTime;
        _fcTime.Text = $"{Lang.DayNames()[(int)t.DayOfWeek]} {t:dd/MM HH}:00 UTC (+{FcHours} h)";
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
        var chk = Check(L("Registro dettagliato attivo", "Verbose log enabled"), CalcLog.Enabled, v => { CalcLog.Enabled = v; _st.S.SetBool("fc_log", v); _st.Dirty = true; });
        var open = OutlineButton(L("Apri il file", "Open file"), () =>
        {
            try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(CalcLog.FilePath) { UseShellExecute = true }); }
            catch (Exception ex) { FcSetStatus(ex.Message); }
        });
        var clear = OutlineButton(L("Svuota la finestra", "Clear window"), () => { CalcLog.Clear(); _fcLogDirty = true; });
        sp.Children.Add(new WrapPanel { Children = { chk, new Border { Width = 8 }, open, new Border { Width = 8 }, clear } });
        var path = TB(CalcLog.FilePath, 11, _th.Muted); path.TextWrapping = TextWrapping.Wrap;
        sp.Children.Add(path);
        _fcLogBox = new TextBox
        {
            IsReadOnly = true, AcceptsReturn = true, TextWrapping = TextWrapping.NoWrap, Height = 320, FontSize = 11,
            FontFamily = new FontFamily("Consolas, Menlo, DejaVu Sans Mono, monospace"), Text = CalcLog.RecentText()
        };
        ScrollViewer.SetHorizontalScrollBarVisibility(_fcLogBox, ScrollBarVisibility.Auto);
        sp.Children.Add(_fcLogBox);
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
        foreach (var (v, t) in new[] { (1.0, "0–2"), (3.0, "2–4"), (5.0, "4–6"), (6.5, "6–7"), (7.5, "7–8,5"), (9.0, "8,5–10") })
        {
            row.Children.Add(new Border { Width = 18, Height = 12, Background = B(Palette.IndexColor(v)), Margin = new Thickness(0, 0, 4, 0), VerticalAlignment = VerticalAlignment.Center });
            row.Children.Add(TB(t, 11, _th.Muted));
            row.Children.Add(new Border { Width = 10 });
        }
        sp.Children.Add(row);
        var t2 = TB(L("Indice = guadagno stimato in dB / 2,5 (25 dB = 10). Quadretti: nessuna previsione propria, Hepburn/F5LEN ≥ 5. " +
                      "Area azzurra: copertura in ottica fino alla prima ombra; linea tratteggiata: punto visibile più lontano. " +
                      "Condotti potenziali: doppio calcolo (A dal GFS, B con lo spessore climatologico ITU-R P.453, © ITU) e si usa il peggiore. " +
                      "Strato fondante: corridoio continuo con strati di almeno 40 m; se c'è terra conta la parte di terra coperta (CSIRO 2026). " +
                      "Confronto P.452: metodo ITU con il ΔN del GFS e p = 20%, solo indicativo. " +
                      "I valori del segnale sono stime: servono a confrontare situazioni, non garantiscono il collegamento.",
                      "Index = estimated gain in dB / 2.5 (25 dB = 10). Checkered: no own forecast, Hepburn/F5LEN ≥ 5. " +
                      "Light-blue area: line of sight up to the first shadow; dashed line: farthest visible point. " +
                      "Potential ducts: two calculations (A from GFS, B with ITU-R P.453 climatological thickness, © ITU), the worst is used. " +
                      "Governing layer: continuous corridor of layers at least 40 m thick; over land the covered land share counts (CSIRO 2026). " +
                      "P.452 check: ITU method with GFS ΔN and p = 20%, indicative only. " +
                      "Signal values are estimates for comparing situations, not a guarantee of a contact."), 11, _th.Muted);
        t2.TextWrapping = TextWrapping.Wrap;
        sp.Children.Add(t2);
        return sp;
    }

    // ------------------------------------------------------------------ mappa

    void FcDrawLayer(DrawingContext ctx)
    {
        var m = _fcMap;
        if (m == null) return;
        var (la1, la2, lo1, lo2) = m.Box;
        double span = Math.Max(la2 - la1, lo2 - lo1);
        double step = Math.Max(0.1, Math.Min(2.0, Math.Round(span / 90 * 20) / 20));
        var t = FcTime; double f = FcFreq;
        double lat0 = Math.Floor(la1 / step) * step, lon0 = Math.Floor(lo1 / step) * step;
        for (double lat = lat0; lat < la2; lat += step)
        {
            if (lat < -85 || lat + step > 85) continue;
            for (double lon = lon0; lon < lo2; lon += step)
            {
                if (!m.Screen(new LatLon(lat, lon), out var p1) || !m.Screen(new LatLon(lat + step, lon + step), out var p3)) continue;
                var rect = new Rect(new Point(Math.Min(p1.X, p3.X), Math.Min(p1.Y, p3.Y)), new Point(Math.Max(p1.X, p3.X), Math.Max(p1.Y, p3.Y)));
                if (rect.Right < 0 || rect.Left > m.Bounds.Width || rect.Bottom < 0 || rect.Top > m.Bounds.Height) continue;
                double v = _own.Index(lat + step / 2, lon + step / 2, t, f);
                if (!double.IsNaN(v))
                {
                    if (v < 0.5) continue;
                    var c = Palette.IndexColor(v);
                    ctx.DrawRectangle(new SolidColorBrush(Color.FromArgb((byte)(255 * Math.Min(0.85, 0.35 + v * 0.06)), c.R, c.G, c.B)), null, rect);
                    continue;
                }
                double hv = _st.Tropo.Value("media", lat + step / 2, lon + step / 2, t);
                if (double.IsNaN(hv) || hv < 5) continue;
                // quadretti: 4×4 caselle alternate col colore della fonte esterna
                var hc = Palette.IndexColor(hv);
                var br = new SolidColorBrush(Color.FromArgb(150, hc.R, hc.G, hc.B));
                double w = rect.Width / 4, h = rect.Height / 4;
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (((i + j) & 1) == 0) ctx.DrawRectangle(br, null, new Rect(rect.X + i * w, rect.Y + j * h, w, h));
            }
        }

        // copertura in ottica
        if (_fcLos is { } los)
        {
            var poly = new List<Point>();
            for (int i = 0; i < los.az.Length; i++)
                if (m.Screen(Geo.Destination(los.origin, los.az[i], los.first[i]), out var s)) poly.Add(s);
            if (poly.Count > 2)
            {
                var geo = new StreamGeometry();
                using (var c = geo.Open()) { c.BeginFigure(poly[0], true); foreach (var p in poly.Skip(1)) c.LineTo(p); c.EndFigure(true); }
                ctx.DrawGeometry(new SolidColorBrush(Color.FromArgb(70, 60, 170, 255)), new Pen(new SolidColorBrush(Color.FromRgb(30, 120, 220)), 1.5), geo);
            }
            var far = new List<Point>();
            for (int i = 0; i < los.az.Length; i++)
                if (m.Screen(Geo.Destination(los.origin, los.az[i], Math.Max(0.1, los.far[i])), out var s)) far.Add(s);
            if (far.Count > 2)
            {
                var geo = new StreamGeometry();
                using (var c = geo.Open()) { c.BeginFigure(far[0], false); foreach (var p in far.Skip(1)) c.LineTo(p); c.LineTo(far[0]); c.EndFigure(false); }
                ctx.DrawGeometry(null, new Pen(new SolidColorBrush(Color.FromRgb(30, 120, 220)), 1.2, new DashStyle(new double[] { 4, 3 }, 0)), geo);
            }
        }

        // percorso verso il corrispondente
        if (LocatorX.TryParse(_fcTxLoc?.Text ?? "", out var a) && LocatorX.TryParse(_fcRxLoc?.Text ?? "", out var b))
        {
            var pts = Geo.GreatCircle(a, b, 60);
            var col = _fcLast != null ? (_fcLast.SnrSsb >= Num("fc_req_ssb", 6) ? _th.Ok : _fcLast.SnrFt8 >= Num("fc_req_ft8", -21) ? Color.Parse("#E0A020") : _th.Bad) : _th.Ink;
            var pen = new Pen(new SolidColorBrush(col), 2.4);
            Point? prev = null;
            foreach (var p in pts)
            {
                if (m.Screen(p, out var s)) { if (prev.HasValue && Math.Abs(prev.Value.X - s.X) < m.Bounds.Width / 2) ctx.DrawLine(pen, prev.Value, s); prev = s; }
                else prev = null;
            }
            if (m.Screen(a, out var sa)) ctx.DrawEllipse(new SolidColorBrush(_th.Accent), null, sa, 5, 5);
        }
    }

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

    void FcUpdateGrid()
    {
        if (_fcMap == null) return;
        _fcCts?.Cancel(); _fcCts = new CancellationTokenSource();
        var ct = _fcCts.Token;
        var (la1, la2, lo1, lo2) = _fcMap.Box;
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
        if (!tx.Precise && tx.AslM <= 0) { err = L("Con un locatore a 4 o 6 caratteri serve la quota dell'antenna sul mare.", "With a 4 or 6 character locator the antenna height above sea level is required."); return false; }
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
        var hint = TB(L("La prima volta si scaricano i riquadri SRTM1 del percorso (6–7 MB l'uno) e i profili GFS: può volerci qualche minuto. L'avanzamento è anche nel registro qui sotto.",
                        "The first time the path's SRTM1 tiles (6–7 MB each) and GFS profiles are downloaded: it can take a few minutes. Progress is also in the log below."), 11, _th.Muted);
        hint.TextWrapping = TextWrapping.Wrap;
        _fcResults.Children.Add(hint);
        Task.Run(async () =>
        {
            try
            {
                // profili della previsione lungo il percorso: dalla griglia se la copre, altrimenti si scaricano
                double d = Geo.DistKm(tx.Pos, rx.Pos);
                int n = Math.Max(2, Math.Min(40, (int)Math.Ceiling(d / 25) + 1));
                var pp = Geo.GreatCircle(tx.Pos, rx.Pos, n - 1);
                var prof = pp.Select(p => _own.At(p.Lat, p.Lon, opt.Utc)).ToList();
                CalcLog.W($"percorso {d:0.0} km: {n} punti di previsione; dalla griglia già scaricata {prof.Count(p => p != null)}");
                if (prof.Count(p => p != null) < prof.Count * 0.8)
                {
                    FcSetStatus(L("GFS: scarico i profili lungo il percorso…", "GFS: downloading profiles along the path…"));
                    var ser = await OwnForecast.FetchPoints(FcHttp, pp, FcSetStatus, ct);
                    prof = ser.Select(s =>
                    {
                        int best = -1; double bd = double.MaxValue;
                        for (int i = 0; i < s.Times.Length; i++) { double dd = Math.Abs((s.Times[i] - opt.Utc).TotalMinutes); if (dd < bd) { bd = dd; best = i; } }
                        return best >= 0 && bd <= 90 ? s.Profiles[best] : null;
                    }).ToList();
                }
                var r = await LinkCalc.Compute(tx, rx, opt, _srtm, prof, ct);
                _fcLast = r;
                FcSetStatus(L("Calcolo completato.", "Done.") + (r.TilesMissing > 0 ? " " + L("Riquadri SRTM1 mancanti: ", "Missing SRTM1 tiles: ") + r.TilesMissing : ""));
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
                FcSetStatus(L("Errore: ", "Error: ") + msg);
                FcResultMessage(L("Calcolo non riuscito: ", "Computation failed: ") + msg, _th.Bad);
            }
        });
    }

    void FcShowResult(LinkResult r)
    {
        _fcProgress = null;
        _fcResults.Children.Clear();
        void Line(string a, string b, Color? c = null, bool bold = false)
        {
            var sp = new DockPanel();
            var ta = TB(a, 12, _th.Muted); DockPanel.SetDock(ta, Dock.Left); ta.Width = 210;
            sp.Children.Add(ta);
            sp.Children.Add(TB(b, 12, c ?? _th.Text, bold, true));
            _fcResults.Children.Add(sp);
        }
        _fcResults.Children.Add(TB(L("Risultato", "Result") + $" · {r.FreqMHz:0.#} MHz · {FcTime:dd/MM HH}:00 UTC", 14, _th.Accent, true));
        Line(L("Distanza / azimut", "Distance / azimuth"), $"{r.DistKm:0.0} km / {r.AzDeg:0}°");
        Line(L("Antenne sul mare TX / RX", "Antennas above sea TX / RX"), $"{r.HtxAsl:0} m / {r.HrxAsl:0} m");
        Line(L("Raggio equivalente k", "Effective radius k"), $"{r.K:0.00}" + (r.ForecastUsed ? " (GFS)" : " (4/3)"));
        Line(L("In vista ottica", "Line of sight"), r.Los ? L("sì", "yes") : L("no (ostacolato)", "no (obstructed)"));
        Line(L("1ª zona di Fresnel", "1st Fresnel zone"), double.IsNaN(r.FresnelMinRatio) ? "—" : (r.FresnelMinRatio >= 0.6 ? L("libera", "clear") : L("invasa", "obstructed")) + $" ({L("minimo", "min")} {r.FresnelMinRatio:0.0} F1)");
        foreach (var ob in r.Obstacles) Line("  " + L("ostacolo", "obstacle") + $" {ob.km:0.0} km", $"{ob.hM:0} m · {ob.ratio:0.0} F1 · ν {ob.nu:0.0}");
        Line(L("Spazio libero", "Free space"), $"{r.LfsDb:0.0} dB");
        Line(L("Diffrazione (Bullington)", "Diffraction (Bullington)"), $"{r.LdiffDb:0.0} dB (+{r.LbullDb:0.0})");
        Line(L("Troposcatter (P.452)", "Troposcatter (P.452)"), $"{r.LscatDb:0.0} dB (θ {r.ThetaMrad:0.0} mrad)");
        Line(L("Senza tropo", "Without tropo"), $"{r.LbaseDb:0.0} dB");
        if (r.ForecastUsed)
        {
            if (r.LayerName != "")
            {
                Line(L("Strato fondante", "Governing layer"), $"{r.LayerName} · " + (r.LayerTraps ? L("condotto", "duct") : L("super-rifrattivo", "super-refractive")), _th.Text, true);
                Line(L("Continuità sulla tratta", "Continuity along path"), $"{r.LayerFromKm:0}–{r.LayerToKm:0} km ({r.LayerFrac * 100:0}%)");
                if (!double.IsNaN(r.LandCov)) Line(L("Terra coperta dallo strato", "Land covered by layer"), $"{r.LandInDuctKm:0} / {r.LandKm:0} km ({r.LandCov * 100:0}%) · " + L("frazione usata", "fraction used") + $" {r.FracUsed * 100:0}%");
                Line(L("Quota della cima", "Top height"), $"{r.TopMean:0} m ({r.TopMin:0}–{r.TopMax:0}), " + L("base", "base") + $" {r.BaseMean:0} m");
                Line(L("Inclinazione", "Tilt"), $"{r.TiltMkm:+0.0;-0.0} m/km");
                if (r.LayerTraps) Line(L("Deficit / λ massima", "Deficit / max λ"), $"{r.DeficitMean:0.0} M / {r.LambdaMin:0.00} m");
                if (r.DualN > 0)
                {
                    Line(L("Calcolo A (GFS, salto in 50 m)", "Calc A (GFS, 50 m jump)"), $"{r.DeficitMeanA:0.0} M / {r.LambdaMinA:0.00} m");
                    Line(L("Calcolo B (P.453, spessore", "Calc B (P.453, thickness") + $" {r.ClimThickMean:0} m)", $"{r.DeficitMeanB:0.0} M / {r.LambdaMinB:0.00} m");
                    Line(L("Usato il peggiore", "Worst used"), (r.DualChosenB * 2 >= r.DualN ? "B" : "A") + $" ({r.DualChosenB}/{r.DualN} " + L("punti B", "points B") + ") · " + L("anomalia", "anomaly") + $" {r.AnomalyMean:0.0}×");
                }
                if (r.TerrainInDuctKm > 0) Line(L("Terreno dentro lo strato", "Terrain inside layer"), $"{r.TerrainInDuctKm:0} km");
                if (r.TxVsDuct != "") Line(L("Antenne e condotto", "Antennas vs duct"), $"TX {r.TxVsDuct}, RX {r.RxVsDuct}");
                if (r.ResidualDb > 0) Line(L("Attenuazione residua", "Residual attenuation"), $"{r.ResidualDb:0.0} dB (+ {r.CouplingTxDb + r.CouplingRxDb:0.0} dB " + L("accoppiamento", "coupling") + ")");
            }
            else Line(L("Strato fondante", "Governing layer"), L("nessuno strato continuo", "no continuous layer"));
            Line(L("Guadagno tropo", "Tropo gain"), $"{r.TropoGainDb:+0.0;-0.0} dB");
        }
        Line(L("Perdita prevista", "Predicted loss"), $"{r.LpredDb:0.0} dB · {r.Mechanism}", _th.Text, true);
        if (r.P452Ok) Line(L("Confronto P.452 (p 20%)", "P.452 check (p 20%)"), $"{r.P452Lb:0.0} dB ({r.P452Lb - r.LpredDb:+0.0;-0.0}) · ΔN {r.P452DeltaN:0} · Lba {r.P452Lba:0.0} dB" + (r.P452Alf > 0 ? $" (Alf {r.P452Alf:0.0})" : ""));
        Line(L("EIRP", "EIRP"), $"{r.EirpDbm:0.0} dBm ({Math.Pow(10, (r.EirpDbm - 30) / 10):0} W)");
        Line(L("Antenne TX / RX", "Antennas TX / RX"), $"{r.GtxDbi:0.0} / {r.GrxDbi:0.0} dBi");
        Line(L("Segnale ricevuto", "Received signal"), $"{r.PrxDbm:0.0} dBm · {LinkResult.SMeter(r.PrxDbm)}", _th.Accent, true);
        Line(L("Rumore (P.372)", "Noise (P.372)"), (r.TgalK > 0 ? $"{L("gal.", "gal.")} {r.TgalK:0} + {L("art.", "m-m")} {r.TmanK:0} = " : "") + $"Ta {r.TaK:0} K · Tsys {r.TsysK:0} K");
        Line(L("Rumore in SSB", "SSB noise"), $"{r.NoiseSsbDbm:0.0} dBm (" + L("ai morsetti d'antenna", "at antenna terminals") + ")");
        Color V(double snr, double req) => snr >= req ? _th.Ok : snr >= req - 6 ? Color.Parse("#D08A10") : _th.Bad;
        Line("S/N SSB", $"{r.SnrSsb:+0.0;-0.0} dB", V(r.SnrSsb, Num("fc_req_ssb", 6)), true);
        Line("S/N CW", $"{r.SnrCw:+0.0;-0.0} dB", V(r.SnrCw, Num("fc_req_cw", 0)), true);
        Line("S/N FT8", $"{r.SnrFt8:+0.0;-0.0} dB", V(r.SnrFt8, Num("fc_req_ft8", -21)), true);
        foreach (var n in r.Notes) { var t = TB("• " + n, 11, _th.Muted); t.TextWrapping = TextWrapping.Wrap; _fcResults.Children.Add(t); }
    }

    void FcComputeLos()
    {
        if (!LocatorX.TryParse((_fcTxLoc.Text ?? "").Trim(), out var tp)) { FcSetStatus(L("Locatore della mia stazione non valido.", "Invalid own locator.")); return; }
        var tl = (_fcTxLoc.Text ?? "").Trim();
        var tx = new Station { Loc = tl, Pos = tp, Precise = tl.Length >= 8, AglM = Num("fc_tx_agl", 10), AslM = Num("fc_tx_asl", 0) };
        if (!tx.Precise && tx.AslM <= 0) { FcSetStatus(L("Con un locatore a 4 o 6 caratteri serve la quota dell'antenna sul mare.", "Antenna height above sea level required with a 4–6 character locator.")); return; }
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
                var (az, first, far) = await LinkCalc.LineOfSight(tx, hrx, k, maxKm, _srtm, ct);
                _fcLos = (az, first, far, tp);
                FcSetStatus(L("Copertura in ottica", "Line of sight") + $": k = {k:0.00}, " + L("antenna a ", "antenna at ") + $"{tx.AntennaAsl:0} m s.l.m., " +
                            L("prima ombra media ", "mean first shadow ") + $"{first.Average():0} km, " + L("massimo visibile ", "farthest visible ") + $"{far.Max():0} km");
                Dispatcher.UIThread.Post(() => _fcMap?.InvalidateVisual());
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { FcSetStatus(L("Interrotto.", "Stopped.")); }
            catch (Exception ex) { CalcLog.W("ERRORE: " + ex); FcSetStatus(L("Errore: ", "Error: ") + (ex is OperationCanceledException ? L("tempo scaduto nello scaricamento, riprova", "download timed out, try again") : ex.Message)); }
        });
    }
}

/// <summary>Profilo del terreno sul percorso con la curvatura (k), il raggio diretto e la fascia del condotto.</summary>
public sealed class ProfileView : Control
{
    public Palette Th;
    LinkResult _r;
    double _ductBase = double.NaN, _ductTop = double.NaN;

    public void Set(LinkResult r, List<ProfileResult> prof)
    {
        _r = r;
        _ductTop = r.RunTop.Length > 0 ? r.RunTop.Max() : double.NaN;
        _ductBase = r.RunBase.Length > 0 ? r.RunBase.Min() : double.NaN;
        InvalidateVisual();
    }

    public override void Render(DrawingContext ctx)
    {
        double W = Bounds.Width, H = Bounds.Height;
        ctx.DrawRectangle(new SolidColorBrush(Th?.MapSea ?? Colors.AliceBlue), null, new Rect(0, 0, W, H));
        if (_r == null || _r.Terrain.Length < 2)
        {
            var e = new FormattedText(Lang.Current == "it" ? "Il profilo del terreno compare qui a calcolo finito." : "The terrain profile appears here when the computation ends.",
                CultureInfo.InvariantCulture, FlowDirection.LeftToRight, Typeface.Default, 11, new SolidColorBrush(Th?.Muted ?? Colors.Gray));
            ctx.DrawText(e, new Point(10, H / 2 - 8));
            return;
        }
        var dist = _r.DistAlong; var h = _r.Terrain; double D = _r.DistKm, ae = _r.K * LinkCalc.Re;
        int n = h.Length;
        var y = new double[n];
        double ymin = 0, ymax = Math.Max(_r.HtxAsl, _r.HrxAsl);
        for (int i = 0; i < n; i++)
        {
            double bulge = dist[i] * (D - dist[i]) / (2 * ae) * 1000;    // rigonfiamento della terra al centro (m)
            y[i] = h[i] + bulge;
            ymax = Math.Max(ymax, y[i]);
        }
        if (!double.IsNaN(_ductTop)) ymax = Math.Max(ymax, _ductTop);
        ymax = ymax * 1.08 + 20;
        double X(double dk) => 8 + (W - 16) * dk / D;
        double Y(double m) => H - 14 - (H - 24) * (m - ymin) / (ymax - ymin);
        if (_r.RunDist.Length > 0)
        {
            // lo strato fondante solo dove esiste, con la sua quota e inclinazione (cima e base punto per punto)
            var dc = Color.FromArgb(_r.LayerTraps ? (byte)90 : (byte)50, 255, 140, 0);
            var band = new StreamGeometry();
            double x0 = Math.Max(0, _r.LayerFromKm), x1 = Math.Min(D, _r.LayerToKm);
            using (var c = band.Open())
            {
                c.BeginFigure(new Point(X(x0), Y(_r.RunTop[0])), true);
                for (int i = 0; i < _r.RunDist.Length; i++) c.LineTo(new Point(X(Math.Max(x0, Math.Min(x1, _r.RunDist[i]))), Y(_r.RunTop[i])));
                c.LineTo(new Point(X(x1), Y(_r.RunTop[_r.RunTop.Length - 1])));
                c.LineTo(new Point(X(x1), Y(Math.Max(0, _r.RunBase[_r.RunBase.Length - 1]))));
                for (int i = _r.RunDist.Length - 1; i >= 0; i--) c.LineTo(new Point(X(Math.Max(x0, Math.Min(x1, _r.RunDist[i]))), Y(Math.Max(0, _r.RunBase[i]))));
                c.LineTo(new Point(X(x0), Y(Math.Max(0, _r.RunBase[0]))));
                c.EndFigure(true);
            }
            ctx.DrawGeometry(new SolidColorBrush(dc), new Pen(new SolidColorBrush(Color.FromRgb(230, 120, 0)), 1), band);
        }
        var g = new StreamGeometry();
        using (var c = g.Open())
        {
            c.BeginFigure(new Point(X(0), Y(ymin)), true);
            int stepI = Math.Max(1, n / 800);
            for (int i = 0; i < n; i += stepI) c.LineTo(new Point(X(dist[i]), Y(y[i])));
            c.LineTo(new Point(X(D), Y(y[n - 1])));
            c.LineTo(new Point(X(D), Y(ymin)));
            c.EndFigure(true);
        }
        ctx.DrawGeometry(new SolidColorBrush(Th?.MapLand ?? Colors.Beige), new Pen(new SolidColorBrush(Th?.MapCoast ?? Colors.SaddleBrown), 1), g);
        // limite inferiore della 1ª zona di Fresnel attorno al raggio diretto
        {
            double lam = 299.792458 / _r.FreqMHz;
            var fz = new StreamGeometry();
            using (var c = fz.Open())
            {
                int stepI = Math.Max(1, n / 300);
                c.BeginFigure(new Point(X(0), Y(_r.HtxAsl)), false);
                for (int i = 0; i < n; i += stepI)
                {
                    double d1 = dist[i], d2 = D - dist[i];
                    double f1 = d1 > 0 && d2 > 0 ? Math.Sqrt(lam * d1 * d2 * 1000 / D) : 0;
                    c.LineTo(new Point(X(d1), Y(_r.HtxAsl + (_r.HrxAsl - _r.HtxAsl) * d1 / D - f1)));
                }
                c.LineTo(new Point(X(D), Y(_r.HrxAsl)));
                c.EndFigure(false);
            }
            ctx.DrawGeometry(null, new Pen(new SolidColorBrush(Color.Parse("#7A5AD8")), 1, new DashStyle(new double[] { 3, 3 }, 0)), fz);
            foreach (var ob in _r.Obstacles) ctx.DrawEllipse(new SolidColorBrush(Color.Parse("#D64545")), null, new Point(X(ob.km), Y(ob.hM + ob.km * (D - ob.km) / (2 * ae) * 1000)), 3, 3);
        }
        var ray = new Pen(new SolidColorBrush(_r.Los ? Color.Parse("#1F9D55") : Color.Parse("#D64545")), 1.6);
        ctx.DrawLine(ray, new Point(X(0), Y(_r.HtxAsl)), new Point(X(D), Y(_r.HrxAsl)));
        var ft = new FormattedText($"{_r.DistKm:0} km · k {_r.K:0.00}", CultureInfo.InvariantCulture, FlowDirection.LeftToRight, Typeface.Default, 11, new SolidColorBrush(Th?.Ink ?? Colors.Black));
        ctx.DrawText(ft, new Point(10, 4));
    }
}
