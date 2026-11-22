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
static class WinFormsExt
{
    /// <summary>BeginInvoke che non solleva eccezioni se la maniglia non c'è ancora o il controllo è chiuso.</summary>
    public static void BeginInvokeSafe(this Control c, Action a)
    {
        try { if (c.IsHandleCreated && !c.IsDisposed) c.BeginInvoke(a); else a(); } catch { }
    }
}

public sealed partial class MainForm : Form
{
    readonly AppState _st;
    Palette _th;
    /// <summary>Immagine di partenza da chiudere quando la finestra è pronta (la imposta Program).</summary>
    public Form StartSplash;
    public DateTime SplashShownAt = DateTime.UtcNow;
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
        Shown += async (_, _) =>
        {
            // prima si chiude lo splash (dopo 2,5 s dall'apertura, o subito con un clic), poi, se serve, si chiede il
            // nominativo: così la richiesta non resta mai nascosta sotto l'immagine di partenza
            var sp = StartSplash;
            if (sp != null && !sp.IsDisposed)
            {
                var left = TimeSpan.FromSeconds(2.5) - (DateTime.UtcNow - SplashShownAt);
                for (var t = TimeSpan.Zero; t < left && !sp.IsDisposed; t += TimeSpan.FromMilliseconds(100))
                    await Task.Delay(100);
                try { if (!sp.IsDisposed) sp.Close(); } catch { }
            }
            StartSplash = null;
            Activate();
            if (!_st.HasValidCall) AskCall(); else ShowDisclaimer();   // 1.3.2: disclaimer a ogni avvio
        };

        _timer = new System.Windows.Forms.Timer { Interval = 1000 };
        _timer.Tick += (_, _) => Tick();
        _timer.Start();

        // prova automatica (CI): LIVETROPOVHF_TOUR=secondi fa girare da solo le linguette per le fotografie
        if (int.TryParse(Environment.GetEnvironmentVariable("LIVETROPOVHF_TOUR") ?? "", out var tour) && tour > 0)
        {
            var steps = new (string tab, string sub)[] { ("map", null), ("fc", null), ("hepburn", null), ("f5len", null), ("graves", null), ("sus", null), ("collect", "dx"), ("collect", "psk"), ("collect", "udp"), ("collect", "calls"), ("doc1", null), ("doc4", null), ("map", null) };
            int k = 0;
            var tt = new System.Windows.Forms.Timer { Interval = tour * 1000 };
            tt.Tick += (_, _) =>
            {
                if (k >= steps.Length) { tt.Stop(); return; }
                var (tab, sub) = steps[k++];
                if (sub != null) ShowSubTab(sub);
                ShowTab(tab);
            };
            tt.Start();
        }
        Move += (_, _) => TrackNormal();
        Resize += (_, _) => TrackNormal();
        ErrorLog.Logged += msg => Ui(() => { _lastErrorNote = msg; _lastErrorAt = DateTime.Now; });
        Updater.Installed += (v, mandatory) => Ui(() => AskRestart(v, mandatory));
        Shown += async (_, _) => { await Task.Delay(4000); if (ErrorLog.HasPending && !IsDisposed) AskSendErrorReport(); };
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
    static string AppVersion => typeof(MainForm).Assembly.GetName().Version?.ToString(3) ?? "";

    /// <summary>Riquadro del disclaimer (1.3.2) come etichetta che va a capo; restituisce l'altezza usata.</summary>
    int AddDisclaimer(Control parent, int x, int y, int w, bool full)
    {
        var text = full ? Lang.T("u.disc_head") + "\n\n" + Lang.T("u.disc_p1") + "\n\n" + Lang.T("u.disc_p2") + "\n\n" + Lang.T("u.disc_p3") : Lang.T("u.disc_short");
        var box = new Panel { Location = new Point(x, y), BackColor = C(_th.Accent), Padding = new Padding(1) };
        var inner = new Panel { Dock = DockStyle.Fill, BackColor = C(_th.Chip), Padding = new Padding(12, 10, 12, 10) };
        var lbl = new Label { Text = text, ForeColor = C(_th.Text), AutoSize = false, Dock = DockStyle.Fill };
        var sz = TextRenderer.MeasureText(text, Font, new Size(w - 26, 2000), TextFormatFlags.WordBreak);
        box.Size = new Size(w, sz.Height + 26);
        inner.Controls.Add(lbl); box.Controls.Add(inner); parent.Controls.Add(box);
        return box.Height;
    }

