using Avalonia;
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
            desktop.MainWindow = new MainWindow();
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
