;  NoteFly a note application.
;  Copyright (C) 2010-2011  Tom
;
;  This program is free software: you can redistribute it and/or modify
;  it under the terms of the GNU General Public License as published by
;  the Free Software Foundation, either version 3 of the License, or
;  (at your option) any later version.
;
;  This program is distributed in the hope that it will be useful,
;  but WITHOUT ANY WARRANTY; without even the implied warranty of
;  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
;  GNU General Public License for more details.
;
;  You should have received a copy of the GNU General Public License
;  along with this program.  If not, see <http://www.gnu.org/licenses/>.

!define VERSION "2.0.0"       ;version number: major.minor.release/build
!define VERSTATUS "beta4"     ;alpha, beta, rc, or nothing for final.
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
VIAddVersionKey "ProductVersion" "${VERSION}.0 ${VERSTATUS}"
VIAddVersionKey "LegalCopyright" "NoteFly"
VIAddVersionKey "FileVersion" "${VERSION}.0 ${VERSTATUS}"

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

Function .onInit

 System::Call 'kernel32::CreateMutexA(i 0, i 0, t "installnotefly") i .r1 ?e'
 Pop $R0
 
 StrCmp $R0 0 +3
   MessageBox MB_OK|MB_ICONEXCLAMATION "The installer is already running."
   Abort
   
  Call GetDotNETVersion
  Pop $0
  ${If} $0 == ".NET not found"
  
  MessageBox MB_OKCANCEL|MB_ICONSTOP ".NET framework 2.0 is not installed.$\nPlease get .NET framework 2.0.$\nPress OK to go to the website to download and install .NET framework 2.0.$\nService Pack2 or higher for .NET framework 2.0 is also required." IDOK downloadDotNet
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
    MessageBox MB_OKCANCEL|MB_ICONSTOP ".NET framework v2.0 or newer is required.$\nPlease get .NET framework 2.0 with Service Pack2 or higher.$\nPress OK to go to the website to download and install." IDOK downloadDotNet2
     Goto done2
    downloadDotNet2:
    ExecShell "open" "http://www.microsoft.com/downloads/details.aspx?familyid=0856eacb-4362-4b0d-8edd-aab15c5e04f5"
    done2:
    Abort
  ${EndIf}
  
  #check if administrator.
    userInfo::getAccountType
    pop $0
    strCmp $0 "Admin" +3
    MessageBox MB_OK "You need to have administrator rights to install NoteFly"
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
StrCmp $R0 "\system32" bad done
bad:
  MessageBox MB_OK|MB_ICONSTOP "The current installation path is not recommended. Please choice an other installation path."
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
  
  ; Kill running NoteFly if still running, using plugin: http://nsis.sourceforge.net/KillProcDLL_plug-in (optimized version, KillProcDLL.dll only)
  KillProcDLL::KillProc "${APPFILE}" 
  sleep 300
  
  ; Check installation directory 
  !insertmacro BadPathsCheck
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR 

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
  File "${LANGFILE}"
  File "${APPFILE}"
  WriteUninstaller "uninstall.exe"
  
SectionEnd

Section "Desktop Shortcut"
SetShellVarContext all


; Get the OS version
nsisos::osversion ; OS plugin from: http://nsis.sourceforge.net/NSIS-OS_plug-in
StrCpy $R0 $0
StrCpy $R1 $1
; Check our version
${If} $R0 == '5'
  CreateShortCut "$DESKTOP\NoteFly2.lnk" "$INSTDIR\${APPFILE}" "" "$INSTDIR\${APPFILE}" 1 ;small icon for win. xp.
${Else}
  CreateShortCut "$DESKTOP\NoteFly2.lnk" "$INSTDIR\${APPFILE}" "" "$INSTDIR\${APPFILE}" 0 ;large icon
${EndIf}
SectionEnd

Section "Start Menu Shortcuts"
  SetShellVarContext all
  ;startmenu shortcut should be for all users or currentuser Not administrator account.
  CreateDirectory "$SMPROGRAMS\NoteFly"
  CreateShortCut "$SMPROGRAMS\NoteFly\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  
${If} $R0 == '5'
  CreateShortCut "$SMPROGRAMS\NoteFly\NoteFly.lnk" "$INSTDIR\${APPFILE}" "" "$INSTDIR\${APPFILE}" 1 ;small icon for win. xp.
${Else}
  CreateShortCut "$SMPROGRAMS\NoteFly\NoteFly.lnk" "$INSTDIR\${APPFILE}" "" "$INSTDIR\${APPFILE}" 0 ;large icon
${EndIf}
SectionEnd

;--------------------------------

; Uninstaller
Section "Uninstall"  

  ; Check installation directory 
  !insertmacro BadPathsCheck
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly"
  DeleteRegKey HKLM SOFTWARE\NoteFly
  
  ; Remove files and uninstaller
  Delete "$INSTDIR\${APPFILE}"
  Delete "$INSTDIR\${LANGFILE}"
  Delete "$INSTDIR\uninstall.exe"
  
  ; Remove directory if empty
  RMDir "$INSTDIR"
  
  IfFileExists "$APPDATA\.NoteFly2\" skipappfoldernotfound
  MessageBox MB_YESNO|MB_ICONQUESTION "Do you want to remove your notes and settings?" IDNO keepsettingnotes
  SetShellVarContext current
  Delete "$APPDATA\.NoteFly2\settings.xml"
  Delete "$APPDATA\.NoteFly2\skins.xml"
  Delete "$APPDATA\.NoteFly2\debug.log"
  
  skipappfoldernotfound:
  keepsettingnotes:
  ; Remove desktop shortcut
  Delete "$DESKTOP\NoteFly.lnk"
    
  ; Remove startmenu shortcuts
  SetShellVarContext all
  Delete "$SMPROGRAMS\NoteFly\NoteFly.lnk"
  Delete "$SMPROGRAMS\NoteFly\Uninstall.lnk"
  
  ; Remove startmenu folder if empty
  RMDir "$SMPROGRAMS\NoteFly"

SectionEnd
