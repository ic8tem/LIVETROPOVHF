using System.Drawing;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using TropoVHF.Core;

namespace TropoVHF.Win7;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        // Windows 7: TLS 1.2 non è attivo da solo per .NET Framework; GitHub, MMMonVHF e le fonti meteo lo richiedono
        try { ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls; } catch { }
        ServicePointManager.DefaultConnectionLimit = Math.Max(ServicePointManager.DefaultConnectionLimit, 16);

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        // registro degli errori sempre attivo (Dati/errori.log): gli errori dell'interfaccia si annotano e il programma continua
        ErrorLog.Context = () => TropoVHF.Forecast.CalcLog.RecentLines(50);
        Application.ThreadException += (_, e) => Report(e.Exception, false);
        AppDomain.CurrentDomain.UnhandledException += (_, e) => Report(e.ExceptionObject as Exception, true);
        TaskScheduler.UnobservedTaskException += (_, e) =>
        {
            e.SetObserved();
            // socket chiuso mentre una ricezione era in attesa (NTP, UDP): normale, non è un errore
            bool chiuso = System.Linq.Enumerable.All(e.Exception.InnerExceptions, x => x is ObjectDisposedException || x is OperationCanceledException || (x is System.Net.Sockets.SocketException se && (se.ErrorCode == 995 || se.SocketErrorCode == System.Net.Sockets.SocketError.OperationAborted || se.SocketErrorCode == System.Net.Sockets.SocketError.Interrupted)));
            if (!chiuso) ErrorLog.Write(e.Exception, "attività in background");
        };

        Splash splash = null;
        try { splash = new Splash(); splash.Show(); Application.DoEvents(); } catch { splash = null; }
        var shownAt = DateTime.UtcNow;
        MainForm main;
        try { main = new MainForm(); }
        catch (Exception ex) { try { splash?.Close(); } catch { } Report(ex, true); return; }
        main.StartSplash = splash;
        main.SplashShownAt = shownAt;
        Application.Run(main);
    }

    static void Report(Exception ex, bool fatal)
    {
        if (ex == null) return;
        ErrorLog.Write(ex, fatal ? "chiusura" : "interfaccia", fatal);
        if (!fatal) return;
        try { File.AppendAllText(Path.Combine(Settings.AppDir, "errore-avvio.txt"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + ex + "\r\n\r\n"); } catch { }
        if (fatal)
            try { MessageBox.Show(ex.Message + "\r\n\r\n" + Path.Combine(Settings.AppDir, "errore-avvio.txt"), "LIVETROPOVHF", MessageBoxButtons.OK, MessageBoxIcon.Error); } catch { }
    }

    public static string Version => typeof(Program).Assembly.GetName().Version?.ToString(3) ?? "";

    public static Stream Resource(string fileName)
    {
        var asm = Assembly.GetExecutingAssembly();
        var n = asm.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));
        return n == null ? null : asm.GetManifestResourceStream(n);
    }

    public static string ResourceText(string fileName)
    {
        // guide: prima la traduzione nella lingua scelta, poi l'inglese, poi l'italiano
        Stream s = null;
        if (fileName.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
        {
            var all = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var n = TropoVHF.Ui.Lang.DocCandidates(fileName).Select(c => all.FirstOrDefault(x => x.EndsWith(c, StringComparison.OrdinalIgnoreCase))).FirstOrDefault(x => x != null);
            if (n != null) s = Assembly.GetExecutingAssembly().GetManifestResourceStream(n);
        }
        s ??= Resource(fileName);
        using var _ = s;
        if (s == null) return "# Documento non trovato\n";
        using var r = new StreamReader(s);
        return r.ReadToEnd();
    }

    public static Icon AppIcon()
    {
        try { var s = Resource("livetropovhf.ico"); return s == null ? null : new Icon(s); } catch { return null; }
    }

    public static Image LoadImage(string fileName)
    {
        try
        {
            using var s = Resource(fileName);
            if (s == null) return null;
            using var tmp = Image.FromStream(s);
            return new Bitmap(tmp);
        }
        catch { return null; }
    }
}

/// <summary>Immagine di partenza con la versione in bianco, in alto a sinistra della fascia rossa.</summary>
public sealed class Splash : Form
{
    readonly Image _img;
    public Splash()
    {
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.CenterScreen;
        ShowInTaskbar = false;
        TopMost = false;
        Text = "LIVETROPOVHF";
        Icon = Program.AppIcon();
        BackColor = Color.FromArgb(0x0F, 0x24, 0x40);
        _img = Program.LoadImage("splash.jpg");
        var wa = Screen.PrimaryScreen.WorkingArea;
        int w = Math.Min(1200, Math.Max(600, (int)(wa.Width * 0.6)));
        ClientSize = new Size(w, w * 896 / 1200);
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        Click += (_, _) => Close();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        if (_img != null) g.DrawImage(_img, new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
        float k = ClientSize.Width / 1200f;
        using var f = new Font("Segoe UI", 44 * k, FontStyle.Bold, GraphicsUnit.Pixel);
        var txt = "v" + Program.Version;
        using (var sh = new SolidBrush(Color.FromArgb(128, 0, 0, 0))) g.DrawString(txt, f, sh, 84 * k, 163 * k);
        g.DrawString(txt, f, Brushes.White, 84 * k, 162 * k);
    }

    protected override void Dispose(bool disposing) { if (disposing) _img?.Dispose(); base.Dispose(disposing); }
}
