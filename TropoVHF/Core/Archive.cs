using Microsoft.Data.Sqlite;

namespace TropoVHF.Core;

/// <summary>Archivio SQLite LIVETROPOVHF.db accanto all'eseguibile: spot, ascolti, decodifiche.</summary>
public sealed class Archive : IDisposable
{
    readonly object _lock = new();
    SqliteConnection _db;
    public string Error = "";
    public long Rows;

    public Archive(string path)
    {
        try
        {
            _db = new SqliteConnection("Data Source=" + path);
            _db.Open();
            Exec(@"PRAGMA journal_mode=WAL;
CREATE TABLE IF NOT EXISTS spots(ts TEXT, src TEXT, spotter TEXT, dx TEXT, freq_khz REAL, band TEXT, mode TEXT, comment TEXT, de_loc TEXT, dx_loc TEXT, airscatter INTEGER);
CREATE TABLE IF NOT EXISTS listen(ts TEXT, band TEXT, rx_call TEXT, rx_grid TEXT, tx_call TEXT, tx_grid TEXT, mode TEXT, snr INTEGER);
CREATE TABLE IF NOT EXISTS decodes(ts TEXT, band TEXT, call TEXT, grid TEXT, snr INTEGER, message TEXT);
CREATE INDEX IF NOT EXISTS ix_spots_ts ON spots(ts);
CREATE INDEX IF NOT EXISTS ix_listen_ts ON listen(ts);
CREATE INDEX IF NOT EXISTS ix_decodes_ts ON decodes(ts);");
        }
        catch (Exception ex) { Error = ex.Message; _db = null; }
    }

    void Exec(string sql)
    {
        using var c = _db.CreateCommand();
        c.CommandText = sql;
        c.ExecuteNonQuery();
    }

    void Insert(string sql, params (string, object)[] p)
    {
        if (_db == null) return;
        lock (_lock)
        {
            try
            {
                using var c = _db.CreateCommand();
                c.CommandText = sql;
                foreach (var (k, v) in p) c.Parameters.AddWithValue(k, v ?? DBNull.Value);
                c.ExecuteNonQuery();
                Rows++;
            }
            catch (Exception ex) { Error = ex.Message; }
        }
    }

    static string Ts(DateTime t) => t.ToString("yyyy-MM-dd HH:mm:ss");

    public void AddSpot(Spot s) => Insert(
        "INSERT INTO spots VALUES($ts,$src,$sp,$dx,$f,$b,$m,$c,$dl,$xl,$as)",
        ("$ts", Ts(s.Recv)), ("$src", s.Src), ("$sp", s.Spotter), ("$dx", s.Dx), ("$f", s.FreqKHz), ("$b", s.Band),
        ("$m", s.Mode), ("$c", s.Comment), ("$dl", s.DeLoc), ("$xl", s.DxLoc), ("$as", s.Airscatter ? 1 : 0));

    public void AddListen(RxReport r) => Insert(
        "INSERT INTO listen VALUES($ts,$b,$rc,$rg,$tc,$tg,$m,$s)",
        ("$ts", Ts(r.Time)), ("$b", r.Band), ("$rc", r.RxCall), ("$rg", r.RxGrid), ("$tc", r.TxCall), ("$tg", r.TxGrid),
        ("$m", r.Mode), ("$s", r.Snr.HasValue ? (object)r.Snr.Value : DBNull.Value));

    public void AddDecode(Decode d) => Insert(
        "INSERT INTO decodes VALUES($ts,$b,$c,$g,$s,$m)",
        ("$ts", Ts(d.Time)), ("$b", d.Band), ("$c", d.Call), ("$g", d.Grid), ("$s", d.Snr), ("$m", d.Message));

    public void Dispose()
    {
        lock (_lock) { try { _db?.Close(); _db?.Dispose(); } catch { } _db = null; }
    }
}
