using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace TropoVHF.Ui;

/// <summary>
/// Immagine di partenza (Assets/splash.jpg, ispirata a Hepburn, Elevated Ducting) con il numero di versione scritto in
/// bianco in alto a sinistra, nella fascia rossa dell'aria calda e secca. Si chiude da sola quando la finestra
/// principale è pronta, oppure con un clic.
/// </summary>
public sealed class Splash : Window
{
    public static Bitmap LoadBitmap(string name)
    {
        var asm = typeof(Splash).Assembly;
        var res = asm.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(name, StringComparison.OrdinalIgnoreCase));
        if (res == null) return null;
        using var s = asm.GetManifestResourceStream(res);
        return new Bitmap(s);
    }

    public static WindowIcon AppIcon()
    {
        try
        {
            var asm = typeof(Splash).Assembly;
            var res = asm.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith("livetropovhf.ico", StringComparison.OrdinalIgnoreCase));
            return res == null ? null : new WindowIcon(asm.GetManifestResourceStream(res));
        }
        catch { return null; }
    }

    public Splash()
    {
        SystemDecorations = SystemDecorations.None;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        CanResize = false;
        Topmost = true;
        Title = "LIVETROPOVHF";
        Icon = AppIcon();
        Background = new SolidColorBrush(Color.Parse("#0F2440"));
        var ver = typeof(Splash).Assembly.GetName().Version?.ToString(3) ?? "";

        // grandezza: il 60% della larghezza dello schermo, al massimo l'immagine intera
        double w = 900;
        try { var sc = Screens.Primary; if (sc != null) w = Math.Min(1200, Math.Max(600, sc.WorkingArea.Width / sc.Scaling * 0.6)); } catch { }
        Width = w; Height = w * 896 / 1200;

        var grid = new Grid { Width = 1200, Height = 896 };
        var bmp = LoadBitmap("splash.jpg");
        if (bmp != null) grid.Children.Add(new Image { Source = bmp, Stretch = Stretch.Fill });
        // versione: bianca, in alto a sinistra della fascia rossa (l'immagine è 1200 × 896, la fascia parte da y ≈ 150)
        grid.Children.Add(new TextBlock
        {
            Text = "v" + ver,
            Foreground = Brushes.White,
            FontSize = 44, FontWeight = FontWeight.Bold,
            HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(84, 162, 0, 0),
            Effect = new DropShadowEffect { BlurRadius = 6, OffsetX = 0, OffsetY = 1, Color = Color.Parse("#80000000") },
        });
        Content = new Viewbox { Stretch = Stretch.Uniform, Child = grid };
        PointerPressed += (_, _) => Close();
    }
}
