namespace TropoVHF.Core;

/// <summary>Tutto lo stato del programma, indipendente dall'interfaccia.</summary>
public sealed class AppState
{
    public readonly Settings S;
    public readonly CallDb CallDb = new();
    static CallDb _db;    // per Refine, usato anche fuori da AppState (elenco degli ascoltatori)
    static SuspectStore _sus;
    /// <summary>Segnalazioni sospette e regole imparate (linguetta Segnalazioni sospette).</summary>
    public readonly SuspectStore Suspects = new();
    public readonly SpotStore Spots = new();
    public readonly ListenerStore Listeners = new();
    public readonly DecodeStore Decodes = new();
    public readonly QsoStore Qsos = new();
    /// <summary>Quarantena dei collegamenti oltre 1000 km (vedi PathGate).</summary>
    public readonly PathGate Gate = new();
    public readonly Archive Archive;
    public readonly TelnetFeed[] Feeds;
    public readonly PskMqtt Mqtt = new();
    public readonly WsjtxUdp Udp = new();
    public readonly NtpChecker Ntp = new();
    public readonly MapData Map = new();
    public readonly Pressure Pressure = new();
    public readonly TropoIndex Tropo = new();
    public readonly HttpClient Http;

    // impostazioni dell'interfaccia
    public string Call = "", Loc = "", ThemeKey = "chiaro", LangCode = "it";
    public string Band = "144", TropoSrc = "media", Proj = "merc";
    public int WindowMin = 15, ForecastH = 0;
    /// <summary>Riquadro lat/lon visibile sulla mappa (lo aggiorna la mappa a ogni disegno).</summary>
    public (double lat1, double lat2, double lon1, double lon2) ViewBox = (30, 72, -30, 48);
    public double TropoOpacity = 0.8;
    public bool ShowBase = true, ShowGrid = true, ShowTropo = true, ShowIso = true, ShowT850 = true, ShowInv = true, ShowRx = true, ShowDec = true, ShowQso = true, ShowGraves = true, ShowSpots = true;
    /// <summary>
    /// Criteri di esclusione generale: un collegamento più corto di ShortKm non si disegna (casella); se anche una sola
    /// delle due stazioni sta oltre LongKm dal proprio QTH (la corona di 1000 km attorno al locatore impostato), il
    /// collegamento si mostra solo come quadrati locator in lilla tratteggiato.
    /// </summary>
    public bool HideShort = false, LongAsSquares = true;
    public double ShortKm = 100, LongKm = 1000;
    /// <summary>Raggi selezionabili per la corona; ShowRings disegna le tre corone attorno al proprio QTH.</summary>
    public static readonly int[] RingKm = { 1000, 700, 500 };
    public bool ShowRings = true;

    /// <summary>0 = si disegna, −1 = escluso perché corto, +1 = oltre LongKm: solo i quadrati.</summary>
    public int DistClass(LatLon a, LatLon b)
    {
        if (HideShort && Geo.DistKm(a, b) < ShortKm) return -1;
        if (!LongAsSquares) return 0;
        // senza un locatore valido si guarda la lunghezza del collegamento
        if (!TryQth(out var me)) return Geo.DistKm(a, b) > LongKm ? 1 : 0;
        return Geo.DistKm(me, a) > LongKm || Geo.DistKm(me, b) > LongKm ? 1 : 0;
    }

    /// <summary>Stazione oltre LongKm dal proprio QTH (con la casella accesa).</summary>
    public bool FarFromMe(LatLon p) => LongAsSquares && TryQth(out var me) && Geo.DistKm(me, p) > LongKm;

    // registri (ultime righe) per la linguetta Raccolta dati e per il debug UDP
    public readonly Dictionary<string, List<(string text, LineClass cls)>> Logs = new();
    public readonly List<(string text, bool decode)> UdpLog = new();
    public readonly List<string> MeteoLog = new();
    readonly List<(DateTime t, string band)> _excluded = new();

    /// <summary>Qualcosa è cambiato: l'interfaccia ridisegna (con un timer, non a ogni evento).</summary>
    public volatile bool Dirty = true;
    public volatile bool LogsDirty = true;

