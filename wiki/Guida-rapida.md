# Guida rapida

## Compilare (Windows 64 bit)
1. Scarica i sorgenti ed estraili in una cartella scrivibile (non in C:\Programmi).
2. Doppio clic su `build-win64.bat`. Se manca il .NET 10 SDK lo installa da solo nella cartella `dotnet-sdk` (senza amministratore).
3. Il programma è in `pubblicato\win-x64\LIVETROPOVHF.exe`. Se qualcosa va storto, il dettaglio è in `compilazione.log`.

## Primo avvio
1. Inserisci nominativo e locatore (meglio a 6 caratteri): il nominativo viene copiato come login dei cluster.
2. Accendi le fonti in *Raccolta dati*: DX cluster, RBN, DM4X, MQTT PSKReporter, UDP (porta 2237 di WSJT-X/JTDX/MSHV).
3. Scegli banda, finestra temporale e strati della mappa.

## File accanto al programma
| File | Contenuto |
|---|---|
| LIVETROPOVHF.ini | impostazioni |
| LIVETROPOVHF.db | archivio di spot, ascolti e decodifiche |
| CALL3.TXT | database nominativo → locatore |
| Dati\CALL3-MANUALE.TXT | voci a mano |
| Dati\CALL3-CLUSTER.TXT | correzioni automatiche dal cluster |
| Dati\APPRENDIMENTO.TXT | regole imparate |
