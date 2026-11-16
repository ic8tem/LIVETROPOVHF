using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TropoVHF.Core;
using TropoVHF.Ui;
using AM = Avalonia.Media;
using AC = Avalonia.Controls;

namespace TropoVHF.Win7;

public sealed partial class MainForm
{
    Label _zoomPct, _fcLabel, _legendSrc, _obsNote, _emptyText;
    Label _nSpots, _nAs, _nExcl, _nRx, _nDec, _nHeld, _mqttCount, _mqttDot;
    bool _showMqttDiscarded;
    ListBox _spotList;
    Panel _infoCard;
    TableLayoutPanel _infoBody;
    AC.ControlHost _mapHost;

    Control BuildMapTab()
    {
        var split = new SplitContainer { Orientation = Orientation.Vertical, BackColor = C(_th.Line), SplitterWidth = 3, FixedPanel = FixedPanel.None };
        split.Panel1.BackColor = C(_th.Page); split.Panel2.BackColor = C(_th.Panel);
        split.HandleCreated += (_, _) => { try { split.SplitterDistance = (int)(split.Width * 0.65); } catch { } };
        split.SizeChanged += (_, _) => { try { if (split.Width > 400) split.SplitterDistance = Math.Clamp((int)(split.Width * 0.65), 200, split.Width - 300); } catch { } };

        // ------------------------------------------------ mappa
        _mapHost = new AC.ControlHost(_map) { Dock = DockStyle.Fill, BackColor = C(_th.MapSea) };
        _mapHost.RenderError += ex => { try { File.AppendAllText(Path.Combine(Settings.AppDir, "errore-avvio.txt"), DateTime.Now + " mappa: " + ex + "\r\n"); } catch { } };

        // zoom
        var zoom = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, WrapContents = false, BackColor = C(_th.Line), Padding = new Padding(1) };
        Button ZB(string t, Action a)
        {
            var b = new Button { Text = t, Size = new Size(40, 34), FlatStyle = FlatStyle.Flat, BackColor = C(_th.Panel), ForeColor = C(_th.Text), Font = F(11f, true), Margin = new Padding(0) };
            b.FlatAppearance.BorderSize = 0;
            b.Click += (_, _) => { a(); _zoomPct.Text = _map.ZoomPercent; };
            return b;
        }
        Avalonia.Point Center() => new(_map.Bounds.Width / 2, _map.Bounds.Height / 2);
        zoom.Controls.Add(ZB("+", () => _map.ZoomAt(1.4, Center())));
        zoom.Controls.Add(ZB("−", () => _map.ZoomAt(1 / 1.4, Center())));
        var zr = ZB("100%", () => _map.ResetView()); zr.Font = F(8f, true);
        _zoomPct = new Label { Visible = false };
        zoom.Controls.Add(zr);
        _zoomPct.TextChanged += (_, _) => zr.Text = _zoomPct.Text;
        _mapHost.Controls.Add(zoom);
        void PlaceZoom() => zoom.Location = new Point(_mapHost.ClientSize.Width - zoom.Width - 10, _mapHost.ClientSize.Height - zoom.Height - 10);
        _mapHost.SizeChanged += (_, _) => PlaceZoom();
        zoom.SizeChanged += (_, _) => PlaceZoom();

        // scheda informazioni (spot, ascolto, punto, MQTT scartati)
        _infoBody = Stack(new Padding(10, 6, 10, 8));
        _infoBody.Dock = DockStyle.None; _infoBody.Width = 320;
        _infoCard = new Panel { AutoScroll = true, BackColor = C(_th.Panel), BorderStyle = BorderStyle.FixedSingle, Width = 340, Visible = false };
        _infoCard.Controls.Add(_infoBody);
        _infoBody.Location = new Point(0, 0);
        _mapHost.Controls.Add(_infoCard);
        void PlaceInfo()
        {
            int h = Math.Min(_infoBody.PreferredSize.Height + 4, Math.Max(120, _mapHost.ClientSize.Height - 24));
            _infoCard.Height = h;
            _infoCard.Location = new Point(_mapHost.ClientSize.Width - _infoCard.Width - 10, 10);
        }
        _mapHost.SizeChanged += (_, _) => PlaceInfo();
        _infoBody.SizeChanged += (_, _) => PlaceInfo();

