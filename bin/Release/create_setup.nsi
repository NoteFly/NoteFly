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
Name "Simple Plain Note 0.9.0 alpha"

; The file to write
OutFile "SimplePlainNote_v0.9.0.exe"

; The default installation directory
InstallDir $PROGRAMFILES\simpleplainnote

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\simpleplainnote" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

; Usage:
;Call IsDotNETInstalled
;Pop $0
;StrCmp $0 1 found.NETFramework no.NETFramework

 Function IsDotNETInstalled
   Push $0
   Push $1

   StrCpy $0 1
   System::Call "mscoree::GetCORVersion(w, i ${NSIS_MAX_STRLEN}, *i) i .r1"
   StrCmp $1 0 +2
     StrCpy $0 0

   Pop $1
   Exch $0
 FunctionEnd
 
;--------------------------------

; Pages

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
  File "simpleplainnote.exe"
  
  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\NSIS_Example2 "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\simpleplainnote" "DisplayName" "simpleplainnote"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\simpleplainnote" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\simpleplainnote" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\simpleplainnote" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
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
  Delete $INSTDIR\simpleplainnote.exe
  Delete $INSTDIR\uninstall.exe

  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\simpleplainnote\*.*"

  ; Remove directories used
  RMDir "$SMPROGRAMS\simpleplainnote"
  ;warning deletes all notes.. 
  ;TODO: need to find out how to ASK for doing this.
  SetShellVarContext current
  RMDir /r "$APPDATA\.simpleplainnote" 
  RMDir "$INSTDIR"  

SectionEnd
