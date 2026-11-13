@echo off
rem Compila per tutte le piattaforme previste (Windows 64/32, Mac Intel/Apple Silicon, Linux x64, Raspberry 64/32).
rem Le versioni diverse da Windows 64 non sono ancora state provate.
cd /d "%~dp0"
for %%R in (win-x64 win-x86 osx-x64 osx-arm64 linux-x64 linux-arm64 linux-arm) do (
  echo Compilo %%R ...
  dotnet publish "TropoVHF\TropoVHF.csproj" -c Release -r %%R --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:DebugType=none -o "pubblicato\%%R" >> compilazione-tutti.log 2>&1
  if exist "CALL3.TXT" copy /y "CALL3.TXT" "pubblicato\%%R\CALL3.TXT" >nul
)
echo Fatto: cartella pubblicato. Eventuali errori in compilazione-tutti.log
pause
