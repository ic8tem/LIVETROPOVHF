"""Traduzione automatica delle guide Markdown di LIVETROPOVHF (italiano -> lingua) con Argos Translate (offline, MIT).
Uso: python translate_docs.py <lingua> <cartella_sorgente> <cartella_uscita>
Mantiene la struttura: blocchi ``` copiati identici, tabelle cella per cella, prefissi di titoli ed elenchi,
formule $...$, `codice`, indirizzi, rimandi [n] e collegamenti protetti con segnaposto."""
import re, sys, os, json
import argostranslate.package, argostranslate.translate

lang, src, out = sys.argv[1], sys.argv[2], sys.argv[3]
os.makedirs(out, exist_ok=True)

def ensure(fr, to):
    inst = argostranslate.package.get_installed_packages()
    if any(p.from_code == fr and p.to_code == to for p in inst): return True
    av = argostranslate.package.get_available_packages()
    p = next((p for p in av if p.from_code == fr and p.to_code == to), None)
    if not p: return False
    argostranslate.package.install_from_path(p.download()); return True

argostranslate.package.update_package_index()
assert ensure('it', 'en')
direct = lang == 'en' or ensure('it', lang)
if not direct: assert ensure('en', lang), 'manca en->' + lang

def tr_raw(t):
    if lang == 'en' or direct:
        return argostranslate.translate.translate(t, 'it', lang)
    en = argostranslate.translate.translate(t, 'it', 'en')
    return argostranslate.translate.translate(en, 'en', lang)

PROT = re.compile(r'(\$[^$\n]+\$|`[^`]+`|https?://\S+|\[[0-9][0-9,\-– ]*\]|\]\([^)]+\)|\b[A-Z]{1,3}[0-9][A-Z0-9]{0,4}/?[A-Z0-9]*\b|\b[A-R]{2}[0-9]{2}(?:[A-X]{2}(?:[0-9]{2})?)?\b)')
cache = {}
def tr(text):
    s = text.strip()
    if not s or not re.search(r'[A-Za-zÀ-ÿ]{2}', s): return text
    key = s
    if key in cache: return cache[key]
    toks = []
    def keep(m):
        toks.append(m.group(0)); return f' X{len(toks)-1}Q '
    body = PROT.sub(keep, s.replace('**', ''))
    try: res = tr_raw(body)
    except Exception as e: res = body
    for i, t in enumerate(toks):
        res, n = re.subn(rf'\s?X\s?{i}\s?Q\s?', ' ' + t + ' ' if not t.startswith('](') else t, res, count=1)
        if n == 0: res += ' ' + t      # segnaposto perso: lo rimetto in fondo
    res = re.sub(r' +', ' ', res).replace(' ](', '](').replace('[ ', '[').strip()
    res = re.sub(r'\s+([.,;:!?)])', r'\1', res)
    cache[key] = res
    return res

def tr_line(line):
    m = re.match(r'^(\s*(?:#{1,6}\s+|[-*+]\s+|\d+\.\s+|>\s*)*)(.*)$', line)
    pre, body = m.group(1), m.group(2)
    if body.startswith('|') or not body.strip(): return line
    return pre + tr(body)

def tr_table(line):
    cells = line.split('|')
    if re.fullmatch(r'[\s|:\-]+', line): return line
    return '|'.join(c if not c.strip() else ' ' + tr(c) + ' ' for c in cells)

NOTE = {"it": ""}
note_it = "Traduzione automatica dall'italiano: in caso di dubbio vale la guida italiana."
for f in sorted(os.listdir(src)):
    if not f.endswith('.md'): continue
    lines = open(os.path.join(src, f), encoding='utf-8').read().split('\n')
    res, code = [], False
    for i, l in enumerate(lines):
        if l.startswith('```'): code = not code; res.append(l); continue
        if code: res.append(l); continue
        if l.lstrip().startswith('|'): res.append(tr_table(l)); continue
        res.append(tr_line(l))
        if l.startswith('# '):
            res.append(''); res.append('> ' + tr(note_it))
    open(os.path.join(out, f), 'w', encoding='utf-8').write('\n'.join(res))
    print(lang, f, len(cache), flush=True)
