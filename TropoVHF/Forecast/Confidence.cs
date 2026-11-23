namespace TropoVHF.Forecast;

/// <summary>Esito del giudizio di affidabilità di una previsione.</summary>
public sealed class ConfidenceResult
{
    public int Score;
    /// <summary>2 = alta, 1 = media, 0 = bassa.</summary>
    public int Grade => Score >= 3 ? 2 : Score >= 0 ? 1 : 0;
    public double ModelSpreadDb = double.NaN; public int Models;
    public double LeadHours = double.NaN;
    public double LayerFrac = double.NaN, Persist = double.NaN;
    public SondeReport Sonde;
    public int Live = -1; public bool LiveContradicts;
    public List<(string key, int pts)> Parts = new();
}

/// <summary>
/// 1.4 — Affidabilità del guadagno previsto: alta, media o bassa. Non è una probabilità calibrata ma una somma di punti
/// su quanto gli indizi indipendenti vanno d'accordo (manuale delle previsioni, § 10 septies):
/// <list type="bullet">
/// <item>accordo fra i modelli: scarto medio fra la perdita prevista e P.452 con i condotti ed equazione parabolica (e fra
/// la linea di base e l'ITM): fino a 3 dB +1, oltre 8 dB −1;</item>
/// <item>anticipo: entro 24 ore +1, oltre 72 ore −1 (l'errore del GFS sui condotti cresce con le ore);</item>
/// <item>lo strato: continuo su almeno il 70% della tratta +1, sotto il 30% −1; persistente nelle ore vicine +1;</item>
/// <item>la verifica col radiosondaggio più vicino (IGRA): GFS che ha visto i condotti osservati almeno 3 volte su 4 +1,
/// meno di una su due −1; scarto medio del gradiente oltre 15 N/km −1;</item>
/// <item>le conferme dal vivo (cluster, PSKReporter, APRS, FM-DX) vicino alla tratta, solo per l'ora attuale: 3 o più +2,
/// 1–2 +1; aperture in corso con un guadagno previsto quasi nullo −1.</item>
/// </list>
/// Alta da 3 punti, media da 0 a 2, bassa sotto lo zero.
/// </summary>
public static class Confidence
{
    public static ConfidenceResult Judge(LinkResult r, DateTime at, DateTime now, SondeReport sonde, int live)
    {
        var c = new ConfidenceResult();
        void P(string k, int p) { if (p != 0) { c.Parts.Add((k, p)); c.Score += p; } }
        // 1. modelli
        var d = new List<double>();
        if (r.P452Ok && !double.IsNaN(r.P452Lb)) d.Add(Math.Abs(r.P452Lb - r.LpredDb));
        if (!double.IsNaN(r.PeTotalDb)) d.Add(Math.Abs(r.PeTotalDb - r.LpredDb));
        if (!double.IsNaN(r.ItmDb) && !double.IsNaN(r.LbaseDb)) d.Add(Math.Abs(r.ItmDb - r.LbaseDb));
        if (d.Count > 0)
        {
            c.Models = d.Count; c.ModelSpreadDb = d.Average();
            P("models", c.ModelSpreadDb <= 3 ? 1 : c.ModelSpreadDb > 8 ? -1 : 0);
        }
        // 2. anticipo
        c.LeadHours = (at - now).TotalHours;
        P("lead", c.LeadHours <= 24 ? 1 : c.LeadHours > 72 ? -1 : 0);
        // 3. strato
        if (r.LayerName != "")
        {
            c.LayerFrac = r.LayerFrac;
            P("layer", r.LayerFrac >= 0.7 ? 1 : r.LayerFrac < 0.3 ? -1 : 0);
            if (!double.IsNaN(r.PersistFrac)) { c.Persist = r.PersistFrac; P("persist", r.PersistFrac >= 0.7 ? 1 : 0); }
        }
        // 4. radiosondaggio
        if (sonde != null && sonde.Error == "" && !double.IsNaN(sonde.Skill))
        {
            c.Sonde = sonde;
            int obsDucts = sonde.Hits + sonde.Misses;
            int p = sonde.Skill >= 0.75 ? 1 : sonde.Skill < 0.5 ? -1 : 0;
            if (obsDucts >= 2 && sonde.Hits * 2 < obsDucts) p = Math.Min(p, 0);
            if (!double.IsNaN(sonde.DnBias) && Math.Abs(sonde.DnBias) > 15) p -= 1;
            P("sonde", Math.Max(-1, p));
        }
        // 5. conferme dal vivo, solo vicino all'ora attuale
        if (live >= 0 && Math.Abs(c.LeadHours) <= 2)
        {
            c.Live = live;
            if (live > 0 && r.TropoGainDb < 2) { c.LiveContradicts = true; P("live", -1); }
            else P("live", live >= 3 ? 2 : live >= 1 ? 1 : 0);
        }
        return c;
    }
}
