#!/usr/bin/env bash
# LIVETROPOVHF - pacchetto .rpm (Fedora, openSUSE, RHEL e derivate), ricavato dal .deb dello stesso rid:
# stessi file (/opt/livetropovhf, lanciatore /usr/bin/livetropovhf che al primo avvio copia il programma in ~/LIVETROPOVHF,
# icona e voce di menu). Serve il comando rpmbuild (pacchetto "rpm" su Ubuntu).
# Uso: installer/make-rpm.sh <rid> <versione> <file .deb> <cartella di uscita>
set -euo pipefail
RID="$1"; VERSION="$2"; DEB="$3"; OUT="$4"
case "$RID" in
  linux-x64)   ARCH=x86_64 ;;
  linux-arm64) ARCH=aarch64 ;;
  linux-arm)   ARCH=armv7hl ;;
  *) echo "rid non gestito: $RID"; exit 1 ;;
esac
TOP="$(mktemp -d)"
TREE="$TOP/albero"
mkdir -p "$TREE" "$TOP/SPECS" "$TOP/RPMS"
dpkg-deb -x "$DEB" "$TREE"
# la versione rpm non accetta il trattino
RV="${VERSION//-/_}"

cat > "$TOP/SPECS/livetropovhf.spec" <<EOF
Name:           livetropovhf
Version:        ${RV}
Release:        1
Summary:        Propagazione troposferica VHF/UHF in tempo reale
License:        Freeware
URL:            https://github.com/ic8tem/LIVETROPOVHF
Packager:       Costantino Cerrotta IC8TEM
AutoReqProv:    no
Requires:       fontconfig, libX11, libICE, libSM
%global debug_package %{nil}
%global __strip /bin/true
%global __os_install_post %{nil}
%global _build_id_links none

%description
Previsione e osservazione in tempo reale della propagazione troposferica
da 144 MHz in su: mappe Hepburn e F5LEN, spot, PSKReporter, WSJT-X.
Al primo avvio il programma si copia in ~/LIVETROPOVHF con i suoi dati.
A cura di Costantino Cerrotta IC8TEM.

%install
mkdir -p %{buildroot}
cp -a ${TREE}/. %{buildroot}/

%post
command -v update-desktop-database >/dev/null 2>&1 && update-desktop-database -q || true
command -v gtk-update-icon-cache >/dev/null 2>&1 && gtk-update-icon-cache -q /usr/share/icons/hicolor || true
exit 0

%files
/opt/livetropovhf
/usr/bin/livetropovhf
/usr/share/applications/livetropovhf.desktop
/usr/share/icons/hicolor/256x256/apps/livetropovhf.png
%doc /usr/share/doc/livetropovhf
EOF

rpmbuild --define "_topdir $TOP" --target "$ARCH" -bb "$TOP/SPECS/livetropovhf.spec" >/dev/null
mkdir -p "$OUT"
cp "$TOP"/RPMS/*/livetropovhf-*.rpm "$OUT/livetropovhf-${VERSION}.${ARCH}.rpm"
rpm -qpi "$OUT/livetropovhf-${VERSION}.${ARCH}.rpm" | head -8 || true
