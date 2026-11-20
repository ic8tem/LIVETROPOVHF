# Previsioni (sperimentale)

Dalla 1.2.0 la linguetta Previsioni calcola da sé la propagazione troposferica, oltre alle mappe Hepburn e F5LEN. È sperimentale: i calcoli sono in fase di verifica e servono a confrontare situazioni, non garantiscono un collegamento.

## Cosa fa
- Mappa del guadagno stimato rispetto all'atmosfera normale, dai profili verticali del modello GFS (Open-Meteo), per 144, 432 e 1296 MHz, fino a 72 ore avanti.
- Calcolo del segnale fra la propria stazione e un corrispondente sul terreno reale SRTM1: diffrazione, troposcatter, strato continuo lungo la tratta, condotti, rumore del ricevitore, S/N in SSB, CW e FT8.
- Linee opzionali: curve di guadagno, isobare, inversione termica.

## Colori
| Guadagno | Colore |
| sotto 7 dB | nessuno |
| 7–10 dB | azzurro |
| 10–13 dB | verde |
| 13–16 dB | giallo (strato forte, nessun condotto per la banda) |
| 16–18,5 dB | arancio |
| 18,5–21 dB | rosso (condotto che intrappola la banda) |
| da 21 dB | viola |

## Da sapere
- Il locatore a 8 o 10 caratteri attiva la quota del suolo da SRTM1; con 4 o 6 caratteri serve la quota dell'antenna sul mare.
- La prima volta si scaricano i riquadri SRTM1 del percorso (6–7 MB l'uno): può volerci qualche minuto.
- Il secondo calcolo dei condotti usa le mappe ITU-R P.453, che sono © ITU e non si ridistribuiscono: chi lo vuole le scarica dall'ITU e le mette in Dati/P453 (vedi il Manuale delle previsioni, § 6).
- Nella versione per Windows 7 e 8.1 la linguetta per ora non c'è.

Il programma contiene il Manuale delle previsioni (ogni dato, formula e fonte) e i Consigli per l'operatore; ogni dato dei risultati ha un fumetto con la fonte.
