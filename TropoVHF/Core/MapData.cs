using System.Text.Json;

namespace TropoVHF.Core;

/// <summary>Coste, confini e terre da Natural Earth (pubblico dominio), salvati in Dati/ accanto al programma.</summary>
public sealed class MapData
{
    public const string BaseUrl = "https://raw.githubusercontent.com/nvkelso/natural-earth-vector/master/geojson/";
    public static readonly string[] Files = { "ne_50m_land.geojson", "ne_50m_coastline.geojson", "ne_50m_admin_0_boundary_lines_land.geojson" };
    public static readonly string[] Files10 = { "ne_10m_land.geojson", "ne_10m_coastline.geojson", "ne_10m_admin_0_boundary_lines_land.geojson" };

    public List<LatLon[]> Land = new();
    public List<LatLon[]> Coast = new();
    public List<LatLon[]> Borders = new();
    public bool Loaded;

    // dettaglio 1:10 milioni, usato quando si ingrandisce
    public List<LatLon[]> Land10 = new();
    public List<LatLon[]> Coast10 = new();
    public List<LatLon[]> Borders10 = new();
    public bool Loaded10;
    public string Status = "";
    public event Action Changed;

    public async Task LoadOrDownloadAsync(HttpClient http)
    {
        var dir = Settings.DataDir;
        try
        {
            foreach (var f in Files)
            {
                var p = Path.Combine(dir, f);
                if (File.Exists(p) && new FileInfo(p).Length > 1000) continue;
                Status = "Scarico " + f + "…";
                Changed?.Invoke();
                var bytes = await http.GetByteArrayAsync(BaseUrl + f);
                await Compat.WriteBytes(p, bytes, CancellationToken.None);
            }
            Land = ReadLines(Path.Combine(dir, Files[0]), true);
            Coast = ReadLines(Path.Combine(dir, Files[1]), false);
            Borders = ReadLines(Path.Combine(dir, Files[2]), false);
            Loaded = true;
            Status = "Natural Earth 1:50M pronto";
        }
        catch (Exception ex)
        {
            Status = "Mappa di base non disponibile: " + ex.Message + " (copia i file Natural Earth in Dati/)";
            Changed?.Invoke();
            return;
        }
        Changed?.Invoke();

        // secondo passo: dettaglio 1:10 milioni (file più grandi, scaricati una volta sola)
        try
        {
            foreach (var f in Files10)
            {
                var p = Path.Combine(dir, f);
                if (File.Exists(p) && new FileInfo(p).Length > 1000) continue;
                Status = "Scarico il dettaglio " + f + "…";
                Changed?.Invoke();
                var bytes = await http.GetByteArrayAsync(BaseUrl + f);
                await Compat.WriteBytes(p, bytes, CancellationToken.None);
            }
            var land = ReadLines(Path.Combine(dir, Files10[0]), true);
            var coast = ReadLines(Path.Combine(dir, Files10[1]), false);
            var borders = ReadLines(Path.Combine(dir, Files10[2]), false);
            Land10 = land; Coast10 = coast; Borders10 = borders;
            Loaded10 = true;
            Status = "";
        }
        catch (Exception ex)
        {
            Status = "Dettaglio 1:10M non disponibile (" + ex.Message + "): resta 1:50M";
        }
        Changed?.Invoke();
    }

    static List<LatLon[]> ReadLines(string path, bool outerRingsOnly)
    {
        var res = new List<LatLon[]>();
        if (!File.Exists(path)) return res;
        using var doc = JsonDocument.Parse(File.ReadAllBytes(path));
        if (!doc.RootElement.TryGetProperty("features", out var feats)) return res;
        foreach (var f in feats.EnumerateArray())
        {
            if (!f.TryGetProperty("geometry", out var g) || g.ValueKind != JsonValueKind.Object) continue;
            var type = g.GetProperty("type").GetString();
            var c = g.GetProperty("coordinates");
            switch (type)
            {
                case "LineString": res.Add(Ring(c)); break;
                case "MultiLineString": foreach (var l in c.EnumerateArray()) res.Add(Ring(l)); break;
                case "Polygon":
                    foreach (var r in c.EnumerateArray()) { res.Add(Ring(r)); if (outerRingsOnly) break; }
                    break;
                case "MultiPolygon":
                    foreach (var poly in c.EnumerateArray())
                        foreach (var r in poly.EnumerateArray()) { res.Add(Ring(r)); if (outerRingsOnly) break; }
                    break;
            }
        }
        return res;
    }

    static LatLon[] Ring(JsonElement arr)
    {
        var l = new List<LatLon>();
        foreach (var p in arr.EnumerateArray())
        {
            double lon = p[0].GetDouble(), lat = p[1].GetDouble();
            l.Add(new LatLon(lat, lon));
        }
        return l.ToArray();
    }
}
