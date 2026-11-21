import gzip, math, struct, urllib.request, os
def tile(n):
    f=n+'.hgt'
    if not os.path.exists(f):
        u='https://s3.amazonaws.com/elevation-tiles-prod/skadi/%s/%s.hgt.gz'%(n[:3],n)
        open(f,'wb').write(gzip.decompress(urllib.request.urlopen(u,timeout=120).read()))
    return open(f,'rb').read()
T={}
def h(lat,lon):
    la=int(math.floor(lat)); lo=int(math.floor(lon))
    n='N%02dE%03d'%(la,lo)
    if n not in T: T[n]=tile(n)
    d=T[n]; r=(la+1-lat)*3600; c=(lon-lo)*3600
    i,j=int(r),int(c); fr,fc=r-i,c-j
    def g(a,b):
        v=struct.unpack('>h',d[(a*3601+b)*2:(a*3601+b)*2+2])[0]; return 0 if v<-500 else v
    return (g(i,j)*(1-fr)*(1-fc)+g(i,j+1)*(1-fr)*fc+g(i+1,j)*fr*(1-fc)+g(i+1,j+1)*fr*fc)
def dest(lat,lon,az,km):
    R=6371.0; a=math.radians(az); p1=math.radians(lat); l1=math.radians(lon); dd=km/R
    p2=math.asin(math.sin(p1)*math.cos(dd)+math.cos(p1)*math.sin(dd)*math.cos(a))
    l2=l1+math.atan2(math.sin(a)*math.sin(dd)*math.cos(p1),math.cos(dd)-math.sin(p1)*math.sin(p2))
    return math.degrees(p2),math.degrees(l2)
out=[]
for name,lat,lon in [('JN70CN83CL',40.55616,14.23420),('JN70CN93SA',40.5510,14.2480)]:
    g0=h(lat,lon); hs=g0+10; ae=1.353*6371
    out.append('%s %.5f %.5f suolo %.0f m, antenna %.0f m s.l.m.'%(name,lat,lon,g0,hs))
    for az in [x*5 for x in range(72)]+[325.3]:
        best=-1e9; bk=0; bh=0
        for i in range(1,2001):
            km=i*0.03
            la,lo=dest(lat,lon,az,km); hg=h(la,lo)
            ang=(hg-km*km/(2*ae)*1000-hs)/(km*1000)*1000
            if ang>best: best,bk,bh=ang,km,hg
        out.append('  az %5.1f: orizzonte %+7.1f mrad (%+5.2f gradi) a %5.2f km, quota %4.0f m'%(az,best,math.degrees(best/1000),bk,bh))
    out.append('  profilo a 325.3 gradi, primi 6 km ogni 150 m:')
    s=[]
    for i in range(0,41):
        km=i*0.15; la,lo=dest(lat,lon,325.3,km); s.append('%.2f:%.0f'%(km,h(la,lo)))
    out.append('  '+' '.join(s))
open('esito-monte.txt','w').write('\n'.join(out)+'\n'); print('\n'.join(out))
