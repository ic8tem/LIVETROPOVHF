namespace TropoVHF.Core;

/// <summary>
/// Radar GRAVES (portato da YA-MS, src/Controls/GravesView.cs – GravesBeam): trasmettitore CW su 143,050 MHz a
/// Broye-lès-Pesmes, JN27SI. Quattro schiere a fasatura coprono il cielo a sud, da 90° a 270° di azimut, in quattro
/// settori da 45° (D, C, B, A), ognuno diviso in 6 passi da 7,5° illuminati uno dopo l'altro, nello stesso istante in
/// tutti i settori; fascio largo circa 25° in elevazione. Sequenza legata all'UTC (PE1ITR 2021: 0,8 s per passo, ciclo 4,8 s).
/// È l'unica eccezione sotto i 144 MHz: difficile da sentire in tropo, ma può succedere.
/// </summary>
public static class Graves
{
    public const double Lat = 47.348, Lon = 5.515;     // trasmettitore (Broye-lès-Pesmes)
    public const double AltKm = 0.2;                   // quota, circa 200 m
    public const double FreqMHz = 143.050;
    public const string Locator = "JN27SI";
    public const string Call = "GRAVES";
    public const double AzMin = 90, AzMax = 270, SectorDeg = 45, StepDeg = 7.5;
    public const double BeamElevDeg = 25;
    public const int Steps = 6;
    public const double StepSec = 0.8;                 // PE1ITR 2021 (la sequenza 2007 era 3,2 s)
    public static readonly string[] Sectors = { "D", "C", "B", "A" };   // indice 0 = 90–135°, … 3 = 225–270°

    public static LatLon Pos => new(Lat, Lon);

    /// <summary>Frequenza di GRAVES: 143,050 MHz (in USB si ascolta su 143,049 con nota a circa 1 kHz), ±10 kHz.</summary>
    public static bool IsGraves(double kHz) => kHz >= 143040 && kHz <= 143060;

    /// <summary>Spot che parla di GRAVES: frequenza giusta o nome nel nominativo o nel commento.</summary>
    public static bool IsGravesSpot(Spot s) =>
        IsGraves(s.FreqKHz) || s.Dx.Contains("GRAVES", StringComparison.OrdinalIgnoreCase);

    /// <summary>Settore (0..3) e passo (0..5) che illuminano l'azimut az visto da GRAVES; false se fuori da 90–270°.</summary>
    public static bool Locate(double az, out int sector, out int step)
    {
        sector = -1; step = -1;
        az = ((az % 360) + 360) % 360;
        if (az < AzMin || az >= AzMax) return false;
        double x = az - AzMin;
        sector = Math.Clamp((int)Math.Floor(x / SectorDeg), 0, 3);
        step = Math.Clamp((int)Math.Floor((x - sector * SectorDeg) / StepDeg), 0, Steps - 1);
        return true;
    }

    /// <summary>Posizione nel ciclo (s): secondi UTC dalle 00:00 modulo il ciclo.</summary>
    public static double Phase(DateTime utc, double stepSec = StepSec)
    {
        double c = Steps * stepSec;
        double p = utc.TimeOfDay.TotalSeconds % c;
        return p < 0 ? p + c : p;
    }

    /// <summary>Passo illuminato adesso (0..5), uguale in tutti i settori.</summary>
    public static int ActiveStep(DateTime utc, double stepSec = StepSec) =>
        Math.Clamp((int)Math.Floor(Phase(utc, stepSec) / stepSec), 0, Steps - 1);

    /// <summary>Descrizione per la scheda di uno spot: dove sta il ricevitore rispetto al fascio di GRAVES.</summary>
    public static string Describe(LatLon rx)
    {
        double az = Geo.Azimuth(Pos, rx), km = Geo.DistKm(Pos, rx);
        return Locate(az, out var sec, out var st)
            ? $"GRAVES → {km:0} km, azimut {az:0}° dal radar: settore {Sectors[sec]}, passo {st + 1} del fascio"
            : $"GRAVES → {km:0} km, azimut {az:0}° dal radar: fuori dal fascio (90°–270°), si sente solo per lobi laterali";
    }
}
