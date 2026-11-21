using System.IO.Compression;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace TropoVHF.Core;

/// <summary>
/// Esporta un documento tecnico (Markdown semplice del programma) in Word .docx:
/// titoli blu con filetto, fonti numerate, tabelle, e formule come equazioni native di Word (OMML).
/// </summary>
public static class DocxWriter
{
    const string W = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
    const string M = "http://schemas.openxmlformats.org/officeDocument/2006/math";
    const string R = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";

    public static void Write(Stream output, string markdown)
    {
        using var zip = new ZipArchive(output, ZipArchiveMode.Create, leaveOpen: true);
        Add(zip, "[Content_Types].xml", ContentTypes);
        Add(zip, "_rels/.rels", RootRels);
        Add(zip, "word/_rels/document.xml.rels", DocRels);
        Add(zip, "word/styles.xml", Styles);
        Add(zip, "word/document.xml", Document(markdown));
    }

    static void Add(ZipArchive zip, string name, string content)
    {
        var e = zip.CreateEntry(name, CompressionLevel.Optimal);
        using var s = e.Open();
        var b = new UTF8Encoding(false).GetBytes(content);
        s.Write(b, 0, b.Length);
    }

    static string X(string s) => SecurityElement.Escape(s ?? "") ?? "";

    // ------------------------------------------------------------ documento

    static string Document(string md)
    {
        var body = new StringBuilder();
        var lines = md.Replace("\r", "").Split('\n');
        bool code = false, math = false, first = true;
        int eqNo = 0;
        var block = new List<string>();
        var table = new List<string[]>();

        void FlushTable()
        {
            if (table.Count == 0) return;
            int n = table.Max(r => r.Length);
            int colW = 9638 / Math.Max(1, n);
            body.Append($"<w:tbl><w:tblPr><w:tblW w:w=\"5000\" w:type=\"pct\"/><w:tblBorders>");
            foreach (var side in new[] { "top", "left", "bottom", "right", "insideH", "insideV" })
                body.Append($"<w:{side} w:val=\"single\" w:sz=\"4\" w:space=\"0\" w:color=\"D4DCE8\"/>");
            body.Append("</w:tblBorders><w:tblCellMar><w:left w:w=\"100\" w:type=\"dxa\"/><w:right w:w=\"100\" w:type=\"dxa\"/></w:tblCellMar></w:tblPr><w:tblGrid>");
            for (int i = 0; i < n; i++) body.Append($"<w:gridCol w:w=\"{colW}\"/>");
            body.Append("</w:tblGrid>");
            for (int r = 0; r < table.Count; r++)
            {
                body.Append("<w:tr>");
                for (int c = 0; c < n; c++)
                {
                    var text = c < table[r].Length ? table[r][c] : "";
                    body.Append("<w:tc><w:tcPr>");
                    if (r == 0) body.Append("<w:shd w:val=\"clear\" w:color=\"auto\" w:fill=\"EEF2F8\"/>");
                    body.Append("</w:tcPr>");
                    body.Append("<w:p><w:pPr><w:pStyle w:val=\"Tabella\"/></w:pPr>");
                    body.Append(Runs(text, r == 0));
                    body.Append("</w:p></w:tc>");
                }
                body.Append("</w:tr>");
            }
            body.Append("</w:tbl><w:p><w:pPr><w:spacing w:after=\"0\"/></w:pPr></w:p>");
            table.Clear();
        }

        foreach (var raw in lines)
        {
            var l = raw.TrimEnd();
            if (l.StartsWith("```"))
            {
                if (code)
                {
                    foreach (var b in block.Where(x => x.Trim().Length > 0))
                    {
                        if (math)
                        {
                            // formula al centro e numero a destra (stile LaTeX) con due tabulazioni
                            bool numbered = !b.Contains("\\notag");
                            body.Append("<w:p><w:pPr><w:pStyle w:val=\"FormulaNum\"/></w:pPr><w:r><w:tab/></w:r><m:oMath>")
                                .Append(Omml(MathParser.Parse(b.Trim())))
                                .Append("</m:oMath>");
                            if (numbered) body.Append($"<w:r><w:tab/><w:t>({++eqNo})</w:t></w:r>");
                            body.Append("</w:p>");
                        }
                        else
                            body.Append("<w:p><w:pPr><w:pStyle w:val=\"Codice\"/></w:pPr><w:r><w:t xml:space=\"preserve\">").Append(X(b)).Append("</w:t></w:r></w:p>");
                    }
                    block.Clear();
                }
                else math = l.Trim() == "```math";
                code = !code;
                continue;
            }
            if (code) { block.Add(l); continue; }
            if (l.StartsWith("| ")) { table.Add(l.Trim().Trim('|').Split('|').Select(c => c.Trim()).ToArray()); continue; }
            FlushTable();
            if (l.Length == 0) continue;

            if (l.StartsWith("# ")) { body.Append(Para("Titolo", l.Substring(2))); first = false; }
            else if (l.StartsWith("## ")) body.Append(Para("Titolo1", l.Substring(3)));
            else if (l.StartsWith("### ")) body.Append(Para("Titolo2", l.Substring(4)));
            else if (l.StartsWith("- ")) body.Append(Para("Elenco", "•\t" + l.Substring(2)));
            else if (char.IsDigit(l[0]) && l.Contains(". ") && l.IndexOf(". ") <= 3)
            {
                int k = l.IndexOf(". ");
                body.Append(Para("Elenco", l.Substring(0, k + 1) + "\t" + l.Substring(k + 2), boldPrefix: l.Substring(0, k + 1)));
            }
            else if (first) { body.Append(Para("Occhiello", l)); }
            else body.Append(Para("Normale", l));
        }
        FlushTable();

        return "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
               $"<w:document xmlns:w=\"{W}\" xmlns:m=\"{M}\" xmlns:r=\"{R}\"><w:body>" + body +
               "<w:sectPr><w:pgSz w:w=\"11906\" w:h=\"16838\"/><w:pgMar w:top=\"1134\" w:right=\"1134\" w:bottom=\"1134\" w:left=\"1134\" w:header=\"567\" w:footer=\"567\" w:gutter=\"0\"/></w:sectPr>" +
               "</w:body></w:document>";
    }

