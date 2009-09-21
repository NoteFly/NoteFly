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

!define VERSION "0.9.7"
!define VERSTATUS "beta"

Name "Simple Plain Note ${VERSION} ${VERSTATUS}"

; The file to write
OutFile ".\SimplePlainNote_v${VERSION}.exe"

; The default installation directory
InstallDir $PROGRAMFILES\simpleplainnote

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\simpleplainnote" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

!include WordFunc.nsh
!insertmacro VersionCompare
 
!include LogicLib.nsh


Function .onInit
  Call GetDotNETVersion
  Pop $0
  ${If} $0 == "not found"
    MessageBox MB_OK|MB_ICONSTOP ".NET runtime library is not installed."
    Abort
  ${EndIf}
 
  StrCpy $0 $0 "" 1 # skip "v"
 
  ${VersionCompare} $0 "2.0" $1
  ${If} $1 == 2
    MessageBox MB_OK|MB_ICONSTOP ".NET runtime library v2.0 or newer is required. You have $0."
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
   LicenseData "..\license.txt"
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
  File "SimplePlainNote.exe"
  File "SimplePlainNote.exe.config"
  
  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\simpleplainnote "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\simpleplainnote" "DisplayName" "simpleplainnote"  
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\simpleplainnote" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\simpleplainnote" "URLInfoAbout" "http://code.google.com/p/simpleplainnote/"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\simpleplainnote" "DisplayVersion" "${VERSION}"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\simpleplainnote" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\simpleplainnote" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
  Exec '"$INSTDIR\simpleplainnote.exe" /firstrun'
  
SectionEnd

; Optional section (can be disabled by the user)
Section "Start Menu Shortcuts"

  CreateDirectory "$SMPROGRAMS\simpleplainnote"
  CreateShortCut "$SMPROGRAMS\simpleplainnote\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortCut "$SMPROGRAMS\simpleplainnote\simpleplainnote.lnk" "$INSTDIR\simpleplainnote.exe" "" "$INSTDIR\simpleplainnote.exe" 0
  
SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\simpleplainnote"
  DeleteRegKey HKLM SOFTWARE\simpleplainnote

  ; Remove files and uninstaller
  Delete $INSTDIR\SimplePlainNote.exe
  Delete $INSTDIR\SimplePlainNote.exe.config
  Delete $INSTDIR\uninstall.exe

  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\simpleplainnote\*.*"

  ; Remove directories used
  RMDir "$SMPROGRAMS\simpleplainnote"
  
  MessageBox MB_YESNO "Do you want to remove your notes and settings too?" IDYES true IDNO false
  true:
  ;deleting all files in appdata folder. 
  SetShellVarContext current
  Delete "$APPDATA\.simpleplainnote\*.*"
  RMDir "$APPDATA\.simpleplainnote" 
  Goto next
  
  false:
  Goto next
  
  next:     
  RMDir "$INSTDIR"  

SectionEnd