    void ShowDisclaimer()
    {
        using var dlg = new Form
        {
            Text = "LIVETROPOVHF", FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false,
            StartPosition = FormStartPosition.CenterParent, ShowInTaskbar = false, BackColor = C(_th.Panel), Font = Font
        };
        var title = new Label { Text = "LIVETROPOVHF", Font = new Font("Georgia", 16f), ForeColor = C(_th.Text), AutoSize = true, Location = new Point(20, 14) };
        var sub = new Label { Text = $"{_st.Call} · {_st.Loc} · " + string.Format(Lang.T("u.disc_version"), AppVersion), ForeColor = C(_th.Muted), AutoSize = true, Location = new Point(20, 50) };
        dlg.Controls.Add(title); dlg.Controls.Add(sub);
        int h = AddDisclaimer(dlg, 20, 76, 520, false);
        int y = 76 + h + 14;
        int left = 10;
        var count = new Label { Text = string.Format(Lang.T("u.disc_autoclose"), left), ForeColor = C(_th.Muted), AutoSize = true, Location = new Point(20, y + 7) };
        bool openSources = false;
        var open = Btn(Lang.T("u.disc_open"), null, false); open.AutoSize = true; open.Location = new Point(250, y);
        var cont = Btn(Lang.T("u.disc_continue"), null, true); cont.Size = new Size(110, 30); cont.Location = new Point(430, y);
        open.Click += (_, _) => { openSources = true; dlg.Close(); };
        cont.Click += (_, _) => dlg.Close();
        dlg.Controls.AddRange(new Control[] { count, open, cont });
        dlg.ClientSize = new Size(560, y + 46);
        using var t = new System.Windows.Forms.Timer { Interval = 1000 };
        t.Tick += (_, _) => { left--; count.Text = string.Format(Lang.T("u.disc_autoclose"), left); if (left <= 0) { t.Stop(); dlg.Close(); } };
        dlg.Shown += (_, _) => t.Start();
        dlg.AcceptButton = cont;
        dlg.ShowDialog(this);
        t.Stop();
        if (openSources) ShowTab("doc6");
    }