    static string Para(string style, string text, string boldPrefix = null)
    {
        var sb = new StringBuilder();
        sb.Append($"<w:p><w:pPr><w:pStyle w:val=\"{style}\"/></w:pPr>");
        if (boldPrefix != null && text.StartsWith(boldPrefix))
        {
            sb.Append("<w:r><w:rPr><w:b/></w:rPr><w:t xml:space=\"preserve\">").Append(X(boldPrefix)).Append("</w:t></w:r>");
            text = text.Substring(boldPrefix.Length);
        }
        sb.Append(Runs(text, false));
        sb.Append("</w:p>");
        return sb.ToString();
    }

    static readonly Regex Cite = new(@"\s?\[(\d+(?:\s*[,–-]\s*\d+)*)\]", RegexOptions.Compiled);

    /// <summary>Testo con i rimandi alle fonti [n] in apice; tabulazioni come w:tab.</summary>
    static string Runs(string text, bool bold)
    {
        var sb = new StringBuilder();
        string rpr = bold ? "<w:rPr><w:b/></w:rPr>" : "";
        int pos = 0;
        foreach (Match m in Cite.Matches(text))
        {
            Plain(sb, text.Substring(pos, m.Index - pos), rpr);
            sb.Append("<w:r><w:rPr><w:vertAlign w:val=\"superscript\"/></w:rPr><w:t>[").Append(X(m.Groups[1].Value)).Append("]</w:t></w:r>");
            pos = m.Index + m.Length;
        }
        Plain(sb, text.Substring(pos), rpr);
        return sb.ToString();
    }

    static readonly Regex Inline = new(@"\*\*(.+?)\*\*|(?<![\w*])\*(?!\s)(.+?)(?<!\s)\*(?![\w*])|`([^`]+)`|\[([^\]]+)\]\((https?://[^)\s]+)\)", RegexOptions.Compiled);

    /// <summary>Markdown in linea: **grassetto**, *corsivo*, `codice`, [testo](indirizzo) → testo sottolineato blu.</summary>
    static void Plain(StringBuilder sb, string t, string rpr)
    {
        if (t.Length == 0) return;
        int pos = 0;
        foreach (Match m in Inline.Matches(t))
        {
            PlainRaw(sb, t.Substring(pos, m.Index - pos), rpr);
            string inner = rpr.Replace("<w:rPr>", "").Replace("</w:rPr>", "");
            if (m.Groups[1].Success) PlainRaw(sb, m.Groups[1].Value, "<w:rPr>" + inner + "<w:b/></w:rPr>");
            else if (m.Groups[2].Success) PlainRaw(sb, m.Groups[2].Value, "<w:rPr>" + inner + "<w:i/></w:rPr>");
            else if (m.Groups[3].Success) PlainRaw(sb, m.Groups[3].Value, "<w:rPr><w:rFonts w:ascii=\"Consolas\" w:hAnsi=\"Consolas\"/>" + inner + "</w:rPr>");
            else PlainRaw(sb, m.Groups[4].Value + " (" + m.Groups[5].Value + ")", "<w:rPr>" + inner + "<w:color w:val=\"1E5FB8\"/></w:rPr>");
            pos = m.Index + m.Length;
        }
        PlainRaw(sb, t.Substring(pos), rpr);
    }

