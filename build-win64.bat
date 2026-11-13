@echo off
rem ============================================================
rem  LIVETROPOVHF - compilazione per Windows 64 bit
rem  Richiede il .NET 10 SDK (https://dotnet.microsoft.com/download/dotnet/10.0)
rem  e la connessione a Internet la prima volta (scarica Avalonia, MQTTnet, SQLite).
rem ============================================================
cd /d "%~dp0"
set "DOTNET_CLI_TELEMETRY_OPTOUT=1"
set "DOTNET_NOLOGO=1"
rem SDK locale installato da prepara-sdk-win64.bat, altrimenti quello del sistema
set "DN=dotnet"
if exist "%~dp0dotnet-sdk\dotnet.exe" set "DN=%~dp0dotnet-sdk\dotnet.exe"
if "%DN%"=="dotnet" (
  where dotnet >nul 2>nul
  if errorlevel 1 goto :manca_sdk
)
"%DN%" --list-sdks 2>nul | findstr /b /c:"10." >nul
if errorlevel 1 goto :manca_sdk
goto :compila

:manca_sdk
echo.
echo  Manca il .NET 10 SDK: lo preparo adesso con prepara-sdk-win64.bat
echo  (installazione locale accanto a questo file, senza toccare il sistema).
echo.
call "%~dp0prepara-sdk-win64.bat" auto
if errorlevel 1 exit /b 1
set "DN=%~dp0dotnet-sdk\dotnet.exe"
if not exist "%DN%" set "DN=dotnet"

:compila
echo.
echo  Compilo LIVETROPOVHF per Windows 64 bit... (la prima volta ci vuole qualche minuto)
echo.
"%DN%" publish "TropoVHF\TropoVHF.csproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:DebugType=none -o "pubblicato\win-x64" > compilazione.log 2>&1
if errorlevel 1 (
  echo.
  echo  ERRORE di compilazione. Il dettaglio e' nel file compilazione.log:
  echo  mandalo in chat e lo correggo.
  echo.
  type compilazione.log | findstr /i "error"
  pause
  exit /b 1
)
if exist "CALL3.TXT" copy /y "CALL3.TXT" "pubblicato\win-x64\CALL3.TXT" >nul
echo.
echo  Fatto. Il programma e' in pubblicato\win-x64\LIVETROPOVHF.exe
echo.
explorer "pubblicato\win-x64"
pause