    void AskCall()
    {
        using var dlg = new Form
        {
            Text = Lang.T("u.ask_call_title"), FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false,
            StartPosition = FormStartPosition.CenterParent, ShowInTaskbar = false, BackColor = C(_th.Panel), Font = Font
        };
        var welcome = new Label { Text = Lang.T("u.disc_welcome"), Font = new Font("Georgia", 16f), ForeColor = C(_th.Text), AutoSize = true, Location = new Point(20, 14) };
        var ver = new Label { Text = string.Format(Lang.T("u.disc_version"), AppVersion), ForeColor = C(_th.Muted), AutoSize = true, Location = new Point(20, 50) };
        dlg.Controls.Add(welcome); dlg.Controls.Add(ver);
        int y = 76 + AddDisclaimer(dlg, 20, 76, 520, true) + 14;
        var title = new Label { Text = Lang.T("u.ask_call_title"), Font = new Font(Font.FontFamily, 11f, FontStyle.Bold), ForeColor = C(_th.Accent), AutoSize = true, Location = new Point(20, y) };
        var text = new Label { Text = Lang.T("u.ask_call_text"), ForeColor = C(_th.Text), AutoSize = false, Bounds = new Rectangle(20, y + 26, 520, 40) };
        var l1 = new Label { Text = Lang.T("call"), ForeColor = C(_th.Muted), AutoSize = true, Location = new Point(20, y + 73) };
        var call = Box(_st.Call, 200); call.Location = new Point(180, y + 70); call.CharacterCasing = CharacterCasing.Upper;
        var l2 = new Label { Text = Lang.T("u.ask_call_loc"), ForeColor = C(_th.Muted), AutoSize = true, Location = new Point(20, y + 108) };
        var loc = Box(_st.Loc, 200); loc.Location = new Point(180, y + 105); loc.CharacterCasing = CharacterCasing.Upper; loc.MaxLength = 10;
        var hint = new Label { Text = Lang.T("u.disc_hint"), ForeColor = C(_th.Muted), AutoSize = false, Bounds = new Rectangle(20, y + 138, 520, 34) };
        var err = new Label { ForeColor = C(_th.Bad), AutoSize = true, Location = new Point(20, y + 174) };
        var ok = Btn(Lang.T("u.disc_read"), null, true); ok.AutoSize = true; ok.MinimumSize = new Size(140, 30); ok.Location = new Point(390, y + 196);
        var later = Btn(Lang.T("u.later"), null, false); later.Size = new Size(110, 30); later.Location = new Point(270, y + 196); later.DialogResult = DialogResult.Cancel;
        dlg.ClientSize = new Size(560, y + 240);
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
        dlg.Controls.AddRange(new Control[] { title, text, l1, call, l2, loc, hint, err, ok, later });
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
        new Font(mono ? MonoFamily : Font.FontFamily, (float)(size * UiScale.F), (bold ? FontStyle.Bold : FontStyle.Regular) | (italic ? FontStyle.Italic : FontStyle.Regular));

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
        c.CheckedChanged += (_, _) => { changed(c.Checked); try { _st.Save(); } catch { } };
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

    /// <summary>Fascia che va a capo, ancorata in alto, alta quanto serve alla larghezza attuale.</summary>
    static FlowLayoutPanel Flow(Color back)
    {
        var f = new FlowLayoutPanel { Dock = DockStyle.Top, WrapContents = true, FlowDirection = FlowDirection.LeftToRight, BackColor = back, Padding = new Padding(8, 4, 8, 4), Height = 60 };
        bool busy = false;
        void Fit()
        {
            if (busy || f.Width < 50) return;
            busy = true;
            try { int h = f.GetPreferredSize(new Size(f.Width, 0)).Height; if (h > 10 && Math.Abs(f.Height - h) > 1) f.Height = h; }
            finally { busy = false; }
        }
        f.SizeChanged += (_, _) => Fit();
        f.ControlAdded += (_, _) => Fit();
        f.Layout += (_, _) => Fit();
        return f;
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
        if (c is FlowLayoutPanel || c is TableLayoutPanel || c is Panel || (c is TextBox tb && tb.Multiline)) c.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        else c.Anchor = AnchorStyles.Left | AnchorStyles.Top;
        t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        t.Controls.Add(c, 0, t.RowCount++);
    }

    /// <summary>
    /// Pagina che scorre in verticale. La larghezza del contenuto tiene sempre conto della barra di scorrimento, così
    /// il testo che va a capo non cambia altezza quando la barra compare o sparisce (niente impaginazioni a ripetizione).
    /// </summary>
    static Panel ScrollBox(Control inner, Color back)
    {
        var p = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = back };
        inner.Dock = DockStyle.None;
        if (inner is TableLayoutPanel t) { t.AutoSize = false; }
        inner.Location = new Point(0, 0);
        p.Controls.Add(inner);
        bool busy = false;
        void Fit()
        {
            if (busy || p.Width < 50) return;
            busy = true;
            try
            {
                int w = Math.Max(200, p.Width - SystemInformation.VerticalScrollBarWidth - 2);
                if (inner.Width != w) inner.Width = w;
                int h = inner.GetPreferredSize(new Size(w, 0)).Height;
                if (h > 0 && Math.Abs(inner.Height - h) > 1) inner.Height = h;
            }
            finally { busy = false; }
        }
        p.SizeChanged += (_, _) => Fit();
        inner.Layout += (_, _) => { if (!busy) p.BeginInvokeSafe(Fit); };
        p.VisibleChanged += (_, _) => { if (p.Visible) Fit(); };
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
        _tabContent["fc"] = BuildForecastTab();
        _tabContent["graves"] = BuildGravesTab();
        _tabContent["sus"] = BuildSuspectsTab();
        _tabContent["collect"] = BuildCollectTab();
        _tabContent["doc1"] = BuildDoc("funzionamento.md", "LIVETROPOVHF - Funzionamento del programma");
        _tabContent["doc2"] = BuildDoc("mappa.md", "LIVETROPOVHF - Costruzione della mappa");
        _tabContent["doc3"] = BuildDoc("tropo.md", "LIVETROPOVHF - Come si forma la tropo");
        _tabContent["doc4"] = BuildDoc("previsioni.md", "LIVETROPOVHF - Manuale delle previsioni");
        _tabContent["doc5"] = BuildDoc("consigli.md", "LIVETROPOVHF - Consigli per l'operatore");
        _tabContent["doc6"] = BuildDoc("fonti.md", "LIVETROPOVHF - Fonti e formule");
        foreach (var kv in _tabContent) { kv.Value.Dock = DockStyle.Fill; kv.Value.Visible = false; _content.Controls.Add(kv.Value); }

        Controls.Add(_content);
        var band = BuildTabBand();
        Controls.Add(_subBar);
        Controls.Add(band);
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
        _locBox = Box(_st.Loc, 120); _locBox.CharacterCasing = CharacterCasing.Upper; _locBox.MaxLength = 10;
        _locBox.TextChanged += (_, _) =>
        {
            _st.Loc = (_locBox.Text ?? "").ToUpperInvariant();
            _locBox.BackColor = _st.Loc.Length > 0 && !Locator.IsValid(_st.Loc) ? C("#F8D7D7") : C(_th.CtlBg);
            _st.Dirty = true;
            _map.InvalidateVisual();
            FcSyncLoc();
        };
        left.Controls.Add(_locBox);
        bar.Controls.Add(left);

        var right = Row(); right.Dock = DockStyle.Right; right.WrapContents = false; right.Padding = new Padding(0, 6, 8, 0);
        _clock = Lbl("", 15f, _th.Clock, true, true); _clock.Margin = new Padding(3, 6, 6, 0);
        _ntp = Lbl("NTP", 15f, _th.Muted, true, true); _ntp.Margin = new Padding(3, 6, 12, 0);
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
        // scala dei testi 80–150% (passi di 10): si applica quando il cursore si ferma
        var scLbl = Lbl($"{UiScale.Percent}%", 8.5f, _th.Muted); scLbl.Margin = new Padding(0, 12, 6, 0);
        var sc = new TrackBar { Minimum = 8, Maximum = 15, Value = Math.Max(8, Math.Min(15, UiScale.Percent / 10)), TickStyle = TickStyle.None, Width = 100, AutoSize = false, Height = 26, BackColor = C(_th.Panel), Margin = new Padding(3, 8, 0, 0) };
        var scTimer = new System.Windows.Forms.Timer { Interval = 700 };
        scTimer.Tick += (_, _) =>
        {
            scTimer.Stop();
            int v = sc.Value * 10;
            if (v == UiScale.Percent) return;
            UiScale.Percent = v; _st.S.SetInt("ui_scale", v); _st.Dirty = true;
            BeginInvoke(new Action(BuildUi));
        };
        sc.ValueChanged += (_, _) => { scLbl.Text = $"{sc.Value * 10}%"; scTimer.Stop(); scTimer.Start(); };
        new ToolTip().SetToolTip(sc, L("Dimensione dei testi (80–150%)", "Text size (80–150%)"));
        right.Controls.Add(sc); right.Controls.Add(scLbl);
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

    // ---------------------------------------------------------------- linguette in 5 gruppi con sotto-linguette

    static readonly (string id, string[] items)[] Groups =
    {
        ("map", new[] { "map" }),
        ("prev", new[] { "fc", "hepburn", "f5len" }),
        ("graves", new[] { "graves" }),
        ("data", new[] { "collect:dx", "collect:psk", "collect:rbn", "collect:udp", "collect:calls", "sus" }),
        ("guide", new[] { "doc1", "doc2", "doc3", "doc4", "doc5", "doc6" }),
    };
    readonly Dictionary<string, Button> _subTabButtons = new();
    FlowLayoutPanel _subBar;
    string _shownGroup = "";

    static string GroupOf(string item) => Groups.First(g => g.items.Contains(item) || g.items.Any(x => x.StartsWith(item + ":"))).id;
    string CurrentItem => _tab == "collect" ? "collect:" + _subTab : _tab;

    string GroupLabel(string g)
    {
        bool it = Lang.Current == "it";
        switch (g)
        {
            case "map": return Lang.T("tab.map");
            case "prev": return Lang.Tr("Previsioni", "Forecasts");
            case "graves": return Lang.T("tab.graves");
            case "data": return Lang.Tr("Dati e fonti", "Data & sources");
            default: return Lang.Tr("Guide", "Guides");
        }
    }

    string ItemLabel(string item)
    {
        bool it = Lang.Current == "it";
        switch (item)
        {
            case "fc": return Lang.Tr("Propria (β)", "Own (β)");
            case "hepburn": return Lang.T("tab.hep");
            case "f5len": return Lang.T("tab.f5");
            case "collect:dx": return "DX cluster";
            case "collect:psk": return "PSKReporter";
            case "collect:rbn": return "Reverse Beacon";
            case "collect:udp": return "UDP (WSJT-X, JTDX, MSHV)";
            case "collect:calls": return Lang.T("c.tab");
            case "sus": return Lang.T("tab.sus");
            case "doc4": return Lang.Tr("Manuale previsioni", "Forecast manual");
            case "doc5": return Lang.Tr("Consigli all'operatore", "Operator tips");
            case "doc6": return Lang.Tr("Fonti e formule", "Sources and formulas");
            default: return Lang.T("tab." + item);
        }
    }

    void GoTo(string tab, string sub = null)
    {
        if (tab == "collect" && sub != null) ShowSubTab(sub);
        ShowTab(tab);
    }

    void GoToItem(string item)
    {
        var i = item.IndexOf(':');
        if (i > 0) GoTo(item.Substring(0, i), item.Substring(i + 1)); else GoTo(item);
    }

    Control BuildTabBand()
    {
        var row = Flow(C(_th.Band)); row.Padding = new Padding(8, 6, 0, 0);
        foreach (var (g, items) in Groups)
        {
            var b = new Button { Text = GroupLabel(g), AutoSize = true, FlatStyle = FlatStyle.Flat, Height = 36, Padding = new Padding(6, 0, 6, 0), Margin = new Padding(0, 0, 2, 0), Font = F(9.5f), Cursor = Cursors.Hand };
            b.FlatAppearance.BorderSize = 0;
            var gg = g; var its = items;
            // ogni gruppo riapre l'ultima sotto-linguetta usata
            b.Click += (_, _) => { var last = _st.S.Get("grp_" + gg, its[0]); GoToItem(its.Contains(last) ? last : its[0]); };
            _tabButtons[g] = b;
            row.Controls.Add(b);
        }
        // barra delle sotto-linguette: la aggiunge BuildUi subito sotto il fascione
        _subBar = Flow(C(_th.Page)); _subBar.Padding = new Padding(8, 4, 8, 4);
        _shownGroup = "";
        return row;
    }

    void RefreshSubBar()
    {
        if (_subBar == null || _subBar.IsDisposed) return;
        var g = GroupOf(CurrentItem);
        var items = Groups.First(x => x.id == g).items;
        if (g != _shownGroup)
        {
            _shownGroup = g;
            _subBar.SuspendLayout();
            _subBar.Controls.Clear(); _subTabButtons.Clear();
            foreach (var it in items)
            {
                var target = it;
                var b = Btn(ItemLabel(it), () => GoToItem(target), false);
                b.Padding = new Padding(10, 3, 10, 3);
                _subTabButtons[it] = b;
                _subBar.Controls.Add(b);
            }
            _subBar.ResumeLayout(true);
        }
        _subBar.Visible = items.Length > 1;
        foreach (var kv in _subTabButtons)
        {
            bool on = kv.Key == CurrentItem;
            kv.Value.BackColor = on ? C(_th.Accent) : C(_th.Chip);
            kv.Value.ForeColor = on ? C(_th.AccentFg) : C(_th.Text2);
            kv.Value.Font = F(9.5f, on);
        }
        _st.S.Set("grp_" + g, CurrentItem);
    }

    void ShowTab(string key)
    {
        _tab = key;
        ErrorLog.CurrentTab = key;
        foreach (var kv in _tabContent) kv.Value.Visible = kv.Key == key;
        var grp = GroupOf(key == "collect" ? "collect:" + _subTab : key);
        foreach (var kv in _tabButtons)
        {
            bool on = kv.Key == grp;
            kv.Value.BackColor = on ? C(_th.Page) : C(_th.Band);
            kv.Value.ForeColor = on ? C(_th.Text) : C(_th.BandFg);
            kv.Value.FlatAppearance.MouseOverBackColor = on ? C(_th.Page) : ControlPaint.Light(C(_th.Band), 0.15f);
            kv.Value.Font = F(9.5f, on);
        }
        if (key == "hepburn" || key == "f5len") FillTropoTab(key);
        RefreshSubBar();
        RefreshLogs(true);
    }

    readonly Dictionary<string, Label> _statusSegs = new();
    ToolTip _statusTips;
    string _lastErrorNote = ""; DateTime _lastErrorAt;

    /// <summary>Barra di stato a segmenti: le fonti sono cliccabili (portano alla fonte) e il fumetto dice perché sono spente.</summary>
    Control BuildStatusBar()
    {
        _statusSegs.Clear();
        _statusTips = new ToolTip { AutoPopDelay = 30000, InitialDelay = 300 };
        var p = new Panel { Dock = DockStyle.Bottom, Height = 26, BackColor = C(_th.Bar) };
        var flow = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, FlowDirection = FlowDirection.LeftToRight, BackColor = C(_th.Bar), Padding = new Padding(8, 4, 0, 0) };
        Label Seg(string id, Action click)
        {
            var l = new Label { AutoSize = true, ForeColor = C(_th.Muted), BackColor = Color.Transparent, Margin = new Padding(0, 0, 16, 0) };
            if (click != null) { l.Cursor = Cursors.Hand; l.Click += (_, _) => click(); }
            _statusSegs[id] = l; flow.Controls.Add(l);
            return l;
        }
        foreach (var f in _st.Feeds)
        {
            var sub = f.Src == "RBN" ? "rbn" : f.Src == "PSK" ? "psk" : "dx";
            Seg("feed:" + f.Id, () => GoTo("collect", sub));
        }
        Seg("mqtt", () => GoTo("collect", "psk"));
        Seg("udp", () => GoTo("collect", "udp"));
        Seg("pressure", null); Seg("tropo", null); Seg("call3", () => GoTo("collect", "calls")); Seg("other", null);
        Seg("error", () => { try { System.Diagnostics.Process.Start(ErrorLog.FilePath); } catch { } });
        Seg("version", () => Updater.CheckNow());
        _status = _statusSegs["version"];
        p.Controls.Add(flow);
        p.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 1, BackColor = C(_th.Line) });
        return p;
    }

    void RefreshStatus()
    {
        if (_statusSegs.Count == 0) return;
        bool it = Lang.Current == "it";
        string Dot(bool ok) => ok ? "●" : "○";
        void Set(string id, string text, AM.Color col, string tip = null)
        {
            if (!_statusSegs.TryGetValue(id, out var l) || l.IsDisposed) return;
            if (l.Text != text) l.Text = text;
            l.ForeColor = C(col); l.Visible = text != "";
            if (tip != null && _statusTips.GetToolTip(l) != tip) _statusTips.SetToolTip(l, tip);
        }
        string Why(string err)
        {
            if (string.IsNullOrEmpty(err)) return Lang.Tr("non collegato (spento o in attesa)", "not connected (off or waiting)");
            var low = err.ToLowerInvariant();
            if (low.Contains("rifiut") || low.Contains("refused")) return err + (Lang.Tr("\r\nIl server rifiuta la connessione: porta sbagliata o servizio spento. Controlla host e porta.", "\r\nThe server refuses the connection: wrong port or service down."));
            if (low.Contains("nodo del cluster fermo")) return err + (Lang.Tr("\r\nNon dipende dal programma: il nodo è fermo dal lato del server. Intanto usa un altro cluster.", "\r\nNot a program fault: the node is down server-side."));
            if (low.Contains("timeout") || low.Contains("tempo")) return err + (Lang.Tr("\r\nNessuna risposta: rete lenta, firewall o server occupato.", "\r\nNo answer: slow network, firewall or busy server."));
            return err;
        }
        string Click = Lang.Tr("\r\nClic: apri la fonte", "\r\nClick: open the source");
        foreach (var f in _st.Feeds)
            Set("feed:" + f.Id, $"{Dot(f.Connected)} {f.Src}", f.Connected ? _th.Ok : f.LastError != "" ? _th.Bad : _th.Muted, $"{f.Host}:{f.Port}\r\n" + (f.Connected ? (Lang.Tr("collegato", "connected")) : Why(f.LastError)) + Click);
        var q = _st.Mqtt;
        Set("mqtt", $"{Dot(q.Connected)} PSKReporter MQTT", q.Connected ? _th.Ok : q.LastError != "" ? _th.Bad : _th.Muted, $"{q.Host}:{q.Port}\r\n" + (q.Connected ? (Lang.Tr("collegato", "connected")) : Why(q.LastError)) + Click);
        var u = _st.Udp;
        Set("udp", $"{Dot(u.Running)} UDP {u.ListenPort}→{u.ForwardPort}", u.Running ? (u.Packets > 0 ? _th.Ok : _th.Muted) : u.LastError != "" ? _th.Bad : _th.Muted,
            (u.Running ? (u.Packets > 0 ? (Lang.TrF("in ascolto, {0} pacchetti", "listening, {0} packets", u.Packets)) : (Lang.Tr("in ascolto, ma WSJT-X / JTDX / MSHV non mandano ancora nulla: controlla Impostazioni → Reporting → UDP Server", "listening, but nothing received yet: check Settings → Reporting → UDP Server"))) : Why(u.LastError)) + Click);
        Set("pressure", _st.Pressure.Status, _th.Muted);
        Set("tropo", _st.Tropo.Status, _th.Muted);
        bool callBad = _st.CallDb.Ready && (_st.CallDb.Error != "" || _st.CallDb.Count == 0);
        Set("call3", _st.CallDb.Ready ? (_st.CallDb.Error != "" ? "⚠ " + _st.CallDb.Error : $"CALL3: {_st.CallDb.Count:N0}") : "CALL3: …", callBad ? _th.Bad : _th.Muted);
        var other = new List<string>();
        if (!_st.Map.Loaded && _st.Map.Status != "") other.Add(_st.Map.Status);
        if (_st.Archive.Error != "") other.Add("DB: " + _st.Archive.Error);
        Set("other", string.Join("   ·   ", other), _th.Muted);
        bool recent = _lastErrorNote != "" && (DateTime.Now - _lastErrorAt).TotalMinutes < 15;
        Set("error", recent ? (Lang.Tr("⚠ errore annotato", "⚠ error logged")) : "", _th.Bad, recent ? $"{_lastErrorAt:HH:mm:ss} {_lastErrorNote}\r\n" + (Lang.Tr("Il programma continua. Clic: apri il registro degli errori", "The program goes on. Click: open the error log")) : null);
        Set("version", "v" + Program.Version + " (Windows 7)" + (Updater.Status != "" ? " · " + Updater.Status : ""), _th.Muted, Lang.Tr("Clic: cerca aggiornamenti adesso", "Click: check for updates now"));
    }

    void AskSendErrorReport()
    {
        bool it = Lang.Current == "it";
        var summary = ErrorLog.PendingSummary.Replace("\n", " · ");
        ErrorLog.ClearPending();
        var r = MessageBox.Show(this, Lang.TrF("L'ultima volta il programma ha annotato un errore ({0}).\r\n\r\nVuoi inviare il rapporto agli autori? Si apre il browser su una segnalazione GitHub già compilata, con nominativo, locatore e nome utente del PC tolti: controlli il testo e premi Invia (serve un account GitHub gratuito).\r\n\r\nIl registro completo è in {1}.", "Last time the program logged an error ({0}).\r\n\r\nSend the report to the authors? The browser opens a prefilled GitHub issue with call, locator and PC user name removed (a free GitHub account is needed).\r\n\r\nThe full log is in {1}.", summary, ErrorLog.FilePath),
            Lang.Tr("Rapporto di errore", "Error report"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        if (r == DialogResult.Yes) try { System.Diagnostics.Process.Start(ErrorLog.IssueUrl(_st.Call, _st.Loc)); } catch (Exception ex) { ErrorLog.Write(ex, "apertura del browser"); }
    }

    bool _restartAsked;
    void AskRestart(string version, bool mandatory)
    {
        if (_restartAsked || IsDisposed) return;
        _restartAsked = true;
        bool it = Lang.Current == "it";
        bool restart;
        if (mandatory)
        {
            MessageBox.Show(this, Lang.TrF("È stata installata la versione {0}, una correzione urgente. Il programma si riavvia per usarla: impostazioni e dati restano.", "Version {0}, an urgent fix, has been installed. The program restarts to use it: settings and data are kept.", version),
                Lang.Tr("Aggiornamento", "Update"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            restart = true;
        }
        else
            restart = MessageBox.Show(this, Lang.TrF("È stata installata la versione {0}. Sarà attiva dal prossimo avvio: vuoi riavviare adesso? Impostazioni e dati restano.", "Version {0} has been installed and will be active at the next start: restart now? Settings and data are kept.", version),
                Lang.Tr("Aggiornamento", "Update"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        if (restart && Updater.StartNewInstance()) { _closeConfirmed = true; Close(); }
    }

    // ================================================================ aggiornamento periodico

    void Tick()
    {
        try { FcAutoCheck(); } catch { }
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

        RefreshStatus();

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
