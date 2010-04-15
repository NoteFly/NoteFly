; Copyright (C) 2009-2010
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
!define VERSION "1.0.0" ;version number: major.minor.release
!define VERSTATUS "pre1" ;alpha, beta, rc, or nothing for final.
!define APPFILE "NoteFly.exe"

Name "NoteFly ${VERSION} ${VERSTATUS}" ; The name of the installer
SetCompressor lzma
AllowRootDirInstall false
CRCCheck on
InstProgressFlags smooth
ShowInstDetails show
SetDatablockOptimize on
Icon ".\..\..\Resources\installer_logo.ico"
BrandingText " "
CompletedText "Installation completed. Please close this installer now."

;put uac.dll in .\setuplib folder to use it. 
;!addplugindir ".\setuplib\"

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

!insertmacro VersionCompare
;!insertmacro GetParent
;!insertmacro GetPathFromString

Function .onInit
 System::Call 'kernel32::CreateMutexA(i 0, i 0, t "installnotefly") i .r1 ?e'
 Pop $R0
 
 StrCmp $R0 0 +3
   MessageBox MB_OK|MB_ICONEXCLAMATION "The installer is already running."
   Abort
   
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

;  UAC_Elevate:
;    UAC::RunElevated 
;    StrCmp 1223 $0 UAC_ElevationAborted ; UAC dialog aborted by user?
;    StrCmp 0 $0 0 UAC_Err ; Error?
;    StrCmp 1 $1 0 UAC_Success ;Are we the real deal or just the wrapper?
;    Quit
 
;  UAC_Err:
;    MessageBox mb_iconstop "Unable to elevate, error $0"
;    Abort
 
;  UAC_ElevationAborted:
;    ;elevation was aborted, run as normal?
;    MessageBox mb_iconstop "This installer requires admin access, aborting!"
;    Abort
 
;  UAC_Success:
;    StrCmp 1 $3 +4 ;Admin?
;    StrCmp 3 $1 0 UAC_ElevationAborted ;Try again?
;    MessageBox mb_iconstop "This installer requires admin access, try again"
;    goto UAC_Elevate 
 
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

Function ExecAppFile    
  RequestExecutionLevel user
  Exec '$INSTDIR\${APPFILE} /firstrun' 
  ;!insertmacro UAC_AsUser_ExecShell 'open' '$INSTDIR\${APPFILE}' '/firstrun' '$INSTDIR' ''  
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
  MessageBox MB_OK|MB_ICONSTOP "Install path invalid!"
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

; The stuff to install
Section "main executable (required)"	
 
  SectionIn RO        
  SetOverwrite on  
  RequestExecutionLevel Admin
    
  !insertmacro BadPathsCheck
  SetOutPath $INSTDIR  ;Set output path to the installation directory.   
  File "${APPFILE}"    ;Put file there  
  ;File "NoteFly.pdb"  ;debuggingsymbols. optional adds ~165kb
    
  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\NoteFly "Install_Dir" "$INSTDIR"   
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "DisplayName" "NoteFly"  
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "URLInfoAbout" "http://www.notefly.tk"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "DisplayVersion" "${VERSION}"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly" "NoRepair" 1
  WriteUninstaller "uninstall.exe"   
  
  Call ExecAppFile
SectionEnd

; Optional section (can be disabled by the user)
Section "Start Menu Shortcuts"  
  SetShellVarContext all
  ;startmenu shortcut should be for all users or currentuser. Not administrator account.
  ;Call CreateDesktopShortcuts  
  
  CreateDirectory "$SMPROGRAMS\NoteFly"
  CreateShortCut "$SMPROGRAMS\NoteFly\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortCut "$SMPROGRAMS\NoteFly\NoteFly.lnk" "$INSTDIR\${APPFILE}" "" "$INSTDIR\${APPFILE}" 0
  
SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"  

!insertmacro BadPathsCheck

  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\NoteFly"  
  DeleteRegKey HKLM SOFTWARE\NoteFly

  ; Remove files and uninstaller
  Delete $INSTDIR\NoteFly.exe
  Delete $INSTDIR\uninstall.exe
  ;Delete $INSTDIR\NoteFly.pdb ;enable if debugging symbols incl.
  
  MessageBox MB_YESNO "Do you want to remove your notes and settings too?" IDYES true IDNO false
  true:
     ;Deleting all files in application data folder of NoteFly.
     SetShellVarContext current
     Delete "$APPDATA\.NoteFly\*.*" ;FIXME: should not be administrator user appdata.
     RMDir "$APPDATA\.NoteFly"  ;FIXME: should not be administrator user appdata.
     DeleteRegKey HKCU "Software\Microsoft\Windows\CurrentVersion\Run\NoteFly"
     Goto next
     
  false:
     Goto next
  
  next:     
     RMDir "$INSTDIR"

SectionEnd
