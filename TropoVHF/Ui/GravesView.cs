using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using TropoVHF.Core;

namespace TropoVHF.Ui;

/// <summary>
/// Il cielo visto da GRAVES (portato da YA-MS, src/Controls/GravesView.cs, adattato alla tropo): pianta azimutale
/// equidistante centrata sul radar, N in alto ed E a destra. I quattro settori D, C, B, A (90°–270°) sono divisi nei
/// 6 passi da 7,5°: il passo illuminato adesso è più chiaro, quello che illumina la mia direzione è giallo.
/// In verde la mia stazione e la tratta GRAVES → me; in arancio chi ha spottato GRAVES.
/// </summary>
public sealed class GravesView : Control
{
    readonly AppState _st;
    public bool HasMe;
    public LatLon Me;
    public string MeLabel = "";
    public List<(LatLon pos, string label)> Receivers = new();
    public int ActiveStep = -1;
    public int MySector = -1, MyStep = -1;

    static readonly Color Back = Color.Parse("#0E1726"), Fore = Color.Parse("#DCE6F2"), Muted = Color.Parse("#8696AB"),
        Silver = Color.Parse("#B8C4D2"), Yellow = Color.Parse("#F5C800"), Green = Color.Parse("#4CD07D"), Orange = Color.Parse("#FF8A3D");
    static readonly Color[] SectorCols = { Color.Parse("#E87BA4"), Color.Parse("#2A9FD6"), Color.Parse("#8B6CF0"), Color.Parse("#EB6834") };

    public GravesView(AppState st) { _st = st; ClipToBounds = true; }

    static IBrush B(Color c, double a = 1) => new SolidColorBrush(c, a);
    static IPen P(Color c, double a, double w, bool dash = false) =>
        new Pen(new SolidColorBrush(c, a), w) { DashStyle = dash ? new DashStyle(new double[] { 5, 4 }, 0) : null };

    static StreamGeometry Wedge(Point c, double r0, double r1, double az0, double az1)
    {
        var g = new StreamGeometry();
        using var gc = g.Open();
        int n = Math.Max(2, (int)Math.Ceiling((az1 - az0) / 1.5));
        for (int i = 0; i <= n; i++)
        {
            double a = (az0 + (az1 - az0) * i / n) * Math.PI / 180;
            var p = new Point(c.X + r1 * Math.Sin(a), c.Y - r1 * Math.Cos(a));
            if (i == 0) gc.BeginFigure(p, true); else gc.LineTo(p);
        }
        for (int i = n; i >= 0; i--)
        {
            double a = (az0 + (az1 - az0) * i / n) * Math.PI / 180;
            gc.LineTo(new Point(c.X + r0 * Math.Sin(a), c.Y - r0 * Math.Cos(a)));
        }
        gc.EndFigure(true);
        return g;
    }

    void Label(DrawingContext ctx, string s, Point at, double size, Color col, bool bold = true, bool center = true)
    {
        var ft = new FormattedText(s, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
            new Typeface("Inter, Segoe UI, sans-serif", FontStyle.Normal, bold ? FontWeight.SemiBold : FontWeight.Normal), size, B(col));
        var p = center ? new Point(at.X - ft.Width / 2, at.Y - ft.Height / 2) : at;
        var halo = ft.BuildGeometry(p);
        if (halo != null) ctx.DrawGeometry(null, new Pen(B(Back, 0.85), 3), halo);
        ctx.DrawText(ft, p);
    }

