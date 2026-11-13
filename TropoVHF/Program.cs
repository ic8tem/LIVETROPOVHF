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

    public override void OnFrameworkInitializationCompleted()
    {
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
        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            // errore all'avvio: lo scrive accanto al programma per poterlo leggere
            try { File.WriteAllText(Path.Combine(Core.Settings.AppDir, "errore-avvio.txt"), ex.ToString()); } catch { }
            throw;
        }
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}