        // ------------------------------------------------ barra dei layer (sopra la mappa)
        var layers = Stack(new Padding(10, 6, 10, 6));
        layers.BackColor = C(_th.Panel);
        var r1 = Row();
        r1.Controls.Add(Lbl(Lang.T("layers"), 8.5f, _th.Muted, true));
        void Layer(string key, bool v, AM.Color sw, Action<bool> set)
        {
            var sp = Row(); sp.WrapContents = false; sp.Margin = new Padding(0, 0, 6, 0);
            var cb = Chk("", v, x => { set(x); _map.InvalidateVisual(); }); cb.Margin = new Padding(3, 4, 0, 0);
            sp.Controls.Add(cb);
            sp.Controls.Add(new Panel { Size = new Size(14, 4), BackColor = C(sw), Margin = new Padding(0, 11, 4, 0) });
            var l = Lbl(Lang.T(key), 9f); l.Margin = new Padding(0, 5, 3, 3);
            l.Click += (_, _) => cb.Checked = !cb.Checked;
            sp.Controls.Add(l);
            r1.Controls.Add(sp);
        }
        Layer("l.base", _st.ShowBase, _th.MapCoast, x => _st.ShowBase = x);
        Layer("l.grid", _st.ShowGrid, _th.MapGrid, x => _st.ShowGrid = x);
        Layer("l.tropo", _st.ShowTropo, AM.Color.Parse("#E8D24A"), x => _st.ShowTropo = x);
        Layer("l.iso", _st.ShowIso, _th.Iso, x => _st.ShowIso = x);
        Layer("l.t850", _st.ShowT850, AM.Color.Parse("#D8452E"), x => _st.ShowT850 = x);
        Layer("l.inv", _st.ShowInv, AM.Color.Parse("#FF7A00"), x => _st.ShowInv = x);
        Layer("l.rx", _st.ShowRx, Palette.ListenerViolet, x => _st.ShowRx = x);
        Layer("l.dec", _st.ShowDec, Palette.DecodeRed, x => _st.ShowDec = x);
        Layer("l.qso", _st.ShowQso, Palette.QsoGreen, x => _st.ShowQso = x);
        Layer("l.spots", _st.ShowSpots, Palette.SpotBlue, x => _st.ShowSpots = x);
        Layer("x.short", _st.HideShort, _th.Muted, x => { _st.HideShort = x; RefreshSpotList(); });
        Layer("x.long", _st.LongAsSquares, Palette.LongLilac, x => { _st.LongAsSquares = x; RefreshSpotList(); });
        var ring = Combo(AppState.RingKm.Select(k => k + " km"), Array.IndexOf(AppState.RingKm, (int)_st.LongKm), 80, i =>
        {
            _st.LongKm = AppState.RingKm[i]; _st.Dirty = true; RefreshSpotList(); _map.InvalidateVisual();
        });
        r1.Controls.Add(ring);
        Layer("x.rings", _st.ShowRings, Palette.LongLilac, x => _st.ShowRings = x);
        Add(layers, r1);

