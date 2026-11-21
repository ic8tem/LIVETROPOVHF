# Prova del calcolo a due vie (equazione parabolica split-step ad angolo largo, Ozgun/PETOOL) per la «via monte»
# da IC8TEM (JN70CN, Capri) verso la Spagna, con terreno SRTM1 reale. Confronto: diretto, due vie in piano, stima bistatica.
import numpy as np, gzip, io, os, math, urllib.request, sys, time
R=6371.0; CACHE='srtm'; os.makedirs(CACHE,exist_ok=True)
tiles={}
def tile(la,lo):
    k=(la,lo)
    if k in tiles: return tiles[k]
    n=f"{'N' if la>=0 else 'S'}{abs(la):02d}{'E' if lo>=0 else 'W'}{abs(lo):03d}"
    p=f"{CACHE}/{n}.hgt"
    if not os.path.exists(p):
        url=f"https://elevation-tiles-prod.s3.amazonaws.com/skadi/{n[:3]}/{n}.hgt.gz"
        try:
            d=urllib.request.urlopen(url,timeout=120).read(); open(p,'wb').write(gzip.decompress(d))
        except Exception as e: open(p,'wb').write(b'')
    b=open(p,'rb').read()
    a=np.frombuffer(b,dtype='>i2').reshape(3601,3601).astype(float) if len(b)==3601*3601*2 else None
    if a is not None: a[a<-1000]=0
    tiles[k]=a; return a
def elev(la,lo):
    a=tile(math.floor(la),math.floor(lo))
    if a is None: return 0.0
    r=(math.floor(la)+1-la)*3600; c=(lo-math.floor(lo))*3600
    return max(0.0,a[int(round(r)),int(round(c))])
def loc(l):
    l=l.upper(); lo=(ord(l[0])-65)*20-180+int(l[2])*2; la=(ord(l[1])-65)*10-90+int(l[3])
    if len(l)>=10:
        lo+=(ord(l[4])-65)/12+int(l[6])/120+(ord(l[8])-65)/2880+1/5760
        la+=(ord(l[5])-65)/24+int(l[7])/240+(ord(l[9])-65)/5760+1/11520
    elif len(l)>=8:
        lo+=(ord(l[4])-65)/12+int(l[6])/120+1/240; la+=(ord(l[5])-65)/24+int(l[7])/240+1/480
    elif len(l)>=6: lo+=(ord(l[4])-65)/12+1/24; la+=(ord(l[5])-65)/24+1/48
    else: lo+=1; la+=0.5
    return la,lo
def dest(la,lo,az,km):
    d=km/R; la1=math.radians(la); lo1=math.radians(lo); a=math.radians(az)
    la2=math.asin(math.sin(la1)*math.cos(d)+math.cos(la1)*math.sin(d)*math.cos(a))
    lo2=lo1+math.atan2(math.sin(a)*math.sin(d)*math.cos(la1),math.cos(d)-math.sin(la1)*math.sin(la2))
    return math.degrees(la2),math.degrees(lo2)
def dist_az(la1,lo1,la2,lo2):
    p1,p2=math.radians(la1),math.radians(la2); dl=math.radians(lo2-lo1)
    d=2*R*math.asin(math.sqrt(math.sin((p2-p1)/2)**2+math.cos(p1)*math.cos(p2)*math.sin(dl/2)**2))
    az=math.degrees(math.atan2(math.sin(dl)*math.cos(p2),math.cos(p1)*math.sin(p2)-math.sin(p1)*math.cos(p2)*math.cos(dl)))%360
    return d,az
def profile(la,lo,az,km,step):
    xs=np.arange(0,km+1e-9,step); return xs,np.array([elev(*dest(la,lo,az,x)) for x in xs])
F=144.3; LAM=299.792458/F; K=2*math.pi/LAM; DZ=2.0; HMAX=8192.0; NZ=int(HMAX/DZ); Z=np.arange(NZ)*DZ
WIN=np.ones(NZ); NA=NZ//2; t=np.arange(NA)/NA; WIN[-NA:]=0.5*(1+np.cos(np.pi*t))
def propagator(dx):
    p=np.fft.fftfreq(2*NZ,d=DZ)*2*np.pi; q=K*K-p*p
    return np.where(q>=0,np.exp(1j*dx*(np.sqrt(np.abs(q))-K)),np.exp(-dx*np.sqrt(np.abs(q))-1j*dx*K))
def source(h,bw=10):
    th=math.radians(bw); A=K*th/(2*math.sqrt(2*math.pi*math.log(2)))
    g=lambda zz: A*np.exp(-(th**2)*K**2*zz**2/(8*math.log(2)))
    return (g(Z-h)-g(Z+h)).astype(complex)
