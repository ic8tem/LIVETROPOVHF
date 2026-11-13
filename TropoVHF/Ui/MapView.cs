using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using TropoVHF.Core;

namespace TropoVHF.Ui;

/// <summary>Proiezione: Mercatore o azimutale equidistante centrata sul QTH. Coordinate "mondo" in radianti.</summary>
public sealed class Projection
{
    const double D2R = Math.PI / 180;
    public bool Azimuthal;
    public double Lat0, Lon0;   // centro (gradi) per l'azimutale

    public string Key => Azimuthal ? $"azi:{Lat0:0.###}:{Lon0:0.###}" : "merc";

    public bool Project(double lat, double lon, out double x, out double y)
    {
        if (!Azimuthal)
        {
            double la = Math.Clamp(lat, -85, 85) * D2R;
            x = lon * D2R;
            y = Math.Log(Math.Tan(Math.PI / 4 + la / 2));
            return true;
        }
        double p = lat * D2R, l = (lon - Lon0) * D2R, p0 = Lat0 * D2R;
        double cosc = Math.Sin(p0) * Math.Sin(p) + Math.Cos(p0) * Math.Cos(p) * Math.Cos(l);
        cosc = Math.Clamp(cosc, -1, 1);
        double c = Math.Acos(cosc);
        if (c > 150 * D2R) { x = y = 0; return false; }
        double k = c < 1e-9 ? 1 : c / Math.Sin(c);
        x = k * Math.Cos(p) * Math.Sin(l);
        y = k * (Math.Cos(p0) * Math.Sin(p) - Math.Sin(p0) * Math.Cos(p) * Math.Cos(l));
        return true;
    }

    public LatLon Inverse(double x, double y)
    {
        if (!Azimuthal)
            return new LatLon((2 * Math.Atan(Math.Exp(y)) - Math.PI / 2) / D2R, x / D2R);
        double rho = Math.Sqrt(x * x + y * y);
        if (rho < 1e-12) return new LatLon(Lat0, Lon0);
        double c = rho, p0 = Lat0 * D2R;
        double lat = Math.Asin(Math.Cos(c) * Math.Sin(p0) + y * Math.Sin(c) * Math.Cos(p0) / rho);
        double lon = Lon0 * D2R + Math.Atan2(x * Math.Sin(c), rho * Math.Cos(p0) * Math.Cos(c) - y * Math.Sin(p0) * Math.Sin(c));
        double ld = lon / D2R;
        while (ld > 180) ld -= 360;
        while (ld < -180) ld += 360;
        return new LatLon(lat / D2R, ld);
    }
}

/// <summary>La mappa a strati: disegnata dal programma a ogni ridisegno.</summary>
public sealed class MapView : Control
{
    readonly AppState _st;
    public Palette Th;
    public readonly Projection Proj = new();

    double _cx, _cy, _scale = -1, _fitScale = 1;
    string _viewKey = "";
    Point _pressPos;
    bool _pressed, _moved;
    double _pcx, _pcy;

    public long? SelectedSpotId;
    public string SelectedListener;   // "band|call"
    public LatLon? Probe;
    public event Action SelectionChanged;

    /// <summary>
    /// Anteprima di una fonte ("hepburn" o "f5len") per le sue linguette: solo mappa di base, indice di quella fonte con la
    /// velatura dove non c'è copertura, griglia e riquadri delle regioni. Null = mappa principale.
    /// </summary>
    public string PreviewSource;
    /// <summary>Ora di previsione dell'anteprima (indipendente dalla mappa principale).</summary>
    public int PreviewHours;

    // cache delle linee proiettate (per livello di dettaglio)
    sealed class ProjLine
    {
        public double[] X, Y; public bool[] Ok; public bool AllOk;
        public double La1, La2, Lo1, Lo2;   // riquadro geografico della linea
    }
    sealed class LineCache { public string Key = ""; public List<ProjLine> Land, Coast, Borders; }
    readonly LineCache _c50 = new(), _c10 = new();

    /// <summary>Con l'ingrandimento si passa al dettaglio 1:10 milioni.</summary>
    bool UseDetail => _st.Map.Loaded10 && _fitScale > 0 && _scale / _fitScale >= 1.8;

    // per il clic sugli spot
    readonly List<(long id, List<Point> pts)> _spotHits = new();
    readonly List<(string key, Point p)> _rxHits = new();

    public MapView(AppState st, Palette th)
    {
        _st = st; Th = th;
        Focusable = true;
        ClipToBounds = true;
        Cursor = new Cursor(StandardCursorType.Hand);
    }

    // ------------------------------------------------ vista

    void UpdateProjection()
    {
        Proj.Azimuthal = PreviewSource == null && _st.Proj == "azi";
        if (_st.TryQth(out var q)) { Proj.Lat0 = q.Lat; Proj.Lon0 = q.Lon; }
        else { Proj.Lat0 = 45; Proj.Lon0 = 10; }
        var key = Proj.Key + "|" + Bounds.Width.ToString("0") + "x" + Bounds.Height.ToString("0");
        if (key != _viewKey || _scale <= 0) { _viewKey = key; FitView(); }   // dentro Render: niente InvalidateVisual
    }

    /// <summary>Vista intera richiesta dall'utente: ricalcola e ridisegna.</summary>
    public void ResetView()
    {
        FitView();
        InvalidateVisual();
    }

    /// <summary>Calcola la vista intera senza chiedere un ridisegno (usabile anche durante Render).</summary>
    void FitView()
    {
        double w = Math.Max(100, Bounds.Width), h = Math.Max(100, Bounds.Height);
        if (!Proj.Azimuthal)
        {
            double la1 = 33, lo1 = -25, la2 = 68, lo2 = 45;
            if (PreviewSource != null)
            {
                // anteprima: inquadra tutte le regioni lette di quella fonte
                var rs = _st.Tropo.Rasters.Where(r => r.Fonte == PreviewSource && r.Fit == null).ToList();
                if (rs.Count > 0)
                {
                    la1 = Math.Max(-80, rs.Min(r => r.LatS)); la2 = Math.Min(80, rs.Max(r => r.LatN));
                    lo1 = Math.Max(-180, rs.Min(r => r.LonW)); lo2 = Math.Min(180, rs.Max(r => r.LonE));
                }
                else { la1 = -55; la2 = 72; lo1 = -170; lo2 = 170; }
            }
            Proj.Project(la1, lo1, out var x1, out var y1);
            Proj.Project(la2, lo2, out var x2, out var y2);
            _cx = (x1 + x2) / 2; _cy = (y1 + y2) / 2;
            _fitScale = Math.Min(w / (x2 - x1), h / (y2 - y1));
        }
        else
        {
            _cx = 0; _cy = 0;
            double r = 2600 / Geo.EarthKm;   // raggio iniziale 2600 km
            _fitScale = Math.Min(w, h) / (2 * r);
        }
        _scale = _fitScale;
    }

    Point ToScreen(double x, double y) => new(Bounds.Width / 2 + (x - _cx) * _scale, Bounds.Height / 2 - (y - _cy) * _scale);

    bool ToScreen(LatLon p, out Point s)
    {
        if (Proj.Project(p.Lat, p.Lon, out var x, out var y)) { s = ToScreen(x, y); return true; }
        s = default; return false;
    }

