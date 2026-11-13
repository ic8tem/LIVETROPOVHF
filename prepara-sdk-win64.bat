@echo off
setlocal EnableExtensions
rem ============================================================
rem  LIVETROPOVHF - prepara gli strumenti per compilare
rem  Controlla che ci sia il .NET 10 SDK e, se manca, lo installa
rem  in una cartella "dotnet-sdk" accanto a questo file:
rem  niente diritti di amministratore, niente registro, niente
rem  modifiche al sistema. Per toglierlo basta cancellare la cartella.
rem  Le librerie del programma (Avalonia, MQTTnet, SQLite, Windows SDK
rem  per l'OCR) le scarica poi build-win64.bat da nuget.org.
rem ============================================================
cd /d "%~dp0"
set "DOTNET_CLI_TELEMETRY_OPTOUT=1"
set "DOTNET_NOLOGO=1"
set "DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1"
set "LOCALSDK=%~dp0dotnet-sdk"

echo.
echo  === Controllo del .NET 10 SDK ===
echo.

rem 1) SDK locale gia' presente accanto a questo file?
if exist "%LOCALSDK%\dotnet.exe" (
  "%LOCALSDK%\dotnet.exe" --list-sdks 2>nul | findstr /b /c:"10." >nul
  if not errorlevel 1 goto :locale_ok
)

rem 2) SDK installato nel sistema?
where dotnet >nul 2>nul
if errorlevel 1 goto :manca
dotnet --list-sdks 2>nul | findstr /b /c:"10." >nul
if errorlevel 1 goto :manca
echo  Trovato il .NET 10 SDK installato nel sistema:
dotnet --list-sdks | findstr /b /c:"10."
set "DN=dotnet"
goto :nuget

:locale_ok
echo  Trovato il .NET 10 SDK locale in "%LOCALSDK%":
"%LOCALSDK%\dotnet.exe" --list-sdks | findstr /b /c:"10."
set "DN=%LOCALSDK%\dotnet.exe"
goto :nuget

:manca
echo  Il .NET 10 SDK non c'e'. Lo scarico e lo installo in:
echo    %LOCALSDK%
echo  (circa 250 MB da scaricare, serve la connessione a Internet)
echo.
if not exist "%LOCALSDK%" mkdir "%LOCALSDK%"
powershell -NoProfile -ExecutionPolicy Bypass -Command ^
  "$ErrorActionPreference='Stop'; [Net.ServicePointManager]::SecurityProtocol=[Net.SecurityProtocolType]::Tls12;" ^
  "Invoke-WebRequest -UseBasicParsing 'https://dot.net/v1/dotnet-install.ps1' -OutFile '%LOCALSDK%\dotnet-install.ps1';" ^
  "& '%LOCALSDK%\dotnet-install.ps1' -Channel 10.0 -Quality GA -Architecture x64 -InstallDir '%LOCALSDK%' -NoPath" > "%~dp0prepara-sdk.log" 2>&1
if errorlevel 1 goto :errore_install
"%LOCALSDK%\dotnet.exe" --list-sdks 2>nul | findstr /b /c:"10." >nul
if errorlevel 1 goto :errore_install
echo  Installato:
"%LOCALSDK%\dotnet.exe" --list-sdks | findstr /b /c:"10."
set "DN=%LOCALSDK%\dotnet.exe"
goto :nuget

:errore_install
echo.
echo  Non sono riuscito a installare l'SDK in automatico.
echo  Il dettaglio e' in prepara-sdk.log: mandalo in chat.
echo.
echo  In alternativa:
echo   - con winget:  winget install Microsoft.DotNet.SDK.10
echo   - a mano:      https://dotnet.microsoft.com/download/dotnet/10.0
echo                  colonna SDK, "Windows x64 - Installer"
echo.
pause
exit /b 1

:nuget
echo.
echo  === Controllo della sorgente delle librerie (nuget.org) ===
"%DN%" nuget list source 2>nul | findstr /i /c:"api.nuget.org" >nul
if errorlevel 1 (
  echo  nuget.org non e' tra le sorgenti: la aggiungo.
  "%DN%" nuget add source "https://api.nuget.org/v3/index.json" -n nuget.org >nul 2>&1
) else (
  echo  nuget.org presente.
)
"%DN%" nuget list source 2>nul | findstr /i /c:"api.nuget.org" | findstr /i /c:"Disabled" >nul
if not errorlevel 1 (
  echo  nuget.org era disabilitata: la riabilito.
  "%DN%" nuget enable source nuget.org >nul 2>&1
)

echo.
echo  === Scarico in anticipo le librerie del programma ===
echo  (Avalonia, MQTTnet, SQLite e il riferimento Windows SDK per l'OCR)
"%DN%" restore "TropoVHF\TropoVHF.csproj" -r win-x64 > "%~dp0prepara-sdk.log" 2>&1
if errorlevel 1 (
  echo.
  echo  Il download delle librerie non e' riuscito. Il dettaglio e' in prepara-sdk.log:
  echo  controlla la connessione o un eventuale proxy aziendale, oppure mandalo in chat.
  echo.
  pause
  exit /b 1
)
echo  Librerie pronte.
echo.
if /i "%~1"=="auto" exit /b 0
echo  Tutto a posto: ora lancia build-win64.bat
echo.
pause
exit /b 0
