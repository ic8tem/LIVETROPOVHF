#!/bin/sh
# LIVETROPOVHF - installazione per Mac nella cartella personale ~/LIVETROPOVHF (LIVETROPOVHF.app e cartella Dati insieme)
SRC="$(cd "$(dirname "$0")" && pwd)"
DEST="$HOME/LIVETROPOVHF"
echo "Installo LIVETROPOVHF in $DEST"
mkdir -p "$DEST"
rm -rf "$DEST/LIVETROPOVHF.app"
cp -R "$SRC/LIVETROPOVHF.app" "$DEST/"
[ -f "$SRC/LEGGIMI.txt" ] && cp "$SRC/LEGGIMI.txt" "$DEST/"
# il programma non e' firmato da Apple: tolgo il blocco di "scaricato da Internet"
xattr -dr com.apple.quarantine "$DEST/LIVETROPOVHF.app" 2>/dev/null || true
echo "Fatto. Apro LIVETROPOVHF..."
open "$DEST/LIVETROPOVHF.app"
