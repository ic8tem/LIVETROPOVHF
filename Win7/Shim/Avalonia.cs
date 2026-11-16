// Strato di compatibilità: le poche API di Avalonia usate da MapView, GravesView e Palette, rifatte sopra GDI+
// (System.Drawing) per la versione Windows 7 (.NET Framework 4.8 + WinForms). Così il disegno della mappa resta
// lo stesso file della versione normale.
using System;
using System.Collections.Generic;
using System.Globalization;
using SD = System.Drawing;
using SD2 = System.Drawing.Drawing2D;
using WF = System.Windows.Forms;

namespace Avalonia
{
    public struct Point
    {
        public double X { get; }
        public double Y { get; }
        public Point(double x, double y) { X = x; Y = y; }
        public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
        public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);
        public static bool operator ==(Point a, Point b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Point a, Point b) => !(a == b);
        public override bool Equals(object o) => o is Point p && p == this;
        public override int GetHashCode() => X.GetHashCode() * 31 + Y.GetHashCode();
        // GDI+ non accetta NaN né coordinate enormi: si limitano (i punti così lontani sono comunque fuori vista)
        static float Safe(double v) => double.IsNaN(v) ? 0f : (float)Math.Max(-4e6, Math.Min(4e6, v));
        internal SD.PointF F => new SD.PointF(Safe(X), Safe(Y));
    }

    public struct Vector
    {
        public double X { get; }
        public double Y { get; }
        public Vector(double x, double y) { X = x; Y = y; }
    }

    public struct Size
    {
        public double Width { get; }
        public double Height { get; }
        public Size(double w, double h) { Width = w; Height = h; }
    }

    public struct Rect
    {
        public double X { get; }
        public double Y { get; }
        public double Width { get; }
        public double Height { get; }
        public Rect(double x, double y, double w, double h) { X = x; Y = y; Width = w; Height = h; }
        public Rect(Point p, Size s) : this(p.X, p.Y, s.Width, s.Height) { }
        public double Left => X; public double Top => Y; public double Right => X + Width; public double Bottom => Y + Height;
        public Point Center => new Point(X + Width / 2, Y + Height / 2);
        public Point TopLeft => new Point(X, Y);
        public Size Size => new Size(Width, Height);
        public bool Contains(Point p) => p.X >= X && p.X <= Right && p.Y >= Y && p.Y <= Bottom;
        internal SD.RectangleF F => new SD.RectangleF((float)X, (float)Y, (float)Width, (float)Height);
    }
}

namespace Avalonia.Threading
{
    public sealed class DispatcherTimer
    {
        readonly WF.Timer _t = new WF.Timer();
        public DispatcherTimer() { _t.Tick += (s, e) => Tick?.Invoke(this, EventArgs.Empty); }
        public TimeSpan Interval { get => TimeSpan.FromMilliseconds(_t.Interval); set => _t.Interval = Math.Max(1, (int)value.TotalMilliseconds); }
        public bool IsEnabled { get => _t.Enabled; set => _t.Enabled = value; }
        public event EventHandler Tick;
        public void Start() => _t.Start();
        public void Stop() => _t.Stop();
    }
}

namespace Avalonia.Input
{
    public enum StandardCursorType { Arrow, Hand, Cross, Ibeam, Wait, SizeAll }
    public sealed class Cursor { public StandardCursorType Type; public Cursor(StandardCursorType t) { Type = t; } }

    public enum Key { None, Add, OemPlus, Subtract, OemMinus, Home, Left, Right, Up, Down, Escape, Enter, Other }

    public class RoutedEventArgs : EventArgs { public bool Handled { get; set; } }

    public class KeyEventArgs : RoutedEventArgs { public Key Key { get; set; } }

    public sealed class PointerPointProperties
    {
        public bool IsLeftButtonPressed { get; set; }
        public bool IsRightButtonPressed { get; set; }
        public bool IsMiddleButtonPressed { get; set; }
    }

    public sealed class PointerPoint
    {
        public Point Position { get; set; }
        public PointerPointProperties Properties { get; set; } = new PointerPointProperties();
    }

    public sealed class Pointer { public void Capture(object target) { } }

