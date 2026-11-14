# Windows 7 e 8.1

## Il problema
Su Windows 7 la versione 1.0.0 non partiva e mostrava l'errore:

> Impossibile trovare il punto di ingresso GetSystemTimePreciseAsFileTime della procedura nella libreria di collegamento dinamico KERNEL32.dll.

LIVETROPOVHF per Windows 10 e 11 è compilato con .NET 10, che all'avvio usa la funzione di Windows `GetSystemTimePreciseAsFileTime`. Quella funzione esiste solo da Windows 8, quindi su Windows 7 il programma si ferma prima ancora di aprirsi.

## La soluzione
Dalla versione 1.0.1 ci sono pacchetti a parte per **Windows 7 SP1 e 8.1**, compilati con .NET 6, l'ultima versione di .NET che funziona su Windows 7. Si trovano nelle [Release](https://github.com/ic8tem/LIVETROPOVHF/releases/latest):

- `LIVETROPOVHF-…-windows7-x64-setup.exe` per Windows a 64 bit, oppure `-windows7-x86-setup.exe` per Windows a 32 bit;
- in alternativa gli zip portatili `LIVETROPOVHF-…-windows7-…-portatile.zip`.

## Che cosa fare
Se avevi installato la 1.0.0 su Windows 7, scarica a mano il setup `windows7` e installalo sopra: la 1.0.0 non si avvia, quindi non può aggiornarsi da sola. Impostazioni e dati restano.

## Che cosa cambia su Windows 7
- Dalla 1.0.1 in poi gli aggiornamenti automatici prendono sempre i pacchetti `windows7`.
- Manca solo la lettura OCR degli assi delle mappe, che richiede Windows 10. Le regioni Hepburn e F5LEN hanno già le misure di fabbrica, quindi in pratica non cambia nulla.

## Se ancora non parte
Può mancare l'aggiornamento di Windows 7 che serve a .NET 6: **KB2533623**, oppure il **Convenience Rollup** di Windows 7 SP1. Installalo da Windows Update o dal catalogo Microsoft Update e riprova; se l'errore resta, apri una segnalazione con la foto del messaggio.
