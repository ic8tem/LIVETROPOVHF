import urllib.request, rasterio, os, struct
for n,u in [('N40E014','https://copernicus-dem-30m.s3.amazonaws.com/Copernicus_DSM_COG_10_N40_00_E014_00_DEM/Copernicus_DSM_COG_10_N40_00_E014_00_DEM.tif'),
            ('N55E009','https://copernicus-dem-30m.s3.amazonaws.com/Copernicus_DSM_COG_10_N55_00_E009_00_DEM/Copernicus_DSM_COG_10_N55_00_E009_00_DEM.tif'),
            ('N40E013 inesistente?','https://copernicus-dem-30m.s3.amazonaws.com/Copernicus_DSM_COG_10_N39_00_E013_00_DEM/Copernicus_DSM_COG_10_N39_00_E013_00_DEM.tif')]:
    try:
        b=urllib.request.urlopen(u,timeout=300).read(); open('t.tif','wb').write(b)
        r=rasterio.open('t.tif'); p=r.profile
        print(n, len(b),'byte', p.get('width'),p.get('height'),p.get('dtype'),'compress',p.get('compress'),'predictor',p.get('predictor'),'block',p.get('blockxsize'),p.get('blockysize'),'nodata',p.get('nodata'),'transform',r.transform, 'overviews',r.overviews(1), 'tags', r.tags(), r.tags(ns='IMAGE_STRUCTURE'))
        print('byteorder', b[:4])
        a=r.read(1); print('min',a.min(),'max',a.max())
    except Exception as e: print(n,'errore',e)
