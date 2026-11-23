using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Themes.Fluent;
using TropoVHF.Ui;

namespace TropoVHF;

public sealed class App : Application
{
    public override void Initialize()
    {
        Styles.Add(new FluentTheme());
    }

    static DateTime _lastUiErr; static string _lastUiMsg = ""; static int _sameUiErr;
    public override void OnFrameworkInitializationCompleted()
    {
        // errori dell'interfaccia: si annotano e il programma continua (stesso errore ripetuto: annotato una volta ogni 30 s)
        Avalonia.Threading.Dispatcher.UIThread.UnhandledException += (_, e) =>
        {
            var msg = e.Exception.GetType().Name + e.Exception.Message;
            var now = DateTime.UtcNow;
            if (msg == _lastUiMsg && (now - _lastUiErr).TotalSeconds < 30) { _sameUiErr++; e.Handled = true; return; }
            if (_sameUiErr > 0) Core.ErrorLog.Note($"(lo stesso errore si è ripetuto altre {_sameUiErr} volte)");
            _lastUiMsg = msg; _lastUiErr = now; _sameUiErr = 0;
            Core.ErrorLog.Write(e.Exception, "interfaccia");
            e.Handled = true;
        };
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // prima l'immagine di partenza, poi la finestra principale; l'immagine resta almeno 2,5 secondi
            Splash splash = null;
            try { splash = new Splash(); splash.Show(); } catch { splash = null; }
            var shownAt = DateTime.UtcNow;
            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
            {
                var w = new MainWindow();
                desktop.MainWindow = w;
                desktop.ShutdownMode = ShutdownMode.OnMainWindowClose;
                w.Show();
                if (splash != null)
                {
                    var left = TimeSpan.FromSeconds(2.5) - (DateTime.UtcNow - shownAt);
                    if (left < TimeSpan.FromMilliseconds(300)) left = TimeSpan.FromMilliseconds(300);
                    Avalonia.Threading.DispatcherTimer.RunOnce(() => { try { splash.Close(); } catch { } }, left);
                }
            }, Avalonia.Threading.DispatcherPriority.Background);
        }
        base.OnFrameworkInitializationCompleted();
    }
}

public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        // registro degli errori sempre attivo: errori non gestiti dell'interfaccia, dei calcoli e delle connessioni
        Core.ErrorLog.Context = () => Forecast.CalcLog.RecentLines(50);
        AppDomain.CurrentDomain.UnhandledException += (_, e) => Core.ErrorLog.Write(e.ExceptionObject as Exception, "AppDomain", e.IsTerminating);
        TaskScheduler.UnobservedTaskException += (_, e) =>
        {
            e.SetObserved();
            // socket chiuso mentre una ricezione era in attesa (NTP, UDP): normale, non è un errore
            bool chiuso = System.Linq.Enumerable.All(e.Exception.InnerExceptions, x => x is ObjectDisposedException || x is OperationCanceledException || (x is System.Net.Sockets.SocketException se && (se.ErrorCode == 995 || se.SocketErrorCode == System.Net.Sockets.SocketError.OperationAborted || se.SocketErrorCode == System.Net.Sockets.SocketError.Interrupted)));
            if (!chiuso) Core.ErrorLog.Write(e.Exception, "attività in background");
        };
        int pf = Array.IndexOf(args, "--prova-fonti");
        if (pf >= 0) { Environment.ExitCode = Forecast.SourceTest.Run(pf + 1 < args.Length ? args[pf + 1] : "prova-fonti.txt"); return; }
        int pv = Array.IndexOf(args, "--valida");
        if (pv >= 0 && pv + 2 < args.Length) { Environment.ExitCode = Forecast.Validation.Run(args[pv + 1], args[pv + 2]); return; }
        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            // errore all'avvio: lo scrive accanto al programma per poterlo leggere
            try { File.WriteAllText(Path.Combine(Core.Settings.AppDir, "errore-avvio.txt"), ex.ToString()); } catch { }
            Core.ErrorLog.Write(ex, "avvio o ciclo principale", true);
            throw;
        }
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}