    public class PointerEventArgs : RoutedEventArgs
    {
        internal Point Pos;
        internal PointerPointProperties Props = new PointerPointProperties();
        public Pointer Pointer { get; } = new Pointer();
        public Point GetPosition(object relativeTo) => Pos;
        public PointerPoint GetCurrentPoint(object relativeTo) => new PointerPoint { Position = Pos, Properties = Props };
    }

    public class PointerPressedEventArgs : PointerEventArgs { public int ClickCount { get; internal set; } = 1; }
    public class PointerReleasedEventArgs : PointerEventArgs { }
    public class PointerWheelEventArgs : PointerEventArgs { public Vector Delta { get; internal set; } }
}

namespace Avalonia.Controls
{
    using Avalonia.Input;
    using Avalonia.Media;

    /// <summary>Controllo "alla Avalonia" disegnato da un <see cref="ControlHost"/> WinForms.</summary>
    public abstract class Control
    {
        internal ControlHost Host;
        public Rect Bounds => Host == null ? new Rect(0, 0, 0, 0) : new Rect(0, 0, Host.ClientSize.Width, Host.ClientSize.Height);
        public bool Focusable { get; set; }
        public bool ClipToBounds { get; set; }
        public bool IsVisible => Host != null && Host.Visible;
        public Cursor Cursor { get; set; }
        public void InvalidateVisual() { var h = Host; if (h != null && h.IsHandleCreated && !h.IsDisposed) h.Invalidate(); }
        public bool Focus() { Host?.Focus(); return true; }
        public virtual void Render(DrawingContext ctx) { }
        protected virtual void OnPointerWheelChanged(PointerWheelEventArgs e) { }
        protected virtual void OnPointerPressed(PointerPressedEventArgs e) { }
        protected virtual void OnPointerMoved(PointerEventArgs e) { }
        protected virtual void OnPointerReleased(PointerReleasedEventArgs e) { }
        protected virtual void OnKeyDown(KeyEventArgs e) { }
        internal void RaiseWheel(PointerWheelEventArgs e) => OnPointerWheelChanged(e);
        internal void RaisePressed(PointerPressedEventArgs e) => OnPointerPressed(e);
        internal void RaiseMoved(PointerEventArgs e) => OnPointerMoved(e);
        internal void RaiseReleased(PointerReleasedEventArgs e) => OnPointerReleased(e);
        internal void RaiseKey(KeyEventArgs e) => OnKeyDown(e);
    }

    /// <summary>Contenitore WinForms con doppio buffer che disegna un <see cref="Control"/> con GDI+.</summary>
    public class ControlHost : WF.Control
    {
        public Control Child { get; }
        public event Action<Exception> RenderError;

        public ControlHost(Control child)
        {
            Child = child; child.Host = this;
            SetStyle(WF.ControlStyles.AllPaintingInWmPaint | WF.ControlStyles.OptimizedDoubleBuffer | WF.ControlStyles.UserPaint |
                     WF.ControlStyles.ResizeRedraw | WF.ControlStyles.Selectable, true);
            TabStop = child.Focusable;
            if (child.Cursor != null)
                Cursor = child.Cursor.Type == StandardCursorType.Hand ? WF.Cursors.Hand : child.Cursor.Type == StandardCursorType.Cross ? WF.Cursors.Cross : WF.Cursors.Default;
        }

        protected override void OnPaint(WF.PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SD2.SmoothingMode.AntiAlias;
            g.PixelOffsetMode = SD2.PixelOffsetMode.HighQuality;
            g.TextRenderingHint = SD.Text.TextRenderingHint.AntiAlias;
            try { Child.Render(new DrawingContext(g)); }
            catch (Exception ex) { RenderError?.Invoke(ex); }
        }

        static PointerPointProperties Props(WF.MouseButtons b) => new PointerPointProperties
        {
            IsLeftButtonPressed = (b & WF.MouseButtons.Left) != 0,
            IsRightButtonPressed = (b & WF.MouseButtons.Right) != 0,
            IsMiddleButtonPressed = (b & WF.MouseButtons.Middle) != 0,
        };

