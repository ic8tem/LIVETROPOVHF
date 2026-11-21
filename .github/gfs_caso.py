# Profili GFS d'archivio (Open-Meteo) lungo IC8TEM JN70CN -> IK1PAG JN35TB, 29/12/2021: N, M, gradienti, inversioni.
import json, math, urllib.request
P=[1000,975,950,925,900,850,800,700,600,500]
a=(40.5625,14.2083); b=(45.0625,7.625)
pts=[(a[0]+(b[0]-a[0])*k/5, a[1]+(b[1]-a[1])*k/5) for k in range(6)]
V="surface_pressure,temperature_2m,relative_humidity_2m,wind_speed_10m,boundary_layer_height,vertical_velocity_850hPa,"+",".join(f"temperature_{p}hPa,relative_humidity_{p}hPa" for p in P)
url=("https://historical-forecast-api.open-meteo.com/v1/forecast?latitude="+",".join(f"{p[0]:.3f}" for p in pts)+
     "&longitude="+",".join(f"{p[1]:.3f}" for p in pts)+f"&hourly={V}&models=gfs_seamless&start_date=2021-12-28&end_date=2021-12-30&timezone=GMT")
d=json.load(urllib.request.urlopen(urllib.request.Request(url,headers={"User-Agent":"LIVETROPOVHF-prova"}),timeout=120))
def es(t): return 6.1121*math.exp(17.502*t/(t+240.97))
def N(p,t,rh): tk=t+273.15; return 77.6/tk*(p+4810*rh/100*es(t)/tk)
print("== GFS d'archivio lungo JN70CN -> JN35TB")
for hh in ["2021-12-29T06:00","2021-12-29T12:00","2021-12-29T16:00","2021-12-30T00:00"]:
  print(" ora",hh)
  for k,it in enumerate(d):
    h=it["hourly"]; i=h["time"].index(hh); el=it.get("elevation",0)
    ps=h["surface_pressure"][i]; lev=[(el+2,ps,h["temperature_2m"][i],h["relative_humidity_2m"][i])]
    for p in P:
      if p>=ps-2: continue
      t=h[f"temperature_{p}hPa"][i]; r=h[f"relative_humidity_{p}hPa"][i]
      z0,p0,t0,r0=lev[-1]; tv=((t0+t)/2+273.15)
      lev.append((z0+287.05*tv/9.80665*math.log(p0/p),p,t,r))
    best=(0,"");inv=""
    for j in range(1,len(lev)):
      za,pa,ta,ra=lev[j-1]; zb,pb,tb,rb=lev[j]; dz=(zb-za)/1000
      g=-(N(pb,tb,rb)-N(pa,ta,ra))/dz
      if g>best[0]: best=(g,f"{pa:.0f}-{pb:.0f}")
      if tb>ta and not inv: inv=f"inversione {pa:.0f}-{pb:.0f} hPa +{tb-ta:.1f}°C, UR {ra:.0f}->{rb:.0f}%"
    print(f"   punto {k} ({pts[k][0]:.2f},{pts[k][1]:.2f}) quota {el:.0f} m, Ps {ps:.0f}, vento {h['wind_speed_10m'][i]}, BLH {h['boundary_layer_height'][i]}, w850 {h['vertical_velocity_850hPa'][i]}: -dN/dh max {best[0]:.0f} N/km ({best[1]}); {inv or 'nessuna inversione'}")
