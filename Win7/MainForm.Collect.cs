using System.Drawing;
using System.Windows.Forms;
using TropoVHF.Core;
using TropoVHF.Ui;
using AM = Avalonia.Media;

namespace TropoVHF.Win7;

public sealed partial class MainForm
{
    string _subTab = "dx";
    readonly Dictionary<string, Control> _subContent = new();
    readonly Dictionary<string, Button> _subButtons = new();
    readonly Dictionary<string, (Label status, Label counters, TextBox log)> _sections = new();
    TextBox _udpLog; Label _udpStatus, _udpInfo; CheckBox _udpShowHb;
    Label _mmmStatus, _callsStatus, _callsMsg; TableLayoutPanel _callsList;

    Control BuildCollectTab()
    {
        _subContent.Clear(); _subButtons.Clear();
        var root = new Panel { BackColor = C(_th.Page) };
        var bar = Row(); bar.Dock = DockStyle.Top; bar.WrapContents = false; bar.Padding = new Padding(8, 8, 8, 6); bar.BackColor = C(_th.Page);
        foreach (var (key, label) in new[] { ("dx", "DX cluster"), ("psk", "PSKReporter"), ("rbn", "Reverse Beacon"), ("udp", "UDP (WSJT-X, JTDX, MSHV)"), ("calls", Lang.T("c.tab")) })
        {
            var k = key;
            var b = Btn(label, () => ShowSubTab(k), false);
            b.Padding = new Padding(10, 3, 10, 3);
            _subButtons[key] = b;
            bar.Controls.Add(b);
        }

        var f = _st.Feeds.ToDictionary(x => x.Id);
        Control Page(params Control[] parts)
        {
            var st = Stack(new Padding(6, 10, 6, 10));
            foreach (var p in parts) Add(st, p);
            return Scroll(st, C(_th.Page));
        }
        _subContent["dx"] = Page(
            FeedSection(f["dxc"], Lang.T("u.1_dx_cluster_of"), Lang.T("u.on_on0dxk_the_vhf"), 0),
            FeedSection(f["dxc2"], Lang.T("u.2_second_dx_cluster"), Lang.T("u.gb7rau_is_a_vhfuhffriendly"), 1),
            FeedSection(f["dxc3"], Lang.T("u.3_second_dx_cluster"), Lang.T("u.s50clx_help"), 3));
        _subContent["psk"] = Page(
            FeedSection(f["psk"], Lang.T("u.pskreporter_cluster_dm4x"), Lang.T("u.cluster_that_forwards_pskreporter"), 2),
            MqttSection());
        _subContent["rbn"] = Page(FeedSection(f["rbn"], "Reverse Beacon Network", Lang.T("u.rbn_port_7000_cw"), 1));
        _subContent["udp"] = Page(UdpSection());
        _subContent["calls"] = Page(CallsSection());
        var content = new Panel { Dock = DockStyle.Fill };
        foreach (var kv in _subContent) { kv.Value.Dock = DockStyle.Fill; content.Controls.Add(kv.Value); }
        root.Controls.Add(content);
        root.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 1, BackColor = C(_th.Line) });
        root.Controls.Add(bar);
        ShowSubTab(_subTab);
        return root;
    }

    void ShowSubTab(string key)
    {
        _subTab = key;
        foreach (var kv in _subContent) kv.Value.Visible = kv.Key == key;
        foreach (var kv in _subButtons)
        {
            bool on = kv.Key == key;
            kv.Value.BackColor = on ? C(_th.Accent) : C(_th.Chip);
            kv.Value.ForeColor = on ? C(_th.AccentFg) : C(_th.Text2);
            kv.Value.Font = F(9.5f, on);
        }
        RefreshLogs(true);
    }

    TextBox LogBox(int height) => new()
    {
        Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Both, WordWrap = false, Height = height,
        Font = new Font(MonoFamily, 9f), BackColor = C(_th.Panel), ForeColor = C(_th.Text), BorderStyle = BorderStyle.None, Margin = new Padding(0, 4, 0, 4)
    };

    FlowLayoutPanel Labeled(string label, Control c)
    {
        var r = Row(); r.WrapContents = false; r.Margin = new Padding(0, 0, 10, 2);
        r.Controls.Add(Lbl(label, 9.5f, _th.Muted));
        r.Controls.Add(c);
        return r;
    }

    Control FeedSection(TelnetFeed f, string title, string help, int colorIdx)
    {
        var (sec, body) = Section(title, colorIdx);
        var row = Row();
        var host = Box(f.Host, 220); host.TextChanged += (_, _) => f.Host = (host.Text ?? "").Trim();
        row.Controls.Add(Labeled(Lang.T("host"), host));
        row.Controls.Add(Labeled(Lang.T("port"), Port(f.Port, v => f.Port = v)));
        var login = Box(f.Login, 120); login.TextChanged += (_, _) => f.Login = (login.Text ?? "").Trim();
        row.Controls.Add(Labeled(Lang.T("login"), login));
        var pass = Box(f.Password, 120); pass.UseSystemPasswordChar = true; pass.TextChanged += (_, _) => f.Password = pass.Text ?? "";
        row.Controls.Add(Labeled(Lang.T("u.password"), pass));
        var cmds = Box(f.Cmds, 300); cmds.TextChanged += (_, _) => f.Cmds = cmds.Text ?? "";
        new ToolTip().SetToolTip(cmds, Lang.T("u.commands_after_login_separated"));
        row.Controls.Add(Labeled(Lang.T("cmds"), cmds));
        Add(body, row);

        var actions = Row();
        actions.Controls.Add(Btn(Lang.T("connect"), () =>
        {
            if (!_st.HasValidCall) { AskCall(); if (!_st.HasValidCall) return; }
            if (string.IsNullOrWhiteSpace(f.Login)) f.Login = _st.LoginOrCall;
            f.Connect(_st.LoginOrCall);
        }, true));
        actions.Controls.Add(Btn(Lang.T("disconnect"), () => f.Disconnect(), false));
        actions.Controls.Add(Chk(Lang.T("recon"), f.Reconnect, v => f.Reconnect = v));
        actions.Controls.Add(Chk(Lang.T("autostart"), f.AutoStart, v => f.AutoStart = v));
        Add(body, actions);
        var status = Lbl("", 10f, _th.Muted);
        var counters = Lbl("", 9.5f, _th.Muted);
        Add(body, status); Add(body, counters);
        Add(body, WrapLbl(help, 9f, _th.Muted));
        var log = LogBox(240);
        Add(body, log);
        var send = Row();
        var line = Box("", 300);
        new ToolTip().SetToolTip(line, Lang.T("cmdhint"));
        void Send() { var t = (line.Text ?? "").Trim(); if (t.Length > 0) { f.Send(t); line.Text = ""; } }
        line.KeyDown += (_, e) => { if (e.KeyCode == Keys.Enter) { Send(); e.SuppressKeyPress = true; } };
        send.Controls.Add(line);
        send.Controls.Add(Btn(Lang.T("send"), Send, false));
        Add(body, send);
        _sections[f.Id] = (status, counters, log);
        return sec;
    }

    Control MqttSection()
    {
        var m = _st.Mqtt;
        var (sec, body) = Section(Lang.T("u.pskreporter_mqtt_who_is"), 3);
        var row = Row();
        var host = Box(m.Host, 220); host.TextChanged += (_, _) => m.Host = (host.Text ?? "").Trim();
        row.Controls.Add(Labeled(Lang.T("host"), host));
        var port = Port(m.Port, v => m.Port = v);
        row.Controls.Add(Labeled(Lang.T("port"), port));
        row.Controls.Add(Chk("TLS (1884)", m.UseTls, v => { m.UseTls = v; port.Value = v ? 1884 : 1883; }));
        foreach (var b in new[] { "144", "222", "432" })
        {
            var bb = b;
            row.Controls.Add(Chk(b + " MHz", m.EnabledBands.Contains(b), v => { if (v) m.EnabledBands.Add(bb); else m.EnabledBands.Remove(bb); }));
        }
        Add(body, row);
        var names = Box(string.Join(";", m.BandTopics.Select(kv => kv.Key + "=" + kv.Value)), 240);
        names.TextChanged += (_, _) =>
        {
            var d = new Dictionary<string, string>();
            foreach (var kv in (names.Text ?? "").Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)) { var p = kv.Split('='); if (p.Length == 2) d[p[0].Trim()] = p[1].Trim(); }
            if (d.Count > 0) m.BandTopics = d;
        };
        var r2 = Row();
        r2.Controls.Add(Labeled(Lang.T("u.topic_bands"), names));
        r2.Controls.Add(Lbl("pskr/filter/v2/{banda}/#", 9f, _th.Sub, false, true));
        Add(body, r2);
        var actions = Row();
        actions.Controls.Add(Btn(Lang.T("connect"), () => m.Connect(), true));
        actions.Controls.Add(Btn(Lang.T("disconnect"), () => m.Disconnect(), false));
        actions.Controls.Add(Chk(Lang.T("recon"), m.Reconnect, v => m.Reconnect = v));
        actions.Controls.Add(Chk(Lang.T("autostart"), m.AutoStart, v => m.AutoStart = v));
        Add(body, actions);
        var status = Lbl("", 10f, _th.Muted);
        var counters = Lbl("", 9.5f, _th.Muted);
        Add(body, status); Add(body, counters);
        Add(body, WrapLbl(Lang.T("u.realtime_reception_reports_locators"), 9f, _th.Muted));
        var log = LogBox(240);
        Add(body, log);
        _sections["mqtt"] = (status, counters, log);
        return sec;
    }

    Control UdpSection()
    {
        var u = _st.Udp;
        var (sec, body) = Section(Lang.T("u.decodes_from_wsjtx_jtdx"), 0);
        var r1 = Row();
        r1.Controls.Add(Lbl(Lang.T("u.listen"), 10f, _th.Text, true));
        var inA = Box(u.ListenAddress, 150); inA.TextChanged += (_, _) => u.ListenAddress = (inA.Text ?? "").Trim();
        r1.Controls.Add(Labeled(Lang.T("u.address"), inA));
        r1.Controls.Add(Labeled(Lang.T("port"), Port(u.ListenPort, v => u.ListenPort = v)));
        r1.Controls.Add(Lbl("127.0.0.1 = " + Lang.T("u.same_computer") + " · 224.0.0.1 = multicast", 9f, _th.Muted));
        Add(body, r1);
        var r2 = Row();
        r2.Controls.Add(Lbl(Lang.T("u.forward"), 10f, _th.Text, true));
        var outA = Box(u.ForwardAddress, 150); outA.TextChanged += (_, _) => u.ForwardAddress = (outA.Text ?? "").Trim();
        r2.Controls.Add(Labeled(Lang.T("u.address"), outA));
        r2.Controls.Add(Labeled(Lang.T("port"), Port(u.ForwardPort, v => u.ForwardPort = v)));
        r2.Controls.Add(Chk(Lang.T("u.repeat_the_same_content"), u.Forward, v => u.Forward = v));
        Add(body, r2);
        var r3 = Row();
        r3.Controls.Add(Btn(Lang.T("u.start_listening"), () => u.Start(), true));
        r3.Controls.Add(Btn(Lang.T("u.stop"), () => u.Stop(), false));
        r3.Controls.Add(Chk(Lang.T("u.start_at_startup"), u.AutoStart, v => u.AutoStart = v));
        _udpStatus = Lbl("", 10f, _th.Muted);
        r3.Controls.Add(_udpStatus);
        Add(body, r3);
        _udpInfo = Lbl("", 9.5f, _th.Text, false, true);
        Add(body, _udpInfo);
        Add(body, WrapLbl(Lang.T("u.each_decoded_station_is"), 9f, _th.Muted));
        var dbg = Row();
        dbg.Controls.Add(Lbl(Lang.T("u.debug_received_text"), 10f, _th.Text, true));
        _udpShowHb = Chk(Lang.T("u.show_heartbeat_and_status"), true, _ => RefreshLogs(true));
        dbg.Controls.Add(_udpShowHb);
        Add(body, dbg);
        _udpLog = LogBox(360);
        Add(body, _udpLog);
        return sec;
    }

    /// <summary>
    /// Nominativi: modifiche a CALL3.TXT senza toccarlo. Voci a mano (Dati/CALL3-MANUALE.TXT, valgono sopra tutto) e
    /// file aggiunti che si sovrappongono (Dati/CALL3-AGGIUNTE/); aggiornamento da MMMonVHF; ultima fonte G7VRD.
    /// </summary>
    Control CallsSection()
    {
        var db = _st.CallDb;
        var (sec, body) = Section(Lang.T("c.title"), 3);
        Add(body, WrapLbl(Lang.T("c.help"), 9.5f, _th.Sub));
        _callsStatus = WrapLbl("", 9f, _th.Muted, false, true);
        Add(body, _callsStatus);

        var call = Box("", 150); call.CharacterCasing = CharacterCasing.Upper;
        var loc = Box("", 100); loc.CharacterCasing = CharacterCasing.Upper; loc.MaxLength = 6;
        _callsMsg = Lbl("", 9f, _th.Sub, false, true);
        _callsList = Stack(new Padding(0)); _callsList.Dock = DockStyle.None;
        void Find()
        {
            var c = (call.Text ?? "").Trim().ToUpperInvariant();
            if (c == "") return;
            var m = db.ManualOf(c); var any = db.Lookup(c); var lv = LearnedLoc.Get(c);
            _callsMsg.Text = Lang.F("c.found", c, m != "" ? m + " (" + Lang.T("c.src_manual") + ")" : any != "" ? any + " (CALL3)" : lv != "" ? lv + " (LOC6)" : "—");
            if (string.IsNullOrEmpty(loc.Text)) loc.Text = m != "" ? m : any != "" ? any : lv;
        }
        var row1 = Row();
        row1.Controls.Add(Labeled(Lang.T("call"), call));
        row1.Controls.Add(Labeled(Lang.T("loc"), loc));
        row1.Controls.Add(Btn(Lang.T("c.save"), () =>
        {
            var c = (call.Text ?? "").Trim().ToUpperInvariant(); var l = (loc.Text ?? "").Trim().ToUpperInvariant();
            if (c.Length < 3 || !Locator.IsValid(l)) { _callsMsg.Text = Lang.T("c.invalid"); return; }
            db.SetManual(c, l); CallsChanged(Lang.F("c.saved", c, l)); call.Text = ""; loc.Text = "";
        }, true));
        row1.Controls.Add(Btn(Lang.T("c.find"), Find, false));
        call.KeyDown += (_, e) => { if (e.KeyCode == Keys.Enter) { Find(); e.SuppressKeyPress = true; } };
        Add(body, row1);
        Add(body, _callsMsg);
        Add(body, Lbl(Lang.T("c.manual_list"), 10f, _th.Text, true));
        Add(body, _callsList);

        Add(body, Lbl(Lang.T("c.files"), 10f, _th.Text, true));
        var r2 = Row();
        r2.Controls.Add(Btn(Lang.T("c.import"), () =>
        {
            using var dlg = new OpenFileDialog { Title = Lang.T("c.import"), Filter = "*.txt;*.csv|*.txt;*.csv|*.*|*.*" };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            try { int n = db.Import(dlg.FileName); CallsChanged(Lang.F("c.imported", Path.GetFileName(dlg.FileName), n)); }
            catch (Exception ex) { _callsMsg.Text = ex.Message; }
        }, true));
        r2.Controls.Add(Btn(Lang.T("c.reload"), () => { db.LoadLayers(); CallsChanged(Lang.T("c.reloaded")); }, false));
        Add(body, r2);

        Add(body, Lbl(Lang.T("c.mmm"), 10f, _th.Text, true));
        var r3 = Row();
        r3.Controls.Add(Btn(Lang.T("c.mmm_now"), () => { _ = _st.UpdateCall3Async(); }, true));
        r3.Controls.Add(Chk(Lang.T("c.mmm_auto"), _st.Call3Auto, v => _st.Call3Auto = v));
        Add(body, r3);
        _mmmStatus = WrapLbl(_st.Call3Status != "" ? _st.Call3Status : _st.Call3Last == DateTime.MinValue ? Lang.T("c.mmm_never") : Lang.F("c.mmm_last", _st.Call3Last.ToLocalTime().ToString("dd/MM/yyyy HH:mm")), 9f, _th.Sub);
        Add(body, _mmmStatus);
        Add(body, Lbl(AppState.Call3Url, 8.5f, _th.Muted, false, true));
        Add(body, Chk(Lang.T("c.g7"), G7Lookup.Enabled, v => { G7Lookup.Enabled = v; _st.S.Set("g7vrd", v ? "1" : "0"); _st.Dirty = true; }));
        Add(body, WrapLbl(Lang.T("c.g7_help"), 8.5f, _th.Muted));
        FillCalls();
        return sec;
    }

    void CallsChanged(string m)
    {
        if (_callsMsg != null && !_callsMsg.IsDisposed) _callsMsg.Text = m;
        FillCalls(); _st.Dirty = true; _map.InvalidateVisual(); RefreshSpotList();
    }

    void FillCalls()
    {
        if (_callsList == null || _callsList.IsDisposed) return;
        var db = _st.CallDb;
        _callsStatus.Text = Lang.F("c.status", db.PathUsed == "" ? "—" : db.PathUsed, db.Count.ToString("N0"),
            db.OverlayFiles.Count == 0 ? "—" : string.Join(", ", db.OverlayFiles), db.Manual().Count, CallDb.OverlayDir, CallDb.ManualPath)
            + "\n" + Lang.F("c.cluster", db.ClusterCount, CallDb.ClusterPath);
        _callsList.SuspendLayout();
        foreach (Control c in _callsList.Controls.Cast<Control>().ToList()) c.Dispose();
        _callsList.Controls.Clear(); _callsList.RowStyles.Clear(); _callsList.RowCount = 0;
        foreach (var (c, l) in db.Manual())
        {
            var row = Row(); row.WrapContents = false;
            row.Controls.Add(Lbl(c, 10.5f, _th.Text, true, true));
            row.Controls.Add(Lbl(l, 10.5f, _th.Accent, false, true));
            var cc = c;
            var del = Btn("✕ " + Lang.T("c.remove"), () => { db.SetManual(cc, ""); CallsChanged(Lang.F("c.removed", cc)); }, false);
            del.Font = F(8f);
            row.Controls.Add(del);
            Add(_callsList, row);
        }
        _callsList.ResumeLayout(true);
    }

    // ---------------------------------------------------------------- registri

    static void FillLog(TextBox box, IEnumerable<string> lines)
    {
        if (box == null || box.IsDisposed) return;
        var text = string.Join("\r\n", lines);
        if (box.Text == text) return;
        bool follow = !box.Focused;
        box.Text = text;
        if (follow) { box.SelectionStart = box.TextLength; box.ScrollToCaret(); }
    }

    void RefreshLogs(bool force)
    {
        if (!force && !_st.LogsDirty) return;
        _st.LogsDirty = false;
        if (_tab != "collect") return;
        foreach (var f in _st.Feeds)
        {
            if (!_sections.TryGetValue(f.Id, out var s) || s.status.IsDisposed) continue;
            s.status.Text = f.Connected ? string.Format(Lang.T("connected"), f.Host + ":" + f.Port) : (f.LastError != "" ? f.LastError : Lang.T("notconn"));
            s.status.ForeColor = C(f.Connected ? _th.Ok : f.LastError != "" ? _th.Bad : _th.Muted);
            s.counters.Text = Lang.F("u.feed_counters", f.Lines, f.Saved, f.OutBand, f.Excluded);
            if (!s.log.Visible) continue;
            List<(string text, LineClass cls)> copy;
            var l = _st.Logs[f.Id];
            lock (l) copy = l.Skip(Math.Max(0, l.Count - 150)).ToList();
            FillLog(s.log, copy.Where(x => x.cls != LineClass.OutOfBand && x.cls != LineClass.Excluded).Select(x => x.text));
        }
        if (_sections.TryGetValue("mqtt", out var m) && !m.status.IsDisposed)
        {
            var q = _st.Mqtt;
            m.status.Text = q.Connected ? string.Format(Lang.T("connected"), q.Host + ":" + q.Port) : (q.LastError != "" ? q.LastError : Lang.T("notconn"));
            m.status.ForeColor = C(q.Connected ? _th.Ok : q.LastError != "" ? _th.Bad : _th.Muted);
            var now = Clock.UtcNow;
            m.counters.Text = Lang.T("u.reports") + q.Reports + " · " + string.Join(" · ", q.EnabledBands.Select(b => $"{b}: {_st.Listeners.Get(b, 15, now).Count}"));
            if (m.log.Visible)
            {
                List<(string text, LineClass cls)> copy;
                var l = _st.Logs["mqtt"];
                lock (l) copy = l.Skip(Math.Max(0, l.Count - 150)).ToList();
                FillLog(m.log, copy.Select(x => x.text));
            }
        }
        if (_subTab == "udp" && _udpStatus != null && !_udpStatus.IsDisposed)
        {
            var u = _st.Udp;
            _udpStatus.Text = u.Running ? $"{Lang.T("u.listening_on")} {u.ListenAddress}:{u.ListenPort}" + (u.Forward ? $" · {Lang.T("u.forward_to")} {u.ForwardAddress}:{u.ForwardPort}" : "") : (u.LastError != "" ? u.LastError : Lang.T("u.stopped"));
            _udpStatus.ForeColor = C(u.Running ? _th.Ok : u.LastError != "" ? _th.Bad : _th.Muted);
            var dial = u.DialHz > 0 ? $"{u.DialHz / 1e6:0.000000} MHz {u.DialMode}" : "—";
            _udpInfo.Text = $"{(u.ProgramId == "" ? "—" : u.ProgramId + " " + u.ProgramVersion)}   ·   {dial}   ·   {Lang.T("u.packets")} {u.Packets}   ·   {Lang.T("u.forwarded")} {u.Forwarded}   ·   decode {u.Decodes}";
            List<(string text, bool decode)> copy;
            lock (_st.UdpLog) copy = _st.UdpLog.Skip(Math.Max(0, _st.UdpLog.Count - 200)).ToList();
            bool hb = _udpShowHb?.Checked == true;
            FillLog(_udpLog, copy.Where(x => hb || x.decode || !(x.text.Contains("Heartbeat") || x.text.Contains("Status"))).Select(x => x.text));
        }
    }
}