        protected override void OnMouseDown(WF.MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (Child.Focusable) Focus();
            Child.RaisePressed(new PointerPressedEventArgs { Pos = new Point(e.X, e.Y), Props = Props(e.Button), ClickCount = e.Clicks });
        }

        protected override void OnMouseMove(WF.MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Child.RaiseMoved(new PointerEventArgs { Pos = new Point(e.X, e.Y), Props = Props(e.Button) });
        }

        protected override void OnMouseUp(WF.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Child.RaiseReleased(new PointerReleasedEventArgs { Pos = new Point(e.X, e.Y), Props = Props(e.Button) });
        }

        protected override void OnMouseWheel(WF.MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            Child.RaiseWheel(new PointerWheelEventArgs { Pos = new Point(e.X, e.Y), Delta = new Vector(0, e.Delta / 120.0) });
        }

        // la rotella va al controllo con il fuoco: passando sulla mappa la si prende, se non si sta scrivendo altrove
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!Child.Focusable) return;
            var f = FindForm();
            if (f == null || !f.ContainsFocus) return;
            var a = f.ActiveControl;
            while (a is WF.ContainerControl cc && cc.ActiveControl != null) a = cc.ActiveControl;
            if (a is WF.TextBoxBase || a is WF.ComboBox || a is WF.UpDownBase || a is WF.ListBox) return;
            Focus();
        }

        protected override bool IsInputKey(WF.Keys k) => true;

        protected override void OnKeyDown(WF.KeyEventArgs e)
        {
            base.OnKeyDown(e);
            Key k;
            switch (e.KeyCode)
            {
                case WF.Keys.Add: k = Key.Add; break;
                case WF.Keys.Oemplus: k = Key.OemPlus; break;
                case WF.Keys.Subtract: k = Key.Subtract; break;
                case WF.Keys.OemMinus: k = Key.OemMinus; break;
                case WF.Keys.Home: k = Key.Home; break;
                case WF.Keys.Left: k = Key.Left; break;
                case WF.Keys.Right: k = Key.Right; break;
                case WF.Keys.Up: k = Key.Up; break;
                case WF.Keys.Down: k = Key.Down; break;
                case WF.Keys.Escape: k = Key.Escape; break;
                case WF.Keys.Enter: k = Key.Enter; break;
                default: k = Key.Other; break;
            }
            var a = new KeyEventArgs { Key = k };
            Child.RaiseKey(a);
            e.Handled = a.Handled;
        }
    }
}

namespace Avalonia.Media
{
    public enum FlowDirection { LeftToRight, RightToLeft }
    public enum FontStyle { Normal, Italic, Oblique }
    public enum FontWeight { Thin = 100, Light = 300, Normal = 400, Medium = 500, SemiBold = 600, Bold = 700, ExtraBold = 800, Black = 900 }
    public enum PenLineCap { Flat, Round, Square }
    public enum PenLineJoin { Miter, Bevel, Round }

