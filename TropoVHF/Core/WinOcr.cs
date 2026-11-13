#if WINDOWS
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Security.Cryptography;

namespace TropoVHF.Core;

/// <summary>OCR integrato in Windows 10/11 (Windows.Media.Ocr): nessuna libreria da installare.</summary>
public sealed class WinOcr : ITextReader
{
    readonly OcrEngine _engine;
    WinOcr(OcrEngine e) { _engine = e; }

    public int MaxDimension => (int)OcrEngine.MaxImageDimension;

    public static WinOcr TryCreate()
    {
        try
        {
            var e = OcrEngine.TryCreateFromLanguage(new Windows.Globalization.Language("en-US"))
                    ?? OcrEngine.TryCreateFromUserProfileLanguages();
            return e == null ? null : new WinOcr(e);
        }
        catch { return null; }
    }

    public async Task<List<OcrWord>> ReadAsync(byte[] bgra, int width, int height)
    {
        var buffer = CryptographicBuffer.CreateFromByteArray(bgra);
        using var bmp = SoftwareBitmap.CreateCopyFromBuffer(buffer, BitmapPixelFormat.Bgra8, width, height, BitmapAlphaMode.Premultiplied);
        var res = await _engine.RecognizeAsync(bmp);
        var list = new List<OcrWord>();
        foreach (var line in res.Lines)
            foreach (var w in line.Words)
                list.Add(new OcrWord(w.Text, w.BoundingRect.X, w.BoundingRect.Y, w.BoundingRect.Width, w.BoundingRect.Height));
        return list;
    }
}
#endif