    public AppState()
    {
        S = new Settings(Settings.AppDir);
        _db = CallDb;
        _myCall = () => LoginOrCall;
        _myLoc = () => (Loc ?? "").Trim().ToUpperInvariant();
        Http = new HttpClient { Timeout = TimeSpan.FromSeconds(40) };
        Http.DefaultRequestHeaders.UserAgent.ParseAdd("LIVETROPOVHF/" + (typeof(AppState).Assembly.GetName().Version?.ToString(3) ?? "0"));
        // archivio: dal vecchio nome TropoVHF.db si passa a LIVETROPOVHF.db una volta sola
        var dbNew = Path.Combine(Settings.AppDir, "LIVETROPOVHF.db"); var dbOld = Path.Combine(Settings.AppDir, "TropoVHF.db");
        try { if (!File.Exists(dbNew) && File.Exists(dbOld)) File.Move(dbOld, dbNew); } catch { }
        Archive = new Archive(dbNew);

        Call = S.Get("call", "");
        Loc = S.Get("loc", "");
        ThemeKey = S.Get("theme", "chiaro");
        LangCode = S.Get("lang", "it");
        Band = S.Get("band", "144");
        if (!Bands.All.Contains(Band)) Band = "144";     // 50 e 70 MHz non ci sono più
        WindowMin = S.GetInt("window", 15);
        TropoSrc = S.Get("tropo_src", "media");
        Proj = S.Get("proj", "merc");
        TropoOpacity = S.GetDouble("tropo_opacity", 0.8);
        ShowBase = S.GetBool("l_base", true); ShowGrid = S.GetBool("l_grid", true); ShowTropo = S.GetBool("l_tropo", true);
        ShowIso = S.GetBool("l_iso", true); ShowT850 = S.GetBool("l_t850", true); ShowInv = S.GetBool("l_inv", true); ShowRx = S.GetBool("l_rx", true); ShowDec = S.GetBool("l_dec", true); ShowQso = S.GetBool("l_qso", true); ShowGraves = S.GetBool("l_graves", true); ShowSpots = S.GetBool("l_spots", true);
        HideShort = S.GetBool("x_short", false); LongAsSquares = S.GetBool("x_long", true); ShowRings = S.GetBool("x_rings", true);
        { var k = S.GetInt("x_longkm", 1000); LongKm = RingKm.Contains(k) ? k : 1000; }

        // GB7RAU non è più una fonte: le sue impostazioni salvate (indirizzo, utente, password) si cancellano
        if (S.Get("dxc2_host", "").Contains("gb7rau", StringComparison.OrdinalIgnoreCase))
            foreach (var k in new[] { "host", "port", "login", "password", "cmds", "recon", "auto" }) S.Remove("dxc2_" + k);
        Feeds = new[]
        {
            NewFeed("dxc", "DXC", "on0dxk.dyndns.org", 8000, "set/filter dxbm/pass 6", true),
            // seconda fonte: 9A0DXC (Zagabria) e S50CLX (Slovenia), login con il proprio nominativo
            NewFeed("dxc2", "DXC", "9a0dxc.hamradio.hr", 8000, "", true),
            NewFeed("dxc3", "DXC", "s50clx.infrax.si", 41112, "", true),
            NewFeed("rbn", "RBN", "telnet.reversebeacon.net", 7001, "", true),
            NewFeed("psk", "PSK", "dm4x.ddns.net", 8500, "", false),
        };

        Mqtt.Host = S.Get("mqtt_host", "mqtt.pskreporter.info");
        Mqtt.Port = S.GetInt("mqtt_port", 1883);
        Mqtt.UseTls = S.GetBool("mqtt_tls", false);
        Mqtt.Reconnect = S.GetBool("mqtt_recon", true);
        Mqtt.AutoStart = S.GetBool("mqtt_auto", true);
        var bandNames = S.Get("mqtt_band_names", "144=2m;222=1.25m;432=70cm");
        Mqtt.BandTopics.Clear();
        foreach (var kv in bandNames.Split(';', StringSplitOptions.RemoveEmptyEntries))
        {
            var p = kv.Split('=');
            if (p.Length == 2) Mqtt.BandTopics[p[0].Trim()] = p[1].Trim();
        }
        Mqtt.EnabledBands = new HashSet<string>(S.Get("mqtt_bands", "144,222,432").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));
        Logs["mqtt"] = new();
        Mqtt.Log += t => AddLog("mqtt", t, LineClass.Other);
        Mqtt.StateChanged += () => { Dirty = true; LogsDirty = true; };
        Listeners.Log += t => AddLog("mqtt", t, LineClass.Other);
        Gate.Log += t => AddLog("mqtt", t, LineClass.Other);
        _sus = Suspects;
        Suspects.Log += t => AddLog("mqtt", t, LineClass.Other);
        Gate.StationRule = c => Suspects.Verdict("lungo", c);
        Gate.Held += (band, a, b, km, why) => Suspects.Report("lungo", a, b, band, "", km, why, Clock.UtcNow);
        Gate.Revealed += (band, a, b, km, why) => { Suspects.Report("lungo", a, b, band, "", km, why, Clock.UtcNow); Suspects.Outcome("lungo", a, +1, why); Suspects.Outcome("lungo", b, +1, why); };
        Gate.Expired += (band, a, b, km, why) => { Suspects.Report("lungo", a, b, band, "", km, why, Clock.UtcNow); Suspects.Outcome("lungo", a, -1, why); Suspects.Outcome("lungo", b, -1, why); };
        Listeners.Rule = c => Suspects.Verdict("ascolto", c);
        Listeners.Judged += (call, band, disc, why) => { Suspects.Report("ascolto", call, "", band, "", 0, (disc ? "scartato: " : "") + why, Clock.UtcNow); Suspects.Outcome("ascolto", call, disc ? -1 : +1, why); };
        // una stazione sentita su bande sotto i 144 MHz negli stessi minuti non conferma né viene svelata
        // …a meno che i DX cluster umani (non dm4x, RBN o altre fonti automatiche) la diano attiva su quella banda
        Gate.LowBand = (call, band, t) => Listeners.ActiveBelow144(call, t) && !Spots.HumanConfirmed(call, band, t);
        Listeners.Confirmed = (call, band, t, listener) => Spots.HumanConfirmed(call, band, t, 30, listener);
        Listeners.Visible = (band, rx, tx) => Gate.Visible(band, rx, tx);
        Spots.Visible = s => Gate.Visible(s.Band, s.Spotter, s.Dx);
        Qsos.Visible = q => Gate.Visible(q.Band, q.A, q.B);
        Mqtt.Report += r =>
        {
            if (r.OtherBand) { Listeners.AddOther(r); return; }   // altre bande: solo per verificare gli ascoltatori VHF
            // meteor scatter (MSK144, FSK441, JT6M): scartato come negli spot dei cluster, si conta soltanto
            bool ms = SpotParser.IsShortMsMode(r.Mode);   // MSK144/FSK441/JTMS: accettati solo entro 250 km
            if (SpotParser.IsExcludedMode(r.Mode) && !ms) { lock (_excluded) _excluded.Add((Clock.UtcNow, r.Band)); return; }
            Mqtt.Watch(r.TxCall);
            // locatori MQTT a meno di 6 caratteri: si prova CALL3.TXT (≈1 milione di nominativi)
            r.RxGrid = RefineIn(r.RxCall, r.RxGrid, r.Band, r.Mode, r.Time);
            r.TxGrid = RefineIn(r.TxCall, r.TxGrid, r.Band, r.Mode, r.Time);
            bool pos = TryPos(r.RxCall, r.RxGrid, out var prx) & TryPos(r.TxCall, r.TxGrid, out var ptx);
            if (ms && (!pos || Geo.DistKm(prx, ptx) > SpotParser.ShortMsKm)) { lock (_excluded) _excluded.Add((Clock.UtcNow, r.Band)); return; }
            // Q65/JT65 via Luna: escluso come il meteor scatter (vedi Segnalazioni sospette)
            if (pos && EmeExclude(r.Band, r.Mode, "", r.TxCall, ptx, r.RxCall, prx, r.Time)) { lock (_excluded) _excluded.Add((Clock.UtcNow, r.Band)); return; }
            Listeners.Add(r);
            if (pos) Gate.Add(r.Band, r.RxCall, prx, r.TxCall, ptx, r.Time);
            Archive.AddListen(r);
            Dirty = true;
            if (Interlocked.Read(ref Mqtt.Reports) % 25 == 0)
                AddLog("mqtt", $"{r.Time:HH:mm:ss}  RX {r.RxCall,-9} {r.RxGrid,-6} sente {r.TxCall,-9} {r.TxGrid,-6} {r.Mode,-4} {(r.Snr.HasValue ? r.Snr.Value + " dB" : "")}  [1 riga ogni 25]", LineClass.Spot);
        };