    public struct Color : IEquatable<Color>
    {
        public byte A { get; }
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }
        public Color(byte a, byte r, byte g, byte b) { A = a; R = r; G = g; B = b; }
        public static Color FromArgb(byte a, byte r, byte g, byte b) => new Color(a, r, g, b);
        public static Color FromRgb(byte r, byte g, byte b) => new Color(255, r, g, b);
        public static Color FromUInt32(uint v) => new Color((byte)(v >> 24), (byte)(v >> 16), (byte)(v >> 8), (byte)v);
        public static Color Parse(string s)
        {
            if (TryParse(s, out var c)) return c;
            throw new FormatException("Colore non valido: " + s);
        }
        public static bool TryParse(string s, out Color c)
        {
            c = default;
            if (string.IsNullOrEmpty(s)) return false;
            s = s.Trim();
            if (s[0] == '#')
            {
                var h = s.Substring(1);
                if (h.Length == 3) h = new string(new[] { h[0], h[0], h[1], h[1], h[2], h[2] });
                if (h.Length == 6) h = "FF" + h;
                if (h.Length != 8 || !uint.TryParse(h, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var v)) return false;
                c = FromUInt32(v); return true;
            }
            var k = SD.Color.FromName(s);
            if (!k.IsKnownColor) return false;
            c = new Color(k.A, k.R, k.G, k.B); return true;
        }
        public bool Equals(Color o) => A == o.A && R == o.R && G == o.G && B == o.B;
        public override bool Equals(object o) => o is Color c && Equals(c);
        public override int GetHashCode() => (A << 24) | (R << 16) | (G << 8) | B;
        public static bool operator ==(Color a, Color b) => a.Equals(b);
        public static bool operator !=(Color a, Color b) => !a.Equals(b);
        public override string ToString() => A == 255 ? $"#{R:X2}{G:X2}{B:X2}" : $"#{A:X2}{R:X2}{G:X2}{B:X2}";
        public SD.Color ToGdi(double opacity = 1) => SD.Color.FromArgb((int)Math.Max(0, Math.Min(255, Math.Round(A * opacity))), R, G, B);
    }

    public static class Colors
    {
        public static Color White => Color.FromRgb(255, 255, 255);
        public static Color Black => Color.FromRgb(0, 0, 0);
        public static Color Transparent => Color.FromArgb(0, 255, 255, 255);
        public static Color Red => Color.FromRgb(255, 0, 0);
        public static Color Gray => Color.FromRgb(128, 128, 128);
    }

    public interface IBrush { double Opacity { get; } }
    public interface IImmutableSolidColorBrush : IBrush { Color Color { get; } }

    public class SolidColorBrush : IImmutableSolidColorBrush
    {
        public Color Color { get; set; }
        public double Opacity { get; set; } = 1;
        public SolidColorBrush() { }
        public SolidColorBrush(Color c, double opacity = 1) { Color = c; Opacity = opacity; }
    }

    public static class Brushes
    {
        public static IBrush White => new SolidColorBrush(Colors.White);
        public static IBrush Black => new SolidColorBrush(Colors.Black);
        public static IBrush Transparent => new SolidColorBrush(Colors.Transparent);
    }

    public interface IDashStyle { IReadOnlyList<double> Dashes { get; } double Offset { get; } }

    public class DashStyle : IDashStyle
    {
        public IReadOnlyList<double> Dashes { get; set; }
        public double Offset { get; set; }
        public DashStyle() { }
        public DashStyle(IEnumerable<double> dashes, double offset) { Dashes = new List<double>(dashes); Offset = offset; }
    }

    public interface IPen { IBrush Brush { get; } double Thickness { get; } IDashStyle DashStyle { get; } PenLineCap LineCap { get; } PenLineJoin LineJoin { get; } }

    public class Pen : IPen
    {
        public IBrush Brush { get; set; }
        public double Thickness { get; set; } = 1;
        public IDashStyle DashStyle { get; set; }
        public PenLineCap LineCap { get; set; }
        public PenLineJoin LineJoin { get; set; }
        public double MiterLimit { get; set; } = 10;
        public Pen() { }
        public Pen(IBrush brush, double thickness = 1, IDashStyle dashStyle = null, PenLineCap lineCap = PenLineCap.Flat,
            PenLineJoin lineJoin = PenLineJoin.Miter, double miterLimit = 10)
        { Brush = brush; Thickness = thickness; DashStyle = dashStyle; LineCap = lineCap; LineJoin = lineJoin; MiterLimit = miterLimit; }
        public Pen(Color c, double thickness = 1) : this(new SolidColorBrush(c), thickness) { }
    }

    /// <summary>Geometria: un elenco di figure (spezzate) convertito in GraphicsPath di GDI+.</summary>
    public abstract class Geometry
    {
        internal abstract SD2.GraphicsPath FillPath();
        internal abstract SD2.GraphicsPath StrokePath();
        public Rect Bounds
        {
            get
            {
                using var p = StrokePath();
                var b = p.GetBounds();
                return new Rect(b.X, b.Y, b.Width, b.Height);
            }
        }
    }

    public sealed class StreamGeometry : Geometry
    {
        internal sealed class Figure { public List<SD.PointF> Pts = new List<SD.PointF>(); public bool Filled, Closed; }
        internal readonly List<Figure> Figures = new List<Figure>();
        public StreamGeometryContext Open() => new StreamGeometryContext(this);

        SD2.GraphicsPath Build(bool fillOnly)
        {
            var p = new SD2.GraphicsPath(SD2.FillMode.Alternate);
            foreach (var f in Figures)
            {
                if (fillOnly && !f.Filled) continue;
                if (f.Pts.Count < 2) continue;
                p.StartFigure();
                p.AddLines(f.Pts.ToArray());
                if (f.Closed || fillOnly) p.CloseFigure();
            }
            return p;
        }
        internal override SD2.GraphicsPath FillPath() => Build(true);
        internal override SD2.GraphicsPath StrokePath() => Build(false);
    }

    public sealed class StreamGeometryContext : IDisposable
    {
        readonly StreamGeometry _g;
        StreamGeometry.Figure _cur;
        internal StreamGeometryContext(StreamGeometry g) { _g = g; }
        public void BeginFigure(Point p, bool isFilled = true)
        {
            _cur = new StreamGeometry.Figure { Filled = isFilled };
            _cur.Pts.Add(p.F);
            _g.Figures.Add(_cur);
        }
        public void LineTo(Point p) { if (_cur == null) BeginFigure(p, false); else _cur.Pts.Add(p.F); }
        public void EndFigure(bool isClosed) { if (_cur != null) _cur.Closed = isClosed; _cur = null; }
        public void Dispose() { }
    }

    public sealed class EllipseGeometry : Geometry
    {
        public Rect Rect { get; }
        public EllipseGeometry(Rect r) { Rect = r; }
        SD2.GraphicsPath Make() { var p = new SD2.GraphicsPath(); p.AddEllipse(Rect.F); return p; }
        internal override SD2.GraphicsPath FillPath() => Make();
        internal override SD2.GraphicsPath StrokePath() => Make();
    }

    public sealed class RectangleGeometry : Geometry
    {
        public Rect Rect { get; }
        public RectangleGeometry(Rect r) { Rect = r; }
        SD2.GraphicsPath Make() { var p = new SD2.GraphicsPath(); p.AddRectangle(Rect.F); return p; }
        internal override SD2.GraphicsPath FillPath() => Make();
        internal override SD2.GraphicsPath StrokePath() => Make();
    }

    sealed class PathGeometryGdi : Geometry
    {
        readonly SD2.GraphicsPath _p;
        public PathGeometryGdi(SD2.GraphicsPath p) { _p = p; }
        internal override SD2.GraphicsPath FillPath() => (SD2.GraphicsPath)_p.Clone();
        internal override SD2.GraphicsPath StrokePath() => (SD2.GraphicsPath)_p.Clone();
    }

    public sealed class FontFamily
    {
        public string Name { get; }
        public FontFamily(string name) { Name = name; }
        public override string ToString() => Name;
    }

    public sealed class Typeface
    {
        public FontFamily FontFamily { get; }
        public FontStyle Style { get; }
        public FontWeight Weight { get; }
        public Typeface(FontFamily family, FontStyle style = FontStyle.Normal, FontWeight weight = FontWeight.Normal) { FontFamily = family; Style = style; Weight = weight; }
        public Typeface(string family, FontStyle style = FontStyle.Normal, FontWeight weight = FontWeight.Normal) : this(new FontFamily(family), style, weight) { }

        static readonly Dictionary<string, SD.FontFamily> Resolved = new Dictionary<string, SD.FontFamily>();
        internal SD.FontFamily Gdi
        {
            get
            {
                var key = FontFamily?.Name ?? "";
                lock (Resolved)
                {
                    if (Resolved.TryGetValue(key, out var f)) return f;
                    f = null;
                    foreach (var raw in key.Split(','))
                    {
                        var n = raw.Trim();
                        if (n.Length == 0) continue;
                        if (n.Equals("monospace", StringComparison.OrdinalIgnoreCase)) { f = SD.FontFamily.GenericMonospace; break; }
                        if (n.Equals("sans-serif", StringComparison.OrdinalIgnoreCase)) { f = SD.FontFamily.GenericSansSerif; break; }
                        if (n.Equals("serif", StringComparison.OrdinalIgnoreCase)) { f = SD.FontFamily.GenericSerif; break; }
                        try { var t = new SD.FontFamily(n); if (t.IsStyleAvailable(SD.FontStyle.Regular)) { f = t; break; } } catch { }
                    }
                    f ??= SD.FontFamily.GenericSansSerif;
                    Resolved[key] = f;
                    return f;
                }
            }
        }
        internal SD.FontStyle GdiStyle =>
            (Weight >= FontWeight.SemiBold ? SD.FontStyle.Bold : SD.FontStyle.Regular) | (Style != FontStyle.Normal ? SD.FontStyle.Italic : SD.FontStyle.Regular);
    }

    public sealed class FormattedText
    {
        static readonly SD.Bitmap MeasureBmp = new SD.Bitmap(1, 1);
        static readonly SD.Graphics MeasureG = SD.Graphics.FromImage(MeasureBmp);
        internal static readonly SD.StringFormat Fmt = MakeFmt();
        static SD.StringFormat MakeFmt()
        {
            var f = (SD.StringFormat)SD.StringFormat.GenericTypographic.Clone();
            f.FormatFlags |= SD.StringFormatFlags.MeasureTrailingSpaces | SD.StringFormatFlags.NoWrap;
            return f;
        }

        public string Text { get; }
        public Typeface Typeface { get; }
        public double FontSize { get; }
        public IBrush Brush { get; }
        public double Width { get; }
        public double Height { get; }

        public FormattedText(string text, CultureInfo culture, FlowDirection dir, Typeface typeface, double emSize, IBrush foreground)
        {
            Text = text ?? ""; Typeface = typeface; FontSize = Math.Max(1, emSize); Brush = foreground;
            var font = FontFor(typeface, FontSize);
            lock (MeasureG)
            {
                var sz = MeasureG.MeasureString(Text.Length == 0 ? " " : Text, font, SD.PointF.Empty, Fmt);
                Width = Text.Length == 0 ? 0 : sz.Width;
                Height = font.GetHeight(MeasureG);
            }
        }

        static readonly Dictionary<string, SD.Font> Fonts = new Dictionary<string, SD.Font>();
        static SD.Font FontFor(Typeface t, double size)
        {
            var fam = t.Gdi; var st = t.GdiStyle;
            var key = fam.Name + "|" + (int)st + "|" + size.ToString("0.##", CultureInfo.InvariantCulture);
            lock (Fonts)
            {
                if (!Fonts.TryGetValue(key, out var f))
                {
                    if (!fam.IsStyleAvailable(st)) st = SD.FontStyle.Regular;
                    f = new SD.Font(fam, (float)size, st, SD.GraphicsUnit.Pixel);
                    Fonts[key] = f;
                }
                return f;
            }
        }

        internal SD2.GraphicsPath Path(Point origin)
        {
            var p = new SD2.GraphicsPath();
            var fam = Typeface.Gdi; var st = Typeface.GdiStyle;
            if (!fam.IsStyleAvailable(st)) st = SD.FontStyle.Regular;
            p.AddString(Text, fam, (int)st, (float)FontSize, origin.F, Fmt);
            return p;
        }

        public Geometry BuildGeometry(Point origin) => new PathGeometryGdi(Path(origin));
    }

    /// <summary>Il contesto di disegno: chiamate di Avalonia tradotte in GDI+, con pila di opacità e ritagli.</summary>
    public sealed class DrawingContext
    {
        readonly SD.Graphics _g;
        readonly Stack<double> _op = new Stack<double>();
        double _opacity = 1;

        public DrawingContext(SD.Graphics g) { _g = g; }

        sealed class Pop : IDisposable
        {
            readonly Action _a; bool _done;
            public Pop(Action a) { _a = a; }
            public void Dispose() { if (_done) return; _done = true; _a(); }
        }

        public IDisposable PushOpacity(double opacity)
        {
            _op.Push(_opacity);
            _opacity *= Math.Max(0, Math.Min(1, opacity));
            return new Pop(() => _opacity = _op.Pop());
        }

        public IDisposable PushGeometryClip(Geometry g)
        {
            var state = _g.Save();
            using (var p = g.FillPath()) _g.SetClip(p, SD2.CombineMode.Intersect);
            return new Pop(() => _g.Restore(state));
        }

        public IDisposable PushClip(Rect r)
        {
            var state = _g.Save();
            _g.SetClip(r.F, SD2.CombineMode.Intersect);
            return new Pop(() => _g.Restore(state));
        }

        SD.Color Col(IBrush b, out bool ok)
        {
            ok = false;
            if (b is IImmutableSolidColorBrush s)
            {
                var c = s.Color.ToGdi(s.Opacity * _opacity);
                ok = c.A > 0;
                return c;
            }
            return SD.Color.Empty;
        }

        SD.Brush Gdi(IBrush b)
        {
            var c = Col(b, out var ok);
            return ok ? new SD.SolidBrush(c) : null;
        }

        SD.Pen Gdi(IPen p)
        {
            if (p == null || p.Thickness <= 0) return null;
            var c = Col(p.Brush, out var ok);
            if (!ok) return null;
            var gp = new SD.Pen(c, (float)p.Thickness);
            switch (p.LineCap)
            {
                case PenLineCap.Round: gp.StartCap = gp.EndCap = SD2.LineCap.Round; gp.DashCap = SD2.DashCap.Round; break;
                case PenLineCap.Square: gp.StartCap = gp.EndCap = SD2.LineCap.Square; break;
                default: gp.StartCap = gp.EndCap = SD2.LineCap.Flat; break;
            }
            gp.LineJoin = p.LineJoin == PenLineJoin.Round ? SD2.LineJoin.Round : p.LineJoin == PenLineJoin.Bevel ? SD2.LineJoin.Bevel : SD2.LineJoin.Miter;
            var d = p.DashStyle?.Dashes;
            if (d != null && d.Count >= 2)
            {
                var arr = new float[d.Count];
                for (int i = 0; i < arr.Length; i++) arr[i] = (float)Math.Max(0.1, d[i]);
                // con estremità arrotondate GDI+ allunga i tratti: si compensa come fa Avalonia
                gp.DashPattern = arr;
                gp.DashOffset = (float)(p.DashStyle.Offset);
            }
            return gp;
        }

        public void DrawRectangle(IBrush brush, IPen pen, Rect r)
        {
            using (var b = brush == null ? null : Gdi(brush)) if (b != null) _g.FillRectangle(b, r.F);
            using (var p = Gdi(pen)) if (p != null) _g.DrawRectangle(p, (float)r.X, (float)r.Y, (float)r.Width, (float)r.Height);
        }

        public void DrawEllipse(IBrush brush, IPen pen, Point center, double rx, double ry)
        {
            var r = new SD.RectangleF((float)(center.X - rx), (float)(center.Y - ry), (float)(2 * rx), (float)(2 * ry));
            using (var b = brush == null ? null : Gdi(brush)) if (b != null) _g.FillEllipse(b, r);
            using (var p = Gdi(pen)) if (p != null) _g.DrawEllipse(p, r);
        }

        public void DrawEllipse(IBrush brush, IPen pen, Rect r) => DrawEllipse(brush, pen, r.Center, r.Width / 2, r.Height / 2);

        public void DrawLine(IPen pen, Point a, Point b)
        {
            using var p = Gdi(pen);
            if (p != null) _g.DrawLine(p, a.F, b.F);
        }

        public void DrawGeometry(IBrush brush, IPen pen, Geometry geo)
        {
            if (geo == null) return;
            if (brush != null)
                using (var b = Gdi(brush))
                    if (b != null) using (var path = geo.FillPath()) if (path.PointCount > 0) _g.FillPath(b, path);
            if (pen != null)
                using (var p = Gdi(pen))
                    if (p != null) using (var path = geo.StrokePath()) if (path.PointCount > 0) _g.DrawPath(p, path);
        }

        public void DrawText(FormattedText ft, Point origin)
        {
            if (ft == null || ft.Text.Length == 0) return;
            using var b = Gdi(ft.Brush);
            if (b == null) return;
            using var path = ft.Path(origin);
            _g.FillPath(b, path);
        }
    }
}
