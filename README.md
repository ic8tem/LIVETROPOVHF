# LIVETROPOVHF

**Live VHF/UHF tropospheric propagation for radio amateurs — by IC8TEM**
*Propagazione troposferica VHF/UHF in tempo reale per radioamatori*

LIVETROPOVHF puts on one map the tropo forecast and what is really happening on the air from 144 MHz up.

![status](https://img.shields.io/badge/status-beta-orange) ![platform](https://img.shields.io/badge/Windows-x64-blue) ![.NET](https://img.shields.io/badge/.NET-10-purple)

## What it does / Che cosa fa
- **Forecast** – Hepburn and F5LEN tropo maps, merged worldwide; isobars, 850 hPa isotherms and thermal inversion (Open-Meteo).
- **Live** – spots from DX clusters (ON0DXK, 9A0DXC, S50CLX), RBN and DM4X, PSKReporter (MQTT), your own WSJT-X / JTDX / MSHV decodes (UDP), QSOs inferred from RR73/73.
- **Clean data** – only 144 MHz and up (GRAVES 143.050 kept); meteor scatter, EME, QO-100 and fake listeners filtered out; paths over 1000 km held until confirmed.
- **Locators** – always 6 characters: CALL3.TXT, manual entries, layered files, automatic corrections from DM4X/RBN, MMMonVHF updates and G7VRD as last source.
- **Tabs** – Map, Hepburn, F5LEN, GRAVES, Suspicious reports (self-learning), Data collection, three technical manuals with sources.
- **Portable** – everything next to the exe (LIVETROPOVHF.ini, LIVETROPOVHF.db, Dati\\): no registry, no AppData. 36 languages.

## Build (Windows 64-bit)
1. Download the source and extract it to a writable folder.
2. Double-click `build-win64.bat`. If the .NET 10 SDK is missing, it is installed locally in `dotnet-sdk\` (no admin rights).
3. Run `pubblicato\win-x64\LIVETROPOVHF.exe`.

If the build fails, the details are in `compilazione.log`.

## Automatic updates
Every 30 minutes the program checks the latest GitHub release of this repository. A release tagged `vX.Y.Z` with the asset `LIVETROPOVHF-win-x64.zip` (containing `LIVETROPOVHF.exe` and `Tarature\`) is downloaded and installed silently; it becomes active at the next start. Settings and data are never touched.

## Documentation
- [Wiki](../../wiki) – quick start, discard criteria, map colours.
- [CHANGELOG.md](CHANGELOG.md) – beta history.
- In the program: tabs *Funzionamento*, *Costruzione della mappa*, *Come si forma la tropo* (Italian, exportable to Word).

## Credits
Tropo maps: W. R. Hepburn (DX Info Centre), F5LEN. Weather: Open-Meteo. Coastlines: Natural Earth. Spots: DX clusters, Reverse Beacon Network, DM4X, PSKReporter. Callsign database: CALL3.TXT / MMMonVHF, G7VRD locator API. GRAVES data: PE1ITR and the YA-MS project.

---
*Beta software for amateur radio use. Forecasts are indications, not guarantees.*
