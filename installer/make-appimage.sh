#!/usr/bin/env bash
# LIVETROPOVHF - AppImage (un solo file, gira su quasi tutte le distribuzioni Linux con glibc).
# L'AppImage e' in sola lettura: al primo avvio (e a ogni nuova versione) copia il programma in ~/LIVETROPOVHF
# e lo avvia da li', cosi' la cartella Dati resta accanto al programma, come negli altri pacchetti.
# Uso: installer/make-appimage.sh <rid> <versione> <cartella pubblicata> <cartella di uscita>
set -euo pipefail
RID="$1"; VERSION="$2"; SRC="$3"; OUT="$4"
case "$RID" in
  linux-x64)   ARCH=x86_64 ;;
  linux-arm64) ARCH=aarch64 ;;
  *) echo "rid non gestito: $RID"; exit 1 ;;
esac
ROOT="$(cd "$(dirname "$0")/.." && pwd)"
A="$(mktemp -d)/LIVETROPOVHF.AppDir"
mkdir -p "$A/opt/livetropovhf" "$A/usr/share/icons/hicolor/256x256/apps"
cp -R "$SRC/." "$A/opt/livetropovhf/"
chmod 755 "$A/opt/livetropovhf/LIVETROPOVHF"
echo "$VERSION" > "$A/opt/livetropovhf/VERSIONE_PACCHETTO"
cp "$ROOT/TropoVHF/Assets/livetropovhf-256.png" "$A/livetropovhf.png"
cp "$ROOT/TropoVHF/Assets/livetropovhf-256.png" "$A/usr/share/icons/hicolor/256x256/apps/livetropovhf.png"
cat > "$A/livetropovhf.desktop" <<'EOF'
[Desktop Entry]
Type=Application
Name=LIVETROPOVHF
Comment=Propagazione troposferica VHF/UHF in tempo reale
Exec=AppRun
Icon=livetropovhf
Terminal=false
Categories=HamRadio;Science;Utility;
EOF
cat > "$A/AppRun" <<'EOF'
#!/bin/sh
# LIVETROPOVHF: copia il programma in ~/LIVETROPOVHF (solo la prima volta o con una versione nuova) e lo avvia.
HERE="$(dirname "$(readlink -f "$0")")"
DEST="$HOME/LIVETROPOVHF"
NEW="$(cat "$HERE/opt/livetropovhf/VERSIONE_PACCHETTO" 2>/dev/null)"
OLD="$(cat "$DEST/VERSIONE_PACCHETTO" 2>/dev/null)"
if [ ! -x "$DEST/LIVETROPOVHF" ] || [ "$NEW" != "$OLD" ]; then
  mkdir -p "$DEST"
  cp -R "$HERE/opt/livetropovhf/." "$DEST/"
  chmod +x "$DEST/LIVETROPOVHF"
fi
cd "$DEST" && exec "$DEST/LIVETROPOVHF" "$@"
EOF
chmod 755 "$A/AppRun"
T="$(mktemp -d)"
curl -fsSL -o "$T/appimagetool" "https://github.com/AppImage/appimagetool/releases/download/continuous/appimagetool-$(uname -m).AppImage"
chmod +x "$T/appimagetool"
mkdir -p "$OUT"
ARCH=$ARCH "$T/appimagetool" --appimage-extract-and-run "$A" "$OUT/LIVETROPOVHF-${VERSION}-${ARCH}.AppImage"
ls -la "$OUT/LIVETROPOVHF-${VERSION}-${ARCH}.AppImage"