def step(u,prop,phase):
    e=np.concatenate([u,-u[::-1]]); e=np.fft.ifft(prop*np.fft.fft(e)); return e[:NZ]*phase
def Mprof(kind):
    M=315+0.118*Z
    if kind=='condotto mare 150 m': M=np.where(Z<150,315+18-18*Z/150+0.0*Z,M+0)   # deficit 18 M sotto 150 m
    if kind=='condotto elevato 300-400 m': M=np.where(Z>300,M-np.minimum(30,(Z-300)/100*30),M)
    return M
def loss(u,x,hr):
    a=np.interp(hr,Z,np.abs(u)); return -20*math.log10(a+1e-300)+20*math.log10(4*math.pi)+10*math.log10(max(x,1))-30*math.log10(LAM)
def run_oneway(u,xs,ter,phase,prop,dx,hr=None):
    for i in range(1,len(xs)):
        u=step(u,prop,phase); ig=int(ter[i]/DZ); u[:ig]=0
    return u
def two_way(htx_asl,ter_b,xs_b,ter_f,xs_f,hr_asl,phase,prop,dx):
    # andata verso il monte: si registra il campo che urta le facce del terreno (riflessione orizzontale, Γ = −1)
    u=source(htx_asl); refl={}; igp=int(ter_b[0]/DZ)
    for i in range(1,len(xs_b)):
        u=step(u,prop,phase); ig=int(ter_b[i]/DZ)
        if ig>igp: refl[i]=-u.copy()*(Z<ig*DZ)*(Z>=igp*DZ)
        u[:ig]=0; igp=ig
    # ritorno: dal fondo verso la stazione, aggiungendo i campi riflessi, poi oltre la stazione verso il corrispondente
    v=np.zeros(NZ,complex)
    for i in range(len(xs_b)-1,0,-1):
        if i in refl: v=v+refl[i]
        v=step(v,prop,phase); v[:int(ter_b[i-1]/DZ)]=0
    for i in range(1,len(xs_f)):
        v=step(v,prop,phase); v[:int(ter_f[i]/DZ)]=0
    return loss(v,xs_f[-1]*1000+xs_b[-1]*1000,hr_asl), loss(v,xs_f[-1]*1000,hr_asl)
def launch(u,z1=20,z2=400):
    m=(Z>=z1)&(Z<=z2); return 10*math.log10(np.mean(np.abs(u[m])**2)+1e-300)
def two_way_field(htx_asl,ter_b,xs_b,ter_f,xs_f,phase,prop):
    u=source(htx_asl); refl={}; igp=int(ter_b[0]/DZ)
    for i in range(1,len(xs_b)):
        u=step(u,prop,phase); ig=int(ter_b[i]/DZ)
        if ig>igp: refl[i]=-u.copy()*(Z<ig*DZ)*(Z>=igp*DZ)
        u[:ig]=0; igp=ig
    v=np.zeros(NZ,complex)
    for i in range(len(xs_b)-1,0,-1):
        if i in refl: v=v+refl[i]
        v=step(v,prop,phase); v[:int(ter_b[i-1]/DZ)]=0
    for i in range(1,len(xs_f)):
        v=step(v,prop,phase); v[:int(ter_f[i]/DZ)]=0
    return v