    public override void Render(DrawingContext ctx)
    {
        double W = Bounds.Width, H = Bounds.Height;
        ctx.DrawRectangle(B(Back), null, new Rect(0, 0, W, H));
        double R = Math.Min(W, H) / 2 - 26;
        if (R < 60) return;
        var c = new Point(W / 2, H / 2);
        var g0 = Graves.Pos;

        // raggio: almeno 1200 km, fino alla mia stazione e ai ricevitori (massimo 3000 km)
        double maxKm = 1200;
        if (HasMe) maxKm = Math.Max(maxKm, Geo.DistKm(g0, Me) * 1.12);
        foreach (var r in Receivers) maxKm = Math.Max(maxKm, Geo.DistKm(g0, r.pos) * 1.12);
        maxKm = Math.Min(3000, Math.Ceiling(maxKm / 250) * 250);
        double k = R / maxKm;
        Point Pt(LatLon ll)
        {
            double d = Geo.DistKm(g0, ll) * k, a = Geo.Azimuth(g0, ll) * Math.PI / 180;
            return new Point(c.X + d * Math.Sin(a), c.Y - d * Math.Cos(a));
        }

        var disc = new Rect(c.X - R, c.Y - R, 2 * R, 2 * R);
        using (ctx.PushGeometryClip(new EllipseGeometry(disc)))
        {
            // settori e passi
            for (int s = 0; s < 4; s++)
            {
                var col = SectorCols[s];
                for (int st = 0; st < Graves.Steps; st++)
                {
                    double a0 = Graves.AzMin + s * Graves.SectorDeg + st * Graves.StepDeg;
                    bool act = st == ActiveStep, mine = s == MySector && st == MyStep;
                    var fill = mine ? B(Yellow, act ? 0.85 : 0.45) : B(col, act ? 0.45 : 0.14);
                    ctx.DrawGeometry(fill, P(col, 0.35, 0.6), Wedge(c, 0, R, a0, a0 + Graves.StepDeg));
                }
                double sa = (Graves.AzMin + s * Graves.SectorDeg) * Math.PI / 180;
                ctx.DrawLine(P(col, 0.55, 1), c, new Point(c.X + R * Math.Sin(sa), c.Y - R * Math.Cos(sa)));
            }
            double ea = Graves.AzMax * Math.PI / 180;
            ctx.DrawLine(P(SectorCols[3], 0.55, 1), c, new Point(c.X + R * Math.Sin(ea), c.Y - R * Math.Cos(ea)));

            // coste e confini (Natural Earth già caricato per la mappa principale)
            void Lines(List<LatLon[]> lines, Color col, double a, double w)
            {
                var pen = P(col, a, w);
                foreach (var line in lines)
                {
                    Point? prev = null;
                    foreach (var ll in line)
                    {
                        if (Geo.DistKm(g0, ll) > maxKm * 1.4) { prev = null; continue; }
                        var q = Pt(ll);
                        if (prev.HasValue) ctx.DrawLine(pen, prev.Value, q);
                        prev = q;
                    }
                }
            }
            if (_st.Map.Loaded) { Lines(_st.Map.Borders, Silver, 0.3, 0.7); Lines(_st.Map.Coast, Silver, 0.6, 0.9); }

            // cerchi delle distanze e assi
            double ring = maxKm <= 1500 ? 250 : 500;
            for (double rk = ring; rk < maxKm; rk += ring) ctx.DrawEllipse(null, P(Fore, 0.18, 0.8), c, rk * k, rk * k);
            ctx.DrawLine(P(Fore, 0.15, 0.8), new Point(c.X, c.Y - R), new Point(c.X, c.Y + R));
            ctx.DrawLine(P(Fore, 0.15, 0.8), new Point(c.X - R, c.Y), new Point(c.X + R, c.Y));

            // tratta GRAVES → me (in questa proiezione i cerchi massimi dal centro sono rette)
            if (HasMe) ctx.DrawLine(P(Green, 0.85, 1.6, true), c, Pt(Me));
        }
        ctx.DrawEllipse(null, P(Silver, 0.6, 1.2), c, R, R);

        // simboli
        ctx.DrawEllipse(B(Yellow), P(Back, 1, 1), c, 5, 5);
        foreach (var r in Receivers)
        {
            var q = Pt(r.pos);
            ctx.DrawEllipse(B(Orange), P(Back, 1, 1), q, 4.5, 4.5);
            Label(ctx, r.label, new Point(q.X, q.Y + 12), 11, Orange);
        }
        if (HasMe)
        {
            var pm = Pt(Me);
            ctx.DrawEllipse(B(Green), P(Back, 1, 1), pm, 5.5, 5.5);
            Label(ctx, MeLabel, new Point(pm.X, pm.Y - 13), 12, Green);
        }
        Label(ctx, "GRAVES", new Point(c.X, c.Y - 13), 12, Yellow);

        // lettere dei settori, punti cardinali, distanze
        for (int s = 0; s < 4; s++)
        {
            double a = (Graves.AzMin + (s + 0.5) * Graves.SectorDeg) * Math.PI / 180, rr = R * 0.82;
            Label(ctx, Graves.Sectors[s], new Point(c.X + rr * Math.Sin(a), c.Y - rr * Math.Cos(a)), 16, SectorCols[s]);
        }
        string west = Lang.Current == "it" ? "O" : "W";
        Label(ctx, "N", new Point(c.X, c.Y - R - 12), 12, Fore);
        Label(ctx, "S", new Point(c.X, c.Y + R + 12), 12, Fore);
        Label(ctx, "E", new Point(c.X + R + 12, c.Y), 12, Fore);
        Label(ctx, west, new Point(c.X - R - 12, c.Y), 12, Fore);
        double rs = maxKm <= 1500 ? 250 : 500;
        for (double rk = rs; rk < maxKm; rk += rs)
            Label(ctx, $"{rk:0} km", new Point(c.X + 4, c.Y - rk * k - 2), 9.5, Muted, false, false);
    }
}
