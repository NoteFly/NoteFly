; This program is free software; you can redistribute it and/or modify it
; Free Software Foundation; either version 2, or (at your option) any
; later version.
; 
; This program is distributed in the hope that it will be useful,
; but WITHOUT ANY WARRANTY; without even the implied warranty of
; MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
; GNU General Public License for more details.
;
; You should have received a copy of the GNU General Public License
; along with this program; if not, write to the Free Software
; Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA
;
; The name of the installer

!define VERSION "1.0.0"
!define VERSTATUS "RC1"

Name "NoteFly ${VERSION} ${VERSTATUS}"
BrandingText " "
InstProgressFlags smooth
AllowRootDirInstall false
CRCCheck on
ShowInstDetails show
CompletedText "Installation completed. Please close this installer now."

; The file to write
OutFile ".\NoteFly_v${VERSION}.exe"

; The default installation directory
InstallDir $PROGRAMFILES\NoteFly

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\NoteFly" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

!include WordFunc.nsh
!insertmacro VersionCompare
 
!include LogicLib.nsh


Function .onInit
  Call GetDotNETVersion
  Pop $0
  ${If} $0 == "not found"
    MessageBox MB_OK|MB_ICONSTOP ".NET framework 2.0 is not installed.\r\nPlease get .NET framework 2.0 from:\r\n http://www.microsoft.com/downloads/details.aspx?familyid=0856eacb-4362-4b0d-8edd-aab15c5e04f5 "
    Abort
  ${EndIf}
 
  StrCpy $0 $0 "" 1 # skip "v"
 
  ${VersionCompare} $0 "2.0" $1
  ${If} $1 == 2
    MessageBox MB_OK|MB_ICONSTOP ".NET framework v2.0 or newer is required. You have $0.\r\nPlease update."
    Abort
  ${EndIf}
  
  System::Call 'kernel32::CreateMutexA(i 0, i 0, t "myMutex") i .r1 ?e'
  Pop $R0
 
  StrCmp $R0 0 +3
   MessageBox MB_OK|MB_ICONEXCLAMATION "The installer is already running."
   Abort

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

; The stuff to install
Section "main executable (required)"	
 
  SectionIn RO     
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File "NoteFly.exe"
  ;File "NoteFly.exe.config"
  
  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\NoteFly "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "DisplayName" "NoteFly"  
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "URLInfoAbout" "http://"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "DisplayVersion" "${VERSION}"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
  Exec '"$INSTDIR\NoteFly.exe" /firstrun'
  
SectionEnd

; Optional section (can be disabled by the user)
Section "Start Menu Shortcuts"
;startmenu shortcut should be for currentuser!
  SetShellVarContext current
  CreateDirectory "$SMPROGRAMS\NoteFly"
  CreateShortCut "$SMPROGRAMS\NoteFly\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortCut "$SMPROGRAMS\NoteFly\NoteFly.lnk" "$INSTDIR\NoteFly.exe" "" "$INSTDIR\NoteFly.exe" 0
  
SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly"
  DeleteRegKey HKLM SOFTWARE\NoteFly

  ; Remove files and uninstaller
  Delete $INSTDIR\NoteFly.exe
  ;Delete $INSTDIR\NoteFly.exe.config
  Delete $INSTDIR\uninstall.exe

  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\NoteFly\*.*"

  ; Remove directories used
  RMDir "$SMPROGRAMS\NoteFly"
  
  MessageBox MB_YESNO "Do you want to remove your notes and settings too?" IDYES true IDNO false
  true:
  ;deleting all files in appdata folder. 
  SetShellVarContext current
  Delete "$APPDATA\.NoteFly\*.*"
  RMDir "$APPDATA\.NoteFly" 
  Goto next
  
  false:
  Goto next
  
  next:     
  RMDir "$INSTDIR"  

SectionEnd