def main3():
    out=[]; W=lambda s:(print(s),out.append(s))
    QTH='JN70CN83CL'; AGL=8.0
    tla,tlo=loc(QTH); g0=elev(tla,tlo); htx=g0+AGL
    W(f"QTH {QTH} = {tla:.5f}, {tlo:.5f}; suolo SRTM1 {g0:.0f} m; antenna {AGL:.0f} m dal suolo ({htx:.0f} m s.l.m.); 144,3 MHz, polarizzazione orizzontale, atmosfera standard")
    W("Misura: potenza media del campo fra 20 e 400 m s.l.m. a 80 km dalla stazione, sulla tratta verso il corrispondente (dB relativi).")
    W("Diretto = antenna verso il corrispondente. Via monte = antenna verso il rilievo, riflessione sulle facce del terreno (Γ = −1), ritorno lungo la tratta.")
    W("«Nel piano» = rilievo entro 40° dalla direzione opposta al corrispondente; altrimenti il numero è solo indicativo (il calcolo è in due dimensioni).")
    peaks={'Vesuvio':(40.8213,14.4261),'Faito/Molare':(40.6560,14.5000),'Monte Solaro':(40.5530,14.2240),'Monte Tiberio':(40.5560,14.2570)}
    dx=100.0; prop=propagator(dx); phase=np.exp(1j*K*Mprof('standard')*1e-6*dx)*WIN
    for pn,(pla,plo) in peaks.items():
        dp,azp=dist_az(tla,tlo,pla,plo); W(f"   {pn}: azimut {azp:.0f}°, {dp:.1f} km, quota SRTM {elev(pla,plo):.0f} m")
    # orizzonte intorno alla stazione
    hz=[]
    for a in range(0,360,10):
        xs,ter=profile(tla,tlo,a,30,0.05)
        hz.append((a,max(math.degrees(math.atan((ter[i]-htx)/(xs[i]*1000))) for i in range(1,len(xs)))))
    W("Orizzonte (elevazione dell'ostacolo più alto entro 30 km): "+", ".join(f"{a}° {h:+.1f}°" for a,h in hz))
    locs=[]
    for l in open('TropoVHF/Tarature/validazione-2021-12-29.txt'):
        p=l.strip().split(';')
        if len(p)>=5 and p[4]=='monte' and p[2] not in locs: locs.append(p[2])
    W(f"\nI {len(locs)} corrispondenti della categoria «monte» (1–2/1/2022):")
    for l in locs:
        rla,rlo=loc(l); d,az=dist_az(tla,tlo,rla,rlo)
        xs,ter=profile(tla,tlo,az,80,dx/1000)
        hzm=max(math.degrees(math.atan((ter[i]-htx)/(xs[i]*1000))) for i in range(1,len(xs)))
        ld=launch(run_oneway(source(htx),xs,ter,phase,prop,dx))
        best=None; line=f"  {l} {d:.0f} km az {az:.0f}° (ostacolo {hzm:+.1f}°): diretto {ld:.1f}"
        for pn,(pla,plo) in peaks.items():
            dp,azp=dist_az(tla,tlo,pla,plo); off=abs(((azp-az+540)%360)-180)
            xb,tb=profile(tla,tlo,azp,min(40,dp+6),dx/1000)
            lv=launch(two_way_field(htx,tb,xb,ter,xs,phase,prop))
            line+=f"; {pn} {lv-ld:+.1f}{'' if off>=140 else '*'}"
            if best is None or lv>best[1]: best=(pn,lv)
        line+=f" → meglio: {'diretto' if best[1]<=ld else best[0]}"
        W(line)
    W("  (* = fuori dal piano, indicativo)")
    W("\nPuntamento migliore, giro completo ogni 10° (due vie nel piano solo vicino alla direzione opposta; il resto indicativo):")
    for name,l in {'EA3 JN11CK':'JN11CK','EA5 IM98PG':'IM98PG','EA6 JM29DV':'JM29DV','I1 JN44ET':'JN44ET'}.items():
        rla,rlo=loc(l); d,az=dist_az(tla,tlo,rla,rlo)
        xs,ter=profile(tla,tlo,az,80,dx/1000)
        ld=launch(run_oneway(source(htx),xs,ter,phase,prop,dx)); res=[]
        for a in range(0,360,10):
            off=abs(((a-az+540)%360)-180)
            if off<30: continue
            xb,tb=profile(tla,tlo,a,40,dx/1000)
            res.append((launch(two_way_field(htx,tb,xb,ter,xs,phase,prop))-ld,a))
        res.sort(reverse=True)
        W(f"  {name} (az {az:.0f}°): "+", ".join(f"{a}° {v:+.1f}" for v,a in res[:6]))
    open('esito-monte.txt','w').write('\n'.join(out)+'\n')