    static void PlainRaw(StringBuilder sb, string t, string rpr)
    {
        if (t.Length == 0) return;
        var parts = t.Split('\t');
        for (int i = 0; i < parts.Length; i++)
        {
            if (i > 0) sb.Append("<w:r>").Append(rpr).Append("<w:tab/></w:r>");
            if (parts[i].Length > 0) sb.Append("<w:r>").Append(rpr).Append("<w:t xml:space=\"preserve\">").Append(X(parts[i])).Append("</w:t></w:r>");
        }
    }

    // ------------------------------------------------------------ formule (Office Math Markup Language)

    static string Omml(MNode n)
    {
        switch (n)
        {
            case MRow row: return string.Concat(row.Items.Select(Omml));
            case MRun r:
            {
                var t = r.Kind == MKind.Func ? r.Text + " " : r.Text;
                var sty = r.Kind == MKind.Var ? "" : "<m:rPr><m:sty m:val=\"p\"/></m:rPr>";
                return $"<m:r>{sty}<w:rPr><w:rFonts w:ascii=\"Cambria Math\" w:hAnsi=\"Cambria Math\"/></w:rPr><m:t xml:space=\"preserve\">{X(t)}</m:t></m:r>";
            }
            case MFrac f: return $"<m:f><m:num>{Omml(f.Num)}</m:num><m:den>{Omml(f.Den)}</m:den></m:f>";
            case MScript s:
                if (s.Sub != null && s.Sup != null) return $"<m:sSubSup><m:e>{Omml(s.Base)}</m:e><m:sub>{Omml(s.Sub)}</m:sub><m:sup>{Omml(s.Sup)}</m:sup></m:sSubSup>";
                if (s.Sub != null) return $"<m:sSub><m:e>{Omml(s.Base)}</m:e><m:sub>{Omml(s.Sub)}</m:sub></m:sSub>";
                return $"<m:sSup><m:e>{Omml(s.Base)}</m:e><m:sup>{Omml(s.Sup)}</m:sup></m:sSup>";
            case MSqrt q: return $"<m:rad><m:radPr><m:degHide m:val=\"1\"/></m:radPr><m:deg/><m:e>{Omml(q.Body)}</m:e></m:rad>";
        }
        return "";
    }

    // ------------------------------------------------------------ parti fisse del pacchetto

    const string ContentTypes = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
        "<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\">" +
        "<Default Extension=\"rels\" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/>" +
        "<Default Extension=\"xml\" ContentType=\"application/xml\"/>" +
        "<Override PartName=\"/word/document.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml\"/>" +
        "<Override PartName=\"/word/styles.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.styles+xml\"/>" +
        "</Types>";

    const string RootRels = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
        "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
        "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\" Target=\"word/document.xml\"/>" +
        "</Relationships>";

    const string DocRels = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
        "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
        "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles\" Target=\"styles.xml\"/>" +
        "</Relationships>";

