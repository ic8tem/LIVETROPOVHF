using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using Avalonia.Threading;
using TropoVHF.Core;

namespace TropoVHF.Ui;

public sealed partial class MainWindow : Window
{
    readonly AppState _st;
    Palette _th;
    readonly MapView _map;
    readonly DispatcherTimer _timer;
    string _tab = "map";

    // controlli aggiornati dal timer
    StackPanel _statusPanel;
    readonly Dictionary<string, TextBlock> _statusSegs = new();
    TextBlock _clock, _ntp, _statusBar, _zoomPct, _fcLabel, _legendSrc, _obsNote, _emptyText;
    TextBlock _nSpots, _nAs, _nExcl, _nRx, _nDec, _nHeld;
    TextBlock _mqttCount; Avalonia.Controls.Shapes.Ellipse _mqttDot;
    bool _showMqttDiscarded;      // scheda con gli ascoltatori MQTT scartati
    StackPanel _spotRows;
    Border _infoCard;
    StackPanel _infoBody;
    readonly Dictionary<string, (TextBlock status, TextBlock counters, StackPanel log, ScrollViewer scroll)> _sections = new();
    StackPanel _udpLog; ScrollViewer _udpScroll; TextBlock _udpStatus, _udpInfo; CheckBox _udpShowHb;
    TextBox _locBox;
    readonly Dictionary<string, Control> _tabContent = new();
    readonly Dictionary<string, Button> _tabButtons = new();

    public MainWindow()
    {
        _st = new AppState();
        Lang.Current = _st.LangCode;
        _th = Palette.Get(_st.ThemeKey);
        _map = new MapView(_st, _th);
        _map.SelectionChanged += () => { RefreshInfoCard(); RefreshSpotList(); };

        Title = "LIVETROPOVHF " + (typeof(MainWindow).Assembly.GetName().Version?.ToString(3) ?? "") + " · IC8TEM";
        MinWidth = 900; MinHeight = 560;
        PlaceAtStartup();

        BuildUi();
        _st.Start();
        // CALL3.TXT mancante o vuoto: lo si dice chiaramente, i locatori restano a 4 caratteri
        _st.CallDbLoaded += () => Dispatcher.UIThread.Post(async () =>
        {
            if (_st.CallDb.Error == "" && _st.CallDb.Count > 0) return;
            var dlg = new Window { Title = "CALL3.TXT", Width = 520, SizeToContent = SizeToContent.Height, CanResize = false, WindowStartupLocation = WindowStartupLocation.CenterOwner, Background = B(_th.Panel) };
            var t = TB(Lang.F("u.call3_missing", Settings.AppDir, _st.CallDb.Error), 13, _th.Text); t.TextWrapping = TextWrapping.Wrap;
            dlg.Content = new StackPanel { Margin = new Thickness(20), Spacing = 12, Children = { TB("CALL3.TXT", 18, _th.Bad, true), t, PrimaryButton("OK", () => dlg.Close()) } };
            if (IsVisible) await dlg.ShowDialog(this);
        });
        // senza nominativo i cluster non partono: lo si chiede appena la finestra è aperta
        // 1.3.2: al primo avvio il disclaimer sta nella finestra di nominativo e locatore; poi compare da solo a ogni avvio
        Opened += async (_, _) => { if (!_st.HasValidCall) await AskCall(); else await ShowDisclaimer(); };

        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
        _timer.Tick += (_, _) => Tick();
        _timer.Start();

        Icon = Splash.AppIcon();
        // chiusura: si chiede conferma con il ringraziamento (come in YAEC), poi si salva tutto
        Closing += async (_, e) =>
        {
            if (!_closeConfirmed)
            {
                e.Cancel = true;
                if (_closeAsking) return;
                _closeAsking = true;
                bool yes = await AskClose();
                _closeAsking = false;
                if (!yes) return;
                _closeConfirmed = true;
                Close();
                return;
            }
            SaveGeometry(); _timer.Stop(); _st.Shutdown();
        };
        // la finestra normale (non massimizzata) si ricorda a ogni spostamento o ridimensionamento
        PositionChanged += (_, _) => TrackNormal();
        Resized += (_, _) => TrackNormal();
        Opened += (_, _) => EnsureTitleVisible();
        // errore annotato: avviso discreto nella barra di stato
        ErrorLog.Logged += msg => Dispatcher.UIThread.Post(() => { _lastErrorNote = msg; _lastErrorAt = DateTime.Now; });
        // errore della volta scorsa: si propone di inviare il rapporto
        Opened += (_, _) => DispatcherTimer.RunOnce(async () => { if (ErrorLog.HasPending && IsVisible) await AskSendErrorReport(); }, TimeSpan.FromSeconds(4));
        // versione nuova installata: riavviare adesso (obbligatoria: la finestra blocca)
        Updater.Installed += (v, mandatory) => Dispatcher.UIThread.Post(async () => await AskRestart(v, mandatory));
    }

    string _lastErrorNote = ""; DateTime _lastErrorAt;