def main2():
    out=[]; W=lambda s:(print(s),out.append(s))
    W("Lancio nel condotto: potenza media del campo fra 20 e 400 m s.l.m. a 80 km dalla stazione, sulla tratta verso il corrispondente (dB relativi).")
    W("Diretto = antenna verso il corrispondente; via monte = antenna verso il rilievo, riflessione sulle facce del terreno (Γ = −1), ritorno lungo la tratta.")
    W("Atmosfera standard (conta il terreno vicino); 144,3 MHz; antenna 10 m dal suolo; passo 100 m.")
    txs={'Capri centro':(40.5508,14.2426),'Capri Marina Grande':(40.5560,14.2370),'Anacapri':(40.5560,14.2160),'Capri Tragara':(40.5450,14.2500)}
    peaks={'Vesuvio':(40.8213,14.4261),'Faito/Molare':(40.6560,14.5000),'Monte Solaro':(40.5530,14.2240)}
    targets={'EA3':'JN11CK','EA5':'IM98PG','EA6':'JM29DV','I1 (mare)':'JN44ET'}
    dx=100.0; prop=propagator(dx); phase=np.exp(1j*K*Mprof('standard')*1e-6*dx)*WIN
    for tn,(tla,tlo) in txs.items():
        g0=elev(tla,tlo); htx=g0+10
        W(f"--- {tn} ({tla:.4f}, {tlo:.4f}), suolo {g0:.0f} m")
        for name,l in targets.items():
            rla,rlo=loc(l); d,az=dist_az(tla,tlo,rla,rlo)
            xs,ter=profile(tla,tlo,az,80,dx/1000)
            hz=max(math.degrees(math.atan((ter[i]-htx)/(xs[i]*1000))) for i in range(1,len(xs)))
            ld=launch(run_oneway(source(htx),xs,ter,phase,prop,dx))
            line=f"    {name} az {az:.0f}° (ostacolo più alto {hz:+.1f}°): diretto {ld:.1f}"
            for pn,(pla,plo) in peaks.items():
                dp,azp=dist_az(tla,tlo,pla,plo); off=abs(((azp-az+540)%360)-180)
                if dp<0.5: continue
                xb,tb=profile(tla,tlo,azp,min(40,dp+6),dx/1000)
                lv=launch(two_way_field(htx,tb,xb,ter,xs,phase,prop))
                line+=f"; via {pn} (az {azp:.0f}°, fuori asse {off:.0f}°{'' if off>=140 else ', fuori piano: solo indicativo'}) {lv:.1f} ({lv-ld:+.1f})"
            W(line)
    open('esito-monte.txt','w').write('\n'.join(out)+'\n')
def main():
    out=[]; W=lambda s:(print(s),out.append(s))
    txs={'Capri centro':(40.5508,14.2426),'Capri Marina Grande':(40.5560,14.2370),'Anacapri':(40.5560,14.2160)}
    peaks={'Vesuvio':(40.8213,14.4261),'Faito/Molare':(40.6560,14.5000)}
    targets={'EA3 JN11CK':'JN11CK','EA3 JN01UF':'JN01UF','EA5 IM98PG':'IM98PG','EA5 IM99SL':'IM99SL','EA6 JM29DV':'JM29DV','I1 JN44ET (mare)':'JN44ET'}
    dx=500.0; prop=propagator(dx)
    for atm in ['standard','condotto mare 150 m','condotto elevato 300-400 m']:
        phase=np.exp(1j*K*Mprof(atm)*1e-6*dx)*WIN
        W(f"\n===== atmosfera: {atm}, 144,3 MHz, antenna 10 m dal suolo, polarizzazione orizzontale =====")
        for tn,(tla,tlo) in txs.items():
            g0=elev(tla,tlo); htx=g0+10
            W(f"--- {tn} ({tla:.4f}, {tlo:.4f}), suolo {g0:.0f} m")
            for pn,(pla,plo) in peaks.items():
                dp,azp=dist_az(tla,tlo,pla,plo); W(f"    {pn}: azimut {azp:.0f}°, {dp:.1f} km")
            for name,l in targets.items():
                rla,rlo=loc(l); d,az=dist_az(tla,tlo,rla,rlo)
                xs,ter=profile(tla,tlo,az,d,dx/1000); hr=ter[-1]+10
                u=run_oneway(source(htx),xs,ter,phase,prop,dx); ld=loss(u,d*1000,hr)
                blk=max(((ter[i]-htx)/ (xs[i]*1000) - xs[i]*1000/(2*8500e3)) for i in range(1,min(len(xs),80)))
                line=f"    {name}: {d:.0f} km az {az:.0f}°, diretto {ld:.1f} dB (orizzonte vicino {math.degrees(math.atan(blk)):+.2f}°)"
                for pn,(pla,plo) in peaks.items():
                    dp,azp=dist_az(tla,tlo,pla,plo); off=((azp-az+540)%360)-180
                    if abs(abs(off)-180)<=40:
                        # due vie quasi in piano: andata verso il monte (fino a 8 km oltre la cima), ritorno lungo la tratta verso il corrispondente
                        xb,tb=profile(tla,tlo,azp,min(40,dp+8),dx/1000)
                        lt,_=two_way(htx,tb,xb,ter,xs,hr,phase,prop,dx)
                        line+=f"; due vie via {pn} (fuori asse {abs(off):.0f}°) {lt:.1f} dB ({lt-ld:+.1f})"
                    else:
                        line+=f"; {pn} fuori asse {abs(off):.0f}°: non in piano"
                W(line)
    open('esito-monte.txt','w').write('\n'.join(out)+'\n')
t0=time.time(); main3(); print('tempo',time.time()-t0)
