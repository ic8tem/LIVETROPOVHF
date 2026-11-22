#!/bin/sh
# LIVETROPOVHF - installazione per Linux nella cartella personale ~/LIVETROPOVHF (programma e cartella Dati insieme)
set -e
SRC="$(cd "$(dirname "$0")" && pwd)"
DEST="$HOME/LIVETROPOVHF"
echo "Installo LIVETROPOVHF in $DEST"
mkdir -p "$DEST"
# copia tutto; i file creati dal programma (LIVETROPOVHF.ini, archivio, Dati) e CALL3.TXT restano
[ -f "$DEST/CALL3.TXT" ] && mv -f "$DEST/CALL3.TXT" "$DEST/CALL3.TXT.tuo"   # CALL3.TXT contiene le voci a mano: non si sovrascrive
cp -R "$SRC/." "$DEST/"
[ -f "$DEST/CALL3.TXT.tuo" ] && mv -f "$DEST/CALL3.TXT.tuo" "$DEST/CALL3.TXT"
rm -f "$DEST/installa.sh"
chmod +x "$DEST/LIVETROPOVHF"
mkdir -p "$HOME/.local/share/applications" "$HOME/.local/share/icons/hicolor/256x256/apps"
cp "$DEST/livetropovhf.png" "$HOME/.local/share/icons/hicolor/256x256/apps/livetropovhf.png"
cat > "$HOME/.local/share/applications/livetropovhf.desktop" <<DESK
[Desktop Entry]
Type=Application
Name=LIVETROPOVHF
Comment=Propagazione troposferica VHF/UHF in tempo reale
Exec="$DEST/LIVETROPOVHF"
Path=$DEST
Icon=livetropovhf
Terminal=false
Categories=HamRadio;Science;Utility;
DESK
command -v update-desktop-database >/dev/null 2>&1 && update-desktop-database "$HOME/.local/share/applications" || true
echo "Fatto. Avvia LIVETROPOVHF dal menu delle applicazioni oppure con: $DEST/LIVETROPOVHF"
