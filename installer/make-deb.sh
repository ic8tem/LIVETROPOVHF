#!/usr/bin/env bash
# LIVETROPOVHF - pacchetto .deb (Debian, Ubuntu, Mint, Raspberry Pi OS).
# Il pacchetto mette il programma in /opt/livetropovhf; al primo avvio (e a ogni nuova versione) il lanciatore
# /usr/bin/livetropovhf lo copia nella cartella personale ~/LIVETROPOVHF e lo avvia da li': cosi' la cartella Dati resta
# accanto al programma, scrivibile dall'utente, e non si scrive nulla altrove.
# Uso: installer/make-deb.sh <rid> <versione> <cartella pubblicata> <cartella di uscita>
set -euo pipefail
RID="$1"; VERSION="$2"; SRC="$3"; OUT="$4"
case "$RID" in
  linux-x64)   ARCH=amd64 ;;
  linux-arm64) ARCH=arm64 ;;
  linux-arm)   ARCH=armhf ;;
  *) echo "rid non gestito: $RID"; exit 1 ;;
esac
ROOT="$(cd "$(dirname "$0")/.." && pwd)"
PKG="$(mktemp -d)/livetropovhf_${VERSION}_${ARCH}"

install -d "$PKG/DEBIAN" "$PKG/opt/livetropovhf" "$PKG/usr/bin" "$PKG/usr/share/applications" \
           "$PKG/usr/share/icons/hicolor/256x256/apps" "$PKG/usr/share/doc/livetropovhf"
cp -R "$SRC/." "$PKG/opt/livetropovhf/"
chmod 755 "$PKG/opt/livetropovhf/LIVETROPOVHF"
echo "$VERSION" > "$PKG/opt/livetropovhf/VERSIONE_PACCHETTO"
install -m 644 "$ROOT/TropoVHF/Assets/livetropovhf-256.png" "$PKG/usr/share/icons/hicolor/256x256/apps/livetropovhf.png"
install -m 644 "$ROOT/README.md" "$PKG/usr/share/doc/livetropovhf/README.md"
printf "LIVETROPOVHF - a cura di Costantino Cerrotta IC8TEM\nhttps://github.com/ic8tem/LIVETROPOVHF\n" > "$PKG/usr/share/doc/livetropovhf/copyright"
chmod 644 "$PKG/usr/share/doc/livetropovhf/copyright"

cat > "$PKG/usr/bin/livetropovhf" <<'EOF'
#!/bin/sh
# LIVETROPOVHF: copia il programma in ~/LIVETROPOVHF (solo la prima volta o con una versione nuova) e lo avvia.
# I file creati dal programma (LIVETROPOVHF.ini, archivio, Dati) non vengono toccati.
DEST="$HOME/LIVETROPOVHF"
NEW="$(cat /opt/livetropovhf/VERSIONE_PACCHETTO 2>/dev/null)"
OLD="$(cat "$DEST/VERSIONE_PACCHETTO" 2>/dev/null)"
if [ ! -x "$DEST/LIVETROPOVHF" ] || [ "$NEW" != "$OLD" ]; then
  mkdir -p "$DEST"
  [ -f "$DEST/CALL3.TXT" ] && mv -f "$DEST/CALL3.TXT" "$DEST/CALL3.TXT.tuo"   # CALL3.TXT contiene le voci a mano: non si sovrascrive
  cp -R /opt/livetropovhf/. "$DEST/"
  [ -f "$DEST/CALL3.TXT.tuo" ] && mv -f "$DEST/CALL3.TXT.tuo" "$DEST/CALL3.TXT"
  chmod +x "$DEST/LIVETROPOVHF"
fi
cd "$DEST" && exec "$DEST/LIVETROPOVHF" "$@"
EOF
chmod 755 "$PKG/usr/bin/livetropovhf"

cat > "$PKG/usr/share/applications/livetropovhf.desktop" <<EOF
[Desktop Entry]
Type=Application
Name=LIVETROPOVHF
Comment=Propagazione troposferica VHF/UHF in tempo reale
Exec=/usr/bin/livetropovhf
Icon=livetropovhf
Terminal=false
Categories=HamRadio;Science;Utility;
EOF

SIZE=$(du -sk "$PKG" | cut -f1)
cat > "$PKG/DEBIAN/control" <<EOF
Package: livetropovhf
Version: ${VERSION}
Section: hamradio
Priority: optional
Architecture: ${ARCH}
Installed-Size: ${SIZE}
Depends: libfontconfig1, libx11-6, libice6, libsm6
Maintainer: Costantino Cerrotta IC8TEM <ic8tem@users.noreply.github.com>
Homepage: https://github.com/ic8tem/LIVETROPOVHF
Description: propagazione troposferica VHF/UHF in tempo reale
 Previsione e osservazione in tempo reale della propagazione troposferica
 da 144 MHz in su: mappe Hepburn e F5LEN, spot, PSKReporter, WSJT-X.
 Al primo avvio il programma si copia in ~/LIVETROPOVHF con i suoi dati.
EOF

cat > "$PKG/DEBIAN/postinst" <<'EOF'
#!/bin/sh
set -e
command -v update-desktop-database >/dev/null 2>&1 && update-desktop-database -q || true
command -v gtk-update-icon-cache >/dev/null 2>&1 && gtk-update-icon-cache -q /usr/share/icons/hicolor || true
exit 0
EOF
chmod 755 "$PKG/DEBIAN/postinst"

mkdir -p "$OUT"
dpkg-deb --root-owner-group --build "$PKG" "$OUT/livetropovhf_${VERSION}_${ARCH}.deb"
dpkg-deb --info "$OUT/livetropovhf_${VERSION}_${ARCH}.deb" | head -12 || true
