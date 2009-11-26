@echo off
echo Making zip archive...

set VERSION="0.9.10"
set 7zpath="C:\Libs\mozilla-build\7zip\7z.exe"
set %SPN_SCR%="%cd%"

IF NOT EXIST C:\Libs\mozilla-build\7zip\7z.exe GOTO NO7ZDIR

REM Please make sure 7-zip is in your PATH or put the full path to 7z.exe
%7zpath% a -tzip "%SPN_SCR%\bin\Release\source archives\src_NoteDesk_v%VERSION%.zip" -r %SPN_SCR%\*.cs %SPN_SCR%\*.resx %SPN_SCR%\*.config %SPN_SCR%\*.sln %SPN_SCR%\*.csproj %SPN_SCR%\*.bat  %SPN_SCR%\*.png %SPN_SCR%\*.ico %SPN_SCR%\*.bat %SPN_SCR%\*.xsd %SPN_SCR%\*.manifest %SPN_SCR%\*.nsi %SPN_SCR%\*.settings %SPN_SCR%\*.zargo %SPN_SCR%\*.csproj.user %SPN_SCR%\*.txt

echo done.
pause

:NO7ZDIR
echo Cannot find 7-zip. Please check whethere 7-zip is installed in: %7zpath%