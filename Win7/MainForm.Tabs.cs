using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TropoVHF.Core;
using TropoVHF.Ui;
using AM = Avalonia.Media;
using AC = Avalonia.Controls;

namespace TropoVHF.Win7;

public sealed partial class MainForm
{
    // ================================================================ linguette Hepburn e F5LEN: anteprima della fusione

    readonly Dictionary<string, Panel> _tropoHolders = new();
    readonly Dictionary<string, MapView> _previews = new();
    readonly Dictionary<string, int> _previewHours = new();
    DateTime _tropoShown = DateTime.MinValue;

    Control BuildTropoTab(string fonte)
    {
        var holder = new Panel { BackColor = C(_th.Page) };
        _tropoHolders[fonte] = holder;
        return holder;
    }

    TropoCalibration CalFor(string fonte)
    {
        var cal = _st.Tropo.Calibrations.FirstOrDefault(c => c.Fonte == fonte);
        if (cal == null)
        {
            _st.Tropo.LoadCalibrations();
            cal = _st.Tropo.Calibrations.FirstOrDefault(c => c.Fonte == fonte);
        }
        if (cal == null)
        {
            cal = new TropoCalibration { Fonte = fonte, Nome = fonte == "hepburn" ? "Hepburn – Europa" : "F5LEN – previsione mondiale", File = fonte == "hepburn" ? "hepburn-europa.json" : "f5len-mondo.json" };
            _st.Tropo.Calibrations.Add(cal);
        }
        return cal;
    }

