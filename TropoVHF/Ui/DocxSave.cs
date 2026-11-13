using Avalonia.Controls;
using Avalonia.Platform.Storage;
using TropoVHF.Core;

namespace TropoVHF.Ui;

/// <summary>Chiede dove salvare e scrive il documento in Word (.docx).</summary>
public static class DocxSave
{
    public static async Task<string> AskAndSave(Control owner, string markdown, string name)
    {
        var top = TopLevel.GetTopLevel(owner);
        if (top?.StorageProvider != null && top.StorageProvider.CanSave)
        {
            var file = await top.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = Lang.T("u.export_to_word"),
                SuggestedFileName = name + ".docx",
                DefaultExtension = "docx",
                FileTypeChoices = new[] { new FilePickerFileType(Lang.T("u.word_document")) { Patterns = new[] { "*.docx" } } }
            });
            if (file == null) return null;
            await using (var s = await file.OpenWriteAsync())
            {
                if (s.CanSeek) s.SetLength(0);
                DocxWriter.Write(s, markdown);
            }
            return file.TryGetLocalPath() ?? file.Name;
        }

        // nessuna finestra di dialogo disponibile: salva nella cartella Documenti accanto al programma
        var dir = Path.Combine(Settings.AppDir, "Documenti");
        Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, name + ".docx");
        await using (var fs = File.Create(path)) DocxWriter.Write(fs, markdown);
        return path;
    }
}