    static void OpenUrl(string url)
    {
        try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true }); }
        catch
        {
            try { if (!Compat.IsWindows) System.Diagnostics.Process.Start(Compat.IsMac ? "open" : "xdg-open", url); } catch (Exception ex) { ErrorLog.Write(ex, "apertura del browser"); }
        }
    }

    async Task AskSendErrorReport()
    {
        var summary = ErrorLog.PendingSummary.Replace("\n", " · ");
        var dlg = new Window
        {
            Title = Lang.Tr("Rapporto di errore", "Error report"), Width = 600, SizeToContent = SizeToContent.Height, CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner, Background = B(_th.Panel), ShowInTaskbar = false, Icon = Splash.AppIcon()
        };
        var t = TB(Lang.Tr("L'ultima volta il programma ha annotato un errore ({0}).\n\nPuoi inviare il rapporto agli autori: si apre il browser su una segnalazione GitHub già compilata, con nominativo, locatore e nome utente del PC tolti. Controlli il testo e premi Invia (serve un account GitHub gratuito). Il registro completo è in {1}.", "Last time the program logged an error ({0}).\n\nYou can send the report to the authors: the browser opens a prefilled GitHub issue, with call, locator and PC user name removed. Check the text and press Submit (a free GitHub account is needed). The full log is in {1}.", summary, ErrorLog.FilePath) + "\n\n" + Lang.Tr("Il rapporto completo viene copiato negli appunti: se nella segnalazione il testo è accorciato, incollalo con Ctrl+V.", "The full report is copied to the clipboard: if the text in the issue is shortened, paste it with Ctrl+V."), 13, _th.Text);
        t.TextWrapping = TextWrapping.Wrap; t.Margin = new Thickness(20, 18, 20, 16);
        var send = PrimaryButton(Lang.Tr("Invia il rapporto", "Send report"), async () =>
        {
            // il rapporto completo negli appunti: l'indirizzo per GitHub porta solo l'inizio (limite di circa 8 KB)
            try { var cb = TopLevel.GetTopLevel(this)?.Clipboard; if (cb != null) await cb.SetTextAsync(ErrorLog.FullReport(_st.Call, _st.Loc)); } catch { }
            OpenUrl(ErrorLog.IssueUrl(_st.Call, _st.Loc)); dlg.Close();
        });
        var file = OutlineButton(Lang.Tr("Apri il registro", "Open log"), () => OpenUrl(ErrorLog.FilePath));
        var no = OutlineButton(Lang.Tr("No, grazie", "No, thanks"), () => dlg.Close());
        dlg.Content = new StackPanel { Children = { t, new Border { Background = B(_th.Chip), BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 1, 0, 0), Padding = new Thickness(16, 10),
            Child = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10, HorizontalAlignment = HorizontalAlignment.Right, Children = { send, file, no } } } } };
        ErrorLog.ClearPending();
        await dlg.ShowDialog(this);
    }

    bool _restartAsked;
    async Task AskRestart(string version, bool mandatory)
    {
        if (_restartAsked || !IsVisible) return;
        _restartAsked = true;
        var dlg = new Window
        {
            Title = Lang.Tr("Aggiornamento", "Update"), Width = 520, SizeToContent = SizeToContent.Height, CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner, Background = B(_th.Panel), ShowInTaskbar = false, Icon = Splash.AppIcon(),
            SystemDecorations = mandatory ? SystemDecorations.BorderOnly : SystemDecorations.Full
        };
        var t = TB(mandatory
            ? (Lang.Tr("È stata installata la versione {0}, una correzione urgente. Il programma deve riavviarsi per usarla: impostazioni e dati restano.", "Version {0}, an urgent fix, has been installed. The program must restart to use it: settings and data are kept.", version))
            : (Lang.Tr("È stata installata la versione {0}. Sarà attiva dal prossimo avvio: vuoi riavviare adesso? Impostazioni e dati restano.", "Version {0} has been installed and will be active at the next start: restart now? Settings and data are kept.", version)), 13, _th.Text);
        t.TextWrapping = TextWrapping.Wrap; t.Margin = new Thickness(20, 18, 20, 16);
        bool restart = false;
        var now = PrimaryButton(Lang.Tr("Riavvia adesso", "Restart now"), () => { restart = true; dlg.Close(); });
        var buttons = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10, HorizontalAlignment = HorizontalAlignment.Right, Children = { now } };
        if (!mandatory) buttons.Children.Add(OutlineButton(Lang.Tr("Più tardi", "Later"), () => dlg.Close()));
        else dlg.Closing += (_, e) => { if (!restart) e.Cancel = true; };
        dlg.Content = new StackPanel { Children = { t, new Border { Background = B(_th.Chip), BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 1, 0, 0), Padding = new Thickness(16, 10), Child = buttons } } };
        await dlg.ShowDialog(this);
        if (restart && Updater.StartNewInstance()) { _closeConfirmed = true; Close(); }
    }

    /// <summary>Finestra iniziale: nominativo (obbligatorio per i cluster) e locatore. Il nominativo diventa il nome utente dei cluster.</summary>
    bool _closeConfirmed, _closeAsking;

    /// <summary>
    /// Finestra di chiusura nello stile di YAEC: icona, domanda, ringraziamento in corsivo, barra grigia con Sì e No.
    /// </summary>
    async Task<bool> AskClose()
    {
        var dlg = new Window
        {
            Title = Lang.T("close.title"), Width = 480, SizeToContent = SizeToContent.Height, CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner, Background = B(_th.Panel), ShowInTaskbar = false,
            Icon = Splash.AppIcon(),
            FlowDirection = Lang.IsRtl(Lang.Current) ? FlowDirection.RightToLeft : FlowDirection.LeftToRight
        };
        bool result = false;
        var img = Splash.LoadBitmap("livetropovhf-256.png");
        var top = new DockPanel { Margin = new Thickness(20, 20, 20, 16) };
        if (img != null)
        {
            var pic = new Border { Width = 64, Height = 64, CornerRadius = new CornerRadius(10), ClipToBounds = true, Margin = new Thickness(0, 0, 16, 0), VerticalAlignment = VerticalAlignment.Top, Child = new Image { Source = img, Stretch = Stretch.UniformToFill } };
            DockPanel.SetDock(pic, Dock.Left);
            top.Children.Add(pic);
        }
        var q = new TextBlock { Text = Lang.T("close.q"), FontSize = (17) * UiScale.F, FontWeight = FontWeight.Bold, Foreground = B(_th.Text), TextWrapping = TextWrapping.Wrap };
        var thanks = new TextBlock { Text = Lang.T("close.thanks"), FontSize = (14) * UiScale.F, FontStyle = FontStyle.Italic, Foreground = B(_th.Sub), TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 8, 0, 0) };
        top.Children.Add(new StackPanel { Children = { q, thanks }, VerticalAlignment = VerticalAlignment.Center });
        var yes = PrimaryButton(Lang.T("yes"), () => { result = true; dlg.Close(); });
        var no = OutlineButton(Lang.T("no"), () => dlg.Close());
        yes.MinWidth = 90; no.MinWidth = 90;
        var bar = new Border
        {
            Background = B(_th.Chip), BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 1, 0, 0), Padding = new Thickness(16, 10),
            Child = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10, HorizontalAlignment = HorizontalAlignment.Right, Children = { yes, no } }
        };
        dlg.Content = new StackPanel { Children = { top, bar } };
        dlg.KeyDown += (_, e) => { if (e.Key == Avalonia.Input.Key.Escape) dlg.Close(); if (e.Key == Avalonia.Input.Key.Enter) { result = true; dlg.Close(); } };
        await dlg.ShowDialog(this);
        return result;
    }

    /// <summary>Domanda sì/no con titolo e testo, nello stile della finestra di chiusura.</summary>
    async Task<bool> AskYesNo(string title, string text, string yesText, string noText)
    {
        var dlg = new Window
        {
            Title = title, Width = 560, SizeToContent = SizeToContent.Height, CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner, Background = B(_th.Panel), ShowInTaskbar = false,
            Icon = Splash.AppIcon(), FlowDirection = Lang.IsRtl(Lang.Current) ? FlowDirection.RightToLeft : FlowDirection.LeftToRight
        };
        bool result = false;
        var body = new TextBlock { Text = text, FontSize = (13) * UiScale.F, Foreground = B(_th.Text), TextWrapping = TextWrapping.Wrap, Margin = new Thickness(20, 18, 20, 16) };
        var yes = PrimaryButton(yesText, () => { result = true; dlg.Close(); });
        var no = OutlineButton(noText, () => dlg.Close());
        yes.MinWidth = 90; no.MinWidth = 90;
        var bar = new Border
        {
            Background = B(_th.Chip), BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 1, 0, 0), Padding = new Thickness(16, 10),
            Child = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10, HorizontalAlignment = HorizontalAlignment.Right, Children = { yes, no } }
        };
        dlg.Content = new StackPanel { Children = { body, bar } };
        dlg.KeyDown += (_, e) => { if (e.Key == Avalonia.Input.Key.Escape) dlg.Close(); };
        await dlg.ShowDialog(this);
        return result;
    }

    static readonly FontFamily DiscSerif = new("Georgia, Cambria, Times New Roman, DejaVu Serif, serif");
    static string AppVersion => typeof(MainWindow).Assembly.GetName().Version?.ToString(3) ?? "";
    static TextBlock Wrap12(TextBlock t) { t.TextWrapping = TextWrapping.Wrap; return t; }

    /// <summary>Riquadro del disclaimer (1.3.2): testo completo al primo avvio, breve agli avvii successivi.</summary>
    Control DisclaimerBox(bool full)
    {
        var sp = new StackPanel { Spacing = 8 };
        if (full)
        {
            sp.Children.Add(TB("ⓘ  " + Lang.T("u.disc_head"), 13, _th.Accent, true));
            foreach (var k in new[] { "u.disc_p1", "u.disc_p2", "u.disc_p3" }) sp.Children.Add(Wrap12(TB(Lang.T(k), 13, _th.Text)));
        }
        else sp.Children.Add(Wrap12(TB(Lang.T("u.disc_short"), 13, _th.Text)));
        return new Border { Background = B(_th.Chip), BorderBrush = B(_th.Accent), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(6), Padding = new Thickness(14, 12), Child = sp };
    }

    /// <summary>Agli avvii successivi (nominativo già salvato): disclaimer breve, si chiude da solo dopo 10 s.</summary>
    async Task ShowDisclaimer()
    {
        var dlg = new Window
        {
            Title = "LIVETROPOVHF", Width = 580, SizeToContent = SizeToContent.Height, CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner, Background = B(_th.Panel), ShowInTaskbar = false,
            FlowDirection = Lang.IsRtl(Lang.Current) ? FlowDirection.RightToLeft : FlowDirection.LeftToRight
        };
        bool openSources = false;
        int left = 10;
        var count = TB(string.Format(Lang.T("u.disc_autoclose"), left), 12, _th.Muted);
        count.VerticalAlignment = VerticalAlignment.Center;
        var timer = new Avalonia.Threading.DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        timer.Tick += (_, _) => { left--; count.Text = string.Format(Lang.T("u.disc_autoclose"), left); if (left <= 0) { timer.Stop(); dlg.Close(); } };
        var bar = new DockPanel { LastChildFill = false };
        DockPanel.SetDock(count, Dock.Left); bar.Children.Add(count);
        var btns = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10 };
        btns.Children.Add(OutlineButton(Lang.T("u.disc_open"), () => { openSources = true; dlg.Close(); }));
        btns.Children.Add(PrimaryButton(Lang.T("u.disc_continue"), () => dlg.Close()));
        DockPanel.SetDock(btns, Dock.Right); bar.Children.Add(btns);
        dlg.Content = new StackPanel
        {
            Margin = new Thickness(20), Spacing = 10,
            Children =
            {
                new TextBlock { Text = "LIVETROPOVHF", FontFamily = DiscSerif, FontSize = (24) * UiScale.F, Foreground = B(_th.Text) },
                TB($"{_st.Call} · {_st.Loc} · " + string.Format(Lang.T("u.disc_version"), AppVersion), 12, _th.Muted),
                DisclaimerBox(false),
                bar
            }
        };
        dlg.Opened += (_, _) => timer.Start();
        dlg.Closed += (_, _) => timer.Stop();
        await dlg.ShowDialog(this);
        if (openSources) ShowTab("doc6");
    }

    async Task AskCall()
    {
        var dlg = new Window
        {
            Title = Lang.T("u.ask_call_title"), Width = 580, SizeToContent = SizeToContent.Height, CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner, Background = B(_th.Panel), ShowInTaskbar = false,
            FlowDirection = Lang.IsRtl(Lang.Current) ? FlowDirection.RightToLeft : FlowDirection.LeftToRight
        };
        var call = Pill(_st.Call, 200, "IC8TEM");
        var loc = Pill(_st.Loc, 200, "JN70CN83CL"); loc.MaxLength = 10;
        var err = TB("", 12, _th.Bad);
        bool ok = false;
        void Confirm()
        {
            var c = (call.Text ?? "").Trim().ToUpperInvariant();
            if (!AppState.IsValidCall(c)) { err.Text = Lang.T("u.ask_call_invalid"); return; }
            var l = (loc.Text ?? "").Trim().ToUpperInvariant();
            if (l != "" && !Locator.IsValid(l)) { err.Text = Lang.T("u.enter_a_46character_locator"); return; }
            _st.Call = c;
            if (l != "") _st.Loc = l;
            ok = true;
            dlg.Close();
        }
        call.KeyDown += (_, e) => { if (e.Key == Avalonia.Input.Key.Enter) Confirm(); };
        loc.KeyDown += (_, e) => { if (e.Key == Avalonia.Input.Key.Enter) Confirm(); };
        var okBtn = PrimaryButton(Lang.T("u.disc_read"), Confirm);
        var later = OutlineButton(Lang.T("u.later"), () => dlg.Close());
        var text = TB(Lang.T("u.ask_call_text"), 13, _th.Text); text.TextWrapping = TextWrapping.Wrap;
        dlg.Content = new StackPanel
        {
            Margin = new Thickness(20), Spacing = 10,
            Children =
            {
                new TextBlock { Text = Lang.T("u.disc_welcome"), FontFamily = DiscSerif, FontSize = (24) * UiScale.F, Foreground = B(_th.Text), TextWrapping = TextWrapping.Wrap },
                TB(string.Format(Lang.T("u.disc_version"), AppVersion), 12, _th.Muted),
                DisclaimerBox(true),
                TB(Lang.T("u.ask_call_title"), 15, _th.Accent, true),
                text,
                new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10, Children = { new Border { Width = 150, Child = TB(Lang.T("call"), 13, _th.Muted) }, call } },
                new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10, Children = { new Border { Width = 150, Child = TB(Lang.T("u.ask_call_loc"), 13, _th.Muted) }, loc } },
                Wrap12(TB(Lang.T("u.disc_hint"), 12, _th.Muted)),
                err,
                new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10, HorizontalAlignment = HorizontalAlignment.Right, Children = { later, okBtn } }
            }
        };
        dlg.Opened += (_, _) => call.Focus();
        await dlg.ShowDialog(this);
        if (!ok) return;
        _st.StartFeeds();          // ricopia il nominativo nei Login vuoti e collega i cluster
        _st.Save();
        BuildUi();                 // la riga in alto e le sezioni dei cluster mostrano il nominativo
    }

    Avalonia.Threading.DispatcherTimer _gravesTimer;

    /// <summary>
    /// Linguetta GRAVES (come in YA-MS): a sinistra le schede (la mia stazione nel fascio, prossimo passaggio del fascio,
    /// come ascoltarlo, spot ricevuti, fonti), a destra il cielo visto da GRAVES. Le schede del fascio si aggiornano
    /// 4 volte al secondo mentre la linguetta è aperta (orologio allineato NTP).
    /// </summary>
    Control BuildGravesTab()
    {
        var g = new Grid { ColumnDefinitions = new ColumnDefinitions("420,*") };
        var list = new StackPanel { Spacing = 10, Margin = new Thickness(14) };
        list.Children.Add(new TextBlock { Text = "GRAVES · 143,050 MHz CW", FontSize = (20) * UiScale.F, FontWeight = FontWeight.Bold, Foreground = B(_th.Accent) });
        var sub = TB(Lang.T("g.sub"), 12, _th.Muted); sub.TextWrapping = TextWrapping.Wrap; list.Children.Add(sub);

        TextBlock Big(out TextBlock small, Control parent, string title, Color accent)
        {
            var t = TB(title, 11, accent, true);
            var big = new TextBlock { FontSize = (20) * UiScale.F, FontWeight = FontWeight.Bold, Foreground = B(accent), TextWrapping = TextWrapping.Wrap };
            small = TB("", 12, _th.Text); small.TextWrapping = TextWrapping.Wrap;
            var sp = new StackPanel { Spacing = 2, Children = { t, big, small } };
            ((StackPanel)parent).Children.Add(new Border { Background = B(_th.Chip), CornerRadius = new CornerRadius(8), Padding = new Thickness(12, 8), BorderBrush = B(accent), BorderThickness = new Thickness(4, 0, 0, 0), Child = sp });
            return big;
        }
        var bigA = Big(out var subA, list, Lang.T("g.card_beam"), Color.Parse("#D97706"));
        var bigB = Big(out var subB, list, Lang.T("g.card_next"), Color.Parse("#2A78D6"));
        var bigC = Big(out var subC, list, Lang.T("g.card_listen"), Color.Parse("#1F9D55"));
        var bigD = Big(out var subD, list, Lang.T("g.card_spots"), Color.Parse("#EB6834"));
        var src = TB("[1] Rob Hardenberg PE1ITR, \"The 143.050 MHz Graves Radar, a VHF Beacon\" (DUBUS 4/2007) e \"Graves Radar Update April 2021\" – pe1itr.com/graves\n[2] Bolidozor wiki, \"Graves\"\n[3] YA-MS, IC8TEM – linguetta Radar GRAVES (github.com/ic8tem/YA-MS)", 11, _th.Muted);
        src.TextWrapping = TextWrapping.Wrap; list.Children.Add(src);
        g.Children.Add(new Border { Background = B(_th.Panel), BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 0, 1, 0), Child = new ScrollViewer { Content = list } });

        var view = new GravesView(_st);
        Grid.SetColumn(view, 1);
        g.Children.Add(view);

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
                    // prossimo passaggio del passo che illumina la mia direzione
                    double cyc = Graves.Steps * Graves.StepSec, ph = Graves.Phase(now);
                    double wait = st * Graves.StepSec - ph; while (wait < 0) wait += cyc;
                    bool lit = view.ActiveStep == st;
                    bigB.Text = lit ? Lang.T("g.now") : Lang.F("g.in_s", wait.ToString("0.0"));
                    var times = Enumerable.Range(0, 3).Select(i => now.AddSeconds(wait + i * cyc)).Select(t => ":" + (t.Second + t.Millisecond / 1000.0).ToString("00.0"));
                    subB.Text = Lang.F("g.next_detail", Graves.Sectors[sec] + (st + 1), cyc.ToString("0.0"), string.Join(" · ", times));
                }
                else
                {
                    bigA.Text = Lang.T("g.no"); bigB.Text = "—"; subB.Text = Lang.T("g.no_detail");
                }
                subA.Text = Lang.F("g.beam_detail", km.ToString("0"), az.ToString("0"));
                bigC.Text = $"USB 143,049 MHz · {back:0}°";
                subC.Text = Lang.F("g.listen_detail", back.ToString("0"));
            }
            else { bigA.Text = Lang.T("g.no_qth"); subA.Text = ""; bigB.Text = "—"; subB.Text = ""; bigC.Text = "USB 143,049 MHz"; subC.Text = Lang.F("g.listen_detail", "—"); }

            // chi ha spottato GRAVES nell'intervallo scelto
            view.Receivers.Clear();
            var gs = _st.Spots.Filter("144", Math.Max(60, _st.WindowMin), now).Where(s => s.Dx == Graves.Call).ToList();
            foreach (var s in gs) if (_st.TryPos(s.Spotter, s.DeLoc, out var p) && view.Receivers.All(r => r.label != s.Spotter)) view.Receivers.Add((p, s.Spotter));
            bigD.Text = gs.Count.ToString();
            subD.Text = gs.Count == 0 ? Lang.T("g.no_spots") : string.Join("\n", gs.Take(8).Select(s => $"{s.Recv:HH:mm}  {s.Spotter}  {s.Comment}"));
            view.InvalidateVisual();
        }
        Update();
        _gravesTimer?.Stop();
        _gravesTimer = new Avalonia.Threading.DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };
        _gravesTimer.Tick += (_, _) => { if (_tab == "graves") Update(); };
        _gravesTimer.Start();
        return g;
    }

    Avalonia.Threading.DispatcherTimer _susTimer;

    /// <summary>
    /// Linguetta Segnalazioni sospette: a sinistra come funziona l'auto-apprendimento e le regole in vigore (imparate o
    /// decise dall'utente), a destra le segnalazioni delle ultime 24 ore. Per ogni segnalazione l'utente può dire
    /// "buono", "scarta" o rimettere la decisione al programma; la regola vale per quel tipo e quel nominativo.
    /// </summary>
    Control BuildSuspectsTab()
    {
        var g = new Grid { ColumnDefinitions = new ColumnDefinitions("440,*") };
        var left = new StackPanel { Spacing = 8, Margin = new Thickness(14) };
        left.Children.Add(new TextBlock { Text = Lang.T("tab.sus"), FontSize = (20) * UiScale.F, FontWeight = FontWeight.Bold, Foreground = B(_th.Accent) });
        var intro = TB(Lang.T("s.intro"), 12, _th.Text); intro.TextWrapping = TextWrapping.Wrap; left.Children.Add(intro);
        foreach (var k in SuspectStore.Kinds)
        {
            var t = TB(Lang.T("s.k." + k), 12, _th.Text, true); var d = TB(Lang.T("s.d." + k), 11, _th.Sub); d.TextWrapping = TextWrapping.Wrap;
            left.Children.Add(new Border { Background = B(_th.Chip), CornerRadius = new CornerRadius(6), Padding = new Thickness(10, 6), Child = new StackPanel { Children = { t, d } } });
        }
        left.Children.Add(TB(Lang.T("s.rules"), 13, _th.Text, true));
        var rules = new StackPanel { Spacing = 4 };
        left.Children.Add(rules);
        g.Children.Add(new Border { Background = B(_th.Panel), BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 0, 1, 0), Child = new ScrollViewer { Content = left } });

        var right = new StackPanel { Spacing = 6, Margin = new Thickness(14) };
        var head = TB("", 13, _th.Muted); right.Children.Add(head);
        var list = new StackPanel { Spacing = 6 };
        right.Children.Add(list);
        var sv = new ScrollViewer { Content = right };
        Grid.SetColumn(sv, 1);
        g.Children.Add(sv);

        string VText(int v, bool user) => (v > 0 ? "✓ " + Lang.T("s.good") : v < 0 ? "✗ " + Lang.T("s.drop") : "—") + (v != 0 ? " · " + Lang.T(user ? "s.user" : "s.auto") : "");
        Color VCol(int v) => v > 0 ? Color.Parse("#1F9D55") : v < 0 ? Color.Parse("#D64545") : _th.Muted;

        Control Buttons(string kind, string call)
        {
            var row = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 6 };
            foreach (var (txt, v) in new[] { ("✓ " + Lang.T("s.good"), 1), ("✗ " + Lang.T("s.drop"), -1), ("↺ " + Lang.T("s.reset"), 0) })
            {
                var b = new Button { Content = txt, FontSize = (11) * UiScale.F, Padding = new Thickness(10, 2), CornerRadius = new CornerRadius(10), Background = B(_th.CtlBg), Foreground = B(_th.Text2) };
                b.Click += (_, _) => { _st.Suspects.SetUser(kind, call, v); _st.Dirty = true; };
                row.Children.Add(b);
            }
            return row;
        }

        int shown = -1;
        void Update()
        {
            if (shown == _st.Suspects.Version) return;
            shown = _st.Suspects.Version;
            rules.Children.Clear();
            var rl = _st.Suspects.Rules();
            if (rl.Count == 0) rules.Children.Add(TB(Lang.T("s.no_rules"), 11, _th.Muted));
            foreach (var r in rl)
            {
                var line = new DockPanel();
                var bt = Buttons(r.Kind, r.Call); DockPanel.SetDock(bt, Dock.Right); line.Children.Add(bt);
                line.Children.Add(new TextBlock { Text = $"{Lang.T("s.k." + r.Kind)} · {r.Call}  {VText(r.Verdict, r.User)}  ({r.Pos}+ {r.Neg}−)", FontSize = (11) * UiScale.F, Foreground = B(VCol(r.Verdict)), TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center });
                rules.Children.Add(line);
            }

            list.Children.Clear();
            var items = _st.Suspects.Items();
            head.Text = Lang.F("s.head", items.Count);
            foreach (var it in items.Take(200))
            {
                var r = _st.Suspects.RuleOf(it.Kind, it.Call);
                int v = r?.Verdict ?? 0;
                var sp = new StackPanel { Spacing = 2 };
                var top = new DockPanel();
                var when = TB($"{it.Last:HH:mm}Z · ×{it.Count}", 11, _th.Muted, false, true); DockPanel.SetDock(when, Dock.Right); top.Children.Add(when);
                top.Children.Add(TB($"{Lang.T("s.k." + it.Kind)}  {it.Call}{(it.Other != "" ? " – " + it.Other : "")}", 14, _th.Text, true, true));
                sp.Children.Add(top);
                var meta = string.Join(" · ", new[] { it.Band != "" ? it.Band + " MHz" : "", it.Mode, it.Km > 0 ? $"{it.Km:0} km" : "" }.Where(x => x != ""));
                if (meta != "") sp.Children.Add(TB(meta, 11, _th.Sub, false, true));
                var d = TB(it.Detail, 12, _th.Text); d.TextWrapping = TextWrapping.Wrap; sp.Children.Add(d);
                var foot = new DockPanel();
                if (it.Kind is "eme" or "lungo" or "ascolto" or "locatore") { var bt = Buttons(it.Kind, it.Call); DockPanel.SetDock(bt, Dock.Right); foot.Children.Add(bt); }
                foot.Children.Add(TB(Lang.T("s.rule") + ": " + VText(v, r?.User ?? false), 11, VCol(v)));
                sp.Children.Add(foot);
                list.Children.Add(new Border { Background = B(_th.Chip), CornerRadius = new CornerRadius(8), Padding = new Thickness(12, 8), BorderBrush = B(VCol(v)), BorderThickness = new Thickness(4, 0, 0, 0), Child = sp });
            }
            if (items.Count == 0) list.Children.Add(TB(Lang.T("s.none"), 12, _th.Muted));
        }
        Update();
        _susTimer?.Stop();
        _susTimer = new Avalonia.Threading.DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
        _susTimer.Tick += (_, _) => { if (_tab == "sus") Update(); };
        _susTimer.Start();
        return g;
    }

    // ================================================================ costruzione

    IBrush B(Color c) => new SolidColorBrush(c);
    static readonly FontFamily Mono = new("Consolas, Menlo, DejaVu Sans Mono, monospace");

    TextBlock TB(string text, double size = 13, Color? col = null, bool bold = false, bool mono = false) => new()
    {
        Text = text, FontSize = (size) * UiScale.F, Foreground = B(col ?? _th.Text),
        FontWeight = bold ? FontWeight.SemiBold : FontWeight.Normal,
        FontFamily = mono ? Mono : FontFamily.Default,
        VerticalAlignment = VerticalAlignment.Center
    };

    // ================================================================ dimensione e posizione della finestra
    //
    // All'avvio: se in LIVETROPOVHF.ini c'è una geometria salvata e la barra del titolo ricade ancora in uno schermo, si
    // riusa (con lo stato massimizzato); altrimenti la finestra occupa il 90% dello spazio utile dello schermo (tolta la
    // barra delle applicazioni), centrata. Sotto 1280 × 720 punti logici utili si parte massimizzati. Tutto accanto
    // all'eseguibile, niente registro.

    const double StartFraction = 0.90;
    PixelRect _normalRect;

    Screen ScreenFor(PixelPoint p) =>
        Screens.All.FirstOrDefault(s => s.WorkingArea.Contains(p)) ?? Screens.Primary ?? Screens.All.FirstOrDefault();

    void PlaceAtStartup()
    {
        Width = 1280; Height = 760;                 // ripiego se gli schermi non sono ancora noti
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        try
        {
            var S = _st.S;
            int x = S.GetInt("win_x", int.MinValue), y = S.GetInt("win_y", int.MinValue), w = S.GetInt("win_w", 0), h = S.GetInt("win_h", 0);
            bool max = S.GetBool("win_max", false);
            if (x != int.MinValue && w > 0 && h > 0)
            {
                // la barra del titolo (40 pixel sotto il bordo alto, a metà larghezza) deve ricadere in uno schermo
                var title = new PixelPoint(x + w / 2, y + 20);
                var scr = Screens.All.FirstOrDefault(s => s.WorkingArea.Contains(title));
                if (scr != null)
                {
                    var wa = scr.WorkingArea;
                    w = Math.Min(w, wa.Width); h = Math.Min(h, wa.Height);
                    x = Math.Clamp(x, wa.X, wa.Right - w); y = Math.Clamp(y, wa.Y, wa.Bottom - h);
                    WindowStartupLocation = WindowStartupLocation.Manual;
                    Position = new PixelPoint(x, y);
                    Width = Math.Max(MinWidth, w / scr.Scaling); Height = Math.Max(MinHeight, h / scr.Scaling);
                    _normalRect = new PixelRect(x, y, w, h);
                    if (max) WindowState = WindowState.Maximized;
                    return;
                }
            }
            var sc = Screens.Primary ?? Screens.All.FirstOrDefault();
            if (sc == null) return;
            var a = sc.WorkingArea;
            double lw = a.Width / sc.Scaling, lh = a.Height / sc.Scaling;   // punti logici utili
            if (lw < 1280 || lh < 720)
            {
                Width = Math.Max(MinWidth, Math.Min(lw, 1280)); Height = Math.Max(MinHeight, Math.Min(lh, 720));
                WindowState = WindowState.Maximized;
                return;
            }
            Width = Math.Round(lw * StartFraction); Height = Math.Round(lh * StartFraction);
            int pw = (int)(Width * sc.Scaling), ph = (int)(Height * sc.Scaling);
            WindowStartupLocation = WindowStartupLocation.Manual;
            Position = new PixelPoint(a.X + (a.Width - pw) / 2, a.Y + (a.Height - ph) / 2);
        }
        catch { }
    }

    /// <summary>Dopo l'apertura: se la barra del titolo è fuori dallo spazio utile, la finestra si riporta dentro.</summary>
    void EnsureTitleVisible()
    {
        try
        {
            if (WindowState != WindowState.Normal) return;
            var scr = ScreenFor(new PixelPoint(Position.X + (int)(Bounds.Width / 2), Position.Y + 20));
            if (scr == null) return;
            var wa = scr.WorkingArea;
            int pw = (int)(Bounds.Width * scr.Scaling), ph = (int)(Bounds.Height * scr.Scaling);
            if (pw > wa.Width || ph > wa.Height)
            {
                Width = Math.Max(MinWidth, Math.Min(Bounds.Width, wa.Width / scr.Scaling * StartFraction));
                Height = Math.Max(MinHeight, Math.Min(Bounds.Height, wa.Height / scr.Scaling * StartFraction));
                pw = (int)(Width * scr.Scaling); ph = (int)(Height * scr.Scaling);
            }
            int x = Math.Clamp(Position.X, wa.X, Math.Max(wa.X, wa.Right - pw)), y = Math.Clamp(Position.Y, wa.Y, Math.Max(wa.Y, wa.Bottom - ph));
            if (x != Position.X || y != Position.Y) Position = new PixelPoint(x, y);
        }
        catch { }
    }

    void TrackNormal()
    {
        if (WindowState != WindowState.Normal || !IsVisible) return;
        try
        {
            var sc = ScreenFor(Position)?.Scaling ?? 1;
            _normalRect = new PixelRect(Position.X, Position.Y, (int)(Bounds.Width * sc), (int)(Bounds.Height * sc));
        }
        catch { }
    }

    void SaveGeometry()
    {
        try
        {
            TrackNormal();
            if (_normalRect.Width <= 0) return;
            var S = _st.S;
            S.SetInt("win_x", _normalRect.X); S.SetInt("win_y", _normalRect.Y);
            S.SetInt("win_w", _normalRect.Width); S.SetInt("win_h", _normalRect.Height);
            S.Set("win_max", WindowState == WindowState.Maximized ? "1" : "0");
        }
        catch { }
    }

    TextBox Pill(string text, double width, string watermark = "", bool mono = true) => new()
    {
        Text = text, Width = width, Watermark = watermark, FontFamily = mono ? Mono : FontFamily.Default,
        CornerRadius = new CornerRadius(16), Background = B(_th.CtlBg), Foreground = B(_th.Text), BorderBrush = B(_th.Ctl),
        VerticalAlignment = VerticalAlignment.Center, MinHeight = 32
    };

    Border Card(Control child, Thickness margin) => new()
    {
        Background = new SolidColorBrush(Color.FromArgb(242, _th.Panel.R, _th.Panel.G, _th.Panel.B)),
        BorderBrush = B(_th.Line), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(10),
        Padding = new Thickness(12, 8), Margin = margin, Child = child
    };

    Button PrimaryButton(string text, Action click)
    {
        var b = new Button { Content = text, Background = B(_th.Accent), Foreground = B(_th.AccentFg), CornerRadius = new CornerRadius(16), Padding = new Thickness(18, 6), FontWeight = FontWeight.SemiBold };
        b.Click += (_, _) => click();
        return b;
    }

    Button OutlineButton(string text, Action click)
    {
        var b = new Button { Content = text, Background = B(_th.CtlBg), Foreground = B(_th.Text2), BorderBrush = B(_th.Ctl), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(16), Padding = new Thickness(18, 6) };
        b.Click += (_, _) => click();
        return b;
    }

    CheckBox Check(string text, bool value, Action<bool> changed)
    {
        var c = new CheckBox { Content = text, IsChecked = value, Foreground = B(_th.Text), VerticalAlignment = VerticalAlignment.Center };
        // la scelta si salva subito: se il programma non si chiude in modo ordinato (riavvio per aggiornamento, spegnimento) non va persa
        c.Click += (_, _) => { changed(c.IsChecked == true); try { _st.Save(); } catch { } };
        return c;
    }

    void BuildUi()
    {
        if (Application.Current != null) Application.Current.RequestedThemeVariant = _th.Dark ? ThemeVariant.Dark : ThemeVariant.Light;
        Background = B(_th.Page);
        FontSize = 14 * UiScale.F;     // controlli senza dimensione propria (caselle, menu, pulsanti)
        (_map.Parent as Panel)?.Children.Remove(_map);
        _map.Th = _th;
        _sections.Clear(); _tabContent.Clear(); _tabButtons.Clear(); _tropoTabs.Clear();

        var root = new DockPanel { LastChildFill = true };
        root.FlowDirection = Lang.IsRtl(_st.LangCode) ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

        var top = BuildTopRow(); DockPanel.SetDock(top, Dock.Top); root.Children.Add(top);
        var tabs = BuildTabBand(); DockPanel.SetDock(tabs, Dock.Top); root.Children.Add(tabs);
        var status = BuildStatusBar(); DockPanel.SetDock(status, Dock.Bottom); root.Children.Add(status);

        var content = new Grid();
        _tabContent["map"] = BuildMapTab();
        _tabContent["hepburn"] = BuildTropoTab("hepburn");
        _tabContent["f5len"] = BuildTropoTab("f5len");
        _tabContent["fc"] = BuildForecastTab();
        _tabContent["graves"] = BuildGravesTab();
        _tabContent["sus"] = BuildSuspectsTab();
        _tabContent["collect"] = BuildCollectTab();
        _tabContent["doc1"] = DocView.Build(DocView.LoadResource("funzionamento.md"), _th, "LIVETROPOVHF - Funzionamento del programma");
        _tabContent["doc2"] = DocView.Build(DocView.LoadResource("mappa.md"), _th, "LIVETROPOVHF - Costruzione della mappa");
        _tabContent["doc3"] = DocView.Build(DocView.LoadResource("tropo.md"), _th, "LIVETROPOVHF - Come si forma la tropo");
        _tabContent["doc4"] = DocView.Build(DocView.LoadResource("previsioni.md"), _th, "LIVETROPOVHF - Manuale delle previsioni");
        _tabContent["doc5"] = DocView.Build(DocView.LoadResource("consigli.md"), _th, "LIVETROPOVHF - Consigli per l'operatore");
        _tabContent["doc6"] = DocView.Build(DocView.LoadResource("fonti.md"), _th, "LIVETROPOVHF - Fonti e formule");
        foreach (var kv in _tabContent) content.Children.Add(kv.Value);
        root.Children.Add(content);

        Content = root;
        ShowTab(_tab);
        Tick();
        RefreshSpotList();
        RefreshInfoCard();
        RefreshLogs(true);
    }

    Control BuildTopRow()
    {
        var row = new DockPanel { Background = B(_th.Bar), Height = 48, LastChildFill = false };
        var left = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Margin = new Thickness(10, 0), VerticalAlignment = VerticalAlignment.Center };
        left.Children.Add(TB(Lang.T("call"), 12, _th.Muted));
        var call = Pill(_st.Call, 110, "IC8TEM");
        call.TextChanged += (_, _) => { _st.Call = (call.Text ?? "").ToUpperInvariant(); _st.Dirty = true; };
        left.Children.Add(call);
        left.Children.Add(TB(Lang.T("loc"), 12, _th.Muted));
        _locBox = Pill(_st.Loc, 130, "JN70CN83CL");
        _locBox.MaxLength = 10;
        _locBox.TextChanged += (_, _) =>
        {
            _st.Loc = (_locBox.Text ?? "").ToUpperInvariant();
            _locBox.BorderBrush = B(_st.Loc.Length > 0 && !Locator.IsValid(_st.Loc) ? Color.Parse("#D64545") : _th.Ctl);
            _st.Dirty = true;
            _map.InvalidateVisual();
            FcSyncLoc();
        };
        left.Children.Add(_locBox);
        DockPanel.SetDock(left, Dock.Left);
        row.Children.Add(left);

        var right = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10, Margin = new Thickness(10, 0), VerticalAlignment = VerticalAlignment.Center };
        _clock = TB("", 30, _th.Clock, true, true);
        _ntp = TB("NTP", 30, _th.Muted, true, true);
        _clock.LineHeight = 40 * UiScale.F; _ntp.LineHeight = 40 * UiScale.F;
        right.Children.Add(_clock);
        right.Children.Add(_ntp);
        var theme = new ComboBox { Width = 180, VerticalAlignment = VerticalAlignment.Center };
        var themeNames = Palette.Keys.Select(k => Lang.T("theme." + k)).ToArray();
        foreach (var n in themeNames) theme.Items.Add(n);
        theme.SelectedIndex = Math.Max(0, Array.IndexOf(Palette.Keys, _st.ThemeKey));
        theme.SelectionChanged += (_, _) =>
        {
            var k = Palette.Keys[Math.Max(0, theme.SelectedIndex)];
            if (k == _st.ThemeKey) return;
            _st.ThemeKey = k; _th = Palette.Get(k);
            Dispatcher.UIThread.Post(BuildUi);   // nessun riavvio: si ridisegna la finestra
        };
        right.Children.Add(theme);
        // scala dei testi 80–150%, a passi di 10: si applica rilasciando il cursore (la finestra si ridisegna)
        var scLbl = TB($"{UiScale.Percent}%", 12, _th.Muted);
        var sc = new Slider { Minimum = 80, Maximum = 150, TickFrequency = 10, IsSnapToTickEnabled = true, Value = UiScale.Percent, Width = 110, VerticalAlignment = VerticalAlignment.Center };
        ToolTip.SetTip(sc, Lang.Tr("Dimensione dei testi (80–150%)", "Text size (80–150%)"));
        DispatcherTimer scTimer = null;
        sc.PropertyChanged += (_, e) =>
        {
            if (e.Property != RangeBase.ValueProperty) return;
            int v = (int)Math.Round(sc.Value);
            scLbl.Text = $"{v}%";
            scTimer?.Stop();
            scTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(700) };
            scTimer.Tick += (_, _) =>
            {
                scTimer.Stop();
                if (v == UiScale.Percent) return;
                UiScale.Percent = v; _st.S.SetInt("ui_scale", v); _st.Dirty = true;
                Dispatcher.UIThread.Post(BuildUi);
            };
            scTimer.Start();
        };
        right.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 4, VerticalAlignment = VerticalAlignment.Center, Children = { TB("A", 11, _th.Muted), sc, TB("A", 17, _th.Muted), scLbl } });
        var lang = new ComboBox { Width = 160, VerticalAlignment = VerticalAlignment.Center };
        foreach (var (_, name) in Lang.All) lang.Items.Add(name);
        lang.SelectedIndex = Math.Max(0, Array.FindIndex(Lang.All, x => x.code == _st.LangCode));
        lang.SelectionChanged += (_, _) =>
        {
            var code = Lang.All[Math.Max(0, lang.SelectedIndex)].code;
            if (code == _st.LangCode) return;
            _st.LangCode = code; Lang.Current = code;
            Dispatcher.UIThread.Post(BuildUi);
        };
        right.Children.Add(lang);
        DockPanel.SetDock(right, Dock.Right);
        row.Children.Add(right);
        return new Border { Child = row, BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 0, 0, 1) };
    }

    // ---------------------------------------------------------------- linguette in 5 gruppi con sotto-linguette

    /// <summary>Gruppi di linguette: (id, voci). Ogni voce è "linguetta" oppure "collect:sottolinguetta".</summary>
    static readonly (string id, string[] items)[] Groups =
    {
        ("map", new[] { "map" }),
        ("prev", new[] { "fc", "hepburn", "f5len" }),
        ("graves", new[] { "graves" }),
        ("data", new[] { "collect:dx", "collect:psk", "collect:rbn", "collect:udp", "collect:calls", "sus" }),
        ("guide", new[] { "doc1", "doc2", "doc3", "doc4", "doc5", "doc6" }),
    };
    readonly Dictionary<string, Button> _subTabButtons = new();
    StackPanel _subBarItems; Border _subBar; TextBox _guideSearch;
    string _shownGroup = "";

    static string GroupOf(string item) => Groups.First(g => g.items.Contains(item) || g.items.Any(x => x.StartsWith(item + ":"))).id;
    string CurrentItem => _tab == "collect" ? "collect:" + _subTab : _tab;

    string GroupLabel(string g)
    {
        return g switch
        {
            "map" => Lang.T("tab.map"),
            "prev" => Lang.Tr("Previsioni", "Forecasts"),
            "graves" => Lang.T("tab.graves"),
            "data" => Lang.Tr("Dati e fonti", "Data & sources"),
            _ => Lang.Tr("Guide", "Guides"),
        };
    }

    string ItemLabel(string item)
    {
        return item switch
        {
            "fc" => Lang.Tr("Propria (β)", "Own (β)"),
            "hepburn" => Lang.T("tab.hep"),
            "f5len" => Lang.T("tab.f5"),
            "collect:dx" => "DX cluster",
            "collect:psk" => "PSKReporter",
            "collect:rbn" => "Reverse Beacon",
            "collect:udp" => "UDP (WSJT-X, JTDX, MSHV)",
            "collect:calls" => Lang.T("c.tab"),
            "sus" => Lang.T("tab.sus"),
            "doc4" => Lang.Tr("Manuale previsioni", "Forecast manual"),
            "doc5" => Lang.Tr("Consigli all'operatore", "Operator tips"),
            "doc6" => Lang.Tr("Fonti e formule", "Sources and formulas"),
            _ => Lang.T("tab." + item),
        };
    }

    /// <summary>Va a una linguetta (e, per la raccolta dati, a una sotto-linguetta).</summary>
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
        _subTabButtons.Clear();
        var band = new DockPanel { Background = B(_th.Band), Height = 48, LastChildFill = false };
        var sp = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 4, Margin = new Thickness(10, 0, 0, 0), VerticalAlignment = VerticalAlignment.Bottom };
        foreach (var (g, items) in Groups)
        {
            var b = new Button { Content = GroupLabel(g), Height = 40, Padding = new Thickness(18, 0), CornerRadius = new CornerRadius(8, 8, 0, 0), FontSize = (14) * UiScale.F, VerticalContentAlignment = VerticalAlignment.Center };
            // ogni gruppo riapre l'ultima sotto-linguetta usata
            b.Click += (_, _) => { var last = _st.S.Get("grp_" + g, items[0]); GoToItem(items.Contains(last) ? last : items[0]); };
            _tabButtons[g] = b;
            sp.Children.Add(b);
        }
        DockPanel.SetDock(sp, Dock.Left);
        band.Children.Add(sp);
        var note = TB(Lang.T("tab.note"), 12, Color.FromArgb(200, _th.BandFg.R, _th.BandFg.G, _th.BandFg.B));
        note.Margin = new Thickness(0, 0, 14, 0);
        DockPanel.SetDock(note, Dock.Right);
        band.Children.Add(note);

        // barra delle sotto-linguette, sotto il fascione (solo per i gruppi con più voci)
        _subBarItems = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 4, Margin = new Thickness(10, 6, 10, 6) };
        _guideSearch = new TextBox { Width = 220, Watermark = Lang.Tr("Cerca nelle guide…", "Search the guides…"), Margin = new Thickness(16, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
        _guideSearch.TextChanged += (_, _) => GuideSearch(false);
        _guideSearch.KeyDown += (_, e) => { if (e.Key == Avalonia.Input.Key.Enter) GuideSearch(true); };
        _subBar = new Border { Background = B(_th.Page), BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 0, 0, 1),
            Child = new ScrollViewer { HorizontalScrollBarVisibility = ScrollBarVisibility.Auto, VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
                Content = new StackPanel { Orientation = Orientation.Horizontal, Children = { _subBarItems, _guideSearch } } } };
        _shownGroup = "";
        return new StackPanel { Children = { band, _subBar } };
    }

    static readonly Dictionary<string, string> _docFiles = new() { ["doc1"] = "funzionamento.md", ["doc2"] = "mappa.md", ["doc3"] = "tropo.md", ["doc4"] = "previsioni.md", ["doc5"] = "consigli.md", ["doc6"] = "fonti.md" };
    readonly Dictionary<string, string> _docText = new();

    /// <summary>Ricerca nelle guide: ogni sotto-linguetta mostra quante volte compare il testo; Invio apre la prima che lo contiene.</summary>
    void GuideSearch(bool open)
    {
        var q = (_guideSearch?.Text ?? "").Trim();
        string first = null;
        foreach (var kv in _docFiles)
        {
            if (!_docText.TryGetValue(kv.Key + "|" + Lang.Current, out var txt)) _docText[kv.Key + "|" + Lang.Current] = txt = DocView.LoadResource(kv.Value) ?? "";
            int n = 0;
            if (q.Length >= 3) for (int i = txt.IndexOf(q, StringComparison.OrdinalIgnoreCase); i >= 0; i = txt.IndexOf(q, i + q.Length, StringComparison.OrdinalIgnoreCase)) n++;
            if (n > 0 && first == null) first = kv.Key;
            if (_subTabButtons.TryGetValue(kv.Key, out var b)) b.Content = ItemLabel(kv.Key) + (n > 0 ? $" ({n})" : "");
        }
        if (open && first != null) GoTo(first);
    }

    void RefreshSubBar()
    {
        if (_subBar == null) return;
        var g = GroupOf(CurrentItem);
        var items = Groups.First(x => x.id == g).items;
        if (g != _shownGroup)
        {
            _shownGroup = g;
            _subBarItems.Children.Clear(); _subTabButtons.Clear();
            foreach (var it in items)
            {
                var b = new Button { Content = ItemLabel(it), Padding = new Thickness(16, 5), CornerRadius = new CornerRadius(16), FontSize = (13) * UiScale.F };
                var target = it;
                b.Click += (_, _) => GoToItem(target);
                _subTabButtons[it] = b;
                _subBarItems.Children.Add(b);
            }
            _guideSearch.IsVisible = g == "guide";
            if (g == "guide") GuideSearch(false);
        }
        _subBar.IsVisible = items.Length > 1;
        foreach (var kv in _subTabButtons)
        {
            bool on = kv.Key == CurrentItem;
            kv.Value.Background = on ? B(_th.Accent) : B(_th.Chip);
            kv.Value.Foreground = on ? B(_th.AccentFg) : B(_th.Text);
            kv.Value.FontWeight = on ? FontWeight.SemiBold : FontWeight.Normal;
        }
        _st.S.Set("grp_" + g, CurrentItem);
    }

    void ShowTab(string key)
    {
        _tab = key;
        ErrorLog.CurrentTab = key;
        foreach (var kv in _tabContent) kv.Value.IsVisible = kv.Key == key;
        var grp = GroupOf(key == "collect" ? "collect:" + _subTab : key);
        foreach (var kv in _tabButtons)
        {
            bool on = kv.Key == grp;
            kv.Value.Background = on ? B(_th.Page) : Brushes.Transparent;
            kv.Value.Foreground = on ? B(_th.Text) : B(_th.BandFg);
            kv.Value.FontWeight = on ? FontWeight.SemiBold : FontWeight.Normal;
        }
        if (key == "hepburn" || key == "f5len") FillTropoTab(key);
        RefreshSubBar();
        RefreshLogs(true);
    }

    Control BuildStatusBar()
    {
        // segmenti: le fonti sono cliccabili (portano alla fonte) e il fumetto dice perché sono spente
        _statusSegs.Clear();
        _statusPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(12, 0), VerticalAlignment = VerticalAlignment.Center };
        TextBlock Seg(string id, Action click, string tip = null)
        {
            var t = TB("", 12, _th.Muted);
            t.Margin = new Thickness(0, 0, 18, 0);
            if (click != null) { t.Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Hand); t.PointerPressed += (_, _) => click(); }
            if (tip != null) ToolTip.SetTip(t, tip);
            _statusSegs[id] = t; _statusPanel.Children.Add(t);
            return t;
        }
        foreach (var f in _st.Feeds)
        {
            var sub = f.Src == "RBN" ? "rbn" : f.Src == "PSK" ? "psk" : "dx";
            Seg("feed:" + f.Id, () => GoTo("collect", sub));
        }
        Seg("mqtt", () => GoTo("collect", "psk"));
        Seg("udp", () => GoTo("collect", "udp"));
        Seg("pressure", null); Seg("tropo", null); Seg("call3", () => GoTo("collect", "calls"));
        Seg("other", null);
        Seg("error", () => OpenUrl(ErrorLog.FilePath), Lang.Tr("Clic: apri il registro degli errori", "Click: open the error log"));
        Seg("version", () => { Updater.CheckNow(); }, Lang.Tr("Clic: cerca aggiornamenti adesso", "Click: check for updates now"));
        _statusBar = _statusSegs["version"];
        return new Border { Background = B(_th.Bar), BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 1, 0, 0), Height = 30, ClipToBounds = true, Child = _statusPanel };
    }

    // ---------------------------------------------------------------- linguetta mappa

    Control BuildMapTab()
    {
        var g = new Grid { ColumnDefinitions = new ColumnDefinitions("65*,35*") };
        var mapPanel = new Grid { ClipToBounds = true };
        _map.FlowDirection = FlowDirection.LeftToRight;
        mapPanel.Children.Add(_map);

        // barra dei layer
        var layers = new WrapPanel { Orientation = Orientation.Horizontal };
        layers.Children.Add(new TextBlock { Text = Lang.T("layers"), FontSize = (11) * UiScale.F, FontWeight = FontWeight.SemiBold, Foreground = B(_th.Muted), VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 10, 0) });
        void Layer(string key, bool v, Color sw, Action<bool> set)
        {
            var sp = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 5, Margin = new Thickness(0, 0, 12, 0) };
            sp.Children.Add(Check("", v, x => { set(x); _map.InvalidateVisual(); }));
            sp.Children.Add(new Border { Width = 14, Height = 4, CornerRadius = new CornerRadius(2), Background = B(sw), VerticalAlignment = VerticalAlignment.Center });
            sp.Children.Add(TB(Lang.T(key), 13));
            layers.Children.Add(sp);
        }
        Layer("l.base", _st.ShowBase, _th.MapCoast, x => _st.ShowBase = x);
        Layer("l.grid", _st.ShowGrid, _th.MapGrid, x => _st.ShowGrid = x);
        Layer("l.tropo", _st.ShowTropo, Color.Parse("#E8D24A"), x => _st.ShowTropo = x);
        Layer("l.iso", _st.ShowIso, _th.Iso, x => _st.ShowIso = x);
        Layer("l.t850", _st.ShowT850, Color.Parse("#D8452E"), x => _st.ShowT850 = x);
        Layer("l.inv", _st.ShowInv, Color.Parse("#FF7A00"), x => _st.ShowInv = x);
        Layer("l.rx", _st.ShowRx, Palette.ListenerViolet, x => _st.ShowRx = x);
        Layer("l.dec", _st.ShowDec, Palette.DecodeRed, x => _st.ShowDec = x);
        Layer("l.qso", _st.ShowQso, Palette.QsoGreen, x => _st.ShowQso = x);
        Layer("l.spots", _st.ShowSpots, Palette.SpotBlue, x => _st.ShowSpots = x);
        // criteri di esclusione generale
        Layer("x.short", _st.HideShort, _th.Muted, x => { _st.HideShort = x; RefreshSpotList(); });
        Layer("x.long", _st.LongAsSquares, Palette.LongLilac, x => { _st.LongAsSquares = x; RefreshSpotList(); });
        // raggio della corona: 1000, 700 o 500 km
        var ring = new ComboBox { Width = 110, Margin = new Thickness(0, 0, 12, 0), VerticalAlignment = VerticalAlignment.Center };
        foreach (var k in AppState.RingKm) ring.Items.Add($"{k} km");
        ring.SelectedIndex = Math.Max(0, Array.IndexOf(AppState.RingKm, (int)_st.LongKm));
        ring.SelectionChanged += (_, _) => { if (ring.SelectedIndex >= 0) { _st.LongKm = AppState.RingKm[ring.SelectedIndex]; _st.Dirty = true; RefreshSpotList(); _map.InvalidateVisual(); } };
        layers.Children.Add(ring);
        Layer("x.rings", _st.ShowRings, Palette.LongLilac, x => _st.ShowRings = x);

        var row2 = new WrapPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 6, 0, 0) };
        var srcGroup = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 14, 0) };
        var srcButtons = new List<(string key, ToggleButton b)>();
        foreach (var (key, label) in new[] { ("media", "src.avg"), ("hepburn", "src.hep"), ("f5len", "src.f5") })
        {
            var tb = new ToggleButton { Content = Lang.T(label), IsChecked = _st.TropoSrc == key, Padding = new Thickness(12, 3), FontSize = (12) * UiScale.F, CornerRadius = new CornerRadius(0) };
            tb.Click += (_, _) =>
            {
                _st.TropoSrc = key;
                foreach (var (k, b) in srcButtons) b.IsChecked = k == key;
                _map.InvalidateVisual(); _legendSrc.Text = SrcName(); RefreshInfoCard();
            };
            srcButtons.Add((key, tb));
            srcGroup.Children.Add(tb);
        }
        row2.Children.Add(srcGroup);
        row2.Children.Add(TB(Lang.T("opacity"), 12, _th.Muted));
        var op = new Slider { Minimum = 10, Maximum = 100, Value = _st.TropoOpacity * 100, Width = 120, Margin = new Thickness(8, 0, 14, 0), VerticalAlignment = VerticalAlignment.Center };
        op.PropertyChanged += (_, e) => { if (e.Property == RangeBase.ValueProperty) { _st.TropoOpacity = op.Value / 100; _map.InvalidateVisual(); } };
        row2.Children.Add(op);
        row2.Children.Add(TB(Lang.T("proj"), 12, _th.Muted));
        var proj = new ComboBox { Width = 210, Margin = new Thickness(8, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
        proj.Items.Add(Lang.T("proj.merc")); proj.Items.Add(Lang.T("proj.azi"));
        proj.SelectedIndex = _st.Proj == "azi" ? 1 : 0;
        proj.SelectionChanged += (_, _) => { _st.Proj = proj.SelectedIndex == 1 ? "azi" : "merc"; _map.InvalidateVisual(); };
        row2.Children.Add(proj);

        var layerCard = Card(new StackPanel { Children = { layers, row2 } }, new Thickness(10, 8, 10, 0));
        layerCard.VerticalAlignment = VerticalAlignment.Top;
        mapPanel.Children.Add(layerCard);

        // scheda informazioni (spot, ascolto, punto)
        _infoBody = new StackPanel { Spacing = 4 };
        _infoCard = Card(_infoBody, new Thickness(0, 118, 10, 0));
        _infoCard.Width = 320; _infoCard.HorizontalAlignment = HorizontalAlignment.Right; _infoCard.VerticalAlignment = VerticalAlignment.Top;
        mapPanel.Children.Add(_infoCard);

        // zoom
        var zoom = new StackPanel { Spacing = 0 };
        Button ZB(string t, Action a) { var b = new Button { Content = t, Width = 38, Height = 36, HorizontalContentAlignment = HorizontalAlignment.Center, FontWeight = FontWeight.SemiBold, CornerRadius = new CornerRadius(0) }; b.Click += (_, _) => a(); return b; }
        var center = new Func<Point>(() => new Point(_map.Bounds.Width / 2, _map.Bounds.Height / 2));
        zoom.Children.Add(ZB("+", () => { _map.ZoomAt(1.4, center()); _zoomPct.Text = _map.ZoomPercent; }));
        zoom.Children.Add(ZB("−", () => { _map.ZoomAt(1 / 1.4, center()); _zoomPct.Text = _map.ZoomPercent; }));
        _zoomPct = TB("100%", 11, _th.Text, true);
        var zr = new Button { Content = _zoomPct, Width = 38, Height = 36, HorizontalContentAlignment = HorizontalAlignment.Center, CornerRadius = new CornerRadius(0) };
        zr.Click += (_, _) => { _map.ResetView(); _zoomPct.Text = _map.ZoomPercent; };
        zoom.Children.Add(zr);
        var zoomCard = new Border { Child = zoom, CornerRadius = new CornerRadius(10), ClipToBounds = true, BorderBrush = B(_th.Line), BorderThickness = new Thickness(1), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(0, 0, 10, 122) };
        mapPanel.Children.Add(zoomCard);

        // barra temporale e legenda
        var fcRow = new DockPanel { LastChildFill = true };
        var fcLeft = new StackPanel { Width = 210, VerticalAlignment = VerticalAlignment.Center };
        fcLeft.Children.Add(TB(Lang.T("forecast"), 11, _th.Muted, true));
        _fcLabel = TB("", 13, _th.Text, true, true);
        fcLeft.Children.Add(_fcLabel);
        DockPanel.SetDock(fcLeft, Dock.Left); fcRow.Children.Add(fcLeft);
        var legend = new StackPanel { Width = 200, Spacing = 2, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(14, 0, 0, 0) };
        _legendSrc = TB(SrcName(), 11, _th.Muted);
        legend.Children.Add(_legendSrc);
        var grad = new LinearGradientBrush { StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative), EndPoint = new RelativePoint(1, 0, RelativeUnit.Relative) };
        foreach (var (o, c) in new[] { (0.0, "#3B82C4"), (0.3, "#5BC07A"), (0.52, "#E8D24A"), (0.68, "#F08A3C"), (0.84, "#E5533D"), (1.0, "#B05AD8") })
            grad.GradientStops.Add(new GradientStop(Color.Parse(c), o));
        legend.Children.Add(new Border { Height = 8, CornerRadius = new CornerRadius(4), Background = grad });
        var ticks = new Grid { ColumnDefinitions = new ColumnDefinitions("*,*,*,*,*,*") };
        for (int i = 0; i <= 5; i++) { var t = TB((i * 2).ToString(), 10, _th.Muted, false, true); Grid.SetColumn(t, i); t.HorizontalAlignment = i == 0 ? HorizontalAlignment.Left : i == 5 ? HorizontalAlignment.Right : HorizontalAlignment.Center; ticks.Children.Add(t); }
        legend.Children.Add(ticks);
        DockPanel.SetDock(legend, Dock.Right); fcRow.Children.Add(legend);
        var fcSlider = new Slider { Minimum = 0, Maximum = 192, TickFrequency = 3, IsSnapToTickEnabled = true, Value = _st.ForecastH, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(6, 0) };
        fcSlider.PropertyChanged += (_, e) => { if (e.Property == RangeBase.ValueProperty) { _st.ForecastH = (int)Math.Round(fcSlider.Value); UpdateFcLabel(); _map.InvalidateVisual(); } };
        fcRow.Children.Add(fcSlider);

        var legRow = new WrapPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 4, 0, 0) };
        void Leg(Color col, bool dashed, string text, Color? edge = null)
        {
            var sp = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 6, Margin = new Thickness(0, 0, 16, 0) };
            var line = new Border { Width = 28, Height = dashed ? 5 : 3, Background = B(edge ?? col), VerticalAlignment = VerticalAlignment.Center, CornerRadius = new CornerRadius(2), Child = dashed ? new Border { Height = 2.5, Margin = new Thickness(0, 1), Background = B(col) } : null };
            sp.Children.Add(line);
            sp.Children.Add(TB(text, 12, _th.Sub));
            legRow.Children.Add(sp);
        }
        Leg(Palette.SpotBlue, false, Lang.T("legend.spot"));
        Leg(Palette.AirscatterYellow, true, Lang.T("legend.as"), Palette.AirscatterEdge);
        Leg(Palette.ListenerViolet, false, Lang.T("legend.rx"));
        Leg(Palette.ListenerLine, false, Lang.T("u.what_they_hear_and"));
        Leg(Palette.DecodeRed, false, Lang.T("legend.dec"));
        Leg(Palette.QsoGreen, false, Lang.T("l.qso"));
        _obsNote = TB("", 12, _th.Muted);
        legRow.Children.Add(_obsNote);

        var bottom = Card(new StackPanel { Spacing = 4, Children = { fcRow, legRow, TB(Lang.T("legend.help"), 11, _th.Muted) } }, new Thickness(10, 0, 10, 8));
        bottom.VerticalAlignment = VerticalAlignment.Bottom;
        mapPanel.Children.Add(bottom);

        Grid.SetColumn(mapPanel, 0);
        g.Children.Add(mapPanel);

        var list = BuildSpotList();
        Grid.SetColumn(list, 1);
        g.Children.Add(list);
        UpdateFcLabel();
        return g;
    }

    string SrcName() => _st.TropoSrc switch
    {
        "hepburn" => Lang.T("l.tropo") + ": Hepburn",
        "f5len" => Lang.T("l.tropo") + ": F5LEN",
        _ => Lang.T("l.tropo") + ": " + (Lang.T("u.hepburn_f5len_average"))
    };

    void UpdateFcLabel()
    {
        if (_fcLabel == null) return;
        var t = Clock.UtcNow.AddHours(_st.ForecastH);
        var days = Lang.DayNames();
        _fcLabel.Text = $"{days[(int)t.DayOfWeek]} {t:dd/MM HH}:00 UTC (+{_st.ForecastH} h)";
        if (_obsNote != null) _obsNote.Text = Lang.T(_st.ForecastH == 0 ? "obs.now" : "obs.dim");
    }

    Control BuildSpotList()
    {
        var dock = new DockPanel { Background = B(_th.Panel), LastChildFill = true };
        var head = new DockPanel { Margin = new Thickness(14, 10), LastChildFill = true };
        var selectors = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };
        var band = new ComboBox { Width = 112 };
        foreach (var b in Bands.All) band.Items.Add(Bands.Label(b));
        band.SelectedIndex = Math.Max(0, Array.IndexOf(Bands.All, _st.Band));
        band.SelectionChanged += (_, _) =>
        {
            _st.Band = Bands.All[Math.Max(0, band.SelectedIndex)];
            // si azzera la vista (selezioni, punto, scheda) e si disegna ciò che è già in memoria per la nuova banda
            _map.SelectedSpotId = null; _map.SelectedListener = null; _map.Probe = null;
            _spotRows.Children.Clear();
            RefreshAll();
        };
        var win = new ComboBox { Width = 92 };
        int[] wins = { 3, 5, 10, 15, 30, 60 };
        foreach (var w in wins) win.Items.Add(w + " min");
        win.SelectedIndex = Math.Max(0, Array.IndexOf(wins, _st.WindowMin));
        win.SelectionChanged += (_, _) => { _st.WindowMin = wins[Math.Max(0, win.SelectedIndex)]; RefreshAll(); };
        var refresh = new Button { Content = "⟳", Width = 38, CornerRadius = new CornerRadius(16) };
        ToolTip.SetTip(refresh, Lang.T("refresh"));
        refresh.Click += (_, _) => RefreshAll();
        selectors.Children.Add(new StackPanel { Children = { TB(Lang.T("band"), 11, _th.Muted), band } });
        selectors.Children.Add(new StackPanel { Children = { TB(Lang.T("last"), 11, _th.Muted), win } });
        selectors.Children.Add(new StackPanel { Children = { TB(" ", 11), refresh } });
        DockPanel.SetDock(selectors, Dock.Right);
        head.Children.Add(selectors);
        head.Children.Add(TB(Lang.T("spots.title"), 15, _th.Text, true));
        DockPanel.SetDock(head, Dock.Top);
        dock.Children.Add(new Border { Child = head, BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 0, 0, 1), [DockPanel.DockProperty] = Dock.Top });

        var counters = new WrapPanel { Margin = new Thickness(14, 8) };
        Border Counter(out TextBlock value, string label, Color? col = null)
        {
            value = TB("0", 16, col ?? _th.Text, true, true);
            return new Border { Background = B(_th.Chip), CornerRadius = new CornerRadius(6), Padding = new Thickness(10, 4), Margin = new Thickness(0, 0, 8, 6), Child = new StackPanel { Children = { value, TB(Lang.T(label), 11, _th.Muted) } } };
        }
        counters.Children.Add(Counter(out _nSpots, "n.spots"));
        counters.Children.Add(Counter(out _nAs, "n.as", _th.AsText));
        counters.Children.Add(Counter(out _nExcl, "n.excl"));
        counters.Children.Add(Counter(out _nRx, "n.rx"));
        counters.Children.Add(Counter(out _nDec, "n.dec", _th.Dark ? Color.Parse("#FF7A7A") : Color.Parse("#B01E2E")));
        counters.Children.Add(Counter(out _nHeld, "n.held", _th.Muted));
        // pulsante MQTT: stato del flusso PSKReporter e ascoltatori scartati; clic = elenco degli scartati con il motivo
        _mqttDot = new Avalonia.Controls.Shapes.Ellipse { Width = 10, Height = 10, Fill = B(_th.Muted), VerticalAlignment = VerticalAlignment.Center };
        _mqttCount = TB("", 11, _th.Muted);
        var mqttBtn = new Button
        {
            Background = B(_th.Chip), CornerRadius = new CornerRadius(6), Padding = new Thickness(10, 4), Margin = new Thickness(0, 0, 8, 6), VerticalAlignment = VerticalAlignment.Stretch,
            Content = new StackPanel { Children = {
                new StackPanel { Orientation = Orientation.Horizontal, Spacing = 6, Children = { _mqttDot, TB("MQTT", 16, _th.Text, true, true) } },
                _mqttCount } }
        };
        ToolTip.SetTip(mqttBtn, Lang.T("u.mqtt_discarded_title"));
        mqttBtn.Click += (_, _) =>
        {
            _showMqttDiscarded = !_showMqttDiscarded;
            if (_showMqttDiscarded) { _map.SelectedListener = null; _map.SelectedSpotId = null; _map.Probe = null; _map.InvalidateVisual(); }
            RefreshInfoCard();
        };
        counters.Children.Add(mqttBtn);
        dock.Children.Add(new Border { Child = counters, BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 0, 0, 1), [DockPanel.DockProperty] = Dock.Top });

        var info = new StackPanel { Margin = new Thickness(14, 6) };
        info.Children.Add(new TextBlock { Text = Lang.T("filter"), FontSize = (11) * UiScale.F, Foreground = B(_th.Muted), TextWrapping = TextWrapping.Wrap });
        info.Children.Add(new TextBlock { Text = Lang.T("asnote"), FontSize = (11) * UiScale.F, Foreground = B(_th.Muted), TextWrapping = TextWrapping.Wrap });
        dock.Children.Add(new Border { Child = info, BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 0, 0, 1), [DockPanel.DockProperty] = Dock.Top });

        _spotRows = new StackPanel();
        _emptyText = new TextBlock { Text = Lang.T("empty"), Foreground = B(_th.Muted), TextWrapping = TextWrapping.Wrap, Margin = new Thickness(14, 30), HorizontalAlignment = HorizontalAlignment.Center, TextAlignment = TextAlignment.Center };
        var listPanel = new StackPanel { Children = { _emptyText, _spotRows } };
        dock.Children.Add(new ScrollViewer { Content = listPanel });
        return new Border { Child = dock, BorderBrush = B(_th.Line), BorderThickness = new Thickness(1, 0, 0, 0) };
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
        if (_spotRows == null) return;
        var now = Clock.UtcNow;
        var spots = _st.Spots.Filter(_st.Band, _st.WindowMin, now);
        _spotRows.Children.Clear();
        int shownRows = 0;
        foreach (var s in spots)
        {
            if (shownRows >= 300) break;
            bool sel = _map.SelectedSpotId == s.Id;
            bool hasPos = _st.TryPos(s.Dx, s.DxLoc, out var dx) & _st.TryPos(s.Spotter, s.DeLoc, out var de);
            int dc = hasPos ? _st.DistClass(dx, de) : 0;
            if (dc < 0) continue;                       // sotto i 100 km con la casella accesa: non si elenca
            shownRows++;
            string km = hasPos ? Geo.DistKm(dx, de).ToString("0") + (dc > 0 ? " ▦" : "") : "—";
            var col = s.Airscatter ? Palette.AirscatterYellow : Palette.SpotBlue;
            var g = new Grid { ColumnDefinitions = new ColumnDefinitions("30,52,*,52,52"), RowDefinitions = new RowDefinitions("Auto,Auto") };
            var line = new Border { Width = 26, Height = s.Airscatter ? 5 : 3, CornerRadius = new CornerRadius(2), Background = B(s.Airscatter ? Palette.AirscatterEdge : col), VerticalAlignment = VerticalAlignment.Center, Child = s.Airscatter ? new Border { Height = 2.5, Margin = new Thickness(0, 1), Background = B(col) } : null };
            g.Children.Add(line);
            void Cell(Control c, int col_, int row) { Grid.SetColumn(c, col_); Grid.SetRow(c, row); g.Children.Add(c); }
            Cell(TB(s.Recv.ToString("HH:mm"), 12, _th.Sub, false, true), 1, 0);
            var calls = TB($"{s.Dx} → {s.Spotter}", 13, _th.Text, true, true); calls.TextTrimming = TextTrimming.CharacterEllipsis;
            Cell(calls, 2, 0);
            Cell(TB(s.Mode, 12, _th.Text, false, true), 3, 0);
            var kmT = TB(km, 12, _th.Text, false, true); kmT.HorizontalAlignment = HorizontalAlignment.Right;
            Cell(kmT, 4, 0);
            Cell(TB(s.Src, 11, _th.Muted, false, true), 1, 1);
            var c2 = TB($"{s.FreqKHz / 1000:0.000} · {s.Comment}{(hasPos ? "" : "  (" + Lang.T("nopos") + ")")}", 11, s.Airscatter ? _th.AsText : _th.Muted, false, true);
            c2.TextTrimming = TextTrimming.CharacterEllipsis;
            Grid.SetColumnSpan(c2, 3);
            Cell(c2, 2, 1);
            var btn = new Button
            {
                Content = g, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = sel ? B(_th.SelRow) : Brushes.Transparent, BorderBrush = B(_th.Line2), BorderThickness = new Thickness(0, 0, 0, 1),
                CornerRadius = new CornerRadius(0), Padding = new Thickness(14, 6)
            };
            var id = s.Id;
            btn.Click += (_, _) => { _map.SelectedSpotId = id; _map.SelectedListener = null; _map.Probe = null; _map.InvalidateVisual(); RefreshSpotList(); RefreshInfoCard(); };
            _spotRows.Children.Add(btn);
        }
        _emptyText.IsVisible = spots.Count == 0;
        _emptyText.Text = _st.Band == "222" ? Lang.T("empty222") + "\n" + Lang.T("empty") : Lang.T("empty");
        _nSpots.Text = spots.Count.ToString();
        _nAs.Text = spots.Count(x => x.Airscatter).ToString();
        _nExcl.Text = _st.ExcludedCount(_st.Band, _st.WindowMin).ToString();
        bool rxBand = _st.Mqtt.EnabledBands.Contains(_st.Band);
        _nRx.Text = rxBand ? _st.Listeners.Get(_st.Band, _st.WindowMin, now).Count.ToString() : "—";
        _nDec.Text = _st.Decodes.Get(_st.Band, _st.WindowMin, now).Count.ToString();
        _nHeld.Text = _st.Gate.HeldCount(_st.Band, _st.WindowMin, now).ToString();
        if (_mqttCount != null)
        {
            _mqttDot.Fill = B(_st.Mqtt.Connected ? _th.Ok : _th.Bad);
            _mqttCount.Text = rxBand ? Lang.F("u.mqtt_discarded", _st.Listeners.Discarded(_st.Band, _st.WindowMin, now).Count) : "—";
        }
        if (_showMqttDiscarded) RefreshInfoCard();
    }

    void InfoLine(string text, double size = 12, Color? col = null, bool bold = false, bool mono = false)
    {
        var t = TB(text, size, col ?? _th.Sub, bold, mono);
        t.TextWrapping = TextWrapping.Wrap;
        _infoBody.Children.Add(t);
    }

    void RefreshInfoCard()
    {
        if (_infoBody == null) return;
        _infoBody.Children.Clear();
        var now = Clock.UtcNow;
        bool show = false;
        if (_map.SelectedListener != null || _map.SelectedSpotId.HasValue || _map.Probe.HasValue) _showMqttDiscarded = false;
        if (_showMqttDiscarded)
        {
            show = true;
            InfoLine("MQTT · PSKREPORTER", 11, _th.Muted, true);
            InfoLine((_st.Mqtt.Connected ? Lang.F("connected", _st.Mqtt.Host + ":" + _st.Mqtt.Port) : Lang.T("notconn")), 12, _st.Mqtt.Connected ? _th.Ok : _th.Bad, true);
            InfoLine(Lang.T("u.mqtt_discarded_title"), 12, _th.Sub);
            var disc = _st.Listeners.Discarded(_st.Band, _st.WindowMin, now);
            if (disc.Count == 0) InfoLine(Lang.T("u.mqtt_none"), 12, _th.Muted);
            foreach (var d in disc.Take(30))
            {
                InfoLine($"{d.call}  {d.grid}  {d.last:HH:mm}Z", 13, _th.Text, true, true);
                InfoLine(string.Join(" · ", d.why.Take(8)), 11, _th.Sub, false, true);
            }
        }
        else if (_map.SelectedListener != null)
        {
            var l = _st.Listeners.Get(_st.Band, _st.WindowMin, now).FirstOrDefault(x => x.Band + "|" + x.Call == _map.SelectedListener);
            if (l != null)
            {
                show = true;
                InfoLine(Lang.T("u.listening_pskreporter"), 11, _th.Muted, true);
                InfoLine($"{l.Call}  {l.Grid}", 17, _th.Text, true, true);
                if (l.Grid.Length < 6) InfoLine(Lang.T("u.grid4"), 11, _th.Muted);
                InfoLine($"{(Lang.T("u.last_report"))} {l.Last:HH:mm} UTC · {string.Join(" · ", l.Modes)}");
                InfoLine($"{(Lang.T("u.heard"))} {l.Heard.Count} · max {l.MaxKm:0} km");
                InfoLine(string.Join("  ", l.Heard.Take(20).Select(h => h.Key + (h.Value.snr.HasValue ? $" {h.Value.snr}dB" : ""))), 11, _th.Text, false, true);
            }
        }
        else if (_map.SelectedSpotId.HasValue)
        {
            var s = _st.Spots.Filter(_st.Band, 60, now).FirstOrDefault(x => x.Id == _map.SelectedSpotId);
            if (s != null)
            {
                show = true;
                InfoLine((Lang.T("u.selected_spot")) + (s.Airscatter ? "AIRSCATTER" : "TROPO"), 11, s.Airscatter ? _th.AsText : _th.Muted, true);
                InfoLine($"{s.Dx} → {s.Spotter}", 17, _th.Text, true, true);
                if (_st.TryPos(s.Dx, s.DxLoc, out var a) && _st.TryPos(s.Spotter, s.DeLoc, out var b))
                {
                    double km = Geo.DistKm(a, b);
                    InfoLine($"{Locator.FromLatLon(a.Lat, a.Lon)} → {Locator.FromLatLon(b.Lat, b.Lon)} · {km:0} km · {Geo.Azimuth(a, b):0}°", 12, _th.Sub, false, true);
                    double sum = 0; int n = 0;
                    foreach (var p in Geo.GreatCircle(a, b, Math.Max(2, (int)(km / 10))))
                    {
                        var v = _st.Tropo.Value(_st.TropoSrc, p.Lat, p.Lon, Clock.UtcNow.AddHours(_st.ForecastH));
                        if (!double.IsNaN(v)) { sum += v; n++; }
                    }
                    InfoLine((Lang.T("u.tropo_index_on_path")) + (n > 0 ? (sum / n).ToString("0.0") : "—"));
                    InfoLine((Lang.T("u.troposcatter_loss_g3ygf_smooth")) + Geo.TroposcatterLossDb(km, s.FreqKHz / 1000).ToString("0") + " dB");
                    if (s.Dx == Graves.Call) InfoLine(Graves.Describe(b), 12, _th.Text, true);
                }
                else InfoLine(Lang.T("nopos"));
                InfoLine($"{s.FreqKHz / 1000:0.000} MHz · {s.Mode} · {s.Src} · {s.Recv:HH:mm} UTC");
                InfoLine(s.Comment, 12, _th.Text, false, true);
            }
        }
        else if (_map.Probe.HasValue)
        {
            show = true;
            var p = _map.Probe.Value;
            InfoLine((Lang.T("u.point")) + Locator.FromLatLon(p.Lat, p.Lon), 11, _th.Muted, true);
            string dist = _st.TryQth(out var q) ? $" · {Geo.DistKm(q, p):0} km, {Geo.Azimuth(q, p):0}°" : "";
            InfoLine($"{Math.Abs(p.Lat):0.00}°{(p.Lat >= 0 ? "N" : "S")} {Math.Abs(p.Lon):0.00}°{(p.Lon >= 0 ? "E" : "W")}{dist}", 12, _th.Sub, false, true);
            string V(string src) { var v = _st.Tropo.Value(src, p.Lat, p.Lon, Clock.UtcNow.AddHours(_st.ForecastH)); return double.IsNaN(v) ? "—" : v.ToString("0.0"); }
            InfoLine($"Hepburn {V("hepburn")} · F5LEN {V("f5len")} · {(Lang.T("u.average"))} {V("media")}", 13, _th.Text, true);
            var pr = _st.Pressure.ValueAt(p, Clock.UtcNow.AddHours(_st.ForecastH));
            InfoLine((Lang.T("u.pressure")) + (double.IsNaN(pr) ? "—" : pr.ToString("0") + " hPa"), 13, _th.Text, true);
            var ft = Clock.UtcNow.AddHours(_st.ForecastH);
            double t2 = _st.Pressure.T2At(p, ft), t925 = _st.Pressure.T925At(p, ft), t850 = _st.Pressure.T850At(p, ft), inv = _st.Pressure.InversionAt(p, ft);
            string T(double v) => double.IsNaN(v) ? "—" : v.ToString("0.0") + " °C";
            InfoLine($"T 2 m {T(t2)} · 925 hPa {T(t925)} · 850 hPa {T(t850)}", 12, _th.Sub, false, true);
            if (!double.IsNaN(inv)) InfoLine(Lang.T("l.inv") + ": " + (inv > 0 ? "+" : "") + inv.ToString("0.0") + " °C", 12, inv > 0 ? _th.Text : _th.Muted, inv > 0);
            if (!_st.Tropo.HasData) InfoLine(Lang.T("tropo.wait"), 11, _th.Muted);
        }
        if (show)
        {
            var close = new Button { Content = "✕", HorizontalAlignment = HorizontalAlignment.Right, Padding = new Thickness(8, 2) };
            close.Click += (_, _) => { _showMqttDiscarded = false; _map.SelectedListener = null; _map.SelectedSpotId = null; _map.Probe = null; _map.InvalidateVisual(); RefreshInfoCard(); RefreshSpotList(); };
            _infoBody.Children.Insert(0, close);
        }
        _infoCard.IsVisible = show;
    }

    // ---------------------------------------------------------------- superlinguetta Raccolta dati

    string _subTab = "dx";
    readonly Dictionary<string, Control> _subContent = new();
    readonly Dictionary<string, Button> _subButtons = new();

    Control BuildCollectTab()
    {
        _subContent.Clear(); _subButtons.Clear();
        var dock = new DockPanel { LastChildFill = true, Background = B(_th.Page) };

        // barra delle sottolinguette
        var bar = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 4, Margin = new Thickness(10, 8, 10, 0) };
        foreach (var (key, label) in new[] { ("dx", "DX cluster"), ("psk", "PSKReporter"), ("rbn", "Reverse Beacon"), ("udp", "UDP (WSJT-X, JTDX, MSHV)"), ("calls", Lang.T("c.tab")) })
        {
            var b = new Button { Content = label, Padding = new Thickness(16, 6), CornerRadius = new CornerRadius(16), FontSize = (13) * UiScale.F };
            b.Click += (_, _) => ShowSubTab(key);
            _subButtons[key] = b;
            bar.Children.Add(b);
        }
        var barBorder = new Border { Child = bar, BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 0, 0, 1), Padding = new Thickness(0, 0, 0, 8), IsVisible = false };   // le sotto-linguette sono nella barra sotto il fascione
        DockPanel.SetDock(barBorder, Dock.Top);
        dock.Children.Add(barBorder);

        var f = _st.Feeds.ToDictionary(x => x.Id);

        var content = new Grid();
        // DX cluster: ON0DXK e la seconda fonte, DXMAPS e S50CLX
        var dxc3 = FeedSection(f["dxc3"], Lang.T("u.3_second_dx_cluster"), Lang.T("u.s50clx_help"), 3);
        dxc3.Margin = new Thickness(7, 0, 7, 14);
        _subContent["dx"] = Wrap(new StackPanel { Children =
        {
            TwoCols(
                FeedSection(f["dxc"], Lang.T("u.1_dx_cluster_of"), Lang.T("u.on_on0dxk_the_vhf"), 0),
                FeedSection(f["dxc2"], Lang.T("u.2_second_dx_cluster"), Lang.T("u.gb7rau_is_a_vhfuhffriendly"), 1)),
            dxc3,
        } });
        _subContent["psk"] = Wrap(TwoCols(
            FeedSection(f["psk"], Lang.T("u.pskreporter_cluster_dm4x"),
                Lang.T("u.cluster_that_forwards_pskreporter"), 2),
            MqttSection()));
        _subContent["rbn"] = Wrap(FeedSection(f["rbn"], "Reverse Beacon Network",
            Lang.T("u.rbn_port_7000_cw"), 1));
        _subContent["udp"] = Wrap(UdpSection());
        _subContent["calls"] = Wrap(CallsSection());
        foreach (var kv in _subContent) content.Children.Add(kv.Value);
        dock.Children.Add(content);
        ShowSubTab(_subTab);
        return dock;
    }

    /// <summary>
    /// Sottolinguetta Nominativi: un solo database, CALL3.TXT. Le voci a mano (anche da un file) si scrivono in cima a
    /// CALL3.TXT, valgono sopra tutto e restano agli aggiornamenti.
    /// </summary>
    Control CallsSection()
    {
        var db = _st.CallDb;
        var (sec, body) = Section(Lang.T("c.title"), 3);
        sec.Margin = new Thickness(7, 0, 7, 14);
        var help = TB(Lang.T("c.help"), 13, _th.Sub); help.TextWrapping = TextWrapping.Wrap; body.Children.Add(help);
        var status = TB("", 12, _th.Muted, false, true); status.TextWrapping = TextWrapping.Wrap; body.Children.Add(status);

        // inserimento a mano
        var call = Pill("", 150, "IV3/HB9CAT");
        var loc = Pill("", 100, "JN65UQ"); loc.MaxLength = 6;
        var msg = TB("", 12, _th.Sub, false, true);
        var list = new StackPanel { Spacing = 3 };
        void Fill()
        {
            status.Text = Lang.F("c.status", db.TargetPath, db.Count.ToString("N0"), db.ManualCount, db.ClusterCount);
            list.Children.Clear();
            foreach (var (c, l) in db.Manual())
            {
                var row = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10 };
                row.Children.Add(TB(c, 14, _th.Text, true, true));
                row.Children.Add(TB(l, 14, _th.Accent, false, true));
                var del = new Button { Content = "✕ " + Lang.T("c.remove"), FontSize = (11) * UiScale.F, Padding = new Thickness(10, 2), CornerRadius = new CornerRadius(10), Background = B(_th.CtlBg), Foreground = B(_th.Text2) };
                var cc = c;
                del.Click += (_, _) => { db.SetManual(cc, ""); Changed(Lang.F("c.removed", cc)); };
                row.Children.Add(del);
                list.Children.Add(row);
            }
        }
        void Changed(string m) { msg.Text = m; Fill(); _st.Dirty = true; _map.InvalidateVisual(); RefreshSpotList(); }
        void Find()
        {
            var c = (call.Text ?? "").Trim().ToUpperInvariant();
            if (c == "") return;
            var any = db.Lookup(c); var src = db.SourceOf(c);
            msg.Text = Lang.F("c.found", c, any == "" ? "—" : any + " (" + (src == "manual" ? Lang.T("c.src_manual") : src == "cluster" ? "cluster" : "CALL3") + ")");
            if (string.IsNullOrEmpty(loc.Text)) loc.Text = any;
        }
        var row1 = new WrapPanel { Orientation = Orientation.Horizontal };
        void Add(string label, Control c) { row1.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Margin = new Thickness(0, 0, 14, 6), Children = { TB(label, 14, _th.Muted), c } }); }
        Add(Lang.T("call"), call);
        Add(Lang.T("loc"), loc);
        row1.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Margin = new Thickness(0, 0, 0, 6), Children =
        {
            PrimaryButton(Lang.T("c.save"), () =>
            {
                var c = (call.Text ?? "").Trim().ToUpperInvariant(); var l = (loc.Text ?? "").Trim().ToUpperInvariant();
                if (c.Length < 3 || !Locator.IsValid(l)) { msg.Text = Lang.T("c.invalid"); return; }
                db.SetManual(c, l); Changed(Lang.F("c.saved", c, l)); call.Text = ""; loc.Text = "";
            }),
            OutlineButton(Lang.T("c.find"), Find),
        } });
        call.KeyDown += (_, e) => { if (e.Key == Avalonia.Input.Key.Enter) Find(); };
        body.Children.Add(row1);
        body.Children.Add(msg);
        body.Children.Add(TB(Lang.T("c.manual_list"), 13, _th.Text, true));
        body.Children.Add(list);

        // elenco di nominativi da un file: entra nelle voci a mano di CALL3.TXT
        body.Children.Add(TB(Lang.T("c.files"), 13, _th.Text, true));
        body.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Children =
        {
            PrimaryButton(Lang.T("c.import"), async () =>
            {
                try
                {
                    var files = await StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions { AllowMultiple = false, Title = Lang.T("c.import") });
                    var path = files.Count > 0 ? files[0].TryGetLocalPath() : null;
                    if (path == null) return;
                    int n = db.Import(path);
                    Changed(Lang.F("c.imported", Path.GetFileName(path), n));
                }
                catch (Exception ex) { msg.Text = ex.Message; }
            }),
            OutlineButton(Lang.T("c.reload"), () => { db.Reload(); Changed(Lang.T("c.reloaded")); }),
        } });

        // aggiornamento del CALL3.TXT da MMMonVHF
        body.Children.Add(TB(Lang.T("c.mmm"), 13, _th.Text, true));
        var mmmSt = TB(_st.Call3Status != "" ? _st.Call3Status : _st.Call3Last == DateTime.MinValue ? Lang.T("c.mmm_never") : Lang.F("c.mmm_last", _st.Call3Last.ToLocalTime().ToString("dd/MM/yyyy HH:mm")), 12, _th.Sub);
        mmmSt.TextWrapping = TextWrapping.Wrap;
        var auto = new CheckBox { Content = Lang.T("c.mmm_auto"), IsChecked = _st.Call3Auto, Foreground = B(_th.Text) };
        auto.IsCheckedChanged += (_, _) => _st.Call3Auto = auto.IsChecked == true;
        body.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12, Children =
        {
            PrimaryButton(Lang.T("c.mmm_now"), async () => { await _st.UpdateCall3Async(); }),
            auto,
        } });
        body.Children.Add(mmmSt);
        body.Children.Add(TB(AppState.Call3Url, 11, _th.Muted, false, true));
        // ultima fonte di locatori: G7VRD
        var g7 = new CheckBox { Content = Lang.T("c.g7"), IsChecked = G7Lookup.Enabled, Foreground = B(_th.Text) };
        g7.IsCheckedChanged += (_, _) => { G7Lookup.Enabled = g7.IsChecked == true; _st.S.Set("g7vrd", G7Lookup.Enabled ? "1" : "0"); _st.Dirty = true; };
        body.Children.Add(g7);
        var g7h = TB(Lang.T("c.g7_help"), 11, _th.Muted); g7h.TextWrapping = TextWrapping.Wrap; body.Children.Add(g7h);
        _st.Call3StatusChanged += m => Dispatcher.UIThread.Post(() => { mmmSt.Text = m; Fill(); _map.InvalidateVisual(); });
        Fill();
        _st.CallDbLoaded += () => Dispatcher.UIThread.Post(Fill);
        return sec;
    }

    Control Wrap(Control c) => new ScrollViewer { Content = new Border { Child = c, Padding = new Thickness(7, 14, 7, 14) }, Background = B(_th.Page) };

    Control TwoCols(Border a, Border b)
    {
        var g = new Grid { ColumnDefinitions = new ColumnDefinitions("*,*") };
        a.Margin = new Thickness(7, 0, 7, 14); b.Margin = new Thickness(7, 0, 7, 14);
        Grid.SetColumn(b, 1);
        g.Children.Add(a); g.Children.Add(b);
        return g;
    }

    void ShowSubTab(string key)
    {
        _subTab = key;
        foreach (var kv in _subContent) kv.Value.IsVisible = kv.Key == key;
        foreach (var kv in _subButtons)
        {
            bool on = kv.Key == key;
            kv.Value.Background = on ? B(_th.Accent) : B(_th.Chip);
            kv.Value.Foreground = on ? B(_th.AccentFg) : B(_th.Text2);
            kv.Value.FontWeight = on ? FontWeight.SemiBold : FontWeight.Normal;
        }
        RefreshLogs(true);
    }

    Color SectionColor(int i)
    {
        var dark = new[] { "#7EA8FF", "#B79BFF", "#FFA35C", "#5BD08A", "#4FD1E0" };
        var light = new[] { "#2459C6", "#6E45D6", "#B85A0C", "#1F7A45", "#0E7C8C" };
        return Color.Parse((_th.Dark ? dark : light)[i % 5]);
    }

    (Border section, StackPanel body) Section(string title, int idx)
    {
        var body = new StackPanel { Spacing = 8, Margin = new Thickness(14, 8) };
        var stack = new StackPanel();
        stack.Children.Add(new TextBlock { Text = title, FontSize = (16) * UiScale.F, FontWeight = FontWeight.Bold, Foreground = B(SectionColor(idx)), Margin = new Thickness(14, 10, 14, 4) });
        stack.Children.Add(new Border { Background = B(_th.Chip), Child = body });
        var section = new Border { Background = B(_th.Panel), BorderBrush = B(_th.Line), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(8), ClipToBounds = true, Child = stack, Margin = new Thickness(0, 0, 0, 14) };
        return (section, body);
    }

    (StackPanel log, ScrollViewer scroll) LogBox(double height = 170)
    {
        var log = new StackPanel();
        var sv = new ScrollViewer { Content = log, Height = height, Background = B(_th.Panel), Padding = new Thickness(14, 6), HorizontalScrollBarVisibility = ScrollBarVisibility.Auto };
        return (log, sv);
    }

    NumericUpDown PortBox(int value, Action<int> set)
    {
        var n = new NumericUpDown { Value = value, Minimum = 1, Maximum = 65535, Increment = 1, FormatString = "0", Width = 140, FontFamily = Mono, VerticalAlignment = VerticalAlignment.Center };
        n.PropertyChanged += (_, e) => { if (e.Property == NumericUpDown.ValueProperty && n.Value.HasValue) set((int)n.Value.Value); };
        return n;
    }

    Border FeedSection(TelnetFeed f, string title, string help, int colorIdx)
    {
        var (sec, body) = Section(title, colorIdx);
        sec.Margin = new Thickness(7, 0, 7, 14);
        var row = new WrapPanel { Orientation = Orientation.Horizontal };
        void Add(string label, Control c) { row.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Margin = new Thickness(0, 0, 14, 6), Children = { TB(label, 14, _th.Muted), c } }); }
        var host = Pill(f.Host, 220); host.TextChanged += (_, _) => f.Host = (host.Text ?? "").Trim();
        Add(Lang.T("host"), host);
        Add(Lang.T("port"), PortBox(f.Port, v => f.Port = v));
        var login = Pill(f.Login, 120, _st.LoginOrCall); login.TextChanged += (_, _) => f.Login = (login.Text ?? "").Trim();
        Add(Lang.T("login"), login);
        var pass = new TextBox { Text = f.Password, Width = 120, PasswordChar = '•', FontFamily = Mono, MinHeight = 32, VerticalContentAlignment = VerticalAlignment.Center };
        pass.TextChanged += (_, _) => f.Password = pass.Text ?? "";
        Add(Lang.T("u.password"), pass);
        var cmds = Pill(f.Cmds, 300, Lang.T("u.commands_after_login_separated")); cmds.TextChanged += (_, _) => f.Cmds = cmds.Text ?? "";
        Add(Lang.T("cmds"), cmds);
        body.Children.Add(row);

        var actions = new WrapPanel { Orientation = Orientation.Horizontal };
        var bConn = PrimaryButton(Lang.T("connect"), async () =>
        {
            if (!_st.HasValidCall) { await AskCall(); if (!_st.HasValidCall) return; }
            if (string.IsNullOrWhiteSpace(f.Login)) f.Login = _st.LoginOrCall;
            f.Connect(_st.LoginOrCall);
        }); bConn.Margin = new Thickness(0, 0, 10, 6);
        var bDisc = OutlineButton(Lang.T("disconnect"), () => f.Disconnect()); bDisc.Margin = new Thickness(0, 0, 14, 6);
        actions.Children.Add(bConn); actions.Children.Add(bDisc);
        var cr = Check(Lang.T("recon"), f.Reconnect, v => f.Reconnect = v); cr.Margin = new Thickness(0, 0, 14, 6);
        var ca = Check(Lang.T("autostart"), f.AutoStart, v => f.AutoStart = v); ca.Margin = new Thickness(0, 0, 14, 6);
        actions.Children.Add(cr); actions.Children.Add(ca);
        body.Children.Add(actions);
        var status = TB("", 14, _th.Muted);
        var counters = TB("", 13, _th.Muted);
        body.Children.Add(status); body.Children.Add(counters);

        var stack = (StackPanel)sec.Child;
        stack.Children.Add(new TextBlock { Text = help, FontSize = (12) * UiScale.F, Foreground = B(_th.Muted), Margin = new Thickness(14, 6), TextWrapping = TextWrapping.Wrap });
        var (log, sv) = LogBox(260);
        stack.Children.Add(new Border { BorderBrush = B(_th.Line2), BorderThickness = new Thickness(0, 1, 0, 0), Child = sv });
        var sendRow = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10, Margin = new Thickness(14, 8) };
        var line = Pill("", 300, Lang.T("cmdhint"));
        var send = OutlineButton(Lang.T("send"), () => { var t = (line.Text ?? "").Trim(); if (t.Length > 0) { f.Send(t); line.Text = ""; } });
        sendRow.Children.Add(line); sendRow.Children.Add(send);
        stack.Children.Add(new Border { Background = B(_th.Chip), Child = sendRow });
        _sections[f.Id] = (status, counters, log, sv);
        return sec;
    }

    Border MqttSection()
    {
        var m = _st.Mqtt;
        var (sec, body) = Section(Lang.T("u.pskreporter_mqtt_who_is"), 3);
        var row = new WrapPanel { Orientation = Orientation.Horizontal };
        var host = Pill(m.Host, 220); host.TextChanged += (_, _) => m.Host = (host.Text ?? "").Trim();
        row.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Margin = new Thickness(0, 0, 14, 6), Children = { TB(Lang.T("host"), 14, _th.Muted), host } });
        var port = PortBox(m.Port, v => m.Port = v);
        row.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Margin = new Thickness(0, 0, 14, 6), Children = { TB(Lang.T("port"), 14, _th.Muted), port } });
        var tls = Check("TLS (1884)", m.UseTls, v => { m.UseTls = v; port.Value = v ? 1884 : 1883; });
        tls.Margin = new Thickness(0, 0, 14, 6);
        row.Children.Add(tls);
        foreach (var b in new[] { "144", "222", "432" })
        {
            var cb = Check(b + " MHz", m.EnabledBands.Contains(b), v => { if (v) m.EnabledBands.Add(b); else m.EnabledBands.Remove(b); });
            cb.Margin = new Thickness(0, 0, 12, 6);
            row.Children.Add(cb);
        }
        body.Children.Add(row);
        var names = Pill(string.Join(";", m.BandTopics.Select(kv => kv.Key + "=" + kv.Value)), 240);
        names.TextChanged += (_, _) =>
        {
            var d = new Dictionary<string, string>();
            foreach (var kv in (names.Text ?? "").Split(';', StringSplitOptions.RemoveEmptyEntries)) { var p = kv.Split('='); if (p.Length == 2) d[p[0].Trim()] = p[1].Trim(); }
            if (d.Count > 0) m.BandTopics = d;
        };
        body.Children.Add(new WrapPanel { Children = { TB(Lang.T("u.topic_bands"), 14, _th.Muted), names, TB("  pskr/filter/v2/{banda}/#", 12, _th.Sub, false, true) } });
        var actions = new WrapPanel { Orientation = Orientation.Horizontal };
        var bc = PrimaryButton(Lang.T("connect"), () => m.Connect()); bc.Margin = new Thickness(0, 0, 10, 6);
        var bd = OutlineButton(Lang.T("disconnect"), () => m.Disconnect()); bd.Margin = new Thickness(0, 0, 14, 6);
        actions.Children.Add(bc); actions.Children.Add(bd);
        var cr = Check(Lang.T("recon"), m.Reconnect, v => m.Reconnect = v); cr.Margin = new Thickness(0, 0, 14, 6);
        var ca = Check(Lang.T("autostart"), m.AutoStart, v => m.AutoStart = v); ca.Margin = new Thickness(0, 0, 14, 6);
        actions.Children.Add(cr); actions.Children.Add(ca);
        body.Children.Add(actions);
        var status = TB("", 14, _th.Muted);
        var counters = TB("", 13, _th.Muted);
        body.Children.Add(status); body.Children.Add(counters);
        var stack = (StackPanel)sec.Child;
        stack.Children.Add(new TextBlock { Text = Lang.T("u.realtime_reception_reports_locators"), FontSize = (12) * UiScale.F, Foreground = B(_th.Muted), Margin = new Thickness(14, 6), TextWrapping = TextWrapping.Wrap });
        var (log, sv) = LogBox(260);
        stack.Children.Add(new Border { BorderBrush = B(_th.Line2), BorderThickness = new Thickness(0, 1, 0, 0), Child = sv });
        _sections["mqtt"] = (status, counters, log, sv);
        return sec;
    }

    Border UdpSection()
    {
        var u = _st.Udp;
        var (sec, body) = Section(Lang.T("u.decodes_from_wsjtx_jtdx"), 0);
        sec.Margin = new Thickness(7, 0, 7, 14);
        var r1 = new WrapPanel { Orientation = Orientation.Horizontal };
        var inA = Pill(u.ListenAddress, 170); inA.TextChanged += (_, _) => u.ListenAddress = (inA.Text ?? "").Trim();
        r1.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10, Margin = new Thickness(0, 0, 0, 6), Children = {
            TB(Lang.T("u.listen"), 14, _th.Text, true), TB(Lang.T("u.address"), 14, _th.Muted), inA,
            TB(Lang.T("port"), 14, _th.Muted), PortBox(u.ListenPort, v => u.ListenPort = v),
            TB("127.0.0.1 = " + (Lang.T("u.same_computer")) + " · 224.0.0.1 = multicast", 12, _th.Muted) } });
        body.Children.Add(r1);
        var outA = Pill(u.ForwardAddress, 170); outA.TextChanged += (_, _) => u.ForwardAddress = (outA.Text ?? "").Trim();
        body.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10, Children = {
            TB(Lang.T("u.forward"), 14, _th.Text, true), TB(Lang.T("u.address"), 14, _th.Muted), outA,
            TB(Lang.T("port"), 14, _th.Muted), PortBox(u.ForwardPort, v => u.ForwardPort = v),
            Check(Lang.T("u.repeat_the_same_content"), u.Forward, v => u.Forward = v) } });
        var r3 = new WrapPanel { Orientation = Orientation.Horizontal };
        var bs = PrimaryButton(Lang.T("u.start_listening"), () => u.Start()); bs.Margin = new Thickness(0, 0, 10, 6);
        var bt = OutlineButton(Lang.T("u.stop"), () => u.Stop()); bt.Margin = new Thickness(0, 0, 14, 6);
        r3.Children.Add(bs); r3.Children.Add(bt);
        var au = Check(Lang.T("u.start_at_startup"), u.AutoStart, v => u.AutoStart = v); au.Margin = new Thickness(0, 0, 14, 6);
        r3.Children.Add(au);
        _udpStatus = TB("", 14, _th.Muted);
        r3.Children.Add(_udpStatus);
        body.Children.Add(r3);
        _udpInfo = TB("", 13, _th.Text, false, true);
        body.Children.Add(_udpInfo);
        var stack = (StackPanel)sec.Child;
        stack.Children.Add(new TextBlock { Text = Lang.T("u.each_decoded_station_is"), FontSize = (12) * UiScale.F, Foreground = B(_th.Muted), Margin = new Thickness(14, 6), TextWrapping = TextWrapping.Wrap });
        var dbgHead = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 14, Margin = new Thickness(14, 6) };
        dbgHead.Children.Add(TB(Lang.T("u.debug_received_text"), 13, _th.Text, true));
        _udpShowHb = Check(Lang.T("u.show_heartbeat_and_status"), true, _ => RefreshLogs(true));
        dbgHead.Children.Add(_udpShowHb);
        stack.Children.Add(new Border { Background = B(_th.Chip), BorderBrush = B(_th.Line2), BorderThickness = new Thickness(0, 1, 0, 0), Child = dbgHead });
        var (log, sv) = LogBox(380);
        _udpLog = log; _udpScroll = sv;
        stack.Children.Add(sv);
        return sec;
    }

    // ---------------------------------------------------------------- linguette Hepburn e F5LEN: banco di controllo e taratura

    sealed class TropoEditor
    {
        public string PageUrl = "";
        public string RegionKey = "";          // regione scelta nel menu ("" = pagina scritta a mano)
        public TropoSources.Frame? Frame;      // cornice trovata nell'immagine mostrata
        public List<(int ore, string url, string file, bool on)> Images = new();
        public int Sel;
        public bool ColorMode;                 // false = punto di controllo, true = colore della legenda
        public bool ShowBench;                 // false = anteprima della fusione, true = banco di taratura manuale
        public int PreviewHours;               // ora dell'anteprima
        public (int x, int y)? Pending;
        public (byte r, byte g, byte b) PendingColor;
        public string Msg = "";
    }

    readonly Dictionary<string, Border> _tropoTabs = new();
    readonly Dictionary<string, TropoEditor> _tropoEd = new();
    DateTime _tropoShown = DateTime.MinValue;

    Control BuildTropoTab(string fonte)
    {
        var holder = new Border { Background = B(_th.Page) };
        _tropoTabs[fonte] = holder;
        FillTropoTab(fonte);
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

    TropoEditor EdFor(string fonte, TropoCalibration cal)
    {
        if (_tropoEd.TryGetValue(fonte, out var ed)) return ed;
        var regs = TropoSources.For(fonte).ToList();
        var rk = _st.S.Get("tropo_region_" + fonte, regs.FirstOrDefault()?.Key ?? "");
        var reg = regs.FirstOrDefault(r => r.Key == rk);
        var page = reg != null ? reg.PageUrl : _st.S.Get("tropo_page_" + fonte, "");
        ed = new TropoEditor { PageUrl = page, RegionKey = reg?.Key ?? "" };
        foreach (var p in cal.Passi.Where(p => p.url != ""))
            ed.Images.Add((p.ore, p.url, CacheFile(fonte, p.url), true));
        _tropoEd[fonte] = ed;
        return ed;
    }

    static string CacheFile(string fonte, string url)
    {
        var ext = Path.GetExtension(new Uri(url, UriKind.RelativeOrAbsolute).IsAbsoluteUri ? new Uri(url).AbsolutePath : url);
        if (string.IsNullOrEmpty(ext) || ext.Length > 5) ext = ".png";
        var h = (uint)url.GetHashCode();
        return Path.Combine(Settings.DataDir, $"src_{fonte}_{h:x8}{ext}");
    }

    /// <summary>Legge la pagina della fonte e scarica tutte le immagini di previsione che trova, nell'ordine della pagina.</summary>
    async Task FetchAll(string fonte, TropoEditor ed)
    {
        ed.Msg = Lang.T("u.reading_page");
        Dispatcher.UIThread.Post(() => FillTropoTab(fonte));
        try
        {
            var refs = new List<TropoImageRef>();
            var reg = TropoSources.For(fonte).FirstOrDefault(r => r.Key == ed.RegionKey);
            if (reg != null)
            {
                var r2 = new TropoRegion { Fonte = reg.Fonte, Key = reg.Key, Nome = reg.Nome, PageUrl = ed.PageUrl, Prefix = reg.Prefix, Code = reg.Code };
                refs = await TropoSources.ListImages(_st.Http, r2);
            }
            else
            {
                // pagina scritta a mano: tutte le immagini citate nella pagina, script compresi
                var baseUri = new Uri(ed.PageUrl);
                var html = await _st.Http.GetStringAsync(baseUri);
                int idx = 0;
                foreach (System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(html, @"[""'(]([^""'()\s]+?\.(?:png|gif|jpg|jpeg|webp))(?:\?[^""'()\s]*)?[""')]", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    var u = new Uri(baseUri, m.Groups[1].Value).ToString();
                    var low = u.ToLowerInvariant();
                    if (low.Contains("logo") || low.Contains("icon") || low.Contains("button") || low.Contains("banner")) continue;
                    if (refs.Any(x => x.Url == u)) continue;
                    var h = TropoSources.HoursFromName(fonte, u);
                    refs.Add(new TropoImageRef(u, h >= 0 ? h : idx, null));
                    idx++;
                }
            }
            ed.Images.Clear();
            int ok = 0;
            foreach (var im in refs)
            {
                var file = CacheFile(fonte, im.Url);
                try
                {
                    var (bytes, _) = await TropoIndex.Download(_st.Http, im.Url);
                    if (bytes.Length < 3000) continue;     // icone e decorazioni: troppo piccole per essere mappe
                    await File.WriteAllBytesAsync(file, bytes);
                    ed.Images.Add((im.Hours, im.Url, file, true));
                    ok++;
                }
                catch { }
                ed.Msg = (Lang.T("u.downloaded")) + ok + " / " + refs.Count;
                Dispatcher.UIThread.Post(() => FillTropoTab(fonte));
            }
            ed.Sel = 0;
            ed.Msg = Lang.F("u.found_images", refs.Count, ok);
        }
        catch (Exception ex) { ed.Msg = (Lang.T("u.page_not_read")) + ex.Message; }
        Dispatcher.UIThread.Post(() => FillTropoTab(fonte));
    }

    readonly Dictionary<string, MapView> _previews = new();

    /// <summary>
    /// Linguetta della fonte: anteprima della fusione di tutte le immagini scaricate, con i riquadri delle regioni e la
    /// velatura grigia dove manca copertura; a sinistra l'elenco delle regioni con il loro stato.
    /// </summary>
    void FillTropoTab(string fonte)
    {
        if (!_tropoTabs.TryGetValue(fonte, out var holder)) return;
        var cal = CalFor(fonte);
        var ed = EdFor(fonte, cal);
        if (ed.ShowBench) { FillTropoBench(fonte); return; }
        string nome = fonte == "hepburn" ? "Hepburn" : "F5LEN";

        var g = new Grid { ColumnDefinitions = new ColumnDefinitions("400,*") };

        // ------------------------------------------- sinistra: regioni
        var list = new StackPanel { Spacing = 6, Margin = new Thickness(14) };
        list.Children.Add(new TextBlock { Text = Lang.F("u.preview_title", nome), FontSize = (18) * UiScale.F, FontWeight = FontWeight.Bold, Foreground = B(_th.Accent), TextWrapping = TextWrapping.Wrap });
        var help = TB(Lang.T("u.preview_help"), 12, _th.Muted); help.TextWrapping = TextWrapping.Wrap;
        list.Children.Add(help);
        var upd = PrimaryButton(Lang.T("u.update_now"), () => Task.Run(async () => { await _st.Tropo.UpdateAsync(_st.Http); Dispatcher.UIThread.Post(() => FillTropoTab(fonte)); }));
        var bench = OutlineButton(Lang.T("u.calibration_bench"), () => { ed.ShowBench = true; FillTropoTab(fonte); });
        list.Children.Add(new WrapPanel { Children = { upd, new Border { Width = 8 }, bench } });
        list.Children.Add(Check(Lang.T("u.active_use_these_maps"), cal.Attivo, v => { cal.Attivo = v; try { cal.Save(TropoIndex.Folder); } catch { } }));
        var have = _st.Tropo.Rasters.Where(r => r.Fonte == fonte).GroupBy(r => r.Region).ToDictionary(x => x.Key, x => x.Count());
        foreach (var reg in TropoSources.For(fonte))
        {
            var box = new StackPanel { Spacing = 2 };
            var head = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 6 };
            head.Children.Add(Check("", _st.Tropo.RegionEnabled(reg), v => _st.S.Set("tropo_reg_" + reg.Key, v ? "1" : "0")));
            head.Children.Add(TB(reg.Nome, 13, _th.Text, true));
            head.Children.Add(TB(reg.Code ?? (reg.Prefix == "" ? "—" : reg.Prefix), 11, _th.Muted, false, true));
            int n = have.TryGetValue(reg.Key, out var c) ? c : 0;
            head.Children.Add(TB(n > 0 ? Lang.F("u.maps_n", n) : Lang.T("u.not_read_yet"), 11, n > 0 ? _th.Ok : _th.Muted, true));
            box.Children.Add(head);
            if (_st.Tropo.RegionStatus.TryGetValue(reg.Key, out var rs))
            {
                var t = TB(rs, 11, _th.Sub); t.TextWrapping = TextWrapping.Wrap; t.Margin = new Thickness(26, 0, 0, 0);
                box.Children.Add(t);
            }
            list.Children.Add(new Border { BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 0, 0, 1), Padding = new Thickness(0, 4), Child = box });
        }
        var stTxt = TB(_st.Tropo.Status, 12, _th.Sub); stTxt.TextWrapping = TextWrapping.Wrap;
        list.Children.Add(stTxt);
        g.Children.Add(new Border { Background = B(_th.Panel), BorderBrush = B(_th.Line), BorderThickness = new Thickness(0, 0, 1, 0), Child = new ScrollViewer { Content = list } });

        // ------------------------------------------- destra: anteprima con la barra delle ore
        var pv = new MapView(_st, _th) { PreviewSource = fonte, PreviewHours = ed.PreviewHours };
        _previews[fonte] = pv;
        var dock = new DockPanel { LastChildFill = true };
        var bar = new DockPanel { Margin = new Thickness(14, 8), LastChildFill = true };
        var lbl = TB("", 13, _th.Text, true, true); lbl.MinWidth = 260; lbl.VerticalAlignment = VerticalAlignment.Center;
        void Lbl() { var t = Clock.UtcNow.AddHours(ed.PreviewHours); lbl.Text = $"{Lang.DayNames()[(int)t.DayOfWeek]} {t:dd/MM HH}:00 UTC (+{ed.PreviewHours} h)"; }
        Lbl();
        var sl = new Slider { Minimum = 0, Maximum = 192, TickFrequency = 3, IsSnapToTickEnabled = true, Value = ed.PreviewHours, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(10, 0) };
        sl.PropertyChanged += (_, e) => { if (e.Property == RangeBase.ValueProperty) { ed.PreviewHours = (int)Math.Round(sl.Value); pv.PreviewHours = ed.PreviewHours; Lbl(); pv.InvalidateVisual(); } };
        DockPanel.SetDock(lbl, Dock.Left); bar.Children.Add(lbl); bar.Children.Add(sl);
        DockPanel.SetDock(bar, Dock.Top); dock.Children.Add(bar);
        dock.Children.Add(pv);
        Grid.SetColumn(dock, 1);
        g.Children.Add(dock);
        holder.Child = g;
    }

    /// <summary>Banco di taratura manuale (solo se la lettura automatica non basta).</summary>
    void FillTropoBench(string fonte)
    {
        if (!_tropoTabs.TryGetValue(fonte, out var holder)) return;
        var cal = CalFor(fonte);
        var ed = EdFor(fonte, cal);
        string nome = fonte == "hepburn" ? "Hepburn" : "F5LEN";

        var g = new Grid { ColumnDefinitions = new ColumnDefinitions("*,420") };

        // ------------------------------------------- sinistra: pagina, elenco immagini, immagine
        var left = new DockPanel { LastChildFill = true, Margin = new Thickness(14) };
        var top = new StackPanel { Spacing = 8, Margin = new Thickness(0, 0, 0, 10) };
        top.Children.Add(OutlineButton(Lang.T("u.back_to_preview"), () => { ed.ShowBench = false; FillTropoTab(fonte); }));
        var pageRow = new DockPanel { LastChildFill = true };
        var fetch = PrimaryButton(Lang.T("u.read_page_and_download"), () => Task.Run(() => FetchAll(fonte, ed)));
        fetch.Margin = new Thickness(10, 0, 0, 0);
        DockPanel.SetDock(fetch, Dock.Right);
        pageRow.Children.Add(fetch);
        var lbl = TB(Lang.T("u.page"), 13, _th.Muted); DockPanel.SetDock(lbl, Dock.Left); pageRow.Children.Add(lbl);
        var page = Pill(ed.PageUrl, 300);
        page.TextChanged += (_, _) => { ed.PageUrl = (page.Text ?? "").Trim(); _st.S.Set("tropo_page_" + fonte, ed.PageUrl); };
        var regList = TropoSources.For(fonte).ToList();
        var region = new ComboBox { ItemsSource = regList.Select(r => r.Nome).Append("—").ToArray(), MinWidth = 150, Margin = new Thickness(8, 0, 8, 0), VerticalAlignment = VerticalAlignment.Center };
        var ridx = regList.FindIndex(r => r.Key == ed.RegionKey);
        region.SelectedIndex = ridx >= 0 ? ridx : regList.Count;
        region.SelectionChanged += (_, _) =>
        {
            int k = region.SelectedIndex;
            if (k < 0) return;
            if (k < regList.Count) { ed.RegionKey = regList[k].Key; ed.PageUrl = regList[k].PageUrl; }
            else ed.RegionKey = "";
            _st.S.Set("tropo_region_" + fonte, ed.RegionKey);
            ed.Images.Clear(); ed.Sel = 0; ed.Pending = null; ed.Msg = "";
            FillTropoTab(fonte);
        };
        ToolTip.SetTip(region, Lang.T("u.region"));
        DockPanel.SetDock(region, Dock.Left); pageRow.Children.Add(region);
        pageRow.Children.Add(page);
        top.Children.Add(pageRow);

        // regione: usarla sulla mappa, estremi della cornice
        var curReg = regList.FirstOrDefault(r => r.Key == ed.RegionKey);
        if (curReg != null)
        {
            var regRow = new WrapPanel { Orientation = Orientation.Horizontal };
            regRow.Children.Add(Check(Lang.T("u.use_on_map"), _st.Tropo.RegionEnabled(curReg), v => _st.S.Set("tropo_reg_" + curReg.Key, v ? "1" : "0")));
            var eff = _st.Tropo.Effective(curReg);
            string F(double? d) => d.HasValue ? d.Value.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) : "";
            var ew = Pill(F(eff.LonW), 56, "W"); var ee = Pill(F(eff.LonE), 56, "E"); var es = Pill(F(eff.LatS), 56, "S"); var en = Pill(F(eff.LatN), 56, "N");
            // scritti a mano valgono più dell'OCR; tutti e quattro vuoti = il programma rilegge gli assi al prossimo aggiornamento
            void SaveExt()
            {
                var t = new[] { ew.Text, ee.Text, es.Text, en.Text }.Select(x => (x ?? "").Trim().Replace(',', '.')).ToArray();
                if (t.All(x => x == "")) { _st.S.Set("tropo_ext_" + curReg.Key, ""); return; }
                var v = new double[4];
                for (int k = 0; k < 4; k++)
                    if (!double.TryParse(t[k], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out v[k])) return;
                var old = _st.Tropo.RegionExtents(curReg);
                if (old != null && old.W == v[0] && old.E == v[1] && old.S == v[2] && old.N == v[3]) return;   // invariati
                _st.S.Set("tropo_ext_" + curReg.Key, new StoredExtents(v[0], v[1], v[2], v[3], eff.Mercator, 0, 0, "manual").ToString());
            }
            foreach (var box in new[] { ew, ee, es, en }) { box.Margin = new Thickness(4, 0, 0, 0); box.LostFocus += (_, _) => SaveExt(); }
            regRow.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(16, 0, 0, 0), Children = { TB(Lang.T("u.extents"), 12, _th.Muted), ew, ee, es, en } });
            top.Children.Add(regRow);
            if (_st.Tropo.RegionStatus.TryGetValue(curReg.Key, out var rs))
            {
                var rsb = TB(rs, 12, _th.Sub); rsb.TextWrapping = TextWrapping.Wrap;
                top.Children.Add(rsb);
            }
        }
        var addRow = new DockPanel { LastChildFill = true };
        var one = Pill("", 300, Lang.T("u.or_paste_a_single"));
        var add = OutlineButton(Lang.T("u.add"), () => Task.Run(async () =>
        {
            var u = (one.Text ?? "").Trim(); if (u == "") return;
            try { var b = await _st.Http.GetByteArrayAsync(u); var f = CacheFile(fonte, u); await File.WriteAllBytesAsync(f, b); ed.Images.Add((ed.Images.Count == 0 ? 0 : ed.Images.Max(x => x.ore) + 3, u, f, true)); ed.Sel = ed.Images.Count - 1; ed.Msg = ""; }
            catch (Exception ex) { ed.Msg = ex.Message; }
            Dispatcher.UIThread.Post(() => FillTropoTab(fonte));
        }));
        add.Margin = new Thickness(10, 0, 0, 0);
        DockPanel.SetDock(add, Dock.Right); addRow.Children.Add(add); addRow.Children.Add(one);
        top.Children.Add(addRow);
        if (ed.Msg != "") { var m = TB(ed.Msg, 13, _th.Sub); m.TextWrapping = TextWrapping.Wrap; top.Children.Add(m); }

        // elenco delle immagini: spunta, ora di previsione, scelta
        if (ed.Images.Count > 0)
        {
            ed.Sel = Math.Clamp(ed.Sel, 0, ed.Images.Count - 1);
            var strip = new WrapPanel();
            for (int i = 0; i < ed.Images.Count; i++)
            {
                int k = i;
                var im = ed.Images[i];
                var cb = new CheckBox { IsChecked = im.on, Margin = new Thickness(0) };
                cb.Click += (_, _) => { var x = ed.Images[k]; ed.Images[k] = (x.ore, x.url, x.file, cb.IsChecked == true); };
                var hours = new TextBox { Text = im.ore.ToString(), Width = 52, FontFamily = Mono, MinHeight = 28, Padding = new Thickness(4, 2) };
                hours.TextChanged += (_, _) => { if (int.TryParse(hours.Text, out var h)) { var x = ed.Images[k]; ed.Images[k] = (h, x.url, x.file, x.on); } };
                var show = new Button { Content = i == ed.Sel ? "●" : "○", Padding = new Thickness(8, 2), Background = B(i == ed.Sel ? _th.Accent : _th.Chip), Foreground = B(i == ed.Sel ? _th.AccentFg : _th.Text2) };
                show.Click += (_, _) => { ed.Sel = k; ed.Pending = null; FillTropoTab(fonte); };
                ToolTip.SetTip(show, im.url);
                strip.Children.Add(new Border { BorderBrush = B(_th.Line), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(6), Padding = new Thickness(4), Margin = new Thickness(0, 0, 6, 6),
                    Child = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 4, Children = { cb, TB("+", 12, _th.Muted), hours, TB("h", 12, _th.Muted), show } } });
            }
            top.Children.Add(strip);
        }
        DockPanel.SetDock(top, Dock.Top);
        left.Children.Add(top);

        // immagine selezionata: clic = punto di controllo o colore della legenda
        if (ed.Images.Count > 0 && File.Exists(ed.Images[ed.Sel].file))
        {
            try
            {
                var bytes = File.ReadAllBytes(ed.Images[ed.Sel].file);
                var bmp = new Avalonia.Media.Imaging.Bitmap(new MemoryStream(bytes));
                var pix = TropoIndex.ReadPixels(bytes);
                ed.Frame = TropoSources.FindFrame(pix);
                var img = new Image { Source = bmp, Stretch = Stretch.Uniform, Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Cross) };
                var overlay = new Canvas { IsHitTestVisible = false };
                var imgGrid = new Grid { Children = { img, overlay } };
                // segni sui punti già inseriti
                img.LayoutUpdated += (_, _) =>
                {
                    overlay.Children.Clear();
                    double bw = img.Bounds.Width, bh = img.Bounds.Height;
                    if (bw < 2) return;
                    double sc = Math.Min(bw / pix.w, bh / pix.h), ox = (bw - pix.w * sc) / 2, oy = (bh - pix.h * sc) / 2;
                    void Mark(double x, double y, Color col)
                    {
                        var e = new Avalonia.Controls.Shapes.Ellipse { Width = 12, Height = 12, Stroke = B(col), StrokeThickness = 2.5 };
                        Canvas.SetLeft(e, ox + x * sc - 6); Canvas.SetTop(e, oy + y * sc - 6);
                        overlay.Children.Add(e);
                    }
                    if (ed.Frame is { } fr)
                    {
                        var rect = new Avalonia.Controls.Shapes.Rectangle { Width = (fr.X1 - fr.X0) * sc, Height = (fr.Y1 - fr.Y0) * sc, Stroke = B(Color.Parse("#1E78E0")), StrokeThickness = 2, StrokeDashArray = new Avalonia.Collections.AvaloniaList<double> { 4, 3 } };
                        Canvas.SetLeft(rect, ox + fr.X0 * sc); Canvas.SetTop(rect, oy + fr.Y0 * sc);
                        overlay.Children.Add(rect);
                    }
                    foreach (var p in cal.Punti) Mark(p.x, p.y, Color.Parse("#D62839"));
                    if (ed.Pending.HasValue) Mark(ed.Pending.Value.x, ed.Pending.Value.y, Color.Parse("#1E78E0"));
                };
                img.PointerPressed += (_, e) =>
                {
                    var pt = e.GetPosition(img);
                    double bw = img.Bounds.Width, bh = img.Bounds.Height;
                    double sc = Math.Min(bw / pix.w, bh / pix.h), ox = (bw - pix.w * sc) / 2, oy = (bh - pix.h * sc) / 2;
                    int x = (int)Math.Round((pt.X - ox) / sc), y = (int)Math.Round((pt.Y - oy) / sc);
                    if (x < 0 || y < 0 || x >= pix.w || y >= pix.h) return;
                    ed.Pending = (x, y);
                    ed.PendingColor = TropoIndex.PixelAt(pix, x, y);
                    FillTropoTab(fonte);
                };
                left.Children.Add(new Border { Background = Brushes.White, BorderBrush = B(_th.Line), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(6), Padding = new Thickness(6), Child = imgGrid });
            }
            catch (Exception ex) { left.Children.Add(TB(Lang.T("u.unreadable_image") + ex.Message, 13, _th.Bad)); }
        }
        else
        {
            left.Children.Add(new TextBlock
            {
                Text = Lang.F("u.no_image", nome),
                Foreground = B(_th.Muted), FontSize = (14) * UiScale.F, TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, MaxWidth = 600, TextAlignment = TextAlignment.Center
            });
        }
        g.Children.Add(left);

        // ------------------------------------------- destra: taratura
        var info = new StackPanel { Spacing = 8, Margin = new Thickness(14) };
        info.Children.Add(new TextBlock { Text = Lang.T("u.calibration") + nome, FontSize = (18) * UiScale.F, FontWeight = FontWeight.Bold, Foreground = B(_th.Accent) });
        var att = Check(Lang.T("u.active_use_these_maps"), cal.Attivo, v => cal.Attivo = v);
        if (ed.Images.Count > 0)
            info.Children.Add(TB(ed.Frame is { } ff ? Lang.F("u.frame_found", ff.X0, ff.X1, ff.Y0, ff.Y1) : Lang.T("u.frame_not_found"), 12, ed.Frame.HasValue ? _th.Sub : _th.Bad));
        info.Children.Add(att);
        info.Children.Add(TB(Lang.T("u.ask_the_author_for"), 12, _th.Muted));

        var mode = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 6 };
        var m1 = new ToggleButton { Content = Lang.T("u.control_point"), IsChecked = !ed.ColorMode, Padding = new Thickness(10, 4) };
        var m2 = new ToggleButton { Content = Lang.T("u.legend_colour"), IsChecked = ed.ColorMode, Padding = new Thickness(10, 4) };
        m1.Click += (_, _) => { ed.ColorMode = false; FillTropoTab(fonte); };
        m2.Click += (_, _) => { ed.ColorMode = true; FillTropoTab(fonte); };
        mode.Children.Add(m1); mode.Children.Add(m2);
        info.Children.Add(mode);

        if (ed.Pending.HasValue)
        {
            var (px, py) = ed.Pending.Value;
            var (r, gg, bb) = ed.PendingColor;
            info.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Children = {
                new Border { Width = 22, Height = 22, CornerRadius = new CornerRadius(4), Background = new SolidColorBrush(Color.FromRgb(r, gg, bb)), BorderBrush = B(_th.Line), BorderThickness = new Thickness(1) },
                TB($"pixel {px},{py}  #{r:X2}{gg:X2}{bb:X2}", 13, _th.Text, false, true) } });
            if (!ed.ColorMode)
            {
                var where = Pill("", 200, Lang.T("u.locator_or_latlon"));
                var ok = PrimaryButton(Lang.T("u.add_point"), () =>
                {
                    var t = (where.Text ?? "").Trim();
                    LatLon ll;
                    var parts = t.Split(',', ';');
                    if (Locator.TryToLatLon(t, out ll) || (parts.Length == 2 && double.TryParse(parts[0].Trim(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var la) && double.TryParse(parts[1].Trim(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var lo) && (ll = new LatLon(la, lo)).Lat == la))
                    {
                        cal.Punti.Add((px, py, ll.Lat, ll.Lon)); cal.Fit(); ed.Pending = null; ed.Msg = "";
                    }
                    else ed.Msg = Lang.T("u.enter_a_46character_locator");
                    FillTropoTab(fonte);
                });
                info.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Children = { where, ok } });
                info.Children.Add(TB(Lang.T("u.click_a_graticule_crossing"), 12, _th.Muted));
            }
            else
            {
                var val = Pill("", 80, "0–10");
                var ok = PrimaryButton(Lang.T("u.add_colour"), () =>
                {
                    if (double.TryParse((val.Text ?? "").Replace(',', '.'), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var v))
                    { cal.Legenda.Add((r, gg, bb, v)); ed.Pending = null; ed.Msg = ""; }
                    FillTropoTab(fonte);
                });
                info.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Children = { TB(Lang.T("u.value"), 13, _th.Muted), val, ok } });
                info.Children.Add(TB(Lang.T("u.click_each_legend_box"), 12, _th.Muted));
            }
        }
        else info.Children.Add(TB(Lang.T("u.click_the_image_to"), 12, _th.Muted));

        // punti di riferimento
        info.Children.Add(TB(Lang.F("u.control_points_n", cal.Punti.Count), 13, _th.Text, true));
        for (int i = 0; i < cal.Punti.Count; i++)
        {
            int k = i; var p = cal.Punti[i];
            var del = new Button { Content = "✕", Padding = new Thickness(6, 0) };
            del.Click += (_, _) => { cal.Punti.RemoveAt(k); cal.Fit(); FillTropoTab(fonte); };
            info.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Children = { del, TB($"{p.x:0},{p.y:0} → {p.lat:0.00}, {p.lon:0.00}  {Locator.FromLatLon(p.lat, p.lon)}", 12, _th.Sub, false, true) } });
        }
        // colori della legenda
        info.Children.Add(TB(Lang.F("u.legend_colours_n", cal.Legenda.Count), 13, _th.Text, true));
        for (int i = 0; i < cal.Legenda.Count; i++)
        {
            int k = i; var l = cal.Legenda[i];
            var del = new Button { Content = "✕", Padding = new Thickness(6, 0) };
            del.Click += (_, _) => { cal.Legenda.RemoveAt(k); FillTropoTab(fonte); };
            info.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Children = { del,
                new Border { Width = 18, Height = 18, CornerRadius = new CornerRadius(3), Background = new SolidColorBrush(Color.FromRgb(l.r, l.g, l.b)) },
                TB($"#{l.r:X2}{l.g:X2}{l.b:X2} = {l.valore:0.##}", 12, _th.Sub, false, true) } });
        }
        var tol = Pill(cal.Tolleranza.ToString(), 60);
        tol.TextChanged += (_, _) => { if (int.TryParse(tol.Text, out var v) && v >= 0) cal.Tolleranza = v; };
        info.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Children = { TB(Lang.T("u.colour_tolerance"), 13, _th.Muted), tol } });
        {
            // moltiplicatore di scala: valore finale = moltiplicatore × valore della legenda (per F5LEN: confronto con Hepburn)
            var a = Pill(cal.ScalaA.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture), 70);
            a.TextChanged += (_, _) => { if (double.TryParse((a.Text ?? "").Replace(',', '.'), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var v) && v > 0) cal.ScalaA = v; };
            info.Children.Add(new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Children = { TB(Lang.T(fonte == "f5len" ? "u.scale_multiplier" : "u.scale_multiplier_hep"), 13, _th.Muted), a } });
        }
        if (cal.HasFit && ed.Images.Count > 0)
        {
            // verifica: dove cade il tuo QTH sull'immagine
            if (_st.TryQth(out var q)) { var (qx, qy) = cal.ToPixel(q.Lat, q.Lon); info.Children.Add(TB(Lang.F("u.check_pixel", qx.ToString("0"), qy.ToString("0")), 12, _th.Sub)); }
        }
        var save = PrimaryButton(Lang.T("u.save_calibration_and_test"), () =>
        {
            cal.Passi = ed.Images.Where(x => x.on).OrderBy(x => x.ore).Select(x => (x.ore, x.url)).ToList();
            cal.Fit();
            try { cal.Save(TropoIndex.Folder); ed.Msg = Lang.T("u.calibration_saved_to") + Path.Combine(TropoIndex.Folder, cal.File); }
            catch (Exception ex) { ed.Msg = ex.Message; }
            Task.Run(async () => { await _st.Tropo.UpdateAsync(_st.Http); Dispatcher.UIThread.Post(() => FillTropoTab(fonte)); });
            FillTropoTab(fonte);
        });
        info.Children.Add(save);
        var st = TB(_st.Tropo.Status + (cal.Usable ? "" : Lang.T("u.calibration_incomplete_or_inactive")), 12, _th.Sub);
        st.TextWrapping = TextWrapping.Wrap;
        info.Children.Add(st);

        var right = new Border { Background = B(_th.Panel), BorderBrush = B(_th.Line), BorderThickness = new Thickness(1, 0, 0, 0), Child = new ScrollViewer { Content = info } };
        Grid.SetColumn(right, 1);
        g.Children.Add(right);
        holder.Child = g;
    }


    // ================================================================ aggiornamento periodico

    string _ntpTip;
    void Tick()
    {
        try { FcAutoCheck(); } catch { }
        var now = Clock.UtcNow;
        // schermi stretti (1366×768): sotto i 1500 pixel solo l'ora, la data sta già nella barra del sistema
        bool compact = Bounds.Width > 0 && Bounds.Width < 1500;
        if (_clock != null) _clock.Text = (compact ? now.ToString("HH:mm:ss") : now.ToString("dd/MM/yyyy HH:mm:ss")) + " UTC";
        if (_ntp != null)
        {
            bool checkedOnce = _st.Ntp.LastCheckUtc != default;
            _ntp.Text = !checkedOnce ? "NTP:…" : Clock.NtpOk ? "NTP:OK" : "NTP:FAILED";
            _ntp.Foreground = B(!checkedOnce ? _th.Muted : Clock.NtpOk ? _th.Ok : _th.Bad);
            { var nt = (!checkedOnce ? Lang.Tr("Controllo dell'orario NTP in corso.", "NTP time check in progress.") : Clock.NtpOk ? Lang.Tr("Orologio allineato con NTP, scarto ", "Clock aligned with NTP, offset ") + Clock.OffsetMs.ToString("0") + " ms" : Lang.Tr("NTP non risponde: il programma continua con l'orologio del PC e riprova ogni 3 minuti.", "NTP not answering: the program carries on with the PC clock and retries every 3 minutes.")); if (nt != _ntpTip) { _ntpTip = nt; ToolTip.SetTip(_ntp, nt); } }
        }
        if (_zoomPct != null) _zoomPct.Text = _map.ZoomPercent;
        if (now.Second == 0) UpdateFcLabel();

        if (_statusPanel != null)
        {
            string Dot(bool ok) => ok ? "●" : "○";
            void Set(string id, string text, Color col, string tip = null)
            {
                if (!_statusSegs.TryGetValue(id, out var t)) return;
                t.Text = text; t.Foreground = B(col); t.IsVisible = text != "";
                if (tip != null) ToolTip.SetTip(t, tip);
            }
            string Why(string err)
            {
                if (string.IsNullOrEmpty(err)) return Lang.Tr("non collegato (spento nelle impostazioni o in attesa)", "not connected (off or waiting)");
                var low = err.ToLowerInvariant();
                if (low.Contains("rifiut") || low.Contains("refused")) return err + (Lang.Tr("\nIl server rifiuta la connessione: porta sbagliata o servizio spento. Controlla host e porta.", "\nThe server refuses the connection: wrong port or service down. Check host and port."));
                if (low.Contains("nodo del cluster fermo")) return err + (Lang.Tr("\nNon dipende dal programma: il nodo è fermo dal lato del server. Intanto usa un altro cluster.", "\nNot a program fault: the node is down server-side. Use another cluster meanwhile."));
                if (low.Contains("timeout") || low.Contains("tempo")) return err + (Lang.Tr("\nNessuna risposta: rete lenta, firewall o server occupato.", "\nNo answer: slow network, firewall or busy server."));
                if (low.Contains("host") || low.Contains("nome") || low.Contains("name")) return err + (Lang.Tr("\nIndirizzo del server non trovato: controlla il nome o la connessione a internet.", "\nServer address not found: check the name or the internet connection."));
                return err;
            }
            foreach (var f in _st.Feeds)
                Set("feed:" + f.Id, $"{Dot(f.Connected)} {f.Src}", f.Connected ? _th.Ok : f.LastError != "" ? _th.Bad : _th.Muted,
                    $"{f.Host}:{f.Port}\n" + (f.Connected ? (Lang.Tr("collegato", "connected")) : Why(f.LastError)) + (Lang.Tr("\nClic: apri la fonte", "\nClick: open the source")));
            var q = _st.Mqtt;
            Set("mqtt", $"{Dot(q.Connected)} PSKReporter MQTT", q.Connected ? _th.Ok : q.LastError != "" ? _th.Bad : _th.Muted,
                $"{q.Host}:{q.Port}\n" + (q.Connected ? (Lang.Tr("collegato", "connected")) : Why(q.LastError)) + (Lang.Tr("\nClic: apri la fonte", "\nClick: open the source")));
            var u = _st.Udp;
            Set("udp", $"{Dot(u.Running)} UDP {u.ListenPort}→{u.ForwardPort}", u.Running ? (u.Packets > 0 ? _th.Ok : _th.Muted) : u.LastError != "" ? _th.Bad : _th.Muted,
                (u.Running ? (u.Packets > 0 ? (Lang.Tr("in ascolto, {0} pacchetti ricevuti", "listening, {0} packets", u.Packets)) : (Lang.Tr("in ascolto, ma WSJT-X / JTDX / MSHV non mandano ancora nulla: controlla in quei programmi Impostazioni → Reporting → UDP Server (indirizzo e porta)", "listening, but WSJT-X / JTDX / MSHV send nothing yet: check Settings → Reporting → UDP Server")))
                           : Why(u.LastError)) + (Lang.Tr("\nClic: apri la fonte", "\nClick: open the source")));
            Set("pressure", _st.Pressure.Status, _th.Muted);
            Set("tropo", _st.Tropo.Status, _th.Muted);
            bool callBad = _st.CallDb.Ready && (_st.CallDb.Error != "" || _st.CallDb.Count == 0);
            Set("call3", _st.CallDb.Ready ? (_st.CallDb.Error != "" ? "⚠ " + _st.CallDb.Error : $"CALL3: {_st.CallDb.Count:N0}") : "CALL3: …", callBad ? _th.Bad : _th.Muted);
            var other = new List<string>();
            if (!_st.Map.Loaded && _st.Map.Status != "") other.Add(_st.Map.Status);
            if (_st.Archive.Error != "") other.Add("DB: " + _st.Archive.Error);
            Set("other", string.Join("   ·   ", other), _th.Muted);
            bool recentErr = _lastErrorNote != "" && (DateTime.Now - _lastErrorAt).TotalMinutes < 15;
            Set("error", recentErr ? (Lang.Tr("⚠ errore annotato", "⚠ error logged")) : "", _th.Bad,
                recentErr ? (Lang.Tr("{0:HH:mm:ss} {1}\nIl programma continua. Clic: apri il registro degli errori ({2})", "{0:HH:mm:ss} {1}\nThe program goes on. Click: open the error log", _lastErrorAt, _lastErrorNote, ErrorLog.FilePath)) : null);
            Set("version", "v" + (typeof(MainWindow).Assembly.GetName().Version?.ToString(3) ?? "") + (Updater.Status != "" ? " · " + Updater.Status : ""), _th.Muted);
        }

        if (_st.Dirty)
        {
            _st.Dirty = false;
            _map.InvalidateVisual();
            if ((_tab == "hepburn" || _tab == "f5len") && _previews.TryGetValue(_tab, out var pvw)) pvw.InvalidateVisual();
            if (_tab == "map") { RefreshSpotList(); RefreshInfoCard(); }
        }
        if (_st.LogsDirty) RefreshLogs(false);
        if ((_tab == "hepburn" || _tab == "f5len") && _st.Tropo.LastUpdateUtc != _tropoShown)
        {
            _tropoShown = _st.Tropo.LastUpdateUtc;
            FillTropoTab(_tab);
        }
    }

    static void FillLog(StackPanel panel, ScrollViewer sv, IEnumerable<(string text, Color col, bool strike)> lines)
    {
        var atBottom = sv.Offset.Y + sv.Viewport.Height >= sv.Extent.Height - 4;
        panel.Children.Clear();
        foreach (var (text, col, strike) in lines)
        {
            var t = new TextBlock { Text = text, FontFamily = Mono, FontSize = (12.5) * UiScale.F, Foreground = new SolidColorBrush(col) };
            if (strike) t.TextDecorations = TextDecorations.Strikethrough;
            panel.Children.Add(t);
        }
        if (atBottom) sv.ScrollToEnd();
    }

    void RefreshLogs(bool force)
    {
        if (!force && !_st.LogsDirty) return;
        _st.LogsDirty = false;
        if (_tab == "collect")
        {
            foreach (var f in _st.Feeds)
            {
                if (!_sections.TryGetValue(f.Id, out var s)) continue;
                s.status.Text = f.Connected ? string.Format(Lang.T("connected"), f.Host + ":" + f.Port) : (f.LastError != "" ? f.LastError : Lang.T("notconn"));
                s.status.Foreground = B(f.Connected ? _th.Ok : f.LastError != "" ? _th.Bad : _th.Muted);
                s.counters.Text = Lang.F("u.feed_counters", f.Lines, f.Saved, f.OutBand, f.Excluded);
                List<(string, LineClass)> copy;
                var l = _st.Logs[f.Id];
                lock (l) copy = l.Skip(Math.Max(0, l.Count - 150)).ToList();
                FillLog(s.log, s.scroll, copy.Where(x => x.Item2 != LineClass.OutOfBand && x.Item2 != LineClass.Excluded).Select(x => (x.Item1, x.Item2 == LineClass.Other ? _th.Sub : _th.Text, false)));
            }
            if (_sections.TryGetValue("mqtt", out var m))
            {
                var q = _st.Mqtt;
                m.status.Text = q.Connected ? string.Format(Lang.T("connected"), q.Host + ":" + q.Port) : (q.LastError != "" ? q.LastError : Lang.T("notconn"));
                m.status.Foreground = B(q.Connected ? _th.Ok : q.LastError != "" ? _th.Bad : _th.Muted);
                var now = Clock.UtcNow;
                m.counters.Text = (Lang.T("u.reports")) + q.Reports + " · " + string.Join(" · ", q.EnabledBands.Select(b => $"{b}: {_st.Listeners.Get(b, 15, now).Count}"));
                List<(string, LineClass)> copy;
                var l = _st.Logs["mqtt"];
                lock (l) copy = l.Skip(Math.Max(0, l.Count - 150)).ToList();
                FillLog(m.log, m.scroll, copy.Select(x => (x.Item1, x.Item2 == LineClass.Other ? _th.Sub : _th.Text, false)));
            }
        }
        if (_tab == "collect" && _subTab == "udp" && _udpStatus != null)
        {
            var u = _st.Udp;
            _udpStatus.Text = u.Running ? $"{(Lang.T("u.listening_on"))} {u.ListenAddress}:{u.ListenPort}" + (u.Forward ? $" · {(Lang.T("u.forward_to"))} {u.ForwardAddress}:{u.ForwardPort}" : "") : (u.LastError != "" ? u.LastError : (Lang.T("u.stopped")));
            _udpStatus.Foreground = B(u.Running ? _th.Ok : u.LastError != "" ? _th.Bad : _th.Muted);
            var dial = u.DialHz > 0 ? $"{u.DialHz / 1e6:0.000000} MHz {u.DialMode}" : "—";
            _udpInfo.Text = $"{(u.ProgramId == "" ? "—" : u.ProgramId + " " + u.ProgramVersion)}   ·   {dial}   ·   {(Lang.T("u.packets"))} {u.Packets}   ·   {(Lang.T("u.forwarded"))} {u.Forwarded}   ·   decode {u.Decodes}";
            List<(string, bool)> copy;
            lock (_st.UdpLog) copy = _st.UdpLog.Skip(Math.Max(0, _st.UdpLog.Count - 200)).ToList();
            bool hb = _udpShowHb?.IsChecked == true;
            var red = _th.Dark ? Color.Parse("#FF7A7A") : Color.Parse("#C62828");
            FillLog(_udpLog, _udpScroll, copy.Where(x => hb || x.Item2 || !(x.Item1.Contains("Heartbeat") || x.Item1.Contains("Status"))).Select(x => (x.Item1, x.Item2 ? red : _th.Sub, false)));

        }
    }
}
