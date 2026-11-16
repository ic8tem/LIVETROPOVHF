# Windows 7 e 8.1

## Il problema
Su Windows 7 la versione 1.0.0 non partiva e mostrava l'errore:

> Impossibile trovare il punto di ingresso GetSystemTimePreciseAsFileTime della procedura nella libreria di collegamento dinamico KERNEL32.dll.

LIVETROPOVHF per Windows 10 e 11 è compilato con .NET 10, che all'avvio usa la funzione di Windows `GetSystemTimePreciseAsFileTime`, presente solo da Windows 8. I pacchetti della 1.0.1, compilati con .NET 6, non partivano comunque su alcuni Windows 7.

## La soluzione: la versione 1.0.2 per Windows 7
Dalla 1.0.2 i pacchetti **windows7** sono un programma scritto apposta per **Microsoft .NET Framework 4.8**, che su Windows 7 SP1 e 8.1 è supportato, con una finestra WinForms. Il motore (cluster, PSKReporter, UDP, CALL3, previsione tropo, criteri di scarto, segnalazioni sospette, aggiornamenti automatici) e il disegno della mappa e del cielo di GRAVES sono gli stessi file della versione normale; cambiano solo la finestra e il modo di disegnare (GDI+). Ci sono le stesse linguette e le stesse 36 lingue.

Si trovano nelle [Release](https://github.com/ic8tem/LIVETROPOVHF/releases/latest):

- `LIVETROPOVHF-…-windows7-x64-setup.exe` per Windows a 64 bit, oppure `-windows7-x86-setup.exe` per Windows a 32 bit;
- in alternativa gli zip portatili `LIVETROPOVHF-…-windows7-…-portatile.zip`.

## Che cosa serve
**.NET Framework 4.8**. Su Windows 10 e 11 c'è già; su Windows 7 SP1 e 8.1 si scarica gratis da Microsoft (https://dotnet.microsoft.com/download/dotnet-framework/net48). L'installatore controlla da solo e, se manca, propone di aprire la pagina di download: dopo l'installazione di .NET Framework 4.8 si avvia di nuovo il setup.

## Che cosa fare
Se avevi installato la 1.0.0 o la 1.0.1 su Windows 7, scarica a mano il setup `windows7` 1.0.2 e installalo sopra: le versioni precedenti non si avviano, quindi non possono aggiornarsi da sole. Impostazioni e dati restano. Con lo zip portatile conviene estrarlo in una cartella nuova e copiarci LIVETROPOVHF.ini, LIVETROPOVHF.db, la cartella Dati e CALL3.TXT.

## Che cosa cambia su Windows 7
- Gli aggiornamenti automatici prendono sempre i pacchetti `windows7`.
- Mancano la lettura OCR degli assi delle mappe e il banco di taratura manuale: le regioni Hepburn e F5LEN hanno già le misure di fabbrica, quindi in pratica non cambia nulla. L'anteprima della fusione con le regioni c'è.

## Se ancora non parte
Accanto al programma si crea il file `errore-avvio.txt` con la descrizione dell'errore: apri una segnalazione allegando quel file o la foto del messaggio.