        var r2 = Row();
        var srcButtons = new List<(string key, RadioButton b)>();
        foreach (var (key, label) in new[] { ("media", "src.avg"), ("hepburn", "src.hep"), ("f5len", "src.f5") })
        {
            var rb = new RadioButton { Text = Lang.T(label), Appearance = Appearance.Button, AutoSize = true, Checked = _st.TropoSrc == key, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 2, 0, 2), Padding = new Padding(6, 0, 6, 0), ForeColor = C(_th.Text), BackColor = C(_th.CtlBg) };
            rb.FlatAppearance.CheckedBackColor = C(_th.Accent);
            rb.FlatAppearance.BorderColor = C(_th.Ctl);
            var k = key;
            rb.CheckedChanged += (_, _) =>
            {
                rb.ForeColor = rb.Checked ? C(_th.AccentFg) : C(_th.Text);
                if (!rb.Checked) return;
                _st.TropoSrc = k; _map.InvalidateVisual(); if (_legendSrc != null) _legendSrc.Text = SrcName(); RefreshInfoCard();
            };
            rb.ForeColor = rb.Checked ? C(_th.AccentFg) : C(_th.Text);
            srcButtons.Add((key, rb));
            r2.Controls.Add(rb);
        }
        r2.Controls.Add(new Panel { Width = 12, Height = 4, BackColor = Color.Transparent });
        r2.Controls.Add(Lbl(Lang.T("opacity"), 9f, _th.Muted));
        var op = new TrackBar { Minimum = 10, Maximum = 100, Value = Math.Clamp((int)Math.Round(_st.TropoOpacity * 100), 10, 100), TickStyle = TickStyle.None, Width = 120, AutoSize = false, Height = 26, BackColor = C(_th.Panel) };
        op.ValueChanged += (_, _) => { _st.TropoOpacity = op.Value / 100.0; _map.InvalidateVisual(); };
        r2.Controls.Add(op);
        r2.Controls.Add(Lbl(Lang.T("proj"), 9f, _th.Muted));
        r2.Controls.Add(Combo(new[] { Lang.T("proj.merc"), Lang.T("proj.azi") }, _st.Proj == "azi" ? 1 : 0, 200, i => { _st.Proj = i == 1 ? "azi" : "merc"; _map.InvalidateVisual(); }));
        Add(layers, r2);
        var layersHolder = new Panel { Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = C(_th.Panel) };
        layersHolder.Controls.Add(layers);

        // ------------------------------------------------ barra temporale e legenda (sotto la mappa)
        var bottom = Stack(new Padding(10, 6, 10, 6));
        bottom.BackColor = C(_th.Panel);
        var fc = new TableLayoutPanel { ColumnCount = 3, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = Color.Transparent, Margin = new Padding(0) };
        fc.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230));
        fc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        fc.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));
        var fcLeft = Stack(new Padding(0)); fcLeft.Dock = DockStyle.Fill;
        Add(fcLeft, Lbl(Lang.T("forecast"), 8.5f, _th.Muted, true));
        _fcLabel = Lbl("", 10f, _th.Text, true, true);
        Add(fcLeft, _fcLabel);
        fc.Controls.Add(fcLeft, 0, 0);
        var fcSlider = new TrackBar { Minimum = 0, Maximum = 64, Value = Math.Clamp(_st.ForecastH / 3, 0, 64), TickStyle = TickStyle.BottomRight, TickFrequency = 8, SmallChange = 1, LargeChange = 8, Dock = DockStyle.Fill, BackColor = C(_th.Panel) };
        fcSlider.ValueChanged += (_, _) => { _st.ForecastH = fcSlider.Value * 3; UpdateFcLabel(); _map.InvalidateVisual(); };
        fc.Controls.Add(fcSlider, 1, 0);
        var legend = new LegendBar(this) { Dock = DockStyle.Fill, Height = 44 };
        _legendSrc = new Label { Visible = false, Text = SrcName() };
        _legendSrc.TextChanged += (_, _) => legend.Invalidate();
        fc.Controls.Add(legend, 2, 0);
        Add(bottom, fc);
        var legRow = Row();
        void Leg(AM.Color col, bool dashed, string text, AM.Color? edge = null)
        {
            var sw = new Panel { Size = new Size(28, dashed ? 5 : 3), BackColor = C(edge ?? col), Margin = new Padding(3, 10, 4, 0) };
            if (dashed) sw.Controls.Add(new Panel { Bounds = new Rectangle(0, 1, 28, 3), BackColor = C(col) });
            legRow.Controls.Add(sw);
            legRow.Controls.Add(Lbl(text, 9f, _th.Sub));
        }
        Leg(Palette.SpotBlue, false, Lang.T("legend.spot"));
        Leg(Palette.AirscatterYellow, true, Lang.T("legend.as"), Palette.AirscatterEdge);
        Leg(Palette.ListenerViolet, false, Lang.T("legend.rx"));
        Leg(Palette.ListenerLine, false, Lang.T("u.what_they_hear_and"));
        Leg(Palette.DecodeRed, false, Lang.T("legend.dec"));
        Leg(Palette.QsoGreen, false, Lang.T("l.qso"));
        _obsNote = Lbl("", 9f, _th.Muted);
        legRow.Controls.Add(_obsNote);
        Add(bottom, legRow);
        Add(bottom, WrapLbl(Lang.T("legend.help"), 8.5f, _th.Muted));
        var bottomHolder = new Panel { Dock = DockStyle.Bottom, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = C(_th.Panel) };
        bottomHolder.Controls.Add(bottom);

        split.Panel1.Controls.Add(_mapHost);
        split.Panel1.Controls.Add(layersHolder);
        split.Panel1.Controls.Add(bottomHolder);

        // ------------------------------------------------ elenco degli spot
        split.Panel2.Controls.Add(BuildSpotList());
        UpdateFcLabel();
        return split;
    }

    /// <summary>Legenda dell'indice tropo: nome della fonte, sfumatura 0–10 e tacche.</summary>
    sealed class LegendBar : Control
    {
        readonly MainForm _f;
        public LegendBar(MainForm f) { _f = f; SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true); }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(C(_f._th.Panel));
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using var small = new Font(_f.Font.FontFamily, 8f);
            using var muted = new SolidBrush(C(_f._th.Muted));
            g.DrawString(_f.SrcName(), small, muted, 0, 0);
            var r = new RectangleF(2, 16, Width - 6, 8);
            if (r.Width < 10) return;
            using (var br = new LinearGradientBrush(r, Color.Black, Color.Black, 0f))
            {
                var stops = new[] { (0f, "#3B82C4"), (0.3f, "#5BC07A"), (0.52f, "#E8D24A"), (0.68f, "#F08A3C"), (0.84f, "#E5533D"), (1f, "#B05AD8") };
                br.InterpolationColors = new ColorBlend { Positions = stops.Select(s => s.Item1).ToArray(), Colors = stops.Select(s => C(s.Item2)).ToArray() };
                g.FillRectangle(br, r);
            }
            using var mono = new Font(MonoFamily, 7.5f);
            for (int i = 0; i <= 5; i++)
            {
                var t = (i * 2).ToString();
                var sz = g.MeasureString(t, mono);
                float x = r.X + r.Width * i / 5f - (i == 0 ? 0 : i == 5 ? sz.Width : sz.Width / 2);
                g.DrawString(t, mono, muted, x, 26);
            }
        }
    }

    string SrcName() => _st.TropoSrc switch
    {
        "hepburn" => Lang.T("l.tropo") + ": Hepburn",
        "f5len" => Lang.T("l.tropo") + ": F5LEN",
        _ => Lang.T("l.tropo") + ": " + Lang.T("u.hepburn_f5len_average")
    };

    void UpdateFcLabel()
    {
        if (_fcLabel == null || _fcLabel.IsDisposed) return;
        var t = Clock.UtcNow.AddHours(_st.ForecastH);
        _fcLabel.Text = $"{Lang.DayNames()[(int)t.DayOfWeek]} {t:dd/MM HH}:00 UTC (+{_st.ForecastH} h)";
        if (_obsNote != null && !_obsNote.IsDisposed) _obsNote.Text = Lang.T(_st.ForecastH == 0 ? "obs.now" : "obs.dim");
    }

    // ---------------------------------------------------------------- elenco degli spot

    Control BuildSpotList()
    {
        var dock = new Panel { Dock = DockStyle.Fill, BackColor = C(_th.Panel) };

        var head = new Panel { Dock = DockStyle.Top, Height = 56, BackColor = C(_th.Panel) };
        var title = Lbl(Lang.T("spots.title"), 11f, _th.Text, true); title.Location = new Point(10, 20);
        head.Controls.Add(title);
        var sel = Row(); sel.WrapContents = false; sel.Dock = DockStyle.Right; sel.Padding = new Padding(0, 4, 6, 0);
        var bandCol = Stack(new Padding(0)); bandCol.Dock = DockStyle.None; bandCol.AutoSize = true;
        Add(bandCol, Lbl(Lang.T("band"), 8f, _th.Muted));
        Add(bandCol, Combo(Bands.All.Select(Bands.Label), Array.IndexOf(Bands.All, _st.Band), 100, i =>
        {
            _st.Band = Bands.All[i];
            _map.SelectedSpotId = null; _map.SelectedListener = null; _map.Probe = null;
            RefreshAll();
        }));
        sel.Controls.Add(bandCol);
        int[] wins = { 3, 5, 10, 15, 30, 60 };
        var winCol = Stack(new Padding(0)); winCol.Dock = DockStyle.None; winCol.AutoSize = true;
        Add(winCol, Lbl(Lang.T("last"), 8f, _th.Muted));
        Add(winCol, Combo(wins.Select(w => w + " min"), Array.IndexOf(wins, _st.WindowMin), 80, i => { _st.WindowMin = wins[i]; RefreshAll(); }));
        sel.Controls.Add(winCol);
        var refresh = Btn("⟳", RefreshAll, false); refresh.Margin = new Padding(3, 20, 3, 0);
        new ToolTip().SetToolTip(refresh, Lang.T("refresh"));
        sel.Controls.Add(refresh);
        head.Controls.Add(sel);

        // contatori
        var counters = Row(); counters.Dock = DockStyle.Top; counters.Padding = new Padding(8, 4, 8, 4);
        Control Counter(out Label value, string label, AM.Color? col = null)
        {
            var box = Stack(new Padding(6, 2, 6, 2)); box.Dock = DockStyle.None; box.AutoSize = true; box.BackColor = C(_th.Chip); box.Margin = new Padding(0, 0, 6, 4);
            value = Lbl("0", 12f, col ?? _th.Text, true, true); value.Margin = new Padding(0);
            Add(box, value);
            var l = Lbl(Lang.T(label), 8f, _th.Muted); l.Margin = new Padding(0);
            Add(box, l);
            return box;
        }
        counters.Controls.Add(Counter(out _nSpots, "n.spots"));
        counters.Controls.Add(Counter(out _nAs, "n.as", _th.AsText));
        counters.Controls.Add(Counter(out _nExcl, "n.excl"));
        counters.Controls.Add(Counter(out _nRx, "n.rx"));
        counters.Controls.Add(Counter(out _nDec, "n.dec", _th.Dark ? AM.Color.Parse("#FF7A7A") : AM.Color.Parse("#B01E2E")));
        counters.Controls.Add(Counter(out _nHeld, "n.held", _th.Muted));
        // MQTT: stato e ascoltatori scartati; clic = elenco degli scartati con il motivo
        var mq = Stack(new Padding(6, 2, 6, 2)); mq.Dock = DockStyle.None; mq.AutoSize = true; mq.BackColor = C(_th.Chip); mq.Margin = new Padding(0, 0, 6, 4); mq.Cursor = Cursors.Hand;
        var mqRow = Row(); mqRow.WrapContents = false; mqRow.Margin = new Padding(0);
        _mqttDot = Lbl("●", 11f, _th.Muted, true); _mqttDot.Margin = new Padding(0);
        mqRow.Controls.Add(_mqttDot);
        var mqT = Lbl("MQTT", 12f, _th.Text, true, true); mqT.Margin = new Padding(0);
        mqRow.Controls.Add(mqT);
        Add(mq, mqRow);
        _mqttCount = Lbl("", 8f, _th.Muted); _mqttCount.Margin = new Padding(0);
        Add(mq, _mqttCount);
        void MqClick()
        {
            _showMqttDiscarded = !_showMqttDiscarded;
            if (_showMqttDiscarded) { _map.SelectedListener = null; _map.SelectedSpotId = null; _map.Probe = null; _map.InvalidateVisual(); }
            RefreshInfoCard();
        }
        foreach (Control c in new Control[] { mq, mqRow, _mqttDot, mqT, _mqttCount }) { c.Click += (_, _) => MqClick(); c.Cursor = Cursors.Hand; }
        new ToolTip().SetToolTip(mq, Lang.T("u.mqtt_discarded_title"));
        counters.Controls.Add(mq);

        var info = Stack(new Padding(10, 4, 10, 6));
        Add(info, WrapLbl(Lang.T("filter"), 8.5f, _th.Muted));
        Add(info, WrapLbl(Lang.T("asnote"), 8.5f, _th.Muted));
        var infoHolder = new Panel { Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
        infoHolder.Controls.Add(info);

        _spotList = new ListBox
        {
            Dock = DockStyle.Fill, DrawMode = DrawMode.OwnerDrawFixed, ItemHeight = 40, BorderStyle = BorderStyle.None,
            BackColor = C(_th.Panel), ForeColor = C(_th.Text), IntegralHeight = false
        };
        _spotList.DrawItem += DrawSpot;
        _spotList.MouseClick += (_, e) =>
        {
            int i = _spotList.IndexFromPoint(e.Location);
            if (i < 0 || i >= _spotList.Items.Count) return;
            if (_spotList.Items[i] is Spot s)
            {
                _map.SelectedSpotId = s.Id; _map.SelectedListener = null; _map.Probe = null;
                _map.InvalidateVisual(); RefreshSpotList(); RefreshInfoCard();
            }
        };
        _emptyText = new Label { Dock = DockStyle.Top, Height = 70, TextAlign = ContentAlignment.MiddleCenter, ForeColor = C(_th.Muted), Text = Lang.T("empty") };

        dock.Controls.Add(_spotList);
        dock.Controls.Add(_emptyText);
        dock.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 1, BackColor = C(_th.Line) });
        dock.Controls.Add(infoHolder);
        dock.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 1, BackColor = C(_th.Line) });
        dock.Controls.Add(counters);
        dock.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 1, BackColor = C(_th.Line) });
        dock.Controls.Add(head);
        return dock;
    }

    void DrawSpot(object sender, DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= _spotList.Items.Count) return;
        var s = (Spot)_spotList.Items[e.Index];
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        var r = e.Bounds;
        bool sel = _map.SelectedSpotId == s.Id;
        using (var bg = new SolidBrush(sel ? C(_th.SelRow) : C(_th.Panel))) g.FillRectangle(bg, r);
        using (var ln = new Pen(C(_th.Line2))) g.DrawLine(ln, r.Left, r.Bottom - 1, r.Right, r.Bottom - 1);
        bool hasPos = _st.TryPos(s.Dx, s.DxLoc, out var dx) & _st.TryPos(s.Spotter, s.DeLoc, out var de);
        int dc = hasPos ? _st.DistClass(dx, de) : 0;
        string km = hasPos ? Geo.DistKm(dx, de).ToString("0") + (dc > 0 ? " ▦" : "") : "—";
        var col = s.Airscatter ? Palette.AirscatterYellow : Palette.SpotBlue;
        int x0 = r.Left + 10, y0 = r.Top;
        if (s.Airscatter)
        {
            using var edge = new SolidBrush(C(Palette.AirscatterEdge)); g.FillRectangle(edge, x0, y0 + 11, 24, 5);
            using var fill = new SolidBrush(C(col)); g.FillRectangle(fill, x0, y0 + 12, 24, 3);
        }
        else using (var fill = new SolidBrush(C(col))) g.FillRectangle(fill, x0, y0 + 12, 24, 3);
        using var mono = new Font(MonoFamily, 9f);
        using var monoB = new Font(MonoFamily, 9.5f, FontStyle.Bold);
        using var small = new Font(MonoFamily, 8f);
        using var text = new SolidBrush(C(_th.Text));
        using var sub = new SolidBrush(C(_th.Sub));
        using var muted = new SolidBrush(C(_th.Muted));
        using var asb = new SolidBrush(C(_th.AsText));
        var fmt = new StringFormat(StringFormatFlags.NoWrap) { Trimming = StringTrimming.EllipsisCharacter };
        int w = r.Width;
        g.DrawString(s.Recv.ToString("HH:mm"), mono, sub, x0 + 32, y0 + 4);
        g.DrawString(s.Src, small, muted, x0 + 32, y0 + 21);
        float callsW = Math.Max(60, w - (x0 - r.Left) - 32 - 46 - 110);
        g.DrawString($"{s.Dx} → {s.Spotter}", monoB, text, new RectangleF(x0 + 80, y0 + 3, callsW, 18), fmt);
        g.DrawString(s.Mode, mono, text, new RectangleF(w - 112, y0 + 4, 54, 18), fmt);
        var rf = new StringFormat(fmt) { Alignment = StringAlignment.Far };
        g.DrawString(km, mono, text, new RectangleF(w - 62, y0 + 4, 54, 18), rf);
        g.DrawString($"{s.FreqKHz / 1000:0.000} · {s.Comment}{(hasPos ? "" : "  (" + Lang.T("nopos") + ")")}", small, s.Airscatter ? asb : muted,
            new RectangleF(x0 + 80, y0 + 21, Math.Max(40, w - x0 - 90), 16), fmt);
    }

    void RefreshAll()
    {
        _st.Dirty = false;
        _map.InvalidateVisual();
        RefreshSpotList();
        RefreshInfoCard();
    }

    void RefreshSpotList()
    {
        if (_spotList == null || _spotList.IsDisposed) return;
        var now = Clock.UtcNow;
        var spots = _st.Spots.Filter(_st.Band, _st.WindowMin, now);
        var rows = new List<Spot>();
        foreach (var s in spots)
        {
            if (rows.Count >= 300) break;
            bool hasPos = _st.TryPos(s.Dx, s.DxLoc, out var dx) & _st.TryPos(s.Spotter, s.DeLoc, out var de);
            if (hasPos && _st.DistClass(dx, de) < 0) continue;   // sotto i 100 km con la casella accesa
            rows.Add(s);
        }
        int top = _spotList.TopIndex;
        _spotList.BeginUpdate();
        _spotList.Items.Clear();
        foreach (var s in rows) _spotList.Items.Add(s);
        if (top > 0 && top < _spotList.Items.Count) _spotList.TopIndex = top;
        _spotList.EndUpdate();
        _spotList.Invalidate();

        _emptyText.Visible = spots.Count == 0;
        _emptyText.Text = _st.Band == "222" ? Lang.T("empty222") + "\n" + Lang.T("empty") : Lang.T("empty");
        _nSpots.Text = spots.Count.ToString();
        _nAs.Text = spots.Count(x => x.Airscatter).ToString();
        _nExcl.Text = _st.ExcludedCount(_st.Band, _st.WindowMin).ToString();
        bool rxBand = _st.Mqtt.EnabledBands.Contains(_st.Band);
        _nRx.Text = rxBand ? _st.Listeners.Get(_st.Band, _st.WindowMin, now).Count.ToString() : "—";
        _nDec.Text = _st.Decodes.Get(_st.Band, _st.WindowMin, now).Count.ToString();
        _nHeld.Text = _st.Gate.HeldCount(_st.Band, _st.WindowMin, now).ToString();
        _mqttDot.ForeColor = C(_st.Mqtt.Connected ? _th.Ok : _th.Bad);
        _mqttCount.Text = rxBand ? Lang.F("u.mqtt_discarded", _st.Listeners.Discarded(_st.Band, _st.WindowMin, now).Count) : "—";
        if (_showMqttDiscarded) RefreshInfoCard();
    }

    void InfoLine(string text, float size = 9f, AM.Color? col = null, bool bold = false, bool mono = false)
    {
        var l = Lbl(text, size, col ?? _th.Sub, bold, mono);
        l.MaximumSize = new Size(300, 0);
        l.Margin = new Padding(0, 1, 0, 1);
        Add(_infoBody, l);
    }

    void RefreshInfoCard()
    {
        if (_infoBody == null || _infoBody.IsDisposed) return;
        _infoBody.SuspendLayout();
        foreach (Control c in _infoBody.Controls.Cast<Control>().ToList()) c.Dispose();
        _infoBody.Controls.Clear(); _infoBody.RowStyles.Clear(); _infoBody.RowCount = 0;
        var now = Clock.UtcNow;
        bool show = false;
        if (_map.SelectedListener != null || _map.SelectedSpotId.HasValue || _map.Probe.HasValue) _showMqttDiscarded = false;
        var close = Btn("✕", () =>
        {
            _showMqttDiscarded = false; _map.SelectedListener = null; _map.SelectedSpotId = null; _map.Probe = null;
            _map.InvalidateVisual(); RefreshInfoCard(); RefreshSpotList();
        }, false);
        close.Anchor = AnchorStyles.Right;
        Add(_infoBody, close);
        close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        if (_showMqttDiscarded)
        {
            show = true;
            InfoLine("MQTT · PSKREPORTER", 8.5f, _th.Muted, true);
            InfoLine(_st.Mqtt.Connected ? Lang.F("connected", _st.Mqtt.Host + ":" + _st.Mqtt.Port) : Lang.T("notconn"), 9f, _st.Mqtt.Connected ? _th.Ok : _th.Bad, true);
            InfoLine(Lang.T("u.mqtt_discarded_title"), 9f, _th.Sub);
            var disc = _st.Listeners.Discarded(_st.Band, _st.WindowMin, now);
            if (disc.Count == 0) InfoLine(Lang.T("u.mqtt_none"), 9f, _th.Muted);
            foreach (var d in disc.Take(30))
            {
                InfoLine($"{d.call}  {d.grid}  {d.last:HH:mm}Z", 10f, _th.Text, true, true);
                InfoLine(string.Join(" · ", d.why.Take(8)), 8.5f, _th.Sub, false, true);
            }
        }
        else if (_map.SelectedListener != null)
        {
            var l = _st.Listeners.Get(_st.Band, _st.WindowMin, now).FirstOrDefault(x => x.Band + "|" + x.Call == _map.SelectedListener);
            if (l != null)
            {
                show = true;
                InfoLine(Lang.T("u.listening_pskreporter"), 8.5f, _th.Muted, true);
                InfoLine($"{l.Call}  {l.Grid}", 13f, _th.Text, true, true);
                if (l.Grid.Length < 6) InfoLine(Lang.T("u.grid4"), 8.5f, _th.Muted);
                InfoLine($"{Lang.T("u.last_report")} {l.Last:HH:mm} UTC · {string.Join(" · ", l.Modes)}");
                InfoLine($"{Lang.T("u.heard")} {l.Heard.Count} · max {l.MaxKm:0} km");
                InfoLine(string.Join("  ", l.Heard.Take(20).Select(h => h.Key + (h.Value.snr.HasValue ? $" {h.Value.snr}dB" : ""))), 8.5f, _th.Text, false, true);
            }
        }
        else if (_map.SelectedSpotId.HasValue)
        {
            var s = _st.Spots.Filter(_st.Band, 60, now).FirstOrDefault(x => x.Id == _map.SelectedSpotId);
            if (s != null)
            {
                show = true;
                InfoLine(Lang.T("u.selected_spot") + (s.Airscatter ? "AIRSCATTER" : "TROPO"), 8.5f, s.Airscatter ? _th.AsText : _th.Muted, true);
                InfoLine($"{s.Dx} → {s.Spotter}", 13f, _th.Text, true, true);
                if (_st.TryPos(s.Dx, s.DxLoc, out var a) && _st.TryPos(s.Spotter, s.DeLoc, out var b))
                {
                    double km = Geo.DistKm(a, b);
                    InfoLine($"{Locator.FromLatLon(a.Lat, a.Lon)} → {Locator.FromLatLon(b.Lat, b.Lon)} · {km:0} km · {Geo.Azimuth(a, b):0}°", 9f, _th.Sub, false, true);
                    double sum = 0; int n = 0;
                    foreach (var p in Geo.GreatCircle(a, b, Math.Max(2, (int)(km / 10))))
                    {
                        var v = _st.Tropo.Value(_st.TropoSrc, p.Lat, p.Lon, Clock.UtcNow.AddHours(_st.ForecastH));
                        if (!double.IsNaN(v)) { sum += v; n++; }
                    }
                    InfoLine(Lang.T("u.tropo_index_on_path") + (n > 0 ? (sum / n).ToString("0.0") : "—"));
                    InfoLine(Lang.T("u.troposcatter_loss_g3ygf_smooth") + Geo.TroposcatterLossDb(km, s.FreqKHz / 1000).ToString("0") + " dB");
                    if (s.Dx == Graves.Call) InfoLine(Graves.Describe(b), 9f, _th.Text, true);
                }
                else InfoLine(Lang.T("nopos"));
                InfoLine($"{s.FreqKHz / 1000:0.000} MHz · {s.Mode} · {s.Src} · {s.Recv:HH:mm} UTC");
                InfoLine(s.Comment, 9f, _th.Text, false, true);
            }
        }
        else if (_map.Probe.HasValue)
        {
            show = true;
            var p = _map.Probe.Value;
            InfoLine(Lang.T("u.point") + Locator.FromLatLon(p.Lat, p.Lon), 8.5f, _th.Muted, true);
            string dist = _st.TryQth(out var q) ? $" · {Geo.DistKm(q, p):0} km, {Geo.Azimuth(q, p):0}°" : "";
            InfoLine($"{Math.Abs(p.Lat):0.00}°{(p.Lat >= 0 ? "N" : "S")} {Math.Abs(p.Lon):0.00}°{(p.Lon >= 0 ? "E" : "W")}{dist}", 9f, _th.Sub, false, true);
            string V(string src) { var v = _st.Tropo.Value(src, p.Lat, p.Lon, Clock.UtcNow.AddHours(_st.ForecastH)); return double.IsNaN(v) ? "—" : v.ToString("0.0"); }
            InfoLine($"Hepburn {V("hepburn")} · F5LEN {V("f5len")} · {Lang.T("u.average")} {V("media")}", 10f, _th.Text, true);
            var ft = Clock.UtcNow.AddHours(_st.ForecastH);
            var pr = _st.Pressure.ValueAt(p, ft);
            InfoLine(Lang.T("u.pressure") + (double.IsNaN(pr) ? "—" : pr.ToString("0") + " hPa"), 10f, _th.Text, true);
            double t2 = _st.Pressure.T2At(p, ft), t925 = _st.Pressure.T925At(p, ft), t850 = _st.Pressure.T850At(p, ft), inv = _st.Pressure.InversionAt(p, ft);
            string T(double v) => double.IsNaN(v) ? "—" : v.ToString("0.0") + " °C";
            InfoLine($"T 2 m {T(t2)} · 925 hPa {T(t925)} · 850 hPa {T(t850)}", 9f, _th.Sub, false, true);
            if (!double.IsNaN(inv)) InfoLine(Lang.T("l.inv") + ": " + (inv > 0 ? "+" : "") + inv.ToString("0.0") + " °C", 9f, inv > 0 ? _th.Text : _th.Muted, inv > 0);
            if (!_st.Tropo.HasData) InfoLine(Lang.T("tropo.wait"), 8.5f, _th.Muted);
        }
        _infoBody.ResumeLayout(true);
        _infoCard.Visible = show;
        if (show)
        {
            _infoCard.Height = Math.Min(_infoBody.PreferredSize.Height + 4, Math.Max(120, (_mapHost?.ClientSize.Height ?? 400) - 24));
            _infoCard.BringToFront();
        }
    }
}
