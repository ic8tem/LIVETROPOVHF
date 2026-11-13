using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using TropoVHF.Core;

namespace TropoVHF.Ui;

/// <summary>Disegna una formula nello stile di un editor di equazioni: variabili in corsivo,
/// funzioni e numeri in tondo, frazioni impilate, pedici e apici rimpiccioliti.</summary>
public static class MathView
{
    static readonly FontFamily MathFont = new("Cambria Math, STIX Two Math, Latin Modern Math, Times New Roman, serif");

    public static Control Build(string latex, double size, IBrush ink) => Row(MathParser.Parse(latex), size, ink);

    static Control Row(MRow row, double size, IBrush ink)
    {
        var sp = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
        for (int i = 0; i < row.Items.Count; i++)
        {
            var n = row.Items[i];
            var c = Node(n, size, ink);
            // piccolo spazio tra una funzione (log, sin…) e ciò che segue
            if (n is MRun r && r.Kind == MKind.Func) c.Margin = new Thickness(0, 0, size * 0.18, 0);
            sp.Children.Add(c);
        }
        return sp;
    }

    static Control Node(MNode n, double size, IBrush ink)
    {
        switch (n)
        {
            case MRun r:
            {
                var t = new TextBlock
                {
                    Text = r.Text, FontFamily = MathFont, FontSize = size, Foreground = ink,
                    FontStyle = r.Kind == MKind.Var ? FontStyle.Italic : FontStyle.Normal,
                    VerticalAlignment = VerticalAlignment.Center
                };
                if (r.Kind == MKind.Op) t.Margin = new Thickness(size * 0.28, 0);
                if (r.Kind == MKind.Space) t.Margin = new Thickness(size * 0.05, 0);
                return t;
            }
            case MRow row:
                return Row(row, size, ink);
            case MFrac f:
            {
                double s2 = size * 0.92;
                var num = Row(f.Num, s2, ink); num.HorizontalAlignment = HorizontalAlignment.Center;
                var den = Row(f.Den, s2, ink); den.HorizontalAlignment = HorizontalAlignment.Center;
                var bar = new Border { Height = Math.Max(1, size / 16), Background = ink, Margin = new Thickness(0, size * 0.08) };
                return new StackPanel { Children = { num, bar, den }, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(size * 0.12, 0) };
            }
            case MScript sc:
            {
                double s2 = size * 0.68;
                var baseC = Node(sc.Base, size, ink);
                var col = new StackPanel { VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(size * 0.03, 0, 0, 0) };
                if (sc.Sup != null) col.Children.Add(Row(sc.Sup, s2, ink));
                else col.Children.Add(new Border { Height = s2 * 0.9 });
                if (sc.Sub != null) col.Children.Add(Row(sc.Sub, s2, ink));
                else col.Children.Add(new Border { Height = s2 * 0.9 });
                return new StackPanel { Orientation = Orientation.Horizontal, Children = { baseC, col }, VerticalAlignment = VerticalAlignment.Center };
            }
            case MSqrt q:
            {
                var body = new Border { BorderBrush = ink, BorderThickness = new Thickness(0, Math.Max(1, size / 16), 0, 0), Padding = new Thickness(size * 0.1, size * 0.05, 0, 0), Child = Row(q.Body, size, ink) };
                return new StackPanel { Orientation = Orientation.Horizontal, Children = { new TextBlock { Text = "√", FontFamily = MathFont, FontSize = size * 1.15, Foreground = ink, VerticalAlignment = VerticalAlignment.Center }, body } };
            }
        }
        return new TextBlock();
    }
}
