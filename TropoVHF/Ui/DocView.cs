using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace TropoVHF.Ui;

/// <summary>Mostra un documento tecnico (Markdown semplice) come foglio bianco con titoli blu e filetto.</summary>
public static class DocView
{
    static readonly IBrush Blue = new SolidColorBrush(Color.Parse("#1F3A7A"));
    static readonly IBrush Body = new SolidColorBrush(Color.Parse("#2A2F36"));
    static readonly IBrush Grey = new SolidColorBrush(Color.Parse("#5A6470"));
    static readonly IBrush Rule = new SolidColorBrush(Color.Parse("#C9D3E6"));
    static readonly IBrush CodeBg = new SolidColorBrush(Color.Parse("#F4F6FA"));
    static readonly IBrush TableHead = new SolidColorBrush(Color.Parse("#EEF2F8"));
    static readonly FontFamily Mono = new("Consolas, Menlo, DejaVu Sans Mono, monospace");

    public static string LoadResource(string fileName)
    {
        var asm = Assembly.GetExecutingAssembly();
        var name = asm.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));
        if (name == null) return "# Documento non trovato\n";
        using var s = asm.GetManifestResourceStream(name);
        using var r = new StreamReader(s);
        return r.ReadToEnd();
    }

    public static Control Build(string markdown, Palette th, string exportName = null)
    {
        var sheet = new StackPanel { Spacing = 8 };
        var lines = markdown.Replace("\r", "").Split('\n');
        bool code = false, math = false;
        var codeLines = new List<string>();
        Grid table = null;
        int tableRow = 0;

        void EndTable() { table = null; tableRow = 0; }

        foreach (var raw in lines)
        {
            var l = raw.TrimEnd();
            if (l.StartsWith("```"))
            {
                if (code && math)
                {
                    // formule in stile editor di equazioni, una per riga, centrate
                    var eqs = new StackPanel { Spacing = 14, HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0, 6, 0, 8) };
                    foreach (var eq in codeLines.Where(x => x.Trim().Length > 0))
                    {
                        var m = MathView.Build(eq.Trim(), 19, Body);
                        m.HorizontalAlignment = HorizontalAlignment.Center;
                        eqs.Children.Add(m);
                    }
                    sheet.Children.Add(eqs);
                    codeLines.Clear();
                }
                else if (code)
                {
                    sheet.Children.Add(new Border
                    {
                        Background = CodeBg, BorderBrush = Rule, BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(4),
                        Padding = new Thickness(14, 10),
                        Child = new TextBlock { Text = string.Join("\n", codeLines), FontFamily = Mono, FontSize = 13, Foreground = Body, TextWrapping = TextWrapping.Wrap, LineHeight = 22 }
                    });
                    codeLines.Clear();
                }
                if (!code) math = l.Trim() == "```math";
                code = !code;
                continue;
            }
            if (code) { codeLines.Add(l); continue; }

            if (l.StartsWith("| "))
            {
                var cells = l.Trim().Trim('|').Split('|').Select(c => c.Trim()).ToArray();
                if (table == null)
                {
                    table = new Grid { Margin = new Thickness(0, 4, 0, 8) };
                    for (int i = 0; i < cells.Length; i++) table.ColumnDefinitions.Add(new ColumnDefinition(i == cells.Length - 1 ? GridLength.Star : GridLength.Auto));
                    sheet.Children.Add(table);
                }
                table.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
                for (int i = 0; i < cells.Length && i < table.ColumnDefinitions.Count; i++)
                {
                    var b = new Border
                    {
                        BorderBrush = new SolidColorBrush(Color.Parse("#D4DCE8")), BorderThickness = new Thickness(1),
                        Background = tableRow == 0 ? TableHead : Brushes.White, Padding = new Thickness(8, 5),
                        Child = new TextBlock { Text = cells[i], TextWrapping = TextWrapping.Wrap, FontSize = 13, Foreground = tableRow == 0 ? Blue : Body, FontWeight = tableRow == 0 ? FontWeight.SemiBold : FontWeight.Normal, MaxWidth = i == cells.Length - 1 ? double.PositiveInfinity : 260 }
                    };
                    Grid.SetRow(b, tableRow); Grid.SetColumn(b, i);
                    table.Children.Add(b);
                }
                tableRow++;
                continue;
            }
            EndTable();

            if (l.Length == 0) continue;
            if (l.StartsWith("# "))
            {
                sheet.Children.Add(new TextBlock { Text = l.Substring(2), FontSize = 28, FontWeight = FontWeight.Bold, Foreground = Blue, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 2, 0, 0) });
            }
            else if (l.StartsWith("## "))
            {
                sheet.Children.Add(new Border
                {
                    BorderBrush = Rule, BorderThickness = new Thickness(0, 0, 0, 1), Padding = new Thickness(0, 0, 0, 6), Margin = new Thickness(0, 18, 0, 2),
                    Child = new TextBlock { Text = l.Substring(3), FontSize = 21, FontWeight = FontWeight.Bold, Foreground = Blue, TextWrapping = TextWrapping.Wrap }
                });
            }
            else if (l.StartsWith("- "))
            {
                sheet.Children.Add(Item("•", l.Substring(2)));
            }
            else if (char.IsDigit(l[0]) && l.Contains(". ") && l.IndexOf(". ") <= 3)
            {
                int k = l.IndexOf(". ");
                sheet.Children.Add(Item(l.Substring(0, k + 1), l.Substring(k + 2)));
            }
            else
            {
                bool kicker = sheet.Children.Count == 0;
                sheet.Children.Add(new TextBlock
                {
                    Text = l, TextWrapping = TextWrapping.Wrap, FontSize = kicker ? 12 : 14, LineHeight = kicker ? 16 : 23,
                    Foreground = kicker ? Grey : Body, FontWeight = kicker ? FontWeight.SemiBold : FontWeight.Normal
                });
            }
        }

        var page = new Border
        {
            Background = Brushes.White,
            CornerRadius = new CornerRadius(4),
            BorderBrush = new SolidColorBrush(Color.Parse("#DDE3EC")), BorderThickness = new Thickness(1),
            Padding = new Thickness(56, 44, 56, 52),
            MaxWidth = 940,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(20, 28, 20, 48),
            Child = sheet
        };
        var outer = new StackPanel();
        if (exportName != null)
        {
            var bar = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12, HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(20, 16, 20, 0), MaxWidth = 940 };
            var btn = new Button { Content = Lang.T("u.export_to_word_docx"), Background = th.B(th.Accent), Foreground = th.B(th.AccentFg), CornerRadius = new CornerRadius(16), Padding = new Thickness(18, 6), FontWeight = FontWeight.SemiBold };
            var msg = new TextBlock { Foreground = th.B(th.Muted), VerticalAlignment = VerticalAlignment.Center, TextWrapping = TextWrapping.Wrap, MaxWidth = 700 };
            btn.Click += async (_, _) =>
            {
                try
                {
                    var path = await DocxSave.AskAndSave(btn, markdown, exportName);
                    if (path != null) msg.Text = (Lang.T("u.saved")) + path;
                }
                catch (Exception ex) { msg.Text = (Lang.T("u.export_failed")) + ex.Message; }
            };
            bar.Children.Add(btn); bar.Children.Add(msg);
            outer.Children.Add(bar);
        }
        page.Margin = new Thickness(20, exportName != null ? 14 : 28, 20, 48);
        outer.Children.Add(page);
        return new ScrollViewer { Content = outer, Background = th.B(th.Page), HorizontalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Disabled };
    }

    static Control Item(string bullet, string text)
    {
        var g = new Grid { ColumnDefinitions = new ColumnDefinitions("30,*") };
        var b = new TextBlock { Text = bullet, FontWeight = FontWeight.Bold, Foreground = new SolidColorBrush(Color.Parse("#1F2A3A")), FontSize = 14 };
        var t = new TextBlock { Text = text, TextWrapping = TextWrapping.Wrap, FontSize = 14, LineHeight = 23, Foreground = Body };
        Grid.SetColumn(t, 1);
        g.Children.Add(b); g.Children.Add(t);
        return g;
    }
}
