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
;
; Special notice: the used KillProcDLL sourcecode/libery is NOT covered under GPL.
; KillProcDLL (by DITMan) sourcecode license is as follows:
;  The original source file for the KILL_PROC_BY_NAME function is provided, the file is: exam28.cpp, and it MUST BE in this zip file.
;  You can redistribute this archive if you do it without changing anything on it, otherwise you're NOT allowed to do so.
;  You may use this source code in any of your projects, while you keep all the files intact, otherwise you CAN NOT use this code.
;

!define PROJNAME   "NoteFly"
!define VERSION    "2.5.0"          ; version number: major.minor.release
!define VERSTATUS  ""            ; alpha, beta, rc, or nothing for final.
!define APPFILE    "NoteFly.exe"    ; main executable.
!define APPIPLUGIN "IPlugin.dll"    ; plugin interface for plugin support.
!define LANGFILE   "langs.xml"      ; lexicon file, for highlighting support.
!define DEMOPLUGIN "helloworld.dll" ; A demo plugin.

Name "${PROJNAME} ${VERSION} ${VERSTATUS}" ; The name of the installer
SetCompressor lzma
AllowRootDirInstall false
CRCCheck on
InstProgressFlags smooth
ShowInstDetails show
SetDatablockOptimize on
Icon ".\..\..\Resources\icon_small.ico"
BrandingText " "
VIProductVersion "${VERSION}.0"
VIAddVersionKey "ProductName" "${PROJNAME}"
VIAddVersionKey "FileDescription" "note taking application"
VIAddVersionKey "ProductVersion" "${VERSION}.0 ${VERSTATUS}"
VIAddVersionKey "LegalCopyright" "${PROJNAME}"
VIAddVersionKey "FileVersion" "${VERSION}.0 ${VERSTATUS}"

; The file to write
OutFile ".\${PROJNAME}_v${VERSION}${VERSTATUS}.exe"

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
  MessageBox MB_OK|MB_ICONSTOP "The current installation path is not recommended.$\nPlease choice an other installation path."
  Abort
done:
!macroend

;--------------------------------

; Pages
PageEx license
   LicenseText "License agreement"
   LicenseData "license.txt"
PageExEnd
; Add this page on a alpha release as warning
;PageEx license
;   LicenseText "Warning"
;   LicenseData "warning_alpha.txt"
;PageExEnd
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
  
  KillProcDLL::KillProc "${APPFILE}"
  ; Simply wait 400ms for a running NoteFly process to close itself then check again if it's running.
  sleep 400
   
  ; Check installation directory 
  !insertmacro BadPathsCheck

  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\NoteFly "Install_Dir" "$INSTDIR"
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "DisplayName" "NoteFly"  
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "URLInfoAbout" "http://www.notefly.org"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "DisplayVersion" "${VERSION}"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "NoRepair" 1
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; write the files main executable and uninstaller.
  File "${LANGFILE}"
  File "${APPFILE}"
  File "${APPIPLUGIN}"
  
  ; skin textures
  File "nyancat.jpg"    ; 4,53 KB
  File "grass.jpg"      ; 9,17 KB
  File "colordrops.jpg" ; 10,0 KB
  
  WriteUninstaller "uninstall.exe"
  
SectionEnd

Section "helloworld demo plugin"
  SetOutPath "$INSTDIR\plugins"
  File ".\plugins\${DEMOPLUGIN}"
SectionEnd

Section "Desktop Shortcut (all users)"
SetShellVarContext all

; Get the OS version, using OS plugin from: http://nsis.sourceforge.net/NSIS-OS_plug-in
nsisos::osversion 
StrCpy $R0 $0
StrCpy $R1 $1
; Check our version
${If} $R0 == '5'
  CreateShortCut "$DESKTOP\${PROJNAME}.lnk" "$INSTDIR\${APPFILE}" "" "$INSTDIR\${APPFILE}" 1 ; small icon for win. xp.
${Else}
  CreateShortCut "$DESKTOP\${PROJNAME}.lnk" "$INSTDIR\${APPFILE}" "" "$INSTDIR\${APPFILE}" 0 ; large icon for win. vista/7
${EndIf}
SectionEnd

Section "Start Menu Shortcuts (all users)"
  SetShellVarContext all
  CreateDirectory "$SMPROGRAMS\${PROJNAME}"
  CreateShortCut "$SMPROGRAMS\${PROJNAME}\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  
${If} $R0 == '5'
  CreateShortCut "$SMPROGRAMS\${PROJNAME}\${PROJNAME}.lnk" "$INSTDIR\${APPFILE}" "" "$INSTDIR\${APPFILE}" 1 ; small icon for win. xp.
${Else}
  CreateShortCut "$SMPROGRAMS\${PROJNAME}\${PROJNAME}.lnk" "$INSTDIR\${APPFILE}" "" "$INSTDIR\${APPFILE}" 0 ; large icon
${EndIf}
SectionEnd

;--------------------------------

; Uninstaller
Section "Uninstall"  
   KillProcDLL::KillProc "${APPFILE}"
   ; Simply wait 400ms for a running NoteFly process to close itself.
   sleep 400
   
  ; Check installation directory 
  !insertmacro BadPathsCheck

  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly"
  DeleteRegKey HKLM SOFTWARE\NoteFly

  ; Remove files and uninstaller
  Delete "$INSTDIR\${LANGFILE}"
  Delete "$INSTDIR\plugins\${DEMOPLUGIN}"
  Delete "$INSTDIR\${APPIPLUGIN}"
  Delete "$INSTDIR\${APPFILE}"

  ; skin textures
  Delete "$INSTDIR\nyancat.jpg"
  Delete "$INSTDIR\grass.jpg"
  Delete "$INSTDIR\colordrops.jpg"
  Delete "$INSTDIR\blackhorse.jpg" ; remove ifexist, in NoteFly 2.5.0 beta1 only.
  
  ; remove uninstaller
  Delete "$INSTDIR\uninstall.exe"
    
  ; Remove plugin directory if empty
  RMDir "$INSTDIR\plugins\"
  ; Remove directory if empty
  RMDir "$INSTDIR"

  IfFileExists "$APPDATA\.NoteFly2" removeadminappdata postremoveadminappdata
  
  removeadminappdata:
  ; This is only going to work for the administrator appdata.
  MessageBox MB_YESNO|MB_ICONQUESTION "Do you want to remove your notes and settings from administrator account stored at $APPDATA\.NoteFly2\ ?" IDNO keepsettingnotes
    Delete "$APPDATA\.NoteFly2\settings.xml"
    Delete "$APPDATA\.NoteFly2\skins.xml"
    Delete "$APPDATA\.NoteFly2\debug.log"
    Delete "$APPDATA\.NoteFly2\*.nfn"

    ; Remove directory if empty
    RMDir "$APPDATA\.NoteFly2"
  postremoveadminappdata:
  keepsettingnotes:
  SetShellVarContext all
  ; Remove desktop shortcut
  Delete "$DESKTOP\${PROJNAME}.lnk"    
  ; Remove startmenu shortcuts
  Delete "$SMPROGRAMS\${PROJNAME}\${PROJNAME}.lnk"
  Delete "$SMPROGRAMS\${PROJNAME}\Uninstall.lnk"
  ; Remove startmenu folder if empty
  RMDir "$SMPROGRAMS\${PROJNAME}"
SectionEnd
