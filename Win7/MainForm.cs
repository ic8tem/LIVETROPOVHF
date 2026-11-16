using System.Drawing;
using System.Windows.Forms;
using TropoVHF.Core;
using TropoVHF.Ui;
using AM = Avalonia.Media;
using AC = Avalonia.Controls;

namespace TropoVHF.Win7;

/// <summary>
/// Finestra principale della versione per Windows 7, in WinForms: stesse linguette, stesse funzioni e stessi testi
/// (36 lingue) della versione normale. La mappa e il cielo di GRAVES sono gli stessi controlli, disegnati con GDI+.
/// </summary>
public sealed partial class MainForm : Form
{
    readonly AppState _st;
    Palette _th;
    readonly MapView _map;
    readonly System.Windows.Forms.Timer _timer;
    string _tab = "map";
    bool _closeConfirmed;
    Rectangle _normal;

    Label _clock, _ntp, _status;
    TextBox _locBox;
    readonly Dictionary<string, Control> _tabContent = new();
    readonly Dictionary<string, Button> _tabButtons = new();
    Panel _content;

    public MainForm()
    {
        _st = new AppState();
        Lang.Current = _st.LangCode;
        _th = Palette.Get(_st.ThemeKey);
        _map = new MapView(_st, _th);
        _map.SelectionChanged += () => { RefreshInfoCard(); RefreshSpotList(); };

        Text = "LIVETROPOVHF " + Program.Version + " · IC8TEM";
        Icon = Program.AppIcon();
        Font = new Font("Segoe UI", 9f);
        MinimumSize = new Size(900, 560);
        KeyPreview = true;
        PlaceAtStartup();

        BuildUi();
        _st.Start();
        _st.CallDbLoaded += () => Ui(() =>
        {
            if (_st.CallDb.Error == "" && _st.CallDb.Count > 0) return;
            MessageBox.Show(this, Lang.F("u.call3_missing", Settings.AppDir, _st.CallDb.Error), "CALL3.TXT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        });
        _st.Call3StatusChanged += m => Ui(() => { if (_mmmStatus != null) _mmmStatus.Text = m; FillCalls(); _map.InvalidateVisual(); });
        _st.CallDbLoaded += () => Ui(FillCalls);
        Shown += (_, _) => { if (!_st.HasValidCall) AskCall(); };

        _timer = new System.Windows.Forms.Timer { Interval = 1000 };
        _timer.Tick += (_, _) => Tick();
        _timer.Start();

        Move += (_, _) => TrackNormal();
        Resize += (_, _) => TrackNormal();
    }

    /// <summary>Esegue sul thread della finestra (gli eventi del motore arrivano dai thread di rete).</summary>
    void Ui(Action a)
    {
        try
        {
            if (IsDisposed) return;
            if (!IsHandleCreated) { HandleCreated += (_, _) => BeginInvoke(a); return; }
            BeginInvoke(a);
        }
        catch { }
    }

    // ================================================================ chiusura

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (!_closeConfirmed && e.CloseReason == CloseReason.UserClosing)
        {
            if (!AskClose()) { e.Cancel = true; return; }
            _closeConfirmed = true;
        }
        SaveGeometry();
        _timer.Stop();
        try { _st.Shutdown(); } catch { }
        base.OnFormClosing(e);
    }

    /// <summary>Finestra di chiusura nello stile di YAEC: icona, domanda, ringraziamento in corsivo, barra con Sì e No.</summary>
    bool AskClose()
    {
        using var dlg = new Form
        {
            Text = Lang.T("close.title"), FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false,
            StartPosition = FormStartPosition.CenterParent, ShowInTaskbar = false, Icon = Icon, BackColor = C(_th.Panel),
            ClientSize = new Size(480, 170), Font = Font, KeyPreview = true
        };
        var img = Program.LoadImage("livetropovhf-256.png");
        if (img != null) dlg.Controls.Add(new PictureBox { Image = img, SizeMode = PictureBoxSizeMode.Zoom, Bounds = new Rectangle(20, 20, 64, 64) });
        var q = new Label { Text = Lang.T("close.q"), Font = new Font(Font.FontFamily, 13f, FontStyle.Bold), ForeColor = C(_th.Text), AutoSize = false, Bounds = new Rectangle(100, 18, 360, 50) };
        var thanks = new Label { Text = Lang.T("close.thanks"), Font = new Font(Font.FontFamily, 10.5f, FontStyle.Italic), ForeColor = C(_th.Sub), AutoSize = false, Bounds = new Rectangle(100, 68, 360, 44) };
        dlg.Controls.Add(q); dlg.Controls.Add(thanks);
        var bar = new Panel { Dock = DockStyle.Bottom, Height = 50, BackColor = C(_th.Chip) };
        var yes = Btn(Lang.T("yes"), null, true); yes.DialogResult = DialogResult.Yes; yes.Size = new Size(100, 30);
        var no = Btn(Lang.T("no"), null, false); no.DialogResult = DialogResult.No; no.Size = new Size(100, 30);
        yes.Location = new Point(480 - 230, 10); no.Location = new Point(480 - 120, 10);
        bar.Controls.Add(yes); bar.Controls.Add(no);
        dlg.Controls.Add(bar);
        dlg.AcceptButton = yes; dlg.CancelButton = no;
        return dlg.ShowDialog(this) == DialogResult.Yes;
    }

    /// <summary>Nominativo (obbligatorio per i cluster) e locatore.</summary>
    void AskCall()
    {
        using var dlg = new Form
        {
            Text = Lang.T("u.ask_call_title"), FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false,
            StartPosition = FormStartPosition.CenterParent, ShowInTaskbar = false, BackColor = C(_th.Panel), ClientSize = new Size(480, 260), Font = Font
        };
        var title = new Label { Text = Lang.T("u.ask_call_title"), Font = new Font(Font.FontFamily, 13f, FontStyle.Bold), ForeColor = C(_th.Accent), AutoSize = true, Location = new Point(20, 16) };
        var text = new Label { Text = Lang.T("u.ask_call_text"), ForeColor = C(_th.Text), AutoSize = false, Bounds = new Rectangle(20, 50, 440, 70) };
        var l1 = new Label { Text = Lang.T("call"), ForeColor = C(_th.Muted), AutoSize = true, Location = new Point(20, 133) };
        var call = Box(_st.Call, 200); call.Location = new Point(180, 130); call.CharacterCasing = CharacterCasing.Upper;
        var l2 = new Label { Text = Lang.T("u.ask_call_loc"), ForeColor = C(_th.Muted), AutoSize = true, Location = new Point(20, 168) };
        var loc = Box(_st.Loc, 200); loc.Location = new Point(180, 165); loc.CharacterCasing = CharacterCasing.Upper; loc.MaxLength = 6;
        var err = new Label { ForeColor = C(_th.Bad), AutoSize = true, Location = new Point(20, 198) };
        var ok = Btn("OK", null, true); ok.Size = new Size(100, 30); ok.Location = new Point(360, 220);
        var later = Btn(Lang.T("u.later"), null, false); later.Size = new Size(110, 30); later.Location = new Point(240, 220); later.DialogResult = DialogResult.Cancel;
        bool okDone = false;
        ok.Click += (_, _) =>
        {
            var c = (call.Text ?? "").Trim().ToUpperInvariant();
            if (!AppState.IsValidCall(c)) { err.Text = Lang.T("u.ask_call_invalid"); return; }
            var l = (loc.Text ?? "").Trim().ToUpperInvariant();
            if (l != "" && !Locator.IsValid(l)) { err.Text = Lang.T("u.enter_a_46character_locator"); return; }
            _st.Call = c;
            if (l != "") _st.Loc = l;
            okDone = true;
            dlg.Close();
        };
        dlg.Controls.AddRange(new Control[] { title, text, l1, call, l2, loc, err, ok, later });
        dlg.AcceptButton = ok; dlg.CancelButton = later;
        dlg.Shown += (_, _) => call.Focus();
        dlg.ShowDialog(this);
        if (!okDone) return;
        _st.StartFeeds();
        _st.Save();
        BuildUi();
    }

    // ================================================================ finestra: posizione e grandezza

    void PlaceAtStartup()
    {
        StartPosition = FormStartPosition.Manual;
        var S = _st.S;
        int x = S.GetInt("win_x", int.MinValue), y = S.GetInt("win_y", int.MinValue), w = S.GetInt("win_w", 0), h = S.GetInt("win_h", 0);
        bool max = S.GetBool("win_max", false);
        if (x != int.MinValue && w > 0 && h > 0)
        {
            var title = new Point(x + w / 2, y + 20);
            var scr = Screen.AllScreens.FirstOrDefault(s => s.WorkingArea.Contains(title));
            if (scr != null)
            {
                var wa = scr.WorkingArea;
                w = Math.Min(w, wa.Width); h = Math.Min(h, wa.Height);
                x = Math.Clamp(x, wa.X, wa.Right - w); y = Math.Clamp(y, wa.Y, wa.Bottom - h);
                Bounds = new Rectangle(x, y, Math.Max(900, w), Math.Max(560, h));
                _normal = Bounds;
                if (max) WindowState = FormWindowState.Maximized;
                return;
            }
        }
        var a = Screen.PrimaryScreen.WorkingArea;
        if (a.Width < 1280 || a.Height < 720)
        {
            Bounds = new Rectangle(a.X, a.Y, Math.Max(900, Math.Min(a.Width, 1280)), Math.Max(560, Math.Min(a.Height, 720)));
            WindowState = FormWindowState.Maximized;
            return;
        }
        int pw = (int)(a.Width * 0.9), ph = (int)(a.Height * 0.9);
        Bounds = new Rectangle(a.X + (a.Width - pw) / 2, a.Y + (a.Height - ph) / 2, pw, ph);
        _normal = Bounds;
    }

    void TrackNormal() { if (WindowState == FormWindowState.Normal && Visible) _normal = Bounds; }

    void SaveGeometry()
    {
        try
        {
            TrackNormal();
            if (_normal.Width <= 0) return;
            var S = _st.S;
            S.SetInt("win_x", _normal.X); S.SetInt("win_y", _normal.Y);
            S.SetInt("win_w", _normal.Width); S.SetInt("win_h", _normal.Height);
            S.Set("win_max", WindowState == FormWindowState.Maximized ? "1" : "0");
        }
        catch { }
    }

    // ================================================================ piccoli attrezzi

    static Color C(AM.Color c) => Color.FromArgb(c.A, c.R, c.G, c.B);
    static Color C(string hex) => C(AM.Color.Parse(hex));
    static readonly FontFamily MonoFamily = PickMono();
    static FontFamily PickMono()
    {
        foreach (var n in new[] { "Consolas", "Lucida Console", "Courier New" })
            try { return new FontFamily(n); } catch { }
        return FontFamily.GenericMonospace;
    }

    Font F(float size, bool bold = false, bool mono = false, bool italic = false) =>
        new Font(mono ? MonoFamily : Font.FontFamily, size, (bold ? FontStyle.Bold : FontStyle.Regular) | (italic ? FontStyle.Italic : FontStyle.Regular));

    Label Lbl(string text, float size = 9f, AM.Color? col = null, bool bold = false, bool mono = false) => new()
    {
        Text = text ?? "", AutoSize = true, Font = F(size, bold, mono), ForeColor = C(col ?? _th.Text), BackColor = Color.Transparent,
        Margin = new Padding(3, 5, 3, 3)
    };

    /// <summary>Etichetta che va a capo seguendo la larghezza del contenitore.</summary>
    Label WrapLbl(string text, float size = 9f, AM.Color? col = null, bool bold = false, bool mono = false)
    {
        var l = Lbl(text, size, col, bold, mono);
        void Fit()
        {
            var p = l.Parent;
            if (p == null) return;
            int w = Math.Max(120, p.ClientSize.Width - p.Padding.Horizontal - l.Margin.Horizontal - 4);
            if (l.MaximumSize.Width != w) l.MaximumSize = new Size(w, 0);
        }
        l.ParentChanged += (_, _) => { Fit(); if (l.Parent != null) l.Parent.SizeChanged += (_, _) => Fit(); };
        return l;
    }

    TextBox Box(string text, int width, bool mono = true) => new()
    {
        Text = text ?? "", Width = width, Font = F(10f, false, mono), BackColor = C(_th.CtlBg), ForeColor = C(_th.Text), BorderStyle = BorderStyle.FixedSingle,
        Margin = new Padding(3, 3, 10, 3)
    };

    Button Btn(string text, Action click, bool primary)
    {
        var b = new Button
        {
            Text = text, AutoSize = true, FlatStyle = FlatStyle.Flat, Padding = new Padding(8, 2, 8, 2), Margin = new Padding(3, 3, 8, 3),
            BackColor = primary ? C(_th.Accent) : C(_th.CtlBg), ForeColor = primary ? C(_th.AccentFg) : C(_th.Text2), Cursor = Cursors.Hand,
            Font = F(9f, primary)
        };
        b.FlatAppearance.BorderColor = primary ? C(_th.Accent) : C(_th.Ctl);
        if (click != null) b.Click += (_, _) => click();
        return b;
    }

    CheckBox Chk(string text, bool value, Action<bool> changed)
    {
        var c = new CheckBox { Text = text, Checked = value, AutoSize = true, ForeColor = C(_th.Text), BackColor = Color.Transparent, Margin = new Padding(3, 4, 10, 3) };
        c.CheckedChanged += (_, _) => changed(c.Checked);
        return c;
    }

    ComboBox Combo(IEnumerable<string> items, int index, int width, Action<int> changed)
    {
        var cb = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = width, Margin = new Padding(3, 3, 10, 3), BackColor = C(_th.CtlBg), ForeColor = C(_th.Text), FlatStyle = FlatStyle.Flat };
        foreach (var i in items) cb.Items.Add(i);
        if (cb.Items.Count > 0) cb.SelectedIndex = Math.Clamp(index, 0, cb.Items.Count - 1);
        cb.SelectedIndexChanged += (_, _) => { if (cb.SelectedIndex >= 0) changed(cb.SelectedIndex); };
        return cb;
    }

    NumericUpDown Port(int value, Action<int> set)
    {
        var n = new NumericUpDown { Minimum = 1, Maximum = 65535, Value = Math.Clamp(value, 1, 65535), Width = 90, Font = F(10f, false, true), Margin = new Padding(3, 3, 10, 3), BackColor = C(_th.CtlBg), ForeColor = C(_th.Text) };
        n.ValueChanged += (_, _) => set((int)n.Value);
        return n;
    }

    static FlowLayoutPanel Row() => new() { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, WrapContents = true, FlowDirection = FlowDirection.LeftToRight, Margin = new Padding(0, 2, 0, 2), BackColor = Color.Transparent };

    /// <summary>Colonna verticale che si allarga con il contenitore (una riga per controllo).</summary>
    static TableLayoutPanel Stack(Padding pad)
    {
        var t = new TableLayoutPanel { ColumnCount = 1, Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Padding = pad, BackColor = Color.Transparent };
        t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        return t;
    }

    static void Add(TableLayoutPanel t, Control c)
    {
        if (c is FlowLayoutPanel || c is TableLayoutPanel || c is Panel) c.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        else c.Anchor = AnchorStyles.Left | AnchorStyles.Top;
        t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        t.Controls.Add(c, 0, t.RowCount++);
    }

    static Panel ScrollBox(Control inner, Color back)
    {
        var p = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = back };
        p.Controls.Add(inner);
        return p;
    }

