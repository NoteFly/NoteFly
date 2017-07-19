;  NoteFly a note application.
;  Copyright (C) 2010-2017  Tom
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
; NSIS setup requirements:
; - nsProcess plugin (http://nsis.sourceforge.net/NsProcess_plugin)
; - NSIS-OS plug-in (http://nsis.sourceforge.net/NSIS-OS_plug-in)
;
!define PROJNAME   "NoteFly"
!define VERSION    "3.0.8"        ; version number: major.minor.release
!define VERSTATUS  ""             ; alpha, beta, rc, or nothing for final.
!define APPFILE    "NoteFly.exe"  ; main executable.
!define APPIPLUGIN "IPlugin.dll"  ; plugin interface for plugin support.
!define LANGFILE   "langs.xml"    ; lexicon file, for highlighting support.

Name "${PROJNAME} ${VERSION} ${VERSTATUS}" ; The name of the installer
;SetCompressor lzma  ; Compression with lzma causes false positives with some anti-virus software.
SetCompressor /SOLID zlib
AllowRootDirInstall false
CRCCheck on
InstProgressFlags smooth
ShowInstDetails show
SetDatablockOptimize on
SetOverwrite on
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
; Launch the installer as adminsitrator.
RequestExecutionLevel admin

!include LogicLib.nsh
!include WordFunc.nsh
!include FileFunc.nsh
!include nsProcess.nsh

Function .onInit
  System::Call 'kernel32::CreateMutexA(i 0, i 0, t "installnotefly") i .r1 ?e'
  Pop $R0
  StrCmp $R0 0 +3
    MessageBox MB_OK|MB_ICONEXCLAMATION "The installer is already running."
    Abort
  Call GetDotNETVersion
  Pop $0
  ${If} $0 == ".NET not found"
    MessageBox MB_OKCANCEL|MB_ICONSTOP ".NET framework 2.0 is not installed.$\nPlease get .NET framework 2.0.$\nPress OK to go to the website to download and install .NET framework 2.0.$\nService Pack2 or higher for .NET framework 2.0 is also required." IDOK downloadDotNet2
    Abort
    Goto done
    downloadDotNet2:
      #ExecShell "open" "http://www.microsoft.com/downloads/details.aspx?familyid=0856eacb-4362-4b0d-8edd-aab15c5e04f5" # 404 Error
      ExecShell "open" "https://www.microsoft.com/en-us/search/result.aspx?q=Microsoft+.NET+Framework+2.0"
    done:
      Abort
  ${EndIf}
  StrCpy $0 $0 "" 1 # skip "v"
  ${VersionCompare} $0 "2.0" $1
  ${If} $1 == 2
    MessageBox MB_OKCANCEL|MB_ICONSTOP ".NET framework v2.0 or newer is required.$\nPlease get .NET framework 2.0 with Service Pack2 or higher.$\nPress OK to go to the website to download and install." IDOK downloadDotNet2SP2
    Goto DotNet2SP2Installed
    downloadDotNet2SP2:
      #ExecShell "open" "http://www.microsoft.com/downloads/details.aspx?familyid=0856eacb-4362-4b0d-8edd-aab15c5e04f5" # 404 Error
      ExecShell "open" "https://www.microsoft.com/en-us/download/details.aspx?id=1639"
    DotNet2SP2Installed:
      Abort
  ${EndIf}
  # check if administrator.
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

;Function CompileAsm
;  sleep 200
; "%windir%\Microsoft.NET\Framework\v2.0.50727\ngen.exe" "C:\Program Files (x86)\NoteFly\NoteFly.exe" /nologo /verbose
;  Exec '"$%windir%\Microsoft.NET\Framework\v2.0.50727\ngen.exe" "$INSTDIR\${APPFILE}" /nologo /verbose'
;  sleep 200
;FunctionEnd

!macro StopRunningNoteFly
  ${nsProcess::KillProcess} "${APPFILE}" $R0
  sleep 300
  ${if} $R0 == '0'
    detailprint "Warning, terminated running ${APPFILE} process."
    sleep 500
  ${elseif} $R0 == '603'
    ;detailprint "Good, no instance of application running."
  ${else}
    detailprint "Error, terminated process failed. (return code: $R0)"
  ${endif}
  
  ${nsProcess::Unload}
!macroend

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
  !insertmacro StopRunningNoteFly
  ; Check installation directory 
  !insertmacro BadPathsCheck
  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\NoteFly "Install_Dir" "$INSTDIR"
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "DisplayName" "NoteFly"  
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "URLInfoAbout" "https://www.notefly.org"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "HelpLink" "https://www.notefly.org/development"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "URLUpdateInfo" "https://www.notefly.org/downloads"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "DisplayVersion" "${VERSION}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "DisplayIcon" '"$INSTDIR\NoteFly.exe",0'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "NoRepair" 1
  ; Set output path to the installation directory.
  SetOutPath "$INSTDIR"
  ; write the files main executable and uninstaller.
  File "${LANGFILE}"
  File "${APPFILE}"
  File "${APPIPLUGIN}"
  ; skin textures
  File "nyancat.jpg"    ; 4,53 KiB
  File "grass.jpg"      ; 9,17 KiB
  File "colordrops.jpg" ; 10,0 KiB
  ; Gettext library
  File "Gettext.Cs.dll" ; 15,5 KiB
  ; translations
  SetOutPath "$INSTDIR\translations\en\"
  File ".\translations\en\Strings.po"
  SetOutPath "$INSTDIR\translations\nl\"
  File ".\translations\nl\Strings.po"
  SetOutPath "$INSTDIR\translations\ko\"
  File ".\translations\ko\Strings.po"
  SetOutPath "$INSTDIR\translations\sv\"
  File ".\translations\sv\Strings.po"
  SetOutPath "$INSTDIR\translations\el\"
  File ".\translations\el\Strings.po"
  File ".\translations\el\Strings.po"
  ; Windows 10/8.1/8 tile startmenu customizations.
  SetOutPath "$INSTDIR\VisualElements\"
  File ".\VisualElements\VisualElements_70.png"  ; 2,78 KiB
  File ".\VisualElements\VisualElements_150.png" ; 5,46 KiB
  SetOutPath "$INSTDIR"
  File "${PROJNAME}.VisualElementsManifest.xml"
  ; Write the NSIS uninstaller
  WriteUninstaller "uninstall.exe"
SectionEnd

Section "Desktop Shortcut (all users)"
  SetShellVarContext all
  SetOutPath "$INSTDIR"
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

; using ngen to compile .NET CLR to native code:
;Section "Compile main assembly"
;   DetailPrint "start compiling NoteFly.exe, ngen=$%windir%\Microsoft.NET\Framework\v2.0.50727\ngen.exe"
;   ${Locate} "$%windir%\Microsoft.NET\Framework\v2.0.50727\" "/L=F /G=0 /M=ngen.exe" "CompileAsm"
;   IfErrors 0 +3
;   DetailPrint "Cannot compile main assembly."
;   goto end
;   DetailPrint "Compiling main assembly succeeded."
;   end:
;SectionEnd

;--------------------------------

; Uninstaller
Section "Uninstall"
  !insertmacro StopRunningNoteFly
  ; Check installation directory 
  !insertmacro BadPathsCheck
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly"
  DeleteRegKey HKLM SOFTWARE\NoteFly
  ; Remove files and uninstaller
  Delete "$INSTDIR\${LANGFILE}"
  Delete "$INSTDIR\${APPIPLUGIN}"
  Delete "$INSTDIR\${APPFILE}"
  Delete "$INSTDIR\Gettext.Cs.dll"
  ; skin textures
  Delete "$INSTDIR\nyancat.jpg"
  Delete "$INSTDIR\grass.jpg"
  Delete "$INSTDIR\colordrops.jpg"
  ; translations
  Delete "$INSTDIR\translations\ko\Strings.po"
  RMDir "$INSTDIR\translations\ko\"
  Delete "$INSTDIR\translations\nl\Strings.po"
  RMDir "$INSTDIR\translations\nl\"
  Delete "$INSTDIR\translations\sv\Strings.po"
  RMDir "$INSTDIR\translations\sv\"
  Delete "$INSTDIR\translations\el\Strings.po"
  RMDir "$INSTDIR\translations\el\"
  Delete "$INSTDIR\translations\en\Strings.po"
  RMDir "$INSTDIR\translations\en\"
  RMDir "$INSTDIR\translations\"
  ; Windows 10/8.1/8 tile startmenu customizations.
  Delete "$INSTDIR\VisualElements\VisualElements_70.png"
  Delete "$INSTDIR\VisualElements\VisualElements_150.png"
  RMDir "$INSTDIR\VisualElements\"
  Delete "$INSTDIR\${PROJNAME}.VisualElementsManifest.xml"
  ; remove uninstaller
  Delete "$INSTDIR\uninstall.exe"
  ; Remove NoteFly install directory, if empty
  RMDir "$INSTDIR"
  IfFileExists "$APPDATA\NoteFly" removeadminappdata postremoveadminappdata
  removeadminappdata:
    ; This is only going to work for the administrator appdata.
    MessageBox MB_YESNO|MB_ICONQUESTION "Do you want to remove your notes settings and plugins from administrator account stored at $APPDATA\NoteFly\ ?" IDNO keepsettingnotes
    Delete "$APPDATA\NoteFly\notes\*.nfn"
    Delete "$APPDATA\NoteFly\settings.xml"
    Delete "$APPDATA\NoteFly\skins.xml"
    Delete "$APPDATA\NoteFly\debug.log"
    ; Remove directory if empty
    RMDir "$APPDATA\NoteFly"
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