    const string Styles = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
        "<w:styles xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\">" +
        "<w:docDefaults><w:rPrDefault><w:rPr><w:rFonts w:ascii=\"Calibri\" w:hAnsi=\"Calibri\" w:cs=\"Calibri\" w:eastAsia=\"Calibri\"/><w:sz w:val=\"21\"/><w:szCs w:val=\"21\"/><w:color w:val=\"2A2F36\"/><w:lang w:val=\"it-IT\"/></w:rPr></w:rPrDefault>" +
        "<w:pPrDefault><w:pPr><w:spacing w:after=\"120\" w:line=\"288\" w:lineRule=\"auto\"/></w:pPr></w:pPrDefault></w:docDefaults>" +
        "<w:style w:type=\"paragraph\" w:default=\"1\" w:styleId=\"Normale\"><w:name w:val=\"Normal\"/><w:qFormat/></w:style>" +
        "<w:style w:type=\"paragraph\" w:styleId=\"Occhiello\"><w:name w:val=\"Occhiello\"/><w:basedOn w:val=\"Normale\"/><w:rPr><w:b/><w:caps/><w:color w:val=\"5A6B85\"/><w:sz w:val=\"17\"/><w:spacing w:val=\"10\"/></w:rPr></w:style>" +
        "<w:style w:type=\"paragraph\" w:styleId=\"Titolo\"><w:name w:val=\"Title\"/><w:basedOn w:val=\"Normale\"/><w:next w:val=\"Normale\"/><w:qFormat/><w:pPr><w:spacing w:after=\"160\"/></w:pPr><w:rPr><w:rFonts w:ascii=\"Georgia\" w:hAnsi=\"Georgia\"/><w:color w:val=\"1F3A7A\"/><w:sz w:val=\"52\"/><w:szCs w:val=\"52\"/></w:rPr></w:style>" +
        "<w:style w:type=\"paragraph\" w:styleId=\"Titolo1\"><w:name w:val=\"heading 1\"/><w:basedOn w:val=\"Normale\"/><w:next w:val=\"Normale\"/><w:qFormat/><w:pPr><w:keepNext/><w:pBdr><w:bottom w:val=\"single\" w:sz=\"6\" w:space=\"4\" w:color=\"C9D3E6\"/></w:pBdr><w:spacing w:before=\"360\" w:after=\"160\"/><w:outlineLvl w:val=\"0\"/></w:pPr><w:rPr><w:rFonts w:ascii=\"Georgia\" w:hAnsi=\"Georgia\"/><w:color w:val=\"1F3A7A\"/><w:sz w:val=\"32\"/><w:szCs w:val=\"32\"/></w:rPr></w:style>" +
        "<w:style w:type=\"paragraph\" w:styleId=\"Elenco\"><w:name w:val=\"Elenco\"/><w:basedOn w:val=\"Normale\"/><w:pPr><w:tabs><w:tab w:val=\"left\" w:pos=\"426\"/></w:tabs><w:spacing w:after=\"80\"/><w:ind w:left=\"426\" w:hanging=\"426\"/></w:pPr></w:style>" +
        "<w:style w:type=\"paragraph\" w:styleId=\"Tabella\"><w:name w:val=\"Testo tabella\"/><w:basedOn w:val=\"Normale\"/><w:pPr><w:spacing w:before=\"40\" w:after=\"40\" w:line=\"252\" w:lineRule=\"auto\"/></w:pPr><w:rPr><w:sz w:val=\"19\"/><w:szCs w:val=\"19\"/></w:rPr></w:style>" +
        "<w:style w:type=\"paragraph\" w:styleId=\"Codice\"><w:name w:val=\"Codice\"/><w:basedOn w:val=\"Normale\"/><w:pPr><w:shd w:val=\"clear\" w:color=\"auto\" w:fill=\"F4F6FA\"/><w:spacing w:after=\"0\"/></w:pPr><w:rPr><w:rFonts w:ascii=\"Consolas\" w:hAnsi=\"Consolas\"/><w:sz w:val=\"19\"/></w:rPr></w:style>" +
        "<w:style w:type=\"paragraph\" w:styleId=\"Titolo2\"><w:name w:val=\"heading 2\"/><w:basedOn w:val=\"Normale\"/><w:next w:val=\"Normale\"/><w:qFormat/><w:pPr><w:keepNext/><w:spacing w:before=\"240\" w:after=\"100\"/><w:outlineLvl w:val=\"1\"/></w:pPr><w:rPr><w:rFonts w:ascii=\"Georgia\" w:hAnsi=\"Georgia\"/><w:b/><w:color w:val=\"1F3A7A\"/><w:sz w:val=\"26\"/><w:szCs w:val=\"26\"/></w:rPr></w:style>" +
        "<w:style w:type=\"paragraph\" w:styleId=\"FormulaNum\"><w:name w:val=\"Formula numerata\"/><w:basedOn w:val=\"Normale\"/><w:pPr><w:tabs><w:tab w:val=\"center\" w:pos=\"4819\"/><w:tab w:val=\"right\" w:pos=\"9638\"/></w:tabs><w:spacing w:before=\"80\" w:after=\"120\"/></w:pPr><w:rPr><w:rFonts w:ascii=\"Cambria Math\" w:hAnsi=\"Cambria Math\"/><w:sz w:val=\"24\"/></w:rPr></w:style>" +
        "<w:style w:type=\"paragraph\" w:styleId=\"Formula\"><w:name w:val=\"Formula\"/><w:basedOn w:val=\"Normale\"/><w:pPr><w:jc w:val=\"center\"/><w:spacing w:before=\"80\" w:after=\"120\"/></w:pPr><w:rPr><w:rFonts w:ascii=\"Cambria Math\" w:hAnsi=\"Cambria Math\"/><w:sz w:val=\"24\"/></w:rPr></w:style>" +
        "</w:styles>";
}
