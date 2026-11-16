; LIVETROPOVHF - installatore per Windows (Inno Setup 6)
; Installa nella cartella personale C:\LIVETROPOVHF (programma, impostazioni e dati insieme), senza permessi di amministratore.
#ifndef AppVersion
  #define AppVersion "1.0.0"
#endif
#ifndef Arch
  #define Arch "x64"
#endif
#ifndef Flavor
  #define Flavor "windows"
#endif
#ifndef SourceDir
  #define SourceDir "..\publish\win-x64"
#endif

[Setup]
AppId={{3E8B6F21-5C47-4D9A-A1E2-7F4C2B9D6E13}
AppName=LIVETROPOVHF
AppVersion={#AppVersion}
AppVerName=LIVETROPOVHF {#AppVersion}
AppPublisher=Costantino Cerrotta IC8TEM
AppPublisherURL=https://github.com/ic8tem/LIVETROPOVHF
AppComments=Previsione e osservazione in tempo reale della propagazione troposferica VHF/UHF
DefaultDirName={sd}\LIVETROPOVHF
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
#if Arch == "x64"
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
#endif
#if Arch == "arm64"
ArchitecturesAllowed=arm64
ArchitecturesInstallIn64BitMode=arm64
#endif
OutputDir=..\dist
OutputBaseFilename=LIVETROPOVHF-{#AppVersion}-{#Flavor}-{#Arch}-setup
#if Flavor == "windows7"
MinVersion=6.1sp1
#endif
SetupIconFile=..\TropoVHF\Assets\livetropovhf.ico
UninstallDisplayIcon={app}\LIVETROPOVHF.exe
Compression=lzma2
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "italian"; MessagesFile: "compiler:Languages\Italian.isl"
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[InstallDelete]
; librerie della versione precedente (anche se era per un altro Windows): si rimettono da capo; dati e impostazioni restano
Type: files; Name: "{app}\*.dll"

[Files]
; programma e tarature: aggiornati a ogni installazione
Source: "{#SourceDir}\*"; DestDir: "{app}"; Excludes: "CALL3.TXT"; Flags: ignoreversion recursesubdirs createallsubdirs
; database nominativi: solo se non c'e' gia' (quello dell'utente puo' essere stato aggiornato da MMMonVHF)
Source: "{#SourceDir}\CALL3.TXT"; DestDir: "{app}"; Flags: onlyifdoesntexist skipifsourcedoesntexist

[Icons]
Name: "{userprograms}\LIVETROPOVHF"; Filename: "{app}\LIVETROPOVHF.exe"
Name: "{userdesktop}\LIVETROPOVHF"; Filename: "{app}\LIVETROPOVHF.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\LIVETROPOVHF.exe"; Description: "{cm:LaunchProgram,LIVETROPOVHF}"; Flags: nowait postinstall skipifsilent

#if Flavor == "windows7"
[Code]
// La versione per Windows 7 e 8.1 e' scritta per Microsoft .NET Framework 4.8: se manca lo si dice e si apre la pagina
// di Microsoft da cui scaricarlo (Windows 7 SP1 e 8.1 lo accettano; su Windows 10 e 11 c'e' gia').
function HasNet48: Boolean;
var r: Cardinal;
begin
  Result := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', r) and (r >= 528040);
end;

function InitializeSetup: Boolean;
var ErrorCode: Integer;
begin
  Result := True;
  if not HasNet48 then
  begin
    if MsgBox('LIVETROPOVHF per Windows 7 richiede Microsoft .NET Framework 4.8, che su questo computer non risulta installato.' + #13#10 + 'LIVETROPOVHF for Windows 7 needs Microsoft .NET Framework 4.8, which is not installed on this computer.' + #13#10#13#10 +
              'Aprire la pagina di download di Microsoft? Dopo l''installazione riavviare questo programma.' + #13#10 + 'Open the Microsoft download page? Run this setup again afterwards.', mbConfirmation, MB_YESNO) = IDYES then
      ShellExec('open', 'https://dotnet.microsoft.com/download/dotnet-framework/net48', '', '', SW_SHOW, ewNoWait, ErrorCode);
    Result := False;
  end;
end;
#endif
