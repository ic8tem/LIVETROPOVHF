#!/usr/bin/env bash
# LIVETROPOVHF - pacchetto Slackware (.txz), ricavato dal .deb dello stesso rid: stessi file del .deb
# (/opt/livetropovhf, lanciatore /usr/bin/livetropovhf che al primo avvio copia il programma in ~/LIVETROPOVHF).
# Si installa da root con: installpkg livetropovhf-<versione>-<arch>-1_ic8tem.txz
# Uso: installer/make-txz.sh <rid> <versione> <file .deb> <cartella di uscita>
set -euo pipefail
RID="$1"; VERSION="$2"; DEB="$3"; OUT="$4"
case "$RID" in
  linux-x64)   ARCH=x86_64 ;;
  linux-arm64) ARCH=aarch64 ;;
  linux-arm)   ARCH=arm ;;
  *) echo "rid non gestito: $RID"; exit 1 ;;
esac
T="$(mktemp -d)"
dpkg-deb -x "$DEB" "$T"
mkdir -p "$T/install"
# slack-desc: 11 righe che iniziano con il nome del pacchetto
cat > "$T/install/slack-desc" <<'EOF'
     |-----handy-ruler------------------------------------------------------|
livetropovhf: livetropovhf (propagazione troposferica VHF/UHF in tempo reale)
livetropovhf:
livetropovhf: Previsione e osservazione in tempo reale della propagazione
livetropovhf: troposferica da 144 MHz in su (Hepburn, F5LEN, spot, PSKReporter).
livetropovhf: Al primo avvio il programma si copia in ~/LIVETROPOVHF con la sua cartella
livetropovhf: data. A cura di Costantino Cerrotta IC8TEM.
livetropovhf:
livetropovhf: https://github.com/ic8tem/LIVETROPOVHF
livetropovhf:
livetropovhf:
livetropovhf:
EOF
cat > "$T/install/doinst.sh" <<'EOF'
if [ -x /usr/bin/update-desktop-database ]; then /usr/bin/update-desktop-database -q usr/share/applications >/dev/null 2>&1; fi
if [ -x /usr/bin/gtk-update-icon-cache ]; then /usr/bin/gtk-update-icon-cache -q usr/share/icons/hicolor >/dev/null 2>&1; fi
EOF
SV="${VERSION//-/_}"
mkdir -p "$OUT"
tar -C "$T" --owner=0 --group=0 -cJf "$OUT/livetropovhf-${SV}-${ARCH}-1_ic8tem.txz" install opt usr
ls -la "$OUT/livetropovhf-${SV}-${ARCH}-1_ic8tem.txz"