    /// <summary>
    /// Anteprima della fusione di tutte le immagini scaricate della fonte, con i riquadri delle regioni e la velatura
    /// grigia dove manca copertura; a sinistra l'elenco delle regioni con il loro stato.
    /// </summary>
    void FillTropoTab(string fonte)
    {
        if (!_tropoHolders.TryGetValue(fonte, out var holder) || holder.IsDisposed) return;
        var cal = CalFor(fonte);
        string nome = fonte == "hepburn" ? "Hepburn" : "F5LEN";
        int hours = _previewHours.TryGetValue(fonte, out var h0) ? h0 : 0;

        holder.SuspendLayout();
        foreach (Control c in holder.Controls.Cast<Control>().ToList()) c.Dispose();
        holder.Controls.Clear();

        // ------------------------------------------- sinistra: regioni
        var list = Stack(new Padding(12, 10, 12, 10));
        Add(list, WrapLbl(Lang.F("u.preview_title", nome), 13f, _th.Accent, true));
        Add(list, WrapLbl(Lang.T("u.preview_help"), 9f, _th.Muted));
        var btns = Row();
        btns.Controls.Add(Btn(Lang.T("u.update_now"), () => Task.Run(async () => { await _st.Tropo.UpdateAsync(_st.Http); Ui(() => FillTropoTab(fonte)); }), true));
        Add(list, btns);
        Add(list, Chk(Lang.T("u.active_use_these_maps"), cal.Attivo, v => { cal.Attivo = v; try { cal.Save(TropoIndex.Folder); } catch { } }));
        var have = _st.Tropo.Rasters.Where(r => r.Fonte == fonte).GroupBy(r => r.Region).ToDictionary(x => x.Key, x => x.Count());
        foreach (var reg in TropoSources.For(fonte))
        {
            var head = Row(); head.WrapContents = true;
            var rk = reg.Key;
            head.Controls.Add(Chk("", _st.Tropo.RegionEnabled(reg), v => _st.S.Set("tropo_reg_" + rk, v ? "1" : "0")));
            head.Controls.Add(Lbl(reg.Nome, 9.5f, _th.Text, true));
            head.Controls.Add(Lbl(reg.Code ?? (reg.Prefix == "" ? "—" : reg.Prefix), 8.5f, _th.Muted, false, true));
            int n = have.TryGetValue(reg.Key, out var c) ? c : 0;
            head.Controls.Add(Lbl(n > 0 ? Lang.F("u.maps_n", n) : Lang.T("u.not_read_yet"), 8.5f, n > 0 ? _th.Ok : _th.Muted, true));
            Add(list, head);
            if (_st.Tropo.RegionStatus.TryGetValue(reg.Key, out var rs))
            {
                var t = WrapLbl(rs, 8.5f, _th.Sub); t.Margin = new Padding(26, 0, 3, 4);
                Add(list, t);
            }
            Add(list, new Panel { Height = 1, BackColor = C(_th.Line), Margin = new Padding(0, 2, 0, 2) });
        }
        Add(list, WrapLbl(_st.Tropo.Status, 9f, _th.Sub));
        Add(list, WrapLbl(Lang.T("u.calibration_bench") + " → LIVETROPOVHF (Windows 10/11, Linux, Mac)", 8.5f, _th.Muted));
        var left = ScrollBox(list, C(_th.Panel));
        left.Dock = DockStyle.Left; left.Width = 400;

        // ------------------------------------------- destra: anteprima con la barra delle ore
        var pv = new MapView(_st, _th) { PreviewSource = fonte, PreviewHours = hours };
        _previews[fonte] = pv;
        var host = new AC.ControlHost(pv) { Dock = DockStyle.Fill, BackColor = C(_th.MapSea) };
        var bar = new TableLayoutPanel { Dock = DockStyle.Top, Height = 46, ColumnCount = 2, BackColor = C(_th.Page), Padding = new Padding(10, 6, 10, 0) };
        bar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 290));
        bar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        var lbl = Lbl("", 10f, _th.Text, true, true);
        void Lbl2() { var t = Clock.UtcNow.AddHours(hours); lbl.Text = $"{Lang.DayNames()[(int)t.DayOfWeek]} {t:dd/MM HH}:00 UTC (+{hours} h)"; }
        Lbl2();
        var sl = new TrackBar { Minimum = 0, Maximum = 64, Value = Math.Clamp(hours / 3, 0, 64), TickFrequency = 8, Dock = DockStyle.Fill, BackColor = C(_th.Page) };
        sl.ValueChanged += (_, _) => { hours = sl.Value * 3; _previewHours[fonte] = hours; pv.PreviewHours = hours; Lbl2(); pv.InvalidateVisual(); };
        bar.Controls.Add(lbl, 0, 0); bar.Controls.Add(sl, 1, 0);

        holder.Controls.Add(host);
        holder.Controls.Add(bar);
        holder.Controls.Add(new Panel { Dock = DockStyle.Left, Width = 1, BackColor = C(_th.Line) });
        holder.Controls.Add(left);
        holder.ResumeLayout(true);
    }

    // ================================================================ linguetta GRAVES

    System.Windows.Forms.Timer _gravesTimer, _susTimer;

    Control BuildGravesTab()
    {
        var root = new Panel { BackColor = C(_th.Page) };
        var list = Stack(new Padding(12, 10, 12, 10));
        Add(list, Lbl("GRAVES · 143,050 MHz CW", 14f, _th.Accent, true));
        Add(list, WrapLbl(Lang.T("g.sub"), 9f, _th.Muted));
        Label Big(out Label small, string title, AM.Color accent)
        {
            var card = Stack(new Padding(10, 6, 10, 8));
            card.BackColor = C(_th.Chip);
            Add(card, Lbl(title, 8.5f, accent, true));
            var big = WrapLbl("", 14f, accent, true);
            Add(card, big);
            small = WrapLbl("", 9f, _th.Text);
            Add(card, small);
            var frame = new Panel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = C(accent), Padding = new Padding(4, 0, 0, 0), Margin = new Padding(0, 4, 0, 6), Dock = DockStyle.Top };
            frame.Controls.Add(card);
            Add(list, frame);
            return big;
        }
        var bigA = Big(out var subA, Lang.T("g.card_beam"), AM.Color.Parse("#D97706"));
        var bigB = Big(out var subB, Lang.T("g.card_next"), AM.Color.Parse("#2A78D6"));
        var bigC = Big(out var subC, Lang.T("g.card_listen"), AM.Color.Parse("#1F9D55"));
        var bigD = Big(out var subD, Lang.T("g.card_spots"), AM.Color.Parse("#EB6834"));
        Add(list, WrapLbl("[1] Rob Hardenberg PE1ITR, \"The 143.050 MHz Graves Radar, a VHF Beacon\" (DUBUS 4/2007) e \"Graves Radar Update April 2021\" – pe1itr.com/graves\n[2] Bolidozor wiki, \"Graves\"\n[3] YA-MS, IC8TEM – linguetta Radar GRAVES (github.com/ic8tem/YA-MS)", 8.5f, _th.Muted));
        var left = ScrollBox(list, C(_th.Panel));
        left.Dock = DockStyle.Left; left.Width = 420;

        var view = new GravesView(_st);
        var host = new AC.ControlHost(view) { Dock = DockStyle.Fill };
        root.Controls.Add(host);
        root.Controls.Add(new Panel { Dock = DockStyle.Left, Width = 1, BackColor = C(_th.Line) });
        root.Controls.Add(left);

        void Update()
        {
            var now = Clock.UtcNow;
            view.ActiveStep = Graves.ActiveStep(now);
            view.HasMe = _st.TryQth(out var me);
            view.Me = me; view.MeLabel = _st.LoginOrCall == "" ? "QTH" : _st.LoginOrCall + " " + _st.Loc.ToUpperInvariant();
            view.MySector = view.MyStep = -1;
            if (view.HasMe)
            {
                double az = Geo.Azimuth(Graves.Pos, me), km = Geo.DistKm(Graves.Pos, me), back = Geo.Azimuth(me, Graves.Pos);
                if (Graves.Locate(az, out var sec, out var st))
                {
                    view.MySector = sec; view.MyStep = st;
                    bigA.Text = Lang.F("g.yes", Graves.Sectors[sec], st + 1);
                    double cyc = Graves.Steps * Graves.StepSec, ph = Graves.Phase(now);
                    double wait = st * Graves.StepSec - ph; while (wait < 0) wait += cyc;
                    bool lit = view.ActiveStep == st;
                    bigB.Text = lit ? Lang.T("g.now") : Lang.F("g.in_s", wait.ToString("0.0"));
                    var times = Enumerable.Range(0, 3).Select(i => now.AddSeconds(wait + i * cyc)).Select(t => ":" + (t.Second + t.Millisecond / 1000.0).ToString("00.0"));
                    subB.Text = Lang.F("g.next_detail", Graves.Sectors[sec] + (st + 1), cyc.ToString("0.0"), string.Join(" · ", times));
                }
                else { bigA.Text = Lang.T("g.no"); bigB.Text = "—"; subB.Text = Lang.T("g.no_detail"); }
                subA.Text = Lang.F("g.beam_detail", km.ToString("0"), az.ToString("0"));
                bigC.Text = $"USB 143,049 MHz · {back:0}°";
                subC.Text = Lang.F("g.listen_detail", back.ToString("0"));
            }
            else { bigA.Text = Lang.T("g.no_qth"); subA.Text = ""; bigB.Text = "—"; subB.Text = ""; bigC.Text = "USB 143,049 MHz"; subC.Text = Lang.F("g.listen_detail", "—"); }

            view.Receivers.Clear();
            var gs = _st.Spots.Filter("144", Math.Max(60, _st.WindowMin), now).Where(s => s.Dx == Graves.Call).ToList();
            foreach (var s in gs) if (_st.TryPos(s.Spotter, s.DeLoc, out var p) && view.Receivers.All(r => r.label != s.Spotter)) view.Receivers.Add((p, s.Spotter));
            bigD.Text = gs.Count.ToString();
            subD.Text = gs.Count == 0 ? Lang.T("g.no_spots") : string.Join("\n", gs.Take(8).Select(s => $"{s.Recv:HH:mm}  {s.Spotter}  {s.Comment}"));
            view.InvalidateVisual();
        }
        Update();
        _gravesTimer?.Stop(); _gravesTimer?.Dispose();
        _gravesTimer = new System.Windows.Forms.Timer { Interval = 250 };
        _gravesTimer.Tick += (_, _) => { if (_tab == "graves" && !root.IsDisposed) Update(); };
        _gravesTimer.Start();
        return root;
    }

    // ================================================================ linguetta Segnalazioni sospette

    Control BuildSuspectsTab()
    {
        var root = new Panel { BackColor = C(_th.Page) };
        var left = Stack(new Padding(12, 10, 12, 10));
        Add(left, Lbl(Lang.T("tab.sus"), 14f, _th.Accent, true));
        Add(left, WrapLbl(Lang.T("s.intro"), 9f, _th.Text));
        foreach (var k in SuspectStore.Kinds)
        {
            var card = Stack(new Padding(8, 4, 8, 6)); card.BackColor = C(_th.Chip); card.Margin = new Padding(0, 2, 0, 4);
            Add(card, Lbl(Lang.T("s.k." + k), 9f, _th.Text, true));
            Add(card, WrapLbl(Lang.T("s.d." + k), 8.5f, _th.Sub));
            Add(left, card);
        }
        Add(left, Lbl(Lang.T("s.rules"), 10f, _th.Text, true));
        var rules = Stack(new Padding(0));
        Add(left, rules);
        var leftScroll = ScrollBox(left, C(_th.Panel));
        leftScroll.Dock = DockStyle.Left; leftScroll.Width = 440;

        var right = Stack(new Padding(12, 10, 12, 10));
        var head = Lbl("", 10f, _th.Muted);
        Add(right, head);
        var list = Stack(new Padding(0));
        Add(right, list);
        var rightScroll = ScrollBox(right, C(_th.Page));

        root.Controls.Add(rightScroll);
        root.Controls.Add(new Panel { Dock = DockStyle.Left, Width = 1, BackColor = C(_th.Line) });
        root.Controls.Add(leftScroll);

        string VText(int v, bool user) => (v > 0 ? "✓ " + Lang.T("s.good") : v < 0 ? "✗ " + Lang.T("s.drop") : "—") + (v != 0 ? " · " + Lang.T(user ? "s.user" : "s.auto") : "");
        AM.Color VCol(int v) => v > 0 ? AM.Color.Parse("#1F9D55") : v < 0 ? AM.Color.Parse("#D64545") : _th.Muted;

        Control Buttons(string kind, string call)
        {
            var row = Row(); row.WrapContents = false;
            foreach (var (txt, v) in new[] { ("✓ " + Lang.T("s.good"), 1), ("✗ " + Lang.T("s.drop"), -1), ("↺ " + Lang.T("s.reset"), 0) })
            {
                var vv = v;
                var b = Btn(txt, () => { _st.Suspects.SetUser(kind, call, vv); _st.Dirty = true; }, false);
                b.Font = F(8f); b.Padding = new Padding(4, 0, 4, 0); b.Margin = new Padding(2);
                row.Controls.Add(b);
            }
            return row;
        }

        void Clear(TableLayoutPanel t)
        {
            foreach (Control c in t.Controls.Cast<Control>().ToList()) c.Dispose();
            t.Controls.Clear(); t.RowStyles.Clear(); t.RowCount = 0;
        }

        int shown = -1;
        void Update()
        {
            if (shown == _st.Suspects.Version) return;
            shown = _st.Suspects.Version;
            rules.SuspendLayout(); list.SuspendLayout();
            Clear(rules);
            var rl = _st.Suspects.Rules();
            if (rl.Count == 0) Add(rules, Lbl(Lang.T("s.no_rules"), 8.5f, _th.Muted));
            foreach (var r in rl)
            {
                var line = Row();
                line.Controls.Add(Lbl($"{Lang.T("s.k." + r.Kind)} · {r.Call}  {VText(r.Verdict, r.User)}  ({r.Pos}+ {r.Neg}−)", 8.5f, VCol(r.Verdict)));
                line.Controls.Add(Buttons(r.Kind, r.Call));
                Add(rules, line);
            }

            Clear(list);
            var items = _st.Suspects.Items();
            head.Text = Lang.F("s.head", items.Count);
            foreach (var it in items.Take(150))
            {
                var r = _st.Suspects.RuleOf(it.Kind, it.Call);
                int v = r?.Verdict ?? 0;
                var card = Stack(new Padding(10, 4, 10, 6)); card.BackColor = C(_th.Chip);
                var top = Row();
                top.Controls.Add(Lbl($"{Lang.T("s.k." + it.Kind)}  {it.Call}{(it.Other != "" ? " – " + it.Other : "")}", 10.5f, _th.Text, true, true));
                top.Controls.Add(Lbl($"{it.Last:HH:mm}Z · ×{it.Count}", 8.5f, _th.Muted, false, true));
                Add(card, top);
                var meta = string.Join(" · ", new[] { it.Band != "" ? it.Band + " MHz" : "", it.Mode, it.Km > 0 ? $"{it.Km:0} km" : "" }.Where(x => x != ""));
                if (meta != "") Add(card, Lbl(meta, 8.5f, _th.Sub, false, true));
                Add(card, WrapLbl(it.Detail, 9f, _th.Text));
                var foot = Row();
                foot.Controls.Add(Lbl(Lang.T("s.rule") + ": " + VText(v, r?.User ?? false), 8.5f, VCol(v)));
                if (it.Kind is "eme" or "lungo" or "ascolto" or "locatore") foot.Controls.Add(Buttons(it.Kind, it.Call));
                Add(card, foot);
                var frame = new Panel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = C(VCol(v)), Padding = new Padding(4, 0, 0, 0), Margin = new Padding(0, 0, 0, 6), Dock = DockStyle.Top };
                frame.Controls.Add(card);
                Add(list, frame);
            }
            if (items.Count == 0) Add(list, Lbl(Lang.T("s.none"), 9f, _th.Muted));
            rules.ResumeLayout(true); list.ResumeLayout(true);
        }
        Update();
        _susTimer?.Stop(); _susTimer?.Dispose();
        _susTimer = new System.Windows.Forms.Timer { Interval = 2000 };
        _susTimer.Tick += (_, _) => { if (_tab == "sus" && !root.IsDisposed) Update(); };
        _susTimer.Start();
        return root;
    }

    // ================================================================ manuali

    /// <summary>Il manuale in un RichTextBox: titoli, elenchi, tabelle e formule in testo; pulsante per salvare in Word.</summary>
    Control BuildDoc(string file, string exportName)
    {
        var md = Program.ResourceText(file);
        var root = new Panel { BackColor = C("#FFFFFF") };
        var bar = new Panel { Dock = DockStyle.Top, Height = 40, BackColor = C(_th.Bar) };
        var save = Btn(Lang.T("u.export_to_word"), () =>
        {
            using var dlg = new SaveFileDialog { Title = Lang.T("u.export_to_word"), FileName = exportName + ".docx", DefaultExt = "docx", Filter = Lang.T("u.word_document") + " (*.docx)|*.docx" };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                using (var fs = File.Create(dlg.FileName)) DocxWriter.Write(fs, md);
                MessageBox.Show(this, dlg.FileName, "LIVETROPOVHF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, "LIVETROPOVHF", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }, true);
        save.Location = new Point(10, 5);
        bar.Controls.Add(save);
        var rtb = new RichTextBox
        {
            Dock = DockStyle.Fill, ReadOnly = true, BorderStyle = BorderStyle.None, BackColor = Color.White, ForeColor = C("#1B2430"),
            DetectUrls = true, Font = new Font("Segoe UI", 10.5f), ScrollBars = RichTextBoxScrollBars.Vertical
        };
        rtb.LinkClicked += (_, e) => { try { System.Diagnostics.Process.Start(e.LinkText); } catch { } };
        bool rendered = false;
        root.VisibleChanged += (_, _) => { if (root.Visible && !rendered) { rendered = true; RenderMarkdown(rtb, md); } };
        var pad = new Panel { Dock = DockStyle.Fill, Padding = new Padding(30, 14, 30, 10), BackColor = Color.White };
        pad.Controls.Add(rtb);
        root.Controls.Add(pad);
        root.Controls.Add(bar);
        return root;
    }

    /// <summary>
    /// Il manuale in RTF, costruito tutto in una volta (molto più veloce che scrivere a pezzi nel RichTextBox): titoli,
    /// elenchi, tabelle in carattere fisso, formule centrate, **grassetto**, *corsivo* e `codice`.
    /// </summary>
    static void RenderMarkdown(RichTextBox rtb, string md)
    {
        var sb = new System.Text.StringBuilder();
        sb.Append(@"{\rtf1\ansi\ansicpg1252\deff0{\fonttbl{\f0\fswiss Segoe UI;}{\f1\fmodern Consolas;}{\f2\froman Cambria Math;}}");
        sb.Append(@"{\colortbl;\red27\green36\blue48;\red31\green95\blue184;\red62\green74\blue87;\red170\green178\blue189;}");
        sb.Append(@"\viewkind4\uc1\f0\fs21\cf1 ");

        string Esc(string t)
        {
            var e = new System.Text.StringBuilder();
            foreach (var ch in t)
            {
                if (ch == '\\' || ch == '{' || ch == '}') e.Append('\\').Append(ch);
                else if (ch == '\t') e.Append(@"\tab ");
                else if (ch < 128) e.Append(ch);
                else e.Append(@"\u").Append((int)(short)ch).Append('?');
            }
            return e.ToString();
        }

        string Inline(string t)
        {
            t = Regex.Replace(t, @"\[([^\]]+)\]\(([^)]+)\)", "$1 ($2)");
            var o = new System.Text.StringBuilder();
            int i = 0;
            foreach (Match m in Regex.Matches(t, @"(\*\*[^*]+\*\*|`[^`]+`|\*[^*\s][^*]*\*)"))
            {
                if (m.Index > i) o.Append(Esc(t.Substring(i, m.Index - i)));
                var v = m.Value;
                if (v.StartsWith("**")) o.Append(@"{\b ").Append(Esc(v.Substring(2, v.Length - 4))).Append('}');
                else if (v.StartsWith("`")) o.Append(@"{\f1\fs19\cf3 ").Append(Esc(v.Substring(1, v.Length - 2))).Append('}');
                else o.Append(@"{\i ").Append(Esc(v.Substring(1, v.Length - 2))).Append('}');
                i = m.Index + m.Length;
            }
            if (i < t.Length) o.Append(Esc(t.Substring(i)));
            return o.ToString();
        }

        void Para(string body, int left = 0, string extra = "", int after = 120) =>
            sb.Append(@"\pard\sa").Append(after).Append(@"\li").Append(left).Append(extra).Append(' ').Append(body).Append(@"\par").Append('\n');

        bool code = false, math = false;
        var table = new List<string[]>();
        void FlushTable()
        {
            if (table.Count == 0) return;
            int cols = table.Max(r => r.Length);
            var w = new int[cols];
            foreach (var r in table) for (int k = 0; k < r.Length; k++) w[k] = Math.Max(w[k], Math.Min(46, r[k].Length));
            bool first = true;
            foreach (var r in table)
            {
                var line = string.Join("  \u2502 ", Enumerable.Range(0, cols).Select(k => (k < r.Length ? r[k] : "").PadRight(w[k])));
                var esc = Esc(line);
                Para((first ? @"{\f1\fs18\b\cf2 " : @"{\f1\fs18 ") + esc + "}", 200, "", 0);
                if (first) Para(@"{\f1\fs18\cf4 " + Esc(string.Join("\u2500\u253C\u2500", w.Select(x => new string('\u2500', x)))) + "}", 200, "", 0);
                first = false;
            }
            Para("", 0, "", 120);
            table.Clear();
        }

        foreach (var raw in md.Replace("\r", "").Split('\n'))
        {
            var l = raw.TrimEnd();
            if (l.StartsWith("```"))
            {
                FlushTable();
                if (!code) math = l.Trim() == "```math";
                code = !code;
                continue;
            }
            if (code)
            {
                if (math) Para(@"{\f2\fs26\i " + Esc(l.Trim()) + "}", 0, @"\qc", 60);
                else Para(@"{\f1\fs19\cf3 " + Esc(l) + "}", 360, "", 0);
                continue;
            }
            if (l.StartsWith("|"))
            {
                var cells = l.Trim().Trim('|').Split('|').Select(c => Regex.Replace(c.Trim(), @"\*\*|`", "")).ToArray();
                if (!cells.All(c => Regex.IsMatch(c, @"^:?-{2,}:?$"))) table.Add(cells);
                continue;
            }
            FlushTable();
            if (l.StartsWith("### ")) { Para(@"{\fs25\b\cf2 " + Inline(l.Substring(4)) + "}", 0, @"\sb120", 80); continue; }
            if (l.StartsWith("## ")) { Para(@"{\fs30\b\cf2 " + Inline(l.Substring(3)) + "}", 0, @"\sb280", 100); continue; }
            if (l.StartsWith("# ")) { Para(@"{\fs40\b " + Inline(l.Substring(2)) + "}", 0, "", 200); continue; }
            if (l.StartsWith("> ")) { Para(@"{\i\cf3 " + Inline(l.Substring(2)) + "}", 360); continue; }
            var bm = Regex.Match(l, @"^(\s*)[-*] (.*)$");
            if (bm.Success) { int ind = 360 + bm.Groups[1].Length * 120; Para(@"{\cf2 \u8226?}\tab " + Inline(bm.Groups[2].Value), ind, @"\fi-240\tx" + ind, 60); continue; }
            var nm = Regex.Match(l, @"^(\d+)\. (.*)$");
            if (nm.Success) { Para(@"{\b\cf2 " + nm.Groups[1].Value + @".}\tab " + Inline(nm.Groups[2].Value), 420, @"\fi-300\tx420", 60); continue; }
            if (l.Trim() == "---") { Para(@"{\cf4 " + new string('_', 70) + "}", 0, @"\qc"); continue; }
            if (l.Trim() == "") continue;
            Para(Inline(l));
        }
        FlushTable();
        sb.Append('}');
        rtb.Rtf = sb.ToString();
        rtb.SelectionStart = 0;
        rtb.ScrollToCaret();
    }
}