        Udp.ListenAddress = S.Get("udp_in_addr", "127.0.0.1");
        Udp.ListenPort = S.GetInt("udp_in_port", 2237);
        Udp.ForwardAddress = S.Get("udp_out_addr", "127.0.0.1");
        Udp.ForwardPort = S.GetInt("udp_out_port", 2238);
        Udp.Forward = S.GetBool("udp_forward", true);
        Udp.AutoStart = S.GetBool("udp_auto", true);
        Udp.Debug += (t, isDec) => { lock (UdpLog) { UdpLog.Add((t, isDec)); if (UdpLog.Count > 400) UdpLog.RemoveRange(0, UdpLog.Count - 400); } LogsDirty = true; };
        Udp.StateChanged += () => { Dirty = true; LogsDirty = true; };
        Udp.Decoded += d =>
        {
            bool ms = SpotParser.IsShortMsMode(d.Mode) || SpotParser.IsShortMsMode(d.TxMode);
            bool exMode = SpotParser.IsExcludedMode(d.Mode) || SpotParser.IsExcludedMode(d.TxMode);
            if (exMode && !ms) { lock (_excluded) _excluded.Add((Clock.UtcNow, d.Band == "" ? Band : d.Band)); return; }
            if (d.Band == "") d.Band = Band;
            d.Grid = RefineIn(d.Call, d.Grid, d.Band, d.Mode, d.Time);
            if (exMode && !(TryQth(out var pmeMs) && TryPos(d.Call, d.Grid, out var pdMs) && Geo.DistKm(pmeMs, pdMs) <= SpotParser.ShortMsKm))
            { lock (_excluded) _excluded.Add((Clock.UtcNow, d.Band)); return; }
            NoteCallToMe(d);
            if (TryQth(out var pme0) && TryPos(d.Call, d.Grid, out var pdc0) &&
                EmeExclude(d.Band, d.Mode, d.Message, d.Call, pdc0, LoginOrCall == "" ? "QTH" : LoginOrCall, pme0, d.Time)) return;
            // "IC8TEM IK3FHP RR73": QSO avvenuto tra le due stazioni; locatori dai messaggi già visti, altrimenti da CALL3.TXT
            if (WsjtxUdp.TryQso(d.Message, out var qa, out var qb))
            {
                string G(string c) => Refine(c, Decodes.GridOf(c));
                var qso = new Qso { Time = d.Time, Band = d.Band, A = qa, GridA = G(qa), B = qb, GridB = G(qb) };
                Qsos.Add(qso);
                if (TryPos(qa, qso.GridA, out var pqa) && TryPos(qb, qso.GridB, out var pqb)) Gate.Add(qso.Band, qa, pqa, qb, pqb, qso.Time);
            }
            Decodes.Add(d);
            // le proprie decodifiche sono certe: sempre visibili, e confermano i collegamenti lunghi simili
            if (TryQth(out var pme) && TryPos(d.Call, d.Grid, out var pdc)) Gate.Add(d.Band, LoginOrCall == "" ? "QTH" : LoginOrCall, pme, d.Call, pdc, d.Time, true);
            Archive.AddDecode(d);
            Dirty = true;
        };

        var servers = S.Get("ntp_servers", string.Join(",", NtpChecker.DefaultServers));
        Ntp.Servers = servers.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
        Ntp.IntervalMinutes = S.GetInt("ntp_minutes", 3);
        Ntp.MinAnswers = S.GetInt("ntp_min", 3);
        Ntp.Updated += () => { Dirty = true; LogsDirty = true; };

