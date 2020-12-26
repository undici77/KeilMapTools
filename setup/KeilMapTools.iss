#define ApplicationName 'KeilMapTools'
#define ApplicationVersion '1.4.0.0'

[Files]
; Applicazione
Source: "..\Bin\KeilMapLib.dll"; DestDir: {app}; Flags: ignoreversion replacesameversion replacesameversion restartreplace; Permissions: everyone-full
Source: "..\Bin\KeilMapViewer.exe"; DestDir: {app}; Flags: ignoreversion replacesameversion replacesameversion restartreplace; Permissions: everyone-full
Source: "..\Bin\KeilRtxStackSize.exe"; DestDir: {app}; Flags: ignoreversion replacesameversion replacesameversion restartreplace; Permissions: everyone-full
Source: "framework\NDP451-KB2859818-Web.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall; AfterInstall: InstallFramework; Check: FrameworkIsNotInstalled
Source: "vc_redist\vc_redist.x64.exe"; DestDir: "{tmp}"; Flags: ignoreversion replacesameversion deleteafterinstall

[Dirs]
Name: "{app}"; Permissions: everyone-full

[Setup]
AppName={#ApplicationName}
AppVersion={#ApplicationVersion}
AppVerName={#ApplicationName} {#ApplicationVersion}
OutputBaseFilename={#ApplicationName}_Setup_{#ApplicationVersion}
AppPublisher=Alessandro Barbieri
AppPublisherURL=https://github.com/undici77/KeilMapTools.git
DefaultDirName={pf64}\Undici77\{#ApplicationName}
DefaultGroupName=Undici77\{#ApplicationName}
UninstallDisplayIcon=yes
UninstallIconFile==res\KeilMapTools.ico
Compression=lzma
SolidCompression=yes
MinVersion=6.1.7600
PrivilegesRequired=admin
AppCopyright=Copyright (C) 2020 Alessandro Barbieri
SetupIconFile=res\KeilMapTools.ico
LicenseFile=license\license.rtf

[Registry]
Root: HKLM; Subkey: "SYSTEM\CurrentControlSet\Control\Session Manager\Environment"; \
    ValueType: expandsz; ValueName: "Path"; ValueData: "{olddata};{pf64}\Undici77\{#ApplicationName}"; \
    Check: NeedsAddPath('{pf64}\Undici77\{#ApplicationName}')

[Run]
Filename: "{tmp}\vc_redist.x64.exe"; Check: VCRedistNeedsInstall

[Icons]
Name: "{group}\KeilMapViewer"; Filename: "{app}\KeilMapViewer.exe"
Name: "{group}\KeilRtxStackSize"; Filename: "{app}\KeilRtxStackSize.exe"
Name: "{group}\Uninstall {#ApplicationName}"; Filename: "{uninstallexe}"

[Code]
#ifdef UNICODE
  #define AW "W"
#else
  #define AW "A"
#endif

type
	INSTALLSTATE = Longint;
const
	INSTALLSTATE_INVALIDARG = -2;  
	INSTALLSTATE_UNKNOWN    = -1;     
	INSTALLSTATE_ADVERTISED = 1;   
	INSTALLSTATE_ABSENT     = 2;       
	INSTALLSTATE_DEFAULT    = 5;      

	// Visual C++ 2015 - 2019 14.21.27702
	VC_2015_2019_REDIST_X64 = '{F7CAC7DF-3524-4C2D-A7DB-E16140A3D5E6}';

function FrameworkIsNotInstalled: Boolean;
begin
  Result := not RegKeyExists(HKEY_LOCAL_MACHINE, 'SOFTWARE\Microsoft\.NETFramework\policy\v4.0');
end;

procedure InstallFramework;
var
  StatusText: string;
  ResultCode: Integer;
begin
  StatusText := WizardForm.StatusLabel.Caption;
  WizardForm.StatusLabel.Caption := 'Installing .NET framework...';
  WizardForm.ProgressGauge.Style := npbstMarquee;
  try
    if not Exec(ExpandConstant('{tmp}\NDP451-KB2859818-Web.exe'), '/q /noreboot', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then
    begin
       MsgBox('.NET installation failed with code: ' + IntToStr(ResultCode) + '.', mbError, MB_OK);
    end;
  finally
    WizardForm.StatusLabel.Caption := StatusText;
    WizardForm.ProgressGauge.Style := npbstNormal;
  end;
end;

function MsiQueryProductState(szProduct: string): INSTALLSTATE; 
	external 'MsiQueryProductState{#AW}@msi.dll stdcall';

function VCVersionInstalled(const ProductID: string): Boolean;
	begin
		Result := MsiQueryProductState(ProductID) = INSTALLSTATE_DEFAULT;
	end;

function VCRedistNeedsInstall: Boolean;
	begin
		Result := not (VCVersionInstalled(VC_2015_2019_REDIST_X64)); 
	end;

function NeedsAddPath(Param: string): boolean;
var
  OrigPath: string;
begin
  if not RegQueryStringValue(HKEY_LOCAL_MACHINE,
    'SYSTEM\CurrentControlSet\Control\Session Manager\Environment',
    'Path', OrigPath)
  then begin
    Result := True;
    exit;
  end;
  Result := Pos(';' + Param + ';', ';' + OrigPath + ';') = 0;
end;