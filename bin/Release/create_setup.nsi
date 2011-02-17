; Copyright (C) 2009-2011
;
; This program is free software; you can redistribute it and/or modify it
; Free Software Foundation; either version 2, or (at your option) any
; later version.
; 
; This program is distributed in the hope that it will be useful,
; but WITHOUT ANY WARRANTY; without even the implied warranty of
; MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
; GNU General Public License for more details.
;

; version
!define VERSION "2.0.0"       ;version number: major.minor.release
!define VERSTATUS "beta2"     ;alpha, beta, rc, or nothing for final.
!define APPFILE "NoteFly.exe"
!define LANGFILE "langs.xml"

Name "NoteFly ${VERSION} ${VERSTATUS}" ; The name of the installer
SetCompressor lzma
AllowRootDirInstall false
CRCCheck on
InstProgressFlags smooth
ShowInstDetails show
SetDatablockOptimize on
Icon ".\..\..\Resources\icon_small.ico"
BrandingText " "
VIProductVersion "${VERSION}.0"
VIAddVersionKey "ProductName" "NoteFly"
VIAddVersionKey "FileDescription" "note taking application"

; The file to write
OutFile ".\NoteFly_v${VERSION}${VERSTATUS}.exe"

; The default installation directory
InstallDir $PROGRAMFILES\NoteFly

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\NoteFly" "Install_Dir"

; nsis UAC plugin requires launch at user level.
RequestExecutionLevel admin

!include LogicLib.nsh
!include WordFunc.nsh
!include FileFunc.nsh

;!insertmacro VersionCompare

Function .onInit

 System::Call 'kernel32::CreateMutexA(i 0, i 0, t "installnotefly") i .r1 ?e'
 Pop $R0
 
 StrCmp $R0 0 +3
   MessageBox MB_OK|MB_ICONEXCLAMATION "The installer is already running."
   Abort
   
  Call GetDotNETVersion
  Pop $0
  ${If} $0 == ".NET not found"
  
  MessageBox MB_OKCANCEL|MB_ICONSTOP ".NET framework 2.0 is not installed.\\
  $\nPlease get .NET framework 2.0. Press OK to go to the website.\\" IDOK downloadDotNet
  Abort
   Goto done
  
  downloadDotNet:
    ExecShell "open" "http://www.microsoft.com/downloads/details.aspx?familyid=0856eacb-4362-4b0d-8edd-aab15c5e04f5"
  done:
    Abort
  ${EndIf}
 
  StrCpy $0 $0 "" 1 # skip "v"
 
  ${VersionCompare} $0 "2.0" $1
  ${If} $1 == 2
    MessageBox MB_OK|MB_ICONSTOP ".NET framework v2.0 or newer is required. You have $0. Please update it first."
    Abort
  ${EndIf}
  
  #check if administrator.
    userInfo::getAccountType
    pop $0
    strCmp $0 "Admin" +3
    messageBox MB_OK "You need to have administrator rights to install NoteFly"
    Abort
    return

FunctionEnd

Function GetDotNETVersion
  Push $0
  Push $1
 
  System::Call "mscoree::GetCORVersion(w .r0, i ${NSIS_MAX_STRLEN}, *i) i .r1 ?u"
  StrCmp $1 "error" 0 +2
    StrCpy $0 "not found"
 
  Pop $1
  Exch $0
FunctionEnd

!macro BadPathsCheck
StrCpy $R0 $INSTDIR "" -2
StrCmp $R0 ":\" bad
StrCpy $R0 $INSTDIR "" -14
StrCmp $R0 "\Program Files" bad
StrCpy $R0 $INSTDIR "" -8
StrCmp $R0 "\Windows" bad
StrCpy $R0 $INSTDIR "" -6
StrCmp $R0 "\WinNT" bad
StrCpy $R0 $INSTDIR "" -9
StrCmp $R0 "\system32" bad
StrCpy $R0 $INSTDIR "" -8
StrCmp $R0 "\Desktop" bad
StrCpy $R0 $INSTDIR "" -22
StrCmp $R0 "\Documents and Settings" bad
StrCpy $R0 $INSTDIR "" -13
StrCmp $R0 "\My Documents" bad done
bad:
  MessageBox MB_OK|MB_ICONSTOP "Install path is invalid. Please choice an other installation path."
  Abort
done:
!macroend

;--------------------------------

; Pages
 PageEx license
   LicenseText "License agreement"
   LicenseData "license.txt"
 PageExEnd
Page components
Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

; The files to install
Section "main executable (required)"	
  SectionIn RO
  SetOverwrite on  
  
  ;Check installation directory and set output path to the installation directory.
  !insertmacro BadPathsCheck
  SetOutPath $INSTDIR 
  
  ;Kill running NoteFly if any, using plugin: http://nsis.sourceforge.net/KillProcDLL_plug-in (optimized version, KillProcDLL.dll only)
  KillProcDLL::KillProc "${APPFILE}" 
  sleep 300

  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\NoteFly "Install_Dir" "$INSTDIR"   
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "DisplayName" "NoteFly"  
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "URLInfoAbout" "http://www.notefly.tk"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "DisplayVersion" "${VERSION}"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "NoRepair" 1
  
  ; write the files main executable and uninstaller.
  File "${APPFILE}"
  File "${LANGFILE}"
  WriteUninstaller "uninstall.exe"   
  
SectionEnd

Section "Desktop Shortcut"
  SetShellVarContext all
  CreateShortCut "$DESKTOP\NoteFly.lnk" "$INSTDIR\${APPFILE}"  ;vista/7 icon default.
SectionEnd

Section "Start Menu Shortcuts"  
  SetShellVarContext all
  ;startmenu shortcut should be for all users or currentuser Not administrator account.
  CreateDirectory "$SMPROGRAMS\NoteFly"
  CreateShortCut "$SMPROGRAMS\NoteFly\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortCut "$SMPROGRAMS\NoteFly\NoteFly.lnk" "$INSTDIR\${APPFILE}" "" "$INSTDIR\${APPFILE}" 0
SectionEnd

;--------------------------------

; Uninstaller
Section "Uninstall"  

  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly"  
  DeleteRegKey HKLM SOFTWARE\NoteFly

  ; Remove files and uninstaller
  Delete "$INSTDIR\${APPFILE}"
  Delete "$INSTDIR\${LANGFILE}"
  Delete "$INSTDIR\uninstall.exe"
           
  RMDir "$INSTDIR"
  
  SetShellVarContext all
  Delete "$SMPROGRAMS\NoteFly\NoteFly.lnk"
  Delete "$SMPROGRAMS\NoteFly\Uninstall.lnk"
  RMDir "$SMPROGRAMS\NoteFly"


SectionEnd