        Map.Changed += () => { Dirty = true; };
        Pressure.Changed += () => { Dirty = true; AddMeteo(Pressure.Status); };
        Tropo.Changed += () => { Dirty = true; AddMeteo(Tropo.Status); };
        Tropo.RegionEnabled = r => S.Get("tropo_reg_" + r.Key, r.DefaultOn ? "1" : "0") == "1";
        Tropo.RegionExtents = r => StoredExtents.Parse(S.Get("tropo_ext_" + r.Key, ""));
        Tropo.SaveExtents = (r, e) => S.Set("tropo_ext_" + r.Key, e?.ToString() ?? "");
#if WINDOWS
        AxisReader.Ocr = WinOcr.TryCreate();   // OCR integrato di Windows 10/11 per leggere gli assi delle mappe
#endif
        Tropo.Log += AddMeteo;
    }

    TelnetFeed NewFeed(string id, string src, string host, int port, string cmds, bool auto, string login = "", string password = "")
    {
        var f = new TelnetFeed
        {
            Id = id, Src = src,
            Host = S.Get(id + "_host", host),
            Port = S.GetInt(id + "_port", port),
            Login = S.Get(id + "_login", login),
            Password = S.Get(id + "_password", password),
            Cmds = S.Get(id + "_cmds", cmds),
            Reconnect = S.GetBool(id + "_recon", true),
            AutoStart = S.GetBool(id + "_auto", auto),
        };
        Logs[id] = new();
        // nel registro solo le righe utili: quelle fuori banda o escluse dal filtro si contano soltanto
        f.Log += (t, c) => { if (c != LineClass.OutOfBand && c != LineClass.Excluded) AddLog(id, t, c); };
        f.StateChanged += () => { Dirty = true; LogsDirty = true; };
        f.SpotReceived += s =>
        {
            if (Graves.IsGravesSpot(s))
            {
                // GRAVES sentito: il trasmettitore è il radar di Broye-lès-Pesmes, JN27SI
                s.Dx = Graves.Call; s.DxLoc = Graves.Locator;
                if (s.Mode == "") s.Mode = "CW";
            }
            // spot automatici (DM4X, RBN): il primo locatore è quello che la stazione che spotta dichiara di sé.
            // Se ha 6 caratteri (non il centro LL/MM) e il database dice altro, il database si aggiorna da solo.
            if (src is "PSK" or "RBN" && s.DeLoc.Length == 6 && !IsPadded(s.DeLoc) && s.Spotter != LoginOrCall)
            {
                var who = SkimmerBase(s.Spotter.ToUpperInvariant());
                var before = CallDb.SetCluster(who, s.DeLoc);
                if (before != null)
                    Suspects.Report("cluster", who, "", s.Band, s.Mode, 0,
                        $"CALL3 aggiornato da {f.Host}: {(before == "" ? "assente" : before)} → {s.DeLoc.ToUpperInvariant()}", s.Recv);
            }
            s.DxLoc = RefineIn(s.Dx, s.DxLoc, s.Band, s.Mode, s.Recv);
            s.DeLoc = RefineIn(s.Spotter, s.DeLoc, s.Band, s.Mode, s.Recv);
            bool pos = TryPos(s.Spotter, s.DeLoc, out var pde) & TryPos(s.Dx, s.DxLoc, out var pdx);
            // MSK144/FSK441/JTMS: accettati solo entro 250 km, altrimenti esclusi come meteor scatter
            if (s.ShortMs && (!pos || Geo.DistKm(pde, pdx) > SpotParser.ShortMsKm)) { lock (_excluded) _excluded.Add((Clock.UtcNow, s.Band)); return; }
            if (pos && EmeExclude(s.Band, s.Mode, s.Comment, s.Dx, pdx, s.Spotter, pde, s.Recv)) { lock (_excluded) _excluded.Add((Clock.UtcNow, s.Band)); return; }
            if (pos) Gate.Add(s.Band, s.Spotter, pde, s.Dx, pdx, s.Recv);
            Spots.Add(s);
            Archive.AddSpot(s);
            Dirty = true;
        };
        f.Log += (t, c) => { if (c == LineClass.Excluded) { var (s, _) = SpotParser.Parse(t, src, Clock.UtcNow); lock (_excluded) _excluded.Add((Clock.UtcNow, s?.Band ?? "")); } };
        return f;
    }

    void AddLog(string id, string text, LineClass cls)
    {
        if (!Logs.TryGetValue(id, out var l)) return;
        lock (l) { l.Add((text, cls)); if (l.Count > 300) l.RemoveRange(0, l.Count - 300); }
        LogsDirty = true;
    }

    public void AddMeteo(string text)
    {
        lock (MeteoLog) { MeteoLog.Add($"{DateTime.UtcNow:HH:mm:ss}  {text}"); if (MeteoLog.Count > 200) MeteoLog.RemoveRange(0, MeteoLog.Count - 200); }
        LogsDirty = true;
    }

    public int ExcludedCount(string band, int minutes)
    {
        var cut = Clock.UtcNow.AddMinutes(-minutes);
        lock (_excluded) { _excluded.RemoveAll(x => x.t < Clock.UtcNow.AddMinutes(-60)); return _excluded.Count(x => x.band == band && x.t > cut); }
    }

    public string LoginOrCall => (Call ?? "").Trim().ToUpperInvariant();

    /// <summary>Nominativo plausibile: prefisso con almeno una cifra e suffisso di lettere (IC8TEM, 9A2PQR, F/IC8TEM/P).</summary>
    /// <summary>Al termine del caricamento di CALL3.TXT (per avvisare se manca).</summary>
    public event Action CallDbLoaded;
    static Func<string> _myCall = () => "", _myLoc = () => "";

    public static bool IsValidCall(string c) =>
        System.Text.RegularExpressions.Regex.IsMatch((c ?? "").Trim().ToUpperInvariant(), @"^([A-Z0-9]{1,4}/)?[A-Z0-9]{0,3}[0-9][A-Z]{1,4}(/[A-Z0-9]{1,4})?$");

    public bool HasValidCall => IsValidCall(Call);

    /// <summary>
    /// Avvia i cluster telnet: prima ricopia il nominativo come nome utente nei Login vuoti.
    /// Senza un nominativo valido non parte niente: la finestra lo chiede all'avvio.
    /// </summary>
    public void StartFeeds()
    {
        foreach (var f in Feeds)
        {
            if (f.Wanted) continue;
            // un cluster con il suo nome utente parte comunque; gli altri aspettano il nominativo, che diventa il loro login
            if (string.IsNullOrWhiteSpace(f.Login))
            {
                if (!HasValidCall) continue;
                f.Login = LoginOrCall;
            }
            if (f.AutoStart) f.Connect(LoginOrCall);
        }
        Dirty = true; LogsDirty = true;
    }

    /// <summary>Posizione di una stazione: locatore dato, altrimenti CALL3.TXT.</summary>
    public bool TryPos(string call, string loc, out LatLon p)
    {
        // anche al disegno: i rapporti arrivati prima che CALL3.TXT fosse caricato si correggono da soli
        var g = Refine(call, loc);
        if (Locator.TryToLatLon(g, out p)) return true;
        return !string.IsNullOrEmpty(loc) && Locator.TryToLatLon(loc, out p);
    }

    public bool TryQth(out LatLon p) => Locator.TryToLatLon(Loc, out p);

    /// <summary>
    /// Locatore più preciso disponibile: se quello ricevuto ha meno di 6 caratteri e CALL3.TXT (≈1 milione di nominativi)
    /// ne ha uno a 6, vale sempre quello di CALL3.TXT; se il locatore manca, quello di CALL3.TXT a 4 o 6 caratteri.
    /// Vale per MQTT, spot dei cluster, decodifiche UDP e QSO desunti.
    /// </summary>
    public static string Refine(string call, string grid)
    {
        grid = (grid ?? "").Trim().ToUpperInvariant();
        if (string.IsNullOrEmpty(call)) return grid;
        var c = call.Trim().ToUpperInvariant().Trim('<', '>');
        var bc = SkimmerBase(c);            // OK2OZL-SDR, DK5KMA-# → OK2OZL, DK5KMA: stessa stazione, stesso QTH
        bool padded = IsPadded(grid);       // JN89MM: centro del quadrato messo da chi tronca il locatore
        // voce inserita a mano: vale sopra tutto, anche sopra il locatore ricevuto
        if (c != _myCall())
        {
            var man = _db?.ManualOf(c) ?? "";
            if (man == "" && bc != c) man = _db?.ManualOf(bc) ?? "";
            if (man != "") return man;
        }
        if (grid.Length >= 6 && !padded)
        {
            // si impara solo ciò che CALL3.TXT non sa già (le voci di CALL3 ripassano da qui quando la mappa si ridisegna)
            if (c != _myCall() && Db(c, bc) != grid[..6]) LearnedLoc.Learn(bc, grid);
            return grid;
        }
        // il proprio nominativo: vale sempre il proprio locatore a 6 caratteri
        if ((c == _myCall() || bc == _myCall()) && _myLoc().Length == 6 && Locator.IsValid(_myLoc())) return _myLoc();
        var db = Db(c, bc);                 // solo nominativo esatto (o base di uno skimmer): F/IC8TEM non diventa JN70
        bool moved = db.Length == 6 && grid.Length >= 4 && db[..4] != grid[..4] && (_sus?.Verdict("locatore", bc) ?? 0) > 0;
        if (db.Length == 6 && !moved) return db;
        // non in CALL3 a 6 caratteri: il locatore a 6 caratteri visto dal vivo per lo stesso nominativo (Dati/LOC6.TXT)
        var lv = LearnedLoc.Get(bc);
        if (lv.Length == 6 && (grid.Length < 4 || lv.StartsWith(grid[..4]))) return lv;
        // ultima fonte: G7VRD (si chiede in sottofondo, la risposta vale dal disegno successivo)
        var g7 = G7Lookup.Get(bc);
        if (g7.Length == 6 && (grid.Length < 4 || g7.StartsWith(grid[..4]))) return g7;
        if (g7 == "" && bc != _myCall()) G7Lookup.Request(bc);
        if (grid.Length == 0 && db.Length == 4) return db;
        return grid;
    }

    static string Db(string c, string bc)
    {
        var d = _db?.Lookup(c) ?? "";
        return d != "" || bc == c ? d : _db?.Lookup(bc) ?? "";
    }

    // ------------------------------------------------ chiamate dirette a me (UDP locale)

    /// <summary>
    /// Chi mi ha chiamato: nominativo → (locatore, ora, messaggio). La linea resta finché sull'UDP passa la chiusura del
    /// QSO (RR73, RRR o 73 tra me e quella stazione), al massimo CallToMeMinutes minuti dall'ultima chiamata.
    /// </summary>
    readonly Dictionary<string, (string grid, DateTime t, string msg, string band)> _callsToMe = new();
    public int CallToMeMinutes = 3;

    /// <summary>
    /// "IC8FAX IK3FHP JN55" passato sull'UDP locale: il primo nominativo sono io, il secondo è chi mi chiama.
    /// Il locatore viene dal messaggio raffinato con CALL3.TXT (voci a mano e file aggiunti compresi).
    /// </summary>
    void NoteCallToMe(Decode d)
    {
        var me = LoginOrCall;
        if (string.IsNullOrEmpty(me) || string.IsNullOrEmpty(d.Message)) return;
        var tok = d.Message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (tok.Length < 2) return;
        string T(string x) => x.Trim('<', '>').ToUpperInvariant();
        var to = T(tok[0]);
        var from = T(tok[1]);
        bool Mine(string c) => c == me || SkimmerBase(c) == me;
        // chiusura del QSO in una delle due direzioni: la linea si spegne
        var last = tok[^1].ToUpperInvariant();
        if (tok.Length >= 3 && last is "RR73" or "RRR" or "73")
        {
            var other = Mine(to) ? from : Mine(from) ? to : null;
            if (other != null) { lock (_callsToMe) { _callsToMe.Remove(other); _qsoEnded[other] = DateTime.UtcNow; } Dirty = true; }
            return;
        }
        if (!Mine(to)) return;
        if (from == me || from.Length < 3 || from.Contains("...")) return;
        var g = Refine(from, from == (d.Call ?? "").ToUpperInvariant() ? d.Grid : "");
        lock (_callsToMe) { _callsToMe[from] = (g, d.Time == default ? Clock.UtcNow : d.Time, d.Message, d.Band); _qsoEnded.Remove(from); }
        Dirty = true;
    }

    readonly Dictionary<string, DateTime> _qsoEnded = new();

    /// <summary>
    /// QSO in corso con quella stazione: mi ha chiamato e non è ancora arrivata la chiusura (RR73, RRR, 73), oppure è il
    /// DX Call di WSJT-X/JTDX con la trasmissione abilitata (Status degli ultimi 2 minuti) e il QSO non è già chiuso.
    /// </summary>
    public bool QsoInProgress(string call, DateTime now)
    {
        if (string.IsNullOrEmpty(call)) return false;
        var c = call.Trim('<', '>').ToUpperInvariant();
        lock (_callsToMe)
        {
            if (_qsoEnded.TryGetValue(c, out var end) && DateTime.UtcNow - end < TimeSpan.FromMinutes(3)) return false;
            if (_callsToMe.TryGetValue(c, out var v) && now - v.t <= TimeSpan.FromMinutes(CallToMeMinutes)) return true;
        }
        return Udp.TxEnabled && Udp.DxCall == c && DateTime.UtcNow - Udp.StatusTime < TimeSpan.FromMinutes(2);
    }

    public List<(string call, string grid, DateTime t, string msg, string band)> CallsToMe(DateTime now)
    {
        lock (_callsToMe)
        {
            foreach (var k in _callsToMe.Where(kv => now - kv.Value.t > TimeSpan.FromMinutes(CallToMeMinutes)).Select(kv => kv.Key).ToList()) _callsToMe.Remove(k);
            return _callsToMe.Select(kv => (kv.Key, kv.Value.grid, kv.Value.t, kv.Value.msg, kv.Value.band)).ToList();
        }
    }

    /// <summary>Locatore a 6 caratteri che finisce con LL o MM: il centro del quadrato, non il vero QTH.</summary>
    public static bool IsPadded(string g) => g != null && g.Length == 6 && (g.EndsWith("LL", StringComparison.OrdinalIgnoreCase) || g.EndsWith("MM", StringComparison.OrdinalIgnoreCase));

    /// <summary>Nominativo base di un ricevitore automatico: toglie -SDR, /SDR, -# e -1…-99 (skimmer RBN, ricevitori PSKReporter).</summary>
    public static string SkimmerBase(string c)
    {
        if (string.IsNullOrEmpty(c)) return c ?? "";
        var u = c.ToUpperInvariant();
        foreach (var suf in new[] { "-SDR", "/SDR", "_SDR", "-#" })
            if (u.EndsWith(suf) && u.Length > suf.Length + 2) return u[..^suf.Length];
        int dash = u.LastIndexOf('-');
        if (dash > 2 && u.Length - dash <= 3 && u[(dash + 1)..].All(char.IsDigit)) return u[..dash];
        return u;
    }

    /// <summary>
    /// Come Refine, ma per i dati appena arrivati: lascia le segnalazioni sospette (skimmer, centro del quadrato,
    /// quadrato diverso da CALL3.TXT) e conta gli esiti per l'auto-apprendimento.
    /// </summary>
    string RefineIn(string call, string grid, string band, string mode, DateTime t)
    {
        var g = (grid ?? "").Trim().ToUpperInvariant();
        if (!string.IsNullOrEmpty(call))
        {
            var c = call.Trim().ToUpperInvariant().Trim('<', '>');
            var bc = SkimmerBase(c);
            if (c != _myCall())
            {
                var db = Db(c, bc);
                if (bc != c && g.Length < 6 && db.Length == 6)
                    Suspects.Report("skimmer", c, "", band, mode, 0, $"{c} → {bc}: locatore {db} da CALL3.TXT al posto di {(g == "" ? "nessuno" : g)}", t);
                if (IsPadded(g))
                    Suspects.Report("centro", bc, "", band, mode, 0, $"{g} finisce con {g[4..]}: probabile centro del quadrato, la fonte tronca il locatore", t);
                if (db.Length >= 4 && g.Length >= 4 && db[..4] != g[..4])
                {
                    bool fresh = Suspects.Report("locatore", bc, "", band, mode, 0, $"ricevuto {g[..4]}, CALL3.TXT dice {db}: stazione spostata o locatore sbagliato", t);
                    if (fresh) Suspects.Outcome("locatore", bc, +1, "stesso quadrato " + g[..4]);
                }
            }
        }
        return Refine(call, g);
    }

    static readonly System.Text.RegularExpressions.Regex _jtRx = new(@"\b(Q65|JT65)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    static readonly System.Text.RegularExpressions.Regex _emeRx = new(@"\bEME\b", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

    /// <summary>
    /// Q65 e JT65 sono i modi dell'EME. Vero (= escludere) se il commento dice EME, oppure se il collegamento supera
    /// 1000 km e la Luna è sopra l'orizzonte a entrambi i capi, oppure se una delle stazioni è stata riconosciuta come EME.
    /// Una regola "buono" (+) dell'utente o imparata lo lascia passare come tropo (resta comunque la quarantena dei lunghi).
    /// </summary>
    bool EmeExclude(string band, string mode, string comment, string a, LatLon pa, string b, LatLon pb, DateTime t)
    {
        bool jt = _jtRx.IsMatch(mode ?? "") || _jtRx.IsMatch(comment ?? "");
        bool eme = _emeRx.IsMatch(comment ?? "") || _emeRx.IsMatch(mode ?? "");
        if (!jt && !eme) return false;
        double km = Geo.DistKm(pa, pb);
        var m = string.IsNullOrEmpty(mode) ? (jt ? _jtRx.Match(comment ?? "").Value.ToUpperInvariant() : "EME") : mode;
        if (eme)
        {
            Suspects.Report("eme", a, b, band, m, km, "EME dichiarato nel commento: escluso", t);
            return true;
        }
        if (km <= Gate.MinKm) return false;
        int va = Suspects.Verdict("eme", a), vb = Suspects.Verdict("eme", b);
        if (va > 0 || vb > 0)
        {
            Suspects.Report("eme", a, b, band, m, km, "regola \"buono\": trattato come tropo (quarantena dei lunghi)", t);
            return false;
        }
        double ea = Moon.Elevation(pa, t), eb = Moon.Elevation(pb, t);
        if (ea > 0 && eb > 0)
        {
            if (Suspects.Report("eme", a, b, band, m, km, $"Luna sopra l'orizzonte a entrambi i capi ({ea:0}° e {eb:0}°): probabile EME, escluso", t))
            { Suspects.Outcome("eme", a, -1, "Luna comune"); Suspects.Outcome("eme", b, -1, "Luna comune"); }
            return true;
        }
        if (va < 0 || vb < 0)
        {
            Suspects.Report("eme", a, b, band, m, km, $"stazione EME (regola imparata), Luna {ea:0}° e {eb:0}°: escluso", t);
            return true;
        }
        Suspects.Report("eme", a, b, band, m, km, $"oltre 1000 km senza Luna comune ({ea:0}° e {eb:0}°): tropo possibile, resta in quarantena", t);
        return false;
    }

    // ------------------------------------------------ aggiornamento di CALL3.TXT da MMMonVHF

    public const string Call3Url = "https://www.mmmonvhf.de/dl.php?dl=call3.txt";
    public bool Call3Auto { get => S.GetBool("call3_auto", false); set { S.Set("call3_auto", value ? "1" : "0"); Dirty = true; } }
    public int Call3AutoDays = 7;
    public DateTime Call3Last
    {
        get => DateTime.TryParse(S.Get("call3_last", ""), System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal | System.Globalization.DateTimeStyles.AssumeUniversal, out var d) ? d : DateTime.MinValue;
        set => S.Set("call3_last", value.ToString("yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture));
    }
    public string Call3Status { get; private set; } = "";
    public event Action<string> Call3StatusChanged;
    int _call3Busy;

    /// <summary>
    /// Scarica CALL3.TXT da MMMonVHF, lo controlla (almeno 100.000 righe valide), salva il vecchio come CALL3.bak e lo
    /// sostituisce nella stessa cartella da cui era stato caricato (o accanto al programma), poi lo ricarica.
    /// Le voci a mano e i file aggiunti restano sopra, invariati.
    /// </summary>
    public async Task<bool> UpdateCall3Async()
    {
        if (Interlocked.Exchange(ref _call3Busy, 1) == 1) return false;
        void St(string m) { Call3Status = m; Call3StatusChanged?.Invoke(m); }
        try
        {
            St("Scarico CALL3.TXT da mmmonvhf.de…");
            using var http = new HttpClient { Timeout = TimeSpan.FromMinutes(5) };
            http.DefaultRequestHeaders.UserAgent.ParseAdd("LIVETROPOVHF/" + (typeof(AppState).Assembly.GetName().Version?.ToString(3) ?? "0"));
            var bytes = await http.GetByteArrayAsync(Call3Url);
            var text = System.Text.Encoding.Latin1.GetString(bytes);
            int ok = 0;
            foreach (var line in text.Split('\n'))
            {
                if (line.StartsWith("//")) continue;
                var p = line.Split(',');
                if (p.Length >= 2 && p[0].Length >= 3 && Locator.IsValid(p[1].Trim())) ok++;
            }
            if (ok < 100_000) { St($"File scaricato non valido ({ok} righe utili): CALL3.TXT non cambiato."); return false; }
            var dest = CallDb.PathUsed != "" ? CallDb.PathUsed : Path.Combine(Settings.AppDir, "CALL3.TXT");
            var tmp = dest + ".nuovo";
            await File.WriteAllBytesAsync(tmp, bytes);
            if (File.Exists(dest))
            {
                File.SetAttributes(dest, FileAttributes.Normal);   // il CALL3.TXT dello zip è in sola lettura
                var bak = Path.ChangeExtension(dest, ".bak");
                if (File.Exists(bak)) File.SetAttributes(bak, FileAttributes.Normal);
                File.Copy(dest, bak, true);
            }
            File.Move(tmp, dest, true);
            CallDb.Load(new[] { dest });
            CallDb.LoadLayers();
            Call3Last = DateTime.UtcNow;
            Save();
            Dirty = true;
            CallDbLoaded?.Invoke();
            St($"CALL3.TXT aggiornato: {CallDb.Count:N0} nominativi ({DateTime.Now:dd/MM/yyyy HH:mm}). Il precedente è in CALL3.bak.");
            return true;
        }
        catch (Exception ex)
        {
            St("Aggiornamento di CALL3.TXT non riuscito: " + ex.Message);
            return false;
        }
        finally { Interlocked.Exchange(ref _call3Busy, 0); }
    }

    public void Start()
    {
        // CALL3.TXT: accanto al programma, nella cartella Dati, nella cartella di avvio o una/due cartelle sopra
        Task.Run(() =>
        {
            var dirs = new[] { Settings.AppDir, Path.Combine(Settings.AppDir, "Dati"), Directory.GetCurrentDirectory(),
                               Path.GetDirectoryName(Settings.AppDir) ?? "", Path.GetDirectoryName(Path.GetDirectoryName(Settings.AppDir) ?? "") ?? "" };
            CallDb.Load(dirs.Where(d => d != "").Select(d => Path.Combine(d, "CALL3.TXT")));
            CallDb.LoadLayers();
            CallDb.LoadCluster();
            G7Lookup.Enabled = S.GetBool("g7vrd", true);
            G7Lookup.Load();
            _ = G7Lookup.RunAsync(Http, () => Dirty = true);
            LearnedLoc.Load();
            Suspects.Load();
            Dirty = true;
            CallDbLoaded?.Invoke();
            // aggiornamento automatico da MMMonVHF, se scelto e se sono passati almeno 7 giorni
            if (Call3Auto && DateTime.UtcNow - Call3Last > TimeSpan.FromDays(Call3AutoDays)) _ = UpdateCall3Async();
        });
        // aggiornamento automatico da GitHub ogni 30 minuti (si spegne con updates=0 in LIVETROPOVHF.ini)
        Updater.Enabled = S.GetBool("updates", true);
        Updater.Changed += () => { Dirty = true; };
        _ = Updater.RunAsync();
        StartFeeds();                       // solo con un nominativo valido
        if (Mqtt.AutoStart) Mqtt.Connect();
        if (Udp.AutoStart) Udp.Start();
        Ntp.Start();
        Task.Run(() => Map.LoadOrDownloadAsync(Http));   // fuori dal thread della finestra: i file 1:10M sono grandi
        Task.Run(async () =>
        {
            while (true)
            {
                await Tropo.UpdateAsync(Http);
                await Task.Delay(TimeSpan.FromHours(Math.Max(1, Pressure.RefreshHours)));
            }
        });
        // isobare: ogni 6 ore, e quando la mappa si sposta o si ingrandisce fuori dalla zona già scaricata
        Task.Run(async () =>
        {
            DateTime lastOk = DateTime.MinValue, lastTry = DateTime.MinValue;
            while (true)
            {
                var box = ViewBox;
                var now = DateTime.UtcNow;
                bool old = now - lastOk > TimeSpan.FromHours(Math.Max(1, Pressure.RefreshHours));
                // spostando la mappa si riscarica al massimo ogni 10 minuti (limiti di Open-Meteo)
                bool moved = !Pressure.Covers(box) && now - lastTry > TimeSpan.FromMinutes(10);
                // dopo un errore si riprova tra 3 minuti, non tra 6 ore
                bool retry = lastOk < lastTry && now - lastTry > TimeSpan.FromMinutes(3);
                if (old && now - lastTry > TimeSpan.FromMinutes(3) || moved || retry)
                {
                    lastTry = now;
                    if (await Pressure.UpdateAsync(Http, box)) lastOk = DateTime.UtcNow;
                }
                await Task.Delay(15000);
            }
        });
        Task.Run(async () =>
        {
            while (true)
            {
                await Task.Delay(30000);
                var now = Clock.UtcNow;
                Spots.Prune(now, TimeSpan.FromMinutes(60));
                Listeners.Prune(now);
                Mqtt.PruneWatched();
                Decodes.Prune(now);
                Qsos.Prune(now);
                Gate.Prune(now);
                LearnedLoc.Save();
                CallDb.SaveCluster();
                G7Lookup.Save();
                Suspects.Prune(now);
                Suspects.Save();
                Dirty = true;
            }
        });
    }

    public void Save()
    {
        S.Set("call", Call); S.Set("loc", Loc); S.Set("theme", ThemeKey); S.Set("lang", LangCode);
        S.Set("band", Band); S.SetInt("window", WindowMin); S.Set("tropo_src", TropoSrc); S.Set("proj", Proj);
        S.SetDouble("tropo_opacity", TropoOpacity);
        S.SetBool("l_base", ShowBase); S.SetBool("l_grid", ShowGrid); S.SetBool("l_tropo", ShowTropo); S.SetBool("l_iso", ShowIso); S.SetBool("l_t850", ShowT850); S.SetBool("l_inv", ShowInv);
        S.SetBool("l_rx", ShowRx); S.SetBool("l_dec", ShowDec); S.SetBool("l_qso", ShowQso); S.SetBool("l_graves", ShowGraves); S.SetBool("l_spots", ShowSpots); S.SetBool("x_short", HideShort); S.SetBool("x_long", LongAsSquares); S.SetBool("x_rings", ShowRings); S.SetInt("x_longkm", (int)LongKm);
        foreach (var f in Feeds)
        {
            S.Set(f.Id + "_host", f.Host); S.SetInt(f.Id + "_port", f.Port); S.Set(f.Id + "_login", f.Login); S.Set(f.Id + "_password", f.Password);
            S.Set(f.Id + "_cmds", f.Cmds); S.SetBool(f.Id + "_recon", f.Reconnect); S.SetBool(f.Id + "_auto", f.AutoStart);
        }
        S.Set("mqtt_host", Mqtt.Host); S.SetInt("mqtt_port", Mqtt.Port); S.SetBool("mqtt_tls", Mqtt.UseTls);
        S.SetBool("mqtt_recon", Mqtt.Reconnect); S.SetBool("mqtt_auto", Mqtt.AutoStart);
        S.Set("mqtt_bands", string.Join(",", Mqtt.EnabledBands));
        S.Set("mqtt_band_names", string.Join(";", Mqtt.BandTopics.Select(kv => kv.Key + "=" + kv.Value)));
        S.Set("udp_in_addr", Udp.ListenAddress); S.SetInt("udp_in_port", Udp.ListenPort);
        S.Set("udp_out_addr", Udp.ForwardAddress); S.SetInt("udp_out_port", Udp.ForwardPort);
        S.SetBool("udp_forward", Udp.Forward); S.SetBool("udp_auto", Udp.AutoStart);
        S.Set("ntp_servers", string.Join(",", Ntp.Servers)); S.SetInt("ntp_minutes", Ntp.IntervalMinutes); S.SetInt("ntp_min", Ntp.MinAnswers);
        S.Save();
    }

    public void Shutdown()
    {
        Save();
        LearnedLoc.Save();
        CallDb.SaveCluster();
        G7Lookup.Save();
        Suspects.Save();
        foreach (var f in Feeds) f.Disconnect();
        Mqtt.Disconnect();
        Udp.Stop();
        Ntp.Stop();
        Archive.Dispose();
    }
}