    LatLon FromScreen(Point s)
    {
        double x = _cx + (s.X - Bounds.Width / 2) / _scale, y = _cy - (s.Y - Bounds.Height / 2) / _scale;
        return Proj.Inverse(x, y);
    }

    public void ZoomAt(double f, Point at)
    {
        var before = (x: _cx + (at.X - Bounds.Width / 2) / _scale, y: _cy - (at.Y - Bounds.Height / 2) / _scale);
        _scale = Math.Clamp(_scale * f, _fitScale * 0.4, _fitScale * 80);
        _cx = before.x - (at.X - Bounds.Width / 2) / _scale;
        _cy = before.y + (at.Y - Bounds.Height / 2) / _scale;
        InvalidateVisual();
    }

    public string ZoomPercent => _fitScale > 0 ? $"{_scale / _fitScale * 100:0}%" : "100%";

    // ------------------------------------------------ input

    protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
    {
        ZoomAt(e.Delta.Y > 0 ? 1.18 : 1 / 1.18, e.GetPosition(this));
        e.Handled = true;
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        Focus();
        if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) return;
        if (e.ClickCount >= 2) { ResetView(); e.Handled = true; return; }
        _pressed = true; _moved = false;
        _pressPos = e.GetPosition(this);
        _pcx = _cx; _pcy = _cy;
        e.Pointer.Capture(this);
        e.Handled = true;
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        if (!_pressed) return;
        var p = e.GetPosition(this);
        double dx = p.X - _pressPos.X, dy = p.Y - _pressPos.Y;
        if (!_moved && Math.Abs(dx) + Math.Abs(dy) < 5) return;
        _moved = true;
        _cx = _pcx - dx / _scale;
        _cy = _pcy + dy / _scale;
        InvalidateVisual();
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        if (!_pressed) return;
        _pressed = false;
        e.Pointer.Capture(null);
        if (_moved) return;
        var p = e.GetPosition(this);
        if (PreviewSource != null) return;      // anteprima: niente selezioni
        // 1) stazione in ascolto
        foreach (var (key, rp) in _rxHits)
            if (Math.Abs(rp.X - p.X) < 12 && Math.Abs(rp.Y - p.Y) < 12)
            {
                SelectedListener = SelectedListener == key ? null : key;
                SelectedSpotId = null; Probe = null;
                SelectionChanged?.Invoke(); InvalidateVisual(); return;
            }
        // 2) linea di uno spot
        foreach (var (id, pts) in _spotHits)
            for (int i = 1; i < pts.Count; i++)
                if (DistToSeg(p, pts[i - 1], pts[i]) < 6)
                {
                    SelectedSpotId = id; SelectedListener = null; Probe = null;
                    SelectionChanged?.Invoke(); InvalidateVisual(); return;
                }
        // 3) valori del punto
        Probe = FromScreen(p);
        SelectedListener = null;
        SelectionChanged?.Invoke();
        InvalidateVisual();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        var c = new Point(Bounds.Width / 2, Bounds.Height / 2);
        if (e.Key == Key.Add || e.Key == Key.OemPlus) { ZoomAt(1.4, c); e.Handled = true; }
        else if (e.Key == Key.Subtract || e.Key == Key.OemMinus) { ZoomAt(1 / 1.4, c); e.Handled = true; }
        else if (e.Key == Key.Home) { ResetView(); e.Handled = true; }
    }

    static double DistToSeg(Point p, Point a, Point b)
    {
        double vx = b.X - a.X, vy = b.Y - a.Y, wx = p.X - a.X, wy = p.Y - a.Y;
        double l2 = vx * vx + vy * vy;
        double t = l2 < 1e-9 ? 0 : Math.Clamp((wx * vx + wy * vy) / l2, 0, 1);
        double dx = a.X + t * vx - p.X, dy = a.Y + t * vy - p.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    // ------------------------------------------------ disegno

    static IBrush Br(Color c, double a = 1) => new SolidColorBrush(Color.FromArgb((byte)Math.Clamp(a * c.A, 0, 255), c.R, c.G, c.B));

    LineCache EnsureCache(bool detail)
    {
        var c = detail ? _c10 : _c50;
        var land = detail ? _st.Map.Land10 : _st.Map.Land;
        var coast = detail ? _st.Map.Coast10 : _st.Map.Coast;
        var borders = detail ? _st.Map.Borders10 : _st.Map.Borders;
        var key = Proj.Key + "|" + coast.Count + "|" + land.Count;
        if (key == c.Key && c.Coast != null) return c;
        c.Key = key;
        c.Land = ProjectAll(land);
        c.Coast = ProjectAll(coast);
        c.Borders = ProjectAll(borders);
        return c;
    }

    List<ProjLine> ProjectAll(List<LatLon[]> lines)
    {
        var res = new List<ProjLine>(lines.Count);
        foreach (var l in lines)
        {
            var pl = new ProjLine { X = new double[l.Length], Y = new double[l.Length], Ok = new bool[l.Length], AllOk = true, La1 = 90, La2 = -90, Lo1 = 180, Lo2 = -180 };
            for (int i = 0; i < l.Length; i++)
            {
                pl.Ok[i] = Proj.Project(l[i].Lat, l[i].Lon, out pl.X[i], out pl.Y[i]);
                if (!pl.Ok[i]) pl.AllOk = false;
                if (l[i].Lat < pl.La1) pl.La1 = l[i].Lat; if (l[i].Lat > pl.La2) pl.La2 = l[i].Lat;
                if (l[i].Lon < pl.Lo1) pl.Lo1 = l[i].Lon; if (l[i].Lon > pl.Lo2) pl.Lo2 = l[i].Lon;
            }
            res.Add(pl);
        }
        return res;
    }

    StreamGeometry LinesGeometry(List<ProjLine> lines, bool closed, bool requireAll, (double lat1, double lat2, double lon1, double lon2) box)
    {
        var g = new StreamGeometry();
        double W = Bounds.Width, H = Bounds.Height;
        using (var c = g.Open())
        {
            foreach (var pl in lines)
            {
                if (requireAll && !pl.AllOk) continue;
                // scarta subito le linee fuori dal riquadro geografico visibile
                if (pl.La2 < box.lat1 || pl.La1 > box.lat2 || pl.Lo2 < box.lon1 || pl.Lo1 > box.lon2) continue;
                var xs = pl.X; var ys = pl.Y; var ok = pl.Ok;
                // scarta le linee fuori vista (controllo grossolano sul riquadro)
                double minx = double.MaxValue, maxx = double.MinValue, miny = double.MaxValue, maxy = double.MinValue;
                for (int i = 0; i < xs.Length; i++)
                {
                    if (!ok[i]) continue;
                    var s = ToScreen(xs[i], ys[i]);
                    if (s.X < minx) minx = s.X; if (s.X > maxx) maxx = s.X;
                    if (s.Y < miny) miny = s.Y; if (s.Y > maxy) maxy = s.Y;
                }
                if (maxx < 0 || minx > W || maxy < 0 || miny > H) continue;
                bool open = false;
                double px = 0;
                for (int i = 0; i < xs.Length; i++)
                {
                    if (!ok[i]) { if (open) { c.EndFigure(false); open = false; } continue; }
                    var s = ToScreen(xs[i], ys[i]);
                    bool jump = open && !Proj.Azimuthal && Math.Abs(xs[i] - px) > Math.PI;
                    if (jump) { c.EndFigure(false); open = false; }
                    if (!open) { c.BeginFigure(s, closed); open = true; }
                    else c.LineTo(s);
                    px = xs[i];
                }
                if (open) c.EndFigure(closed);
            }
        }
        return g;
    }

    void Polyline(DrawingContext ctx, IPen pen, List<LatLon> pts, List<Point> collect = null)
    {
        var g = new StreamGeometry();
        using (var c = g.Open())
        {
            bool open = false;
            double px = 0;
            foreach (var p in pts)
            {
                if (!Proj.Project(p.Lat, p.Lon, out var x, out var y)) { if (open) { c.EndFigure(false); open = false; } continue; }
                if (open && !Proj.Azimuthal && Math.Abs(x - px) > Math.PI) { c.EndFigure(false); open = false; }
                var s = ToScreen(x, y);
                collect?.Add(s);
                if (!open) { c.BeginFigure(s, false); open = true; } else c.LineTo(s);
                px = x;
            }
            if (open) c.EndFigure(false);
        }
        ctx.DrawGeometry(null, pen, g);
    }

    void Text(DrawingContext ctx, string s, Point at, double size, Color col, bool bold, Color? halo, bool center = true)
    {
        if (string.IsNullOrEmpty(s)) return;
        var tf = new Typeface(new FontFamily("Consolas, Menlo, DejaVu Sans Mono, monospace"), FontStyle.Normal, bold ? FontWeight.Bold : FontWeight.Normal);
        var ft = new FormattedText(s, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, tf, size, Br(col));
        var o = center ? new Point(at.X - ft.Width / 2, at.Y - ft.Height / 2) : at;
        if (halo.HasValue)
        {
            var geo = ft.BuildGeometry(o);
            if (geo != null) ctx.DrawGeometry(null, new Pen(Br(halo.Value), 3.5, lineJoin: PenLineJoin.Round), geo);
        }
        ctx.DrawText(ft, o);
    }

    public override void Render(DrawingContext ctx)
    {
        var W = Bounds.Width; var H = Bounds.Height;
        if (W < 10 || H < 10) return;
        UpdateProjection();
        var th = Th;
        ctx.DrawRectangle(Br(th.MapSea), null, new Rect(0, 0, W, H));
        var now = Clock.UtcNow;
        var fcTime = now.AddHours(_st.ForecastH);

        // 1) base: terre, coste, confini
        if (_st.ShowBase && _st.Map.Loaded)
        {
            bool detail = UseDetail;
            var lc = EnsureCache(detail);
            var box = VisibleBox();
            ctx.DrawGeometry(Br(th.MapLand), null, LinesGeometry(lc.Land, true, true, box));
            {
                // confini nazionali: spessore doppio e colore spinto verso quello delle coste, per staccarli dalla griglia
                static byte M(byte a, byte b) => (byte)(a + (b - a) * 0.6);
                var bc = Color.FromRgb(M(th.MapBorder.R, th.MapCoast.R), M(th.MapBorder.G, th.MapCoast.G), M(th.MapBorder.B, th.MapCoast.B));
                ctx.DrawGeometry(null, new Pen(Br(bc), detail ? 2.0 : 1.6, lineJoin: PenLineJoin.Round), LinesGeometry(lc.Borders, false, false, box));
            }
            ctx.DrawGeometry(null, new Pen(Br(th.MapCoast), detail ? 1.1 : 1.0, lineJoin: PenLineJoin.Round), LinesGeometry(lc.Coast, false, false, box));
        }

        if (PreviewSource != null)
        {
            // anteprima di una fonte: fusione delle sue immagini, buchi in grigio, riquadri delle regioni
            var pt = now.AddHours(PreviewHours);
            DrawTropo(ctx, PreviewSource, pt, 1.0);
            DrawGrid(ctx);
            DrawRegionFrames(ctx, PreviewSource, pt);
            return;
        }

        // 2) indice tropo
        _st.ViewBox = VisibleBox();
        if (_st.ShowTropo && _st.Tropo.HasData) DrawTropo(ctx, _st.TropoSrc, now.AddHours(_st.ForecastH), _st.TropoOpacity);

        // inversione termica (campitura arancio, sotto le linee)
        if (_st.ShowInv) DrawInversion(ctx, fcTime);

        // 3) griglia locator
        if (_st.ShowGrid) DrawGrid(ctx);

        // 4) isobare e isoterme a 850 hPa
        if (_st.ShowIso) DrawIsobars(ctx, fcTime);
        if (_st.ShowT850) DrawIsotherms(ctx, fcTime);

        // osservazioni (spot, ascolto, decodifiche): attenuate se la barra non è su +0
        using (ctx.PushOpacity(_st.ForecastH == 0 ? 1.0 : 0.45))
        {
            _rxHits.Clear();
            _longSquares.Clear();
            _labels.Clear();
            if (_st.ShowRx) DrawListeners(ctx, now);
            if (_st.ShowDec) DrawDecodes(ctx, now);
            if (_st.ShowQso) DrawQsos(ctx, now);
            _spotHits.Clear();
            if (_st.ShowSpots) DrawSpots(ctx, now);
            DrawLongSquares(ctx);
            DrawRings(ctx);
            DrawCallsToMe(ctx, now);
        }

        // QTH
        if (_st.TryQth(out var q) && ToScreen(q, out var qs))
        {
            ctx.DrawEllipse(null, new Pen(Br(th.Accent), 2.5), qs, 10, 10);
            ctx.DrawEllipse(Br(th.Accent), null, qs, 4, 4);
            Text(ctx, (_st.Call ?? "").ToUpperInvariant() + " " + _st.Loc.ToUpperInvariant(), new Point(qs.X, qs.Y - 20), 13, th.Accent, true, th.Halo);
        }

        // punto cliccato
        if (Probe.HasValue && ToScreen(Probe.Value, out var ps))
        {
            var pen = new Pen(Br(th.Ink), 1.6);
            ctx.DrawEllipse(null, pen, ps, 7, 7);
            ctx.DrawLine(pen, new Point(ps.X - 12, ps.Y), new Point(ps.X + 12, ps.Y));
            ctx.DrawLine(pen, new Point(ps.X, ps.Y - 12), new Point(ps.X, ps.Y + 12));
        }
    }

    void DrawGrid(DrawingContext ctx)
    {
        var th = Th;
        var pen = new Pen(Br(th.MapGrid, 0.9), 0.8);
        var penField = new Pen(Br(th.MapGrid), 1.3);
        // dimensione di un quadrato (2°) in pixel vicino al centro
        var c = FromScreen(new Point(Bounds.Width / 2, Bounds.Height / 2));
        double sqPx = 0;
        if (ToScreen(new LatLon(c.Lat, c.Lon), out var a) && ToScreen(new LatLon(c.Lat, c.Lon + 2), out var b)) sqPx = Math.Abs(b.X - a.X) + Math.Abs(b.Y - a.Y);
        bool squares = sqPx > 16, subs = sqPx > 260;

        double lonStep = squares ? 2 : 20, latStep = squares ? 1 : 10;
        var (bLat1, bLat2, bLon1, bLon2) = VisibleBox();
        if (!squares) { bLat1 = -84; bLat2 = 84; bLon1 = -180; bLon2 = 180; }
        double sLon1 = Math.Floor(bLon1 / lonStep) * lonStep, sLat1 = Math.Floor(bLat1 / latStep) * latStep;
        double pStep = squares ? 0.25 : 1;
        for (double lon = sLon1; lon <= bLon2; lon += lonStep)
        {
            var pts = new List<LatLon>();
            for (double lat = Math.Max(-84, bLat1); lat <= Math.Min(84, bLat2); lat += pStep) pts.Add(new LatLon(lat, lon));
            Polyline(ctx, Math.Abs(lon % 20) < 1e-6 ? penField : pen, pts);
        }
        for (double lat = Math.Max(-80, sLat1); lat <= Math.Min(80, bLat2); lat += latStep)
        {
            var pts = new List<LatLon>();
            for (double lon = bLon1; lon <= bLon2; lon += pStep) pts.Add(new LatLon(lat, lon));
            Polyline(ctx, Math.Abs(lat % 10) < 1e-6 ? penField : pen, pts);
        }
        if (subs)
        {
            var thin = new Pen(Br(th.MapGrid, 0.5), 0.5);
            var tl = FromScreen(new Point(0, 0)); var br = FromScreen(new Point(Bounds.Width, Bounds.Height));
            double lo1 = Math.Floor(Math.Min(tl.Lon, br.Lon)), lo2 = Math.Ceiling(Math.Max(tl.Lon, br.Lon));
            double la1 = Math.Floor(Math.Min(tl.Lat, br.Lat)), la2 = Math.Ceiling(Math.Max(tl.Lat, br.Lat));
            if (lo2 - lo1 < 30 && la2 - la1 < 20)
            {
                for (double lon = lo1; lon <= lo2; lon += 5.0 / 60) ctx.DrawLine(thin, Pt(la1, lon), Pt(la2, lon));
                for (double lat = la1; lat <= la2; lat += 2.5 / 60) ctx.DrawLine(thin, Pt(lat, lo1), Pt(lat, lo2));
            }
        }
        // lettere dei campi, grandi e sbiadite; con lo zoom il nome di ogni quadrato
        if (!squares)
        {
            for (int fl = 0; fl < 18; fl++)
                for (int ft = 0; ft < 18; ft++)
                {
                    double lon = -180 + fl * 20 + 10, lat = -90 + ft * 10 + 5;
                    if (lat < -80 || lat > 80) continue;
                    if (!ToScreen(new LatLon(lat, lon), out var s)) continue;
                    if (s.X < -100 || s.Y < -100 || s.X > Bounds.Width + 100 || s.Y > Bounds.Height + 100) continue;
                    Text(ctx, $"{(char)('A' + fl)}{(char)('A' + ft)}", s, 34, th.MapGridLabel, true, null);
                }
        }
        else
        {
            for (double lon = Math.Floor((bLon1 + 180) / 2) * 2 - 180; lon < bLon2; lon += 2)
                for (double lat = Math.Floor(bLat1); lat < bLat2; lat += 1)
                {
                    if (lat < -89 || lat > 89 || lon < -180 || lon >= 180) continue;
                    if (!ToScreen(new LatLon(lat + 0.5, lon + 1), out var ss)) continue;
                    if (ss.X < -20 || ss.Y < -20 || ss.X > Bounds.Width + 20 || ss.Y > Bounds.Height + 20) continue;
                    Text(ctx, Locator.FromLatLon(lat + 0.5, lon + 1).Substring(0, 4), ss, Math.Clamp(sqPx / 5, 9, 16), th.MapGridLabel, true, null);
                }
        }
    }

    Point Pt(double lat, double lon) { ToScreen(new LatLon(lat, lon), out var s); return s; }

    /// <summary>Riquadro lat/lon visibile (campionando lo schermo), con un margine.</summary>
    (double lat1, double lat2, double lon1, double lon2) VisibleBox()
    {
        double la1 = 90, la2 = -90, lo1 = 180, lo2 = -180;
        for (int i = 0; i <= 6; i++)
            for (int j = 0; j <= 6; j++)
            {
                var p = FromScreen(new Point(Bounds.Width * i / 6, Bounds.Height * j / 6));
                if (double.IsNaN(p.Lat) || double.IsNaN(p.Lon)) continue;
                la1 = Math.Min(la1, p.Lat); la2 = Math.Max(la2, p.Lat);
                lo1 = Math.Min(lo1, p.Lon); lo2 = Math.Max(lo2, p.Lon);
            }
        if (lo2 - lo1 > 300) { lo1 = -180; lo2 = 180; }
        return (Math.Max(-90, la1 - 2), Math.Min(90, la2 + 2), Math.Max(-180, lo1 - 4), Math.Min(180, lo2 + 4));
    }

    void DrawTropo(DrawingContext ctx, string src, DateTime t, double opacity)
    {
        // griglia mondiale: si campiona solo la zona visibile, con un passo che dà circa 100 celle sul lato lungo
        var (la1, la2, lo1, lo2) = VisibleBox();
        double span = Math.Max(la2 - la1, lo2 - lo1);
        double step = Math.Clamp(Math.Round(span / 100 * 20) / 20, 0.1, 2.0);
        var noData = Br(Color.FromRgb(128, 128, 128), (PreviewSource != null ? 0.30 : 0.13) * opacity);
        double lat0 = Math.Floor(la1 / step) * step, lon0 = Math.Floor(lo1 / step) * step;
        for (double lat = lat0; lat < la2; lat += step)
        {
            if (lat < -85 || lat + step > 85) continue;
            for (double lon = lon0; lon < lo2; lon += step)
            {
                if (!ToScreen(new LatLon(lat, lon), out var p1) || !ToScreen(new LatLon(lat, lon + step), out var p2) ||
                    !ToScreen(new LatLon(lat + step, lon + step), out var p3) || !ToScreen(new LatLon(lat + step, lon), out var p4)) continue;
                if (Math.Max(Math.Max(p1.X, p2.X), Math.Max(p3.X, p4.X)) < 0 || Math.Min(Math.Min(p1.X, p2.X), Math.Min(p3.X, p4.X)) > Bounds.Width) continue;
                if (Math.Max(Math.Max(p1.Y, p2.Y), Math.Max(p3.Y, p4.Y)) < 0 || Math.Min(Math.Min(p1.Y, p2.Y), Math.Min(p3.Y, p4.Y)) > Bounds.Height) continue;
                double v = _st.Tropo.Value(src, lat + step / 2, lon + step / 2, t);
                IBrush brush;
                if (double.IsNaN(v)) brush = noData;                       // nessuna fonte copre questo punto
                else if (v < 0.5) continue;                                // atmosfera normale: trasparente
                else brush = Br(Palette.IndexColor(v), opacity * Math.Min(1, 0.35 + v * 0.065));
                var g = new StreamGeometry();
                using (var c = g.Open()) { c.BeginFigure(p1, true); c.LineTo(p2); c.LineTo(p3); c.LineTo(p4); c.EndFigure(true); }
                ctx.DrawGeometry(brush, null, g);
            }
        }
    }

    /// <summary>Riquadri delle regioni lette di una fonte, con nome e ora valida dell'immagine usata.</summary>
    void DrawRegionFrames(DrawingContext ctx, string src, DateTime t)
    {
        var cols = new[] { "#1E78E0", "#D62839", "#2A9D4B", "#B85A0C", "#7B3FD1", "#0E7C8C", "#C2185B", "#5D6D00" };
        var rs = _st.Tropo.Rasters.Where(r => r.Fonte == src && r.Fit == null).GroupBy(r => r.Region).ToList();
        int k = 0;
        foreach (var g in rs)
        {
            var r = g.OrderBy(x => Math.Abs((x.ValidUtc - t).TotalHours)).First();
            var col = Color.Parse(cols[k++ % cols.Length]);
            var pts = new List<LatLon>();
            const int n = 24;
            for (int i = 0; i <= n; i++) pts.Add(new LatLon(r.LatN, r.LonW + (r.LonE - r.LonW) * i / n));
            for (int i = 0; i <= n; i++) pts.Add(new LatLon(r.LatN - (r.LatN - r.LatS) * i / n, r.LonE));
            for (int i = 0; i <= n; i++) pts.Add(new LatLon(r.LatS, r.LonE - (r.LonE - r.LonW) * i / n));
            for (int i = 0; i <= n; i++) pts.Add(new LatLon(r.LatS + (r.LatN - r.LatS) * i / n, r.LonW));
            var pen = new Pen(Br(col, 0.95), 2) { DashStyle = new DashStyle(new double[] { 6, 3 }, 0) };
            for (int i = 1; i < pts.Count; i++)
            {
                var a0 = pts[i - 1]; var b0 = pts[i];
                if (a0.Lon > 180) a0 = new LatLon(a0.Lat, a0.Lon - 360);
                if (b0.Lon > 180) b0 = new LatLon(b0.Lat, b0.Lon - 360);
                if (Math.Abs(a0.Lon - b0.Lon) > 90) continue;      // salto dell'antimeridiano
                if (ToScreen(a0, out var a) && ToScreen(b0, out var b)) ctx.DrawLine(pen, a, b);
            }
            var name = TropoSources.Regions.FirstOrDefault(x => x.Key == g.Key)?.Nome ?? g.Key;
            var lonL = r.LonW > 180 ? r.LonW - 360 : r.LonW;
            if (ToScreen(new LatLon(r.LatN, lonL), out var tl))
                Text(ctx, $"{name} · {r.ValidUtc:dd/MM HH}Z", new Point(tl.X + 6, tl.Y + 10), 12, col, true, Th.Halo, false);
        }
    }

    /// <summary>Dove l'aria a 925/850 hPa è più calda che al suolo: campitura arancio, più intensa con l'inversione più forte.</summary>
    void DrawInversion(DrawingContext ctx, DateTime t)
    {
        var (la1, la2, lo1, lo2) = VisibleBox();
        double span = Math.Max(la2 - la1, lo2 - lo1);
        double step = Math.Clamp(Math.Round(span / 80 * 4) / 4, 0.25, 3.0);
        double lat0 = Math.Floor(la1 / step) * step, lon0 = Math.Floor(lo1 / step) * step;
        var col = Color.Parse("#FF7A00");
        for (double lat = lat0; lat < la2; lat += step)
        {
            if (lat < -85 || lat + step > 85) continue;
            for (double lon = lon0; lon < lo2; lon += step)
            {
                double v = _st.Pressure.InversionAt(new LatLon(lat + step / 2, lon + step / 2), t);
                if (double.IsNaN(v) || v < 0.5) continue;
                if (!ToScreen(new LatLon(lat, lon), out var p1) || !ToScreen(new LatLon(lat, lon + step), out var p2) ||
                    !ToScreen(new LatLon(lat + step, lon + step), out var p3) || !ToScreen(new LatLon(lat + step, lon), out var p4)) continue;
                if (Math.Max(Math.Max(p1.X, p2.X), Math.Max(p3.X, p4.X)) < 0 || Math.Min(Math.Min(p1.X, p2.X), Math.Min(p3.X, p4.X)) > Bounds.Width) continue;
                if (Math.Max(Math.Max(p1.Y, p2.Y), Math.Max(p3.Y, p4.Y)) < 0 || Math.Min(Math.Min(p1.Y, p2.Y), Math.Min(p3.Y, p4.Y)) > Bounds.Height) continue;
                var g = new StreamGeometry();
                using (var c = g.Open()) { c.BeginFigure(p1, true); c.LineTo(p2); c.LineTo(p3); c.LineTo(p4); c.EndFigure(true); }
                ctx.DrawGeometry(Br(col, Math.Min(0.45, 0.08 + v * 0.05)), null, g);
            }
        }
    }

    /// <summary>Isoterme a 850 hPa ogni 4 °C: tratteggio rosso sopra lo zero, blu sotto, viola sullo zero.</summary>
    void DrawIsotherms(DrawingContext ctx, DateTime t)
    {
        var th = Th;
        var segs = _st.Pressure.IsothermsFor(t);
        if (segs.Count == 0) return;
        var dash = new DashStyle(new double[] { 5, 3 }, 0);
        Pen PenFor(int level) => new Pen(Br(level > 0 ? Color.Parse("#D8452E") : level < 0 ? Color.Parse("#2E6FD8") : Color.Parse("#8A2BE2"), 0.85), level == 0 ? 1.6 : 1.1) { DashStyle = dash };
        var pens = new Dictionary<int, Pen>();
        var labels = new List<Point>();
        foreach (var s in segs)
        {
            if (!ToScreen(s.A, out var a) || !ToScreen(s.B, out var b)) continue;
            if (Math.Max(a.X, b.X) < 0 || Math.Min(a.X, b.X) > Bounds.Width || Math.Max(a.Y, b.Y) < 0 || Math.Min(a.Y, b.Y) > Bounds.Height) continue;
            if (!pens.TryGetValue(s.Level, out var pen)) pens[s.Level] = pen = PenFor(s.Level);
            ctx.DrawLine(pen, a, b);
            var m = new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
            if (m.X < 20 || m.Y < 60 || m.X > Bounds.Width - 20 || m.Y > Bounds.Height - 60) continue;
            if (labels.Count < 40 && labels.All(l => Math.Abs(l.X - m.X) + Math.Abs(l.Y - m.Y) > 260))
            {
                labels.Add(m);
                Text(ctx, s.Level + "°", m, 10, s.Level > 0 ? Color.Parse("#B2432F") : Color.Parse("#1F5FA8"), true, th.Halo);
            }
        }
    }

    void DrawIsobars(DrawingContext ctx, DateTime t)
    {
        var th = Th;
        var (segs, centers) = _st.Pressure.ContoursFor(t);
        if (segs.Count == 0) return;
        var pen = new Pen(Br(th.Iso, 0.85), 1.3);
        var labels = new List<Point>();
        foreach (var s in segs)
        {
            if (!ToScreen(s.A, out var a) || !ToScreen(s.B, out var b)) continue;
            if (Math.Max(a.X, b.X) < 0 || Math.Min(a.X, b.X) > Bounds.Width || Math.Max(a.Y, b.Y) < 0 || Math.Min(a.Y, b.Y) > Bounds.Height) continue;
            ctx.DrawLine(pen, a, b);
            var m = new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
            if (m.X < 20 || m.Y < 60 || m.X > Bounds.Width - 20 || m.Y > Bounds.Height - 60) continue;
            if (labels.Count < 60 && labels.All(l => Math.Abs(l.X - m.X) + Math.Abs(l.Y - m.Y) > 230))
            {
                labels.Add(m);
                Text(ctx, s.Level.ToString(), m, 11, th.Ink, true, th.Halo);
            }
        }
        foreach (var c in centers)
        {
            if (!ToScreen(c.Pos, out var p)) continue;
            Text(ctx, c.High ? "H" : "L", p, 28, c.High ? Color.Parse("#1F5FA8") : Color.Parse("#B2432F"), true, th.Halo);
            Text(ctx, c.Value.ToString("0"), new Point(p.X, p.Y + 20), 12, th.Ink, true, th.Halo);
        }
    }

    void DrawListeners(DrawingContext ctx, DateTime now)
    {
        var th = Th;
        var list = _st.Listeners.Get(_st.Band, _st.WindowMin, now);
        // 1) in rosa, SEMPRE visibili senza cliccare: ciò che ogni stazione in ascolto sente e rimanda in rete
        var thin = new Pen(Br(Palette.ListenerLine, 0.9), 2.0, lineCap: PenLineCap.Round);   // piene e sempre visibili, anche senza selezione
        var bold = new Pen(Br(Palette.ListenerLine, 1.0), 3.0, lineCap: PenLineCap.Round);
        const bool labels = true;   // nominativi sempre scritti, senza dover cliccare
        var heardDots = new Dictionary<string, Point>();
        Listener sel = null;
        foreach (var l in list)
        {
            if (!Locator.TryToLatLon(l.Grid, out var me)) continue;
            bool isSel = l.Band + "|" + l.Call == SelectedListener;
            if (isSel) { sel = l; continue; }
            foreach (var h in l.Heard)
            {
                if (!_st.TryPos(h.Key, h.Value.grid, out var tp) || !Pass(me, tp)) continue;
                Polyline(ctx, thin, Geo.GreatCircle(me, tp, 16));
                if (ToScreen(tp, out var ts)) heardDots[h.Key] = ts;
            }
        }
        foreach (var kv in heardDots)
        {
            ctx.DrawEllipse(Br(Palette.ListenerLine), new Pen(Brushes.White, 1.2), kv.Value, 3.5 * 1.6, 3.5 * 1.6);   // stazioni sentite: pallino il 60% più grande
            if (labels) Label(ctx, kv.Key, new Point(kv.Value.X, kv.Value.Y + 14), 10, Palette.ListenerViolet, false, false);
        }
        if (sel != null && Locator.TryToLatLon(sel.Grid, out var sp))
            foreach (var h in sel.Heard)
            {
                if (!_st.TryPos(h.Key, h.Value.grid, out var tp) || !Pass(sp, tp)) continue;
                Polyline(ctx, bold, Geo.GreatCircle(sp, tp, 24));
                if (ToScreen(tp, out var ts))
                {
                    ctx.DrawEllipse(Br(Palette.ListenerLine), new Pen(Brushes.White, 1.2), ts, 3.5 * 1.6, 3.5 * 1.6);
                    Text(ctx, h.Key, new Point(ts.X, ts.Y + 13), 10.5, Palette.ListenerViolet, true, th.Halo);
                }
            }
        // 2) le stazioni in ascolto: cerchio vuoto rosa scuro; oltre 1000 km dal proprio QTH solo il quadrato in lilla
        foreach (var l in list)
        {
            if (!Locator.TryToLatLon(l.Grid, out var p)) continue;
            if (_st.FarFromMe(p)) { AddLong(p); continue; }
            if (!ToScreen(p, out var s)) continue;
            var key = l.Band + "|" + l.Call;
            _rxHits.Add((key, s));
            bool isSel = key == SelectedListener;
            // cerchio vuoto: il 40% più grande del vecchio rombo; nominativo il 25% più grande
            DrawRing(ctx, s, (isSel ? 9 : 6.5) * 1.4, isSel ? 2.6 : 1.8, 1);
            if (isSel || labels) Label(ctx, l.Call, new Point(s.X, s.Y - (isSel ? 24 : 20)), (isSel ? 12 : 10.5) * 1.25, Palette.ListenerViolet, true, isSel);
        }
    }

    /// <summary>Stazione in ascolto: cerchio vuoto rosa scuro con alone.</summary>
    void DrawRing(DrawingContext ctx, Point s, double r, double w, double op)
    {
        ctx.DrawEllipse(null, new Pen(Br(Th.Halo, 0.9 * op), w + 2.2), s, r, r);
        ctx.DrawEllipse(null, new Pen(Br(Palette.ListenerViolet, op), w), s, r, r);
    }

    void DrawDiamond(DrawingContext ctx, Point s, double r, double w, double op)
    {
        var g = new StreamGeometry();
        using (var c = g.Open())
        {
            c.BeginFigure(new Point(s.X, s.Y - r), false);
            c.LineTo(new Point(s.X + r, s.Y));
            c.LineTo(new Point(s.X, s.Y + r));
            c.LineTo(new Point(s.X - r, s.Y));
            c.EndFigure(true);
        }
        ctx.DrawGeometry(null, new Pen(Br(Th.Halo, 0.9 * op), w + 2.2, lineJoin: PenLineJoin.Round), g);
        ctx.DrawGeometry(null, new Pen(Br(Palette.ListenerViolet, op), w, lineJoin: PenLineJoin.Round), g);
    }

    void DrawTriangle(DrawingContext ctx, Point s, double sz, Color fill, Color stroke, double op)
    {
        var g = new StreamGeometry();
        using (var c = g.Open())
        {
            c.BeginFigure(new Point(s.X, s.Y - sz), true);
            c.LineTo(new Point(s.X + sz * 0.9, s.Y + sz * 0.65));
            c.LineTo(new Point(s.X - sz * 0.9, s.Y + sz * 0.65));
            c.EndFigure(true);
        }
        ctx.DrawGeometry(Br(fill, op), new Pen(Br(stroke, op), 1.6), g);
    }

    void DrawDecodes(DrawingContext ctx, DateTime now)
    {
        if (!_st.TryQth(out var q)) return;
        var th = Th;
        var halo = new Pen(Br(th.Halo, 0.8), 5, lineCap: PenLineCap.Round);
        var pen = new Pen(Br(Palette.DecodeRed), 2.2, lineCap: PenLineCap.Round);
        // QSO in corso con quella stazione: linea rossa a tratti che lampeggia
        bool on = (DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond / 500) % 2 == 0;
        var live = new Pen(Br(Palette.DecodeRed, on ? 1 : 0.25), 2.6, lineCap: PenLineCap.Flat) { DashStyle = new DashStyle(new double[] { 4, 3 }, 0) };
        foreach (var d in _st.Decodes.Get(_st.Band, _st.WindowMin, now))
        {
            if (!_st.TryPos(d.Call, d.Grid, out var p) || !Pass(q, p)) continue;
            var gc = Geo.GreatCircle(q, p, 32);
            bool inQso = _st.QsoInProgress(d.Call, now);
            if (inQso) EnsureBlink();
            Polyline(ctx, halo, gc);
            Polyline(ctx, inQso ? live : pen, gc);
            if (ToScreen(p, out var s))
            {
                ctx.DrawEllipse(Br(Palette.DecodeRed), new Pen(Brushes.White, 1.5), s, 4.5, 4.5);
                Text(ctx, d.Call, new Point(s.X, s.Y + 14), 11, Color.Parse("#B01E2E"), true, th.Halo);
            }
        }
    }

    /// <summary>
    /// GRAVES: il radar a JN27SI con i quattro settori del fascio (D, C, B, A, da 90° a 270°) fino a 1000 km,
    /// divisi nei 6 passi da 7,5°; il passo illuminato adesso è più marcato (la sequenza è legata all'UTC).
    /// </summary>
    void DrawGraves(DrawingContext ctx, DateTime now)
    {
        var th = Th;
        var cols = new[] { "#E87BA4", "#2A9FD6", "#7B5CE0", "#EB6834" };
        int act = Graves.ActiveStep(now);
        const double R = 1000;
        for (int sec = 0; sec < 4; sec++)
            for (int st = 0; st < Graves.Steps; st++)
            {
                double az0 = Graves.AzMin + sec * Graves.SectorDeg + st * Graves.StepDeg, az1 = az0 + Graves.StepDeg;
                var pts = new List<Point>();
                if (!ToScreen(Graves.Pos, out var c0)) return;
                pts.Add(c0);
                for (double az = az0; az <= az1 + 0.01; az += 1.5)
                    if (ToScreen(Geo.Destination(Graves.Pos, az, R), out var p)) pts.Add(p);
                if (pts.Count < 3) continue;
                var g = new StreamGeometry();
                using (var c = g.Open()) { c.BeginFigure(pts[0], true); foreach (var p in pts.Skip(1)) c.LineTo(p); c.EndFigure(true); }
                var col = Color.Parse(cols[sec]);
                ctx.DrawGeometry(Br(col, st == act ? 0.16 : 0.05), new Pen(Br(col, 0.35), 0.6), g);
            }
        if (ToScreen(Graves.Pos, out var gp))
        {
            ctx.DrawEllipse(Br(Color.Parse("#EB6834")), new Pen(Brushes.White, 1.5), gp, 5, 5);
            Text(ctx, "GRAVES 143,050", new Point(gp.X, gp.Y - 14), 11, th.Dark ? Color.Parse("#F5A27A") : Color.Parse("#A3431B"), true, th.Halo);
        }
    }

    /// <summary>QSO desunti dal live: linea verde tra le due stazioni, con pallino e nominativo a ciascun capo.</summary>
    void DrawQsos(DrawingContext ctx, DateTime now)
    {
        var th = Th;
        var halo = new Pen(Br(th.Halo, 0.8), 5, lineCap: PenLineCap.Round);
        var pen = new Pen(Br(Palette.QsoGreen), 2.2, lineCap: PenLineCap.Round);
        var ink = th.Dark ? Color.Parse("#5BD08A") : Color.Parse("#15703C");
        foreach (var q in _st.Qsos.Get(_st.Band, _st.WindowMin, now))
        {
            if (!_st.TryPos(q.A, q.GridA, out var a) || !_st.TryPos(q.B, q.GridB, out var b) || !Pass(a, b)) continue;
            var gc = Geo.GreatCircle(a, b, 32);
            Polyline(ctx, halo, gc);
            Polyline(ctx, pen, gc);
            foreach (var (p, call) in new[] { (a, q.A), (b, q.B) })
                if (ToScreen(p, out var s))
                {
                    ctx.DrawEllipse(Br(Palette.QsoGreen), new Pen(Brushes.White, 1.5), s, 4.5, 4.5);
                    Text(ctx, call, new Point(s.X, s.Y + 14), 11, ink, true, th.Halo);
                }
        }
    }

    // ------------------------------------------------ criteri di esclusione generale

    readonly HashSet<string> _longSquares = new();
    readonly HashSet<string> _labels = new();

    /// <summary>
    /// Nominativo sulla mappa: lo stesso nominativo nello stesso punto (stessa stazione in più spot) si scrive una
    /// volta sola per disegno; quello forzato (selezione) si scrive comunque.
    /// </summary>
    void Label(DrawingContext ctx, string text, Point p, double size, Color col, bool bold, bool force)
    {
        if (string.IsNullOrEmpty(text)) return;
        var key = text + "|" + (int)(p.X / 6) + "|" + (int)(p.Y / 6);
        if (!_labels.Add(key) && !force) return;
        Text(ctx, text, p, size, col, bold, Th.Halo);
    }

    /// <summary>
    /// Vero se il collegamento a–b si disegna. Sotto i 100 km (con la casella) si scarta; se una delle due stazioni sta
    /// oltre 1000 km dal proprio QTH non si disegna la linea ma si segnano i quadrati locator (4 caratteri) delle due
    /// stazioni, in lilla tratteggiato a 45°.
    /// </summary>
    bool Pass(LatLon a, LatLon b)
    {
        int c = _st.DistClass(a, b);
        if (c == 0) return true;
        if (c > 0) { AddLong(a); AddLong(b); }
        return false;
    }

    /// <summary>
    /// Segna un quadrato lilla, ma mai il proprio: il quadrato del QTH (dal locatore impostato, qualunque nominativo usi
    /// chi sta alla stazione) resta sempre libero, anche se una stazione oltre la corona ci ascolta.
    /// </summary>
    void AddLong(LatLon p)
    {
        var sq = Square(p);
        if (sq == "") return;
        if (_st.TryQth(out var me))
        {
            if (Square(me) == sq) return;
            // a ridosso della corona no: si colora solo un quadrato che sta tutto oltre la corona scelta
            // (il suo punto più vicino al QTH è più lontano del raggio)
            double lon0 = (sq[0] - 'A') * 20 - 180 + (sq[2] - '0') * 2, lat0 = (sq[1] - 'A') * 10 - 90 + (sq[3] - '0');
            var near = new LatLon(Math.Clamp(me.Lat, lat0, lat0 + 1), Math.Clamp(me.Lon, lon0, lon0 + 2));
            if (_st.LongAsSquares && Geo.DistKm(me, near) <= _st.LongKm) return;
        }
        _longSquares.Add(sq);
    }

    static string Square(LatLon p) { var l = Locator.FromLatLon(p.Lat, p.Lon); return l.Length >= 4 ? l[..4] : ""; }

    Avalonia.Threading.DispatcherTimer _blink;
    bool _blinkUsed;   // qualcosa in questo disegno lampeggia: il timer resta acceso

    void EnsureBlink()
    {
        _blinkUsed = true;
        if (_blink == null)
        {
            _blink = new Avalonia.Threading.DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
            _blink.Tick += (_, _) => InvalidateVisual();
        }
        if (!_blink.IsEnabled) _blink.Start();
    }

    /// <summary>
    /// Chi mi ha chiamato sull'UDP locale: linea nera tratteggiata lampeggiante dal mio QTH alla stazione (locatore da
    /// CALL3.TXT). Si disegna sempre, sopra tutto e senza criteri di esclusione, finché la chiamata è recente.
    /// </summary>
    void DrawCallsToMe(DrawingContext ctx, DateTime now)
    {
        var list = _st.CallsToMe(now);
        if (list.Count == 0) { if (!_blinkUsed) _blink?.Stop(); _blinkUsed = false; return; }
        EnsureBlink();
        _blinkUsed = false;
        if (!_st.TryQth(out var me)) return;
        bool on = (DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond / 500) % 2 == 0;
        var halo = new Pen(Br(Colors.White, on ? 0.9 : 0.35), 5, lineCap: PenLineCap.Round);
        var pen = new Pen(Br(Colors.Black, on ? 1 : 0.25), 2.6, lineCap: PenLineCap.Flat) { DashStyle = new DashStyle(new double[] { 4, 3 }, 0) };
        foreach (var c in list)
        {
            if (!_st.TryPos(c.call, c.grid, out var p)) continue;
            var gc = Geo.GreatCircle(me, p, 48);
            Polyline(ctx, halo, gc);
            Polyline(ctx, pen, gc);
            if (ToScreen(p, out var s))
            {
                ctx.DrawEllipse(Br(Colors.Black, on ? 1 : 0.4), new Pen(Br(Colors.White), 1.5), s, 5, 5);
                Text(ctx, $"{c.call} {c.grid} → {(_st.LoginOrCall)}", new Point(s.X + 8, s.Y - 8), 12, Colors.Black, true, Colors.White, false);
            }
        }
    }

    /// <summary>Corone di 1000, 700 e 500 km attorno al proprio QTH; quella scelta per il criterio è più marcata.</summary>
    void DrawRings(DrawingContext ctx)
    {
        if (!_st.ShowRings || !_st.TryQth(out var me)) return;
        var col = Palette.LongLilac;
        foreach (var km in AppState.RingKm)
        {
            bool sel = _st.LongAsSquares && Math.Abs(km - _st.LongKm) < 1;
            var pen = new Pen(Br(col, sel ? 0.95 : 0.55), sel ? 2.2 : 1.2) { DashStyle = new DashStyle(sel ? new double[] { 6, 3 } : new double[] { 3, 4 }, 0) };
            var pts = new List<LatLon>();
            for (int az = 0; az <= 360; az += 3) pts.Add(Geo.Destination(me, az, km));
            Polyline(ctx, pen, pts);
            // etichetta a nord
            if (ToScreen(Geo.Destination(me, 0, km), out var s))
                Text(ctx, $"{km} km", new Point(s.X + 4, s.Y - 14), 10, col, sel, Th.Halo, false);
        }
    }

    void DrawLongSquares(DrawingContext ctx)
    {
        if (_longSquares.Count == 0) return;
        var col = Palette.LongLilac;
        var hatch = new Pen(Br(col, 0.85), 1.6);
        var edge = new Pen(Br(col, 0.95), 1.4);
        foreach (var sq in _longSquares)
        {
            if (sq.Length < 4) continue;
            double lon0 = (sq[0] - 'A') * 20 - 180 + (sq[2] - '0') * 2, lat0 = (sq[1] - 'A') * 10 - 90 + (sq[3] - '0');
            var pts = new List<Point>();
            void Edge(double la1, double lo1, double la2, double lo2)
            {
                for (int i = 0; i < 8; i++)
                    if (ToScreen(new LatLon(la1 + (la2 - la1) * i / 8, lo1 + (lo2 - lo1) * i / 8), out var s)) pts.Add(s);
            }
            Edge(lat0, lon0, lat0, lon0 + 2); Edge(lat0, lon0 + 2, lat0 + 1, lon0 + 2);
            Edge(lat0 + 1, lon0 + 2, lat0 + 1, lon0); Edge(lat0 + 1, lon0, lat0, lon0);
            if (pts.Count < 3) continue;
            var g = new StreamGeometry();
            using (var c = g.Open()) { c.BeginFigure(pts[0], true); foreach (var p in pts.Skip(1)) c.LineTo(p); c.EndFigure(true); }
            double x0 = pts.Min(p => p.X), x1 = pts.Max(p => p.X), y0 = pts.Min(p => p.Y), y1 = pts.Max(p => p.Y);
            if (x1 < 0 || y1 < 0 || x0 > Bounds.Width || y0 > Bounds.Height) continue;
            ctx.DrawGeometry(Br(col, 0.12), null, g);
            using (ctx.PushGeometryClip(g))
            {
                double step = 7, h = y1 - y0;
                // linee a 45°: da (x, y1) a (x + h, y0)
                for (double x = x0 - h; x <= x1; x += step)
                    ctx.DrawLine(hatch, new Point(x, y1), new Point(x + h, y0));
            }
            ctx.DrawGeometry(null, edge, g);
        }
    }

    void DrawSpots(DrawingContext ctx, DateTime now)
    {
        var th = Th;
        var spots = _st.Spots.Filter(_st.Band, _st.WindowMin, now);
        Spot selSpot = null;
        foreach (var s in spots)
        {
            if (s.Id == SelectedSpotId) { selSpot = s; continue; }
            DrawSpot(ctx, s, false);
        }
        if (selSpot != null) DrawSpot(ctx, selSpot, true);
    }

    void DrawSpot(DrawingContext ctx, Spot s, bool sel)
    {
        var th = Th;
        if (!_st.TryPos(s.Dx, s.DxLoc, out var dx) || !_st.TryPos(s.Spotter, s.DeLoc, out var de) || !Pass(dx, de)) return;
        var gc = Geo.GreatCircle(dx, de, 32);
        var col = s.Airscatter ? Palette.AirscatterYellow : Palette.SpotBlue;
        double w = sel ? 4 : 2.6;
        var haloCol = s.Airscatter ? Palette.AirscatterEdge : th.Halo;
        Polyline(ctx, new Pen(Br(haloCol, 0.85), w + (sel ? 5 : 2.6), lineCap: PenLineCap.Round), gc);
        var dash = s.Airscatter ? new DashStyle(new double[] { 3, 2 }, 0) : null;
        var pts = new List<Point>();
        Polyline(ctx, new Pen(Br(col, sel ? 1 : 0.9), w, dash, PenLineCap.Round), gc, pts);
        _spotHits.Add((s.Id, pts));
        double r = sel ? 7 : 5;
        if (ToScreen(dx, out var a)) ctx.DrawEllipse(Br(col), new Pen(Brushes.White, 2), a, r, r);
        if (ToScreen(de, out var b)) ctx.DrawEllipse(Br(col), new Pen(Brushes.White, 2), b, r, r);
        // nominativi sempre scritti, senza dover cliccare (più grandi sullo spot selezionato)
        if (ToScreen(dx, out a)) Label(ctx, s.Dx, new Point(a.X, a.Y - 16), sel ? 13 : 11.5, th.Ink, true, sel);
        if (ToScreen(de, out b)) Label(ctx, s.Spotter, new Point(b.X, b.Y - 16), sel ? 13 : 11.5, th.Ink, true, sel);
    }
}