    /// <summary>Riquadro con titolo colorato e corpo (come le sezioni della versione normale).</summary>
    (Panel section, TableLayoutPanel body) Section(string title, int idx)
    {
        var outer = Stack(new Padding(0));
        outer.BackColor = C(_th.Panel);
        outer.Margin = new Padding(8, 0, 8, 12);
        var t = Lbl(title, 12f, SectionColor(idx), true); t.Margin = new Padding(10, 8, 10, 4);
        Add(outer, t);
        var body = Stack(new Padding(10, 6, 10, 8));
        body.BackColor = C(_th.Chip);
        Add(outer, body);
        var holder = new Panel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Dock = DockStyle.Top, Padding = new Padding(1), BackColor = C(_th.Line), Margin = new Padding(8, 0, 8, 12) };
        holder.Controls.Add(outer);
        return (holder, body);
    }

    AM.Color SectionColor(int i)
    {
        var dark = new[] { "#7EA8FF", "#B79BFF", "#FFA35C", "#5BD08A", "#4FD1E0" };
        var light = new[] { "#2459C6", "#6E45D6", "#B85A0C", "#1F7A45", "#0E7C8C" };
        return AM.Color.Parse((_th.Dark ? dark : light)[i % 5]);
    }

    // ================================================================ costruzione

    void BuildUi()
    {
        SuspendLayout();
        var old = Controls.Cast<Control>().ToList();
        Controls.Clear();
        foreach (var c in old) try { c.Dispose(); } catch { }
        _map.Th = _th;
        _tabContent.Clear(); _tabButtons.Clear(); _sections.Clear(); _previews.Clear(); _tropoHolders.Clear();
        BackColor = C(_th.Page);
        ForeColor = C(_th.Text);

        _content = new Panel { Dock = DockStyle.Fill, BackColor = C(_th.Page) };
        _tabContent["map"] = BuildMapTab();
        _tabContent["hepburn"] = BuildTropoTab("hepburn");
        _tabContent["f5len"] = BuildTropoTab("f5len");
        _tabContent["graves"] = BuildGravesTab();
        _tabContent["sus"] = BuildSuspectsTab();
        _tabContent["collect"] = BuildCollectTab();
        _tabContent["doc1"] = BuildDoc("funzionamento.md", "LIVETROPOVHF - Funzionamento del programma");
        _tabContent["doc2"] = BuildDoc("mappa.md", "LIVETROPOVHF - Costruzione della mappa");
        _tabContent["doc3"] = BuildDoc("tropo.md", "LIVETROPOVHF - Come si forma la tropo");
        foreach (var kv in _tabContent) { kv.Value.Dock = DockStyle.Fill; kv.Value.Visible = false; _content.Controls.Add(kv.Value); }

        Controls.Add(_content);
        Controls.Add(BuildTabBand());
        Controls.Add(BuildTopRow());
        Controls.Add(BuildStatusBar());
        ResumeLayout(true);
        ShowTab(_tab);
        Tick();
        RefreshSpotList();
        RefreshInfoCard();
        RefreshLogs(true);
    }

    Control BuildTopRow()
    {
        var bar = new Panel { Dock = DockStyle.Top, Height = 52, BackColor = C(_th.Bar) };
        var left = Row(); left.Dock = DockStyle.Left; left.WrapContents = false; left.Padding = new Padding(8, 12, 0, 0);
        left.Controls.Add(Lbl(Lang.T("call"), 9f, _th.Muted));
        var call = Box(_st.Call, 110); call.CharacterCasing = CharacterCasing.Upper;
        call.TextChanged += (_, _) => { _st.Call = (call.Text ?? "").ToUpperInvariant(); _st.Dirty = true; };
        left.Controls.Add(call);
        left.Controls.Add(Lbl(Lang.T("loc"), 9f, _th.Muted));
        _locBox = Box(_st.Loc, 90); _locBox.CharacterCasing = CharacterCasing.Upper; _locBox.MaxLength = 6;
        _locBox.TextChanged += (_, _) =>
        {
            _st.Loc = (_locBox.Text ?? "").ToUpperInvariant();
            _locBox.BackColor = _st.Loc.Length > 0 && !Locator.IsValid(_st.Loc) ? C("#F8D7D7") : C(_th.CtlBg);
            _st.Dirty = true;
            _map.InvalidateVisual();
        };
        left.Controls.Add(_locBox);
        bar.Controls.Add(left);

        var right = Row(); right.Dock = DockStyle.Right; right.WrapContents = false; right.Padding = new Padding(0, 6, 8, 0);
        _clock = Lbl("", 20f, _th.Clock, true, true); _clock.Margin = new Padding(3, 2, 6, 0);
        _ntp = Lbl("NTP", 20f, _th.Muted, true, true); _ntp.Margin = new Padding(3, 2, 12, 0);
        right.Controls.Add(_clock); right.Controls.Add(_ntp);
        var themeBox = Combo(Palette.Keys.Select(k => Lang.T("theme." + k)), Array.IndexOf(Palette.Keys, _st.ThemeKey), 150, i =>
        {
            var k = Palette.Keys[i];
            if (k == _st.ThemeKey) return;
            _st.ThemeKey = k; _th = Palette.Get(k);
            BeginInvoke(new Action(BuildUi));
        });
        themeBox.Margin = new Padding(3, 10, 8, 0);
        right.Controls.Add(themeBox);
        var langBox = Combo(Lang.All.Select(x => x.name), Array.FindIndex(Lang.All, x => x.code == _st.LangCode), 150, i =>
        {
            var code = Lang.All[i].code;
            if (code == _st.LangCode) return;
            _st.LangCode = code; Lang.Current = code;
            BeginInvoke(new Action(BuildUi));
        });
        langBox.Margin = new Padding(3, 10, 8, 0);
        right.Controls.Add(langBox);
        bar.Controls.Add(right);
        var line = new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = C(_th.Line) };
        bar.Controls.Add(line);
        return bar;
    }

    Control BuildTabBand()
    {
        var band = new Panel { Dock = DockStyle.Top, Height = 42, BackColor = C(_th.Band) };
        var row = Row(); row.Dock = DockStyle.Fill; row.WrapContents = false; row.Padding = new Padding(8, 6, 0, 0);
        foreach (var (key, label) in new[] { ("map", "tab.map"), ("hepburn", "tab.hep"), ("f5len", "tab.f5"), ("graves", "tab.graves"), ("sus", "tab.sus"), ("collect", "tab.collect"), ("doc1", "tab.doc1"), ("doc2", "tab.doc2"), ("doc3", "tab.doc3") })
        {
            var b = new Button { Text = Lang.T(label), AutoSize = true, FlatStyle = FlatStyle.Flat, Height = 36, Padding = new Padding(10, 0, 10, 0), Margin = new Padding(0, 0, 3, 0), Font = F(10f), Cursor = Cursors.Hand };
            b.FlatAppearance.BorderSize = 0;
            var k = key;
            b.Click += (_, _) => ShowTab(k);
            _tabButtons[key] = b;
            row.Controls.Add(b);
        }
        band.Controls.Add(row);
        return band;
    }

    void ShowTab(string key)
    {
        _tab = key;
        foreach (var kv in _tabContent) kv.Value.Visible = kv.Key == key;
        foreach (var kv in _tabButtons)
        {
            bool on = kv.Key == key;
            kv.Value.BackColor = on ? C(_th.Page) : C(_th.Band);
            kv.Value.ForeColor = on ? C(_th.Text) : C(_th.BandFg);
            kv.Value.FlatAppearance.MouseOverBackColor = on ? C(_th.Page) : ControlPaint.Light(C(_th.Band), 0.15f);
            kv.Value.Font = F(10f, on);
        }
        if (key == "hepburn" || key == "f5len") FillTropoTab(key);
        RefreshLogs(true);
    }

    Control BuildStatusBar()
    {
        var p = new Panel { Dock = DockStyle.Bottom, Height = 26, BackColor = C(_th.Bar) };
        _status = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = C(_th.Muted), Padding = new Padding(10, 0, 0, 0), AutoEllipsis = true };
        p.Controls.Add(_status);
        p.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 1, BackColor = C(_th.Line) });
        return p;
    }

    // ================================================================ aggiornamento periodico

    void Tick()
    {
        var now = Clock.UtcNow;
        if (_clock != null) _clock.Text = now.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        if (_ntp != null)
        {
            bool once = _st.Ntp.LastCheckUtc != default;
            _ntp.Text = !once ? "NTP:…" : Clock.NtpOk ? "NTP:OK" : "NTP:FAILED";
            _ntp.ForeColor = C(!once ? _th.Muted : Clock.NtpOk ? _th.Ok : _th.Bad);
        }
        if (_zoomPct != null) _zoomPct.Text = _map.ZoomPercent;
        if (now.Second == 0) UpdateFcLabel();

        if (_status != null)
        {
            string Dot(bool ok) => ok ? "●" : "○";
            var parts = new List<string>();
            foreach (var f in _st.Feeds) parts.Add($"{Dot(f.Connected)} {f.Src}");
            parts.Add($"{Dot(_st.Mqtt.Connected)} PSKReporter MQTT");
            parts.Add($"{Dot(_st.Udp.Running)} UDP {_st.Udp.ListenPort}→{_st.Udp.ForwardPort}");
            parts.Add(_st.Pressure.Status);
            parts.Add(_st.Tropo.Status);
            parts.Add(_st.CallDb.Ready ? (_st.CallDb.Error != "" ? "⚠ " + _st.CallDb.Error : $"CALL3: {_st.CallDb.Count:N0}") : "CALL3: …");
            if (!_st.Map.Loaded && _st.Map.Status != "") parts.Add(_st.Map.Status);
            if (_st.Archive.Error != "") parts.Add("DB: " + _st.Archive.Error);
            parts.Add("v" + Program.Version + " (Windows 7)" + (Updater.Status != "" ? " · " + Updater.Status : ""));
            var s = string.Join("   ·   ", parts.Where(x => !string.IsNullOrEmpty(x)));
            if (_status.Text != s) _status.Text = s;
            _status.ForeColor = C(_st.CallDb.Ready && (_st.CallDb.Error != "" || _st.CallDb.Count == 0) ? _th.Bad : _th.Muted);
        }

        if (_st.Dirty)
        {
            _st.Dirty = false;
            _map.InvalidateVisual();
            if ((_tab == "hepburn" || _tab == "f5len") && _previews.TryGetValue(_tab, out var pv)) pv.InvalidateVisual();
            if (_tab == "map") { RefreshSpotList(); RefreshInfoCard(); }
        }
        if (_st.LogsDirty) RefreshLogs(false);
        if ((_tab == "hepburn" || _tab == "f5len") && _st.Tropo.LastUpdateUtc != _tropoShown)
        {
            _tropoShown = _st.Tropo.LastUpdateUtc;
            FillTropoTab(_tab);
        }
    }
}
