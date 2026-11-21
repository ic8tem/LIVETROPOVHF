import math, struct, gzip, urllib.request, os
import rasterio
def dest(lat,lon,az,km):
    R=6371.0; a=math.radians(az); p1=math.radians(lat); l1=math.radians(lon); dd=km/R
    p2=math.asin(math.sin(p1)*math.cos(dd)+math.cos(p1)*math.sin(dd)*math.cos(a))
    l2=l1+math.atan2(math.sin(a)*math.sin(dd)*math.cos(p1),math.cos(dd)-math.sin(p1)*math.sin(p2))
    return math.degrees(p2),math.degrees(l2)
sk=gzip.decompress(urllib.request.urlopen('https://s3.amazonaws.com/elevation-tiles-prod/skadi/N40/N40E014.hgt.gz',timeout=120).read())
def hs(lat,lon):
    r=(41-lat)*3600; c=(lon-14)*3600; i,j=int(round(r)),int(round(c))
    return struct.unpack('>h',sk[(i*3601+j)*2:(i*3601+j)*2+2])[0]
u='https://copernicus-dem-30m.s3.amazonaws.com/Copernicus_DSM_COG_10_N40_00_E014_00_DEM/Copernicus_DSM_COG_10_N40_00_E014_00_DEM.tif'
open('cop.tif','wb').write(urllib.request.urlopen(u,timeout=300).read())
cop=rasterio.open('cop.tif'); band=cop.read(1)
def hc(lat,lon):
    r,c=cop.index(lon,lat); return float(band[r,c])
out=['Confronto SRTM1 (skadi, AWS Terrain Tiles) e Copernicus GLO-30 (ESA/Airbus), punti piu vicini']
lat0,lon0=40.55616,14.23420
out.append('stazione JN70CN83CL: skadi %d m, Copernicus %.1f m'%(hs(lat0,lon0),hc(lat0,lon0)))
for az in [0,325.3,300,45]:
    out.append('azimut %.1f: distanza km -> skadi / Copernicus (m)'%az)
    s=[]
    for i in range(0,17):
        km=i*0.1; la,lo=dest(lat0,lon0,az,km); s.append('%.1f:%d/%.0f'%(km,hs(la,lo),hc(la,lo)))
    out.append('  '+'  '.join(s))
# punti sicuramente in mare aperto vicino a Capri
for la,lo,n in [(40.5617,14.2290,'mare NO Marina Grande'),(40.5650,14.2342,'mare N stazione'),(40.5700,14.2500,'golfo')]:
    out.append('%s (%.4f, %.4f): skadi %d m, Copernicus %.1f m'%(n,la,lo,hs(la,lo),hc(la,lo)))
open('esito-monte.txt','w').write('\n'.join(out)+'\n'); print('\n'.join(out))
