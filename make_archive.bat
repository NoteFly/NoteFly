@echo off
echo Making zip archive...

REM Path where sourcecode of SimplePlainNotes is stored. Change this.
set SPN_SCR=M:\Public\sourcecode\Projects_Csharp\simpleplainnote
set VERSION="0.9.9b"

REM Please make sure 7-zip is in your PATH or put the full path to 7z.exe
C:\Libs\mozilla-build\7zip\7z.exe a -tzip "%SPN_SCR%\bin\Release\source archives\src_SimplePlainNote_v%VERSION%.zip" -r %SPN_SCR%\*.cs %SPN_SCR%\*.resx %SPN_SCR%\*.config %SPN_SCR%\*.sln %SPN_SCR%\*.csproj %SPN_SCR%\*.bat  %SPN_SCR%\*.png %SPN_SCR%\*.ico %SPN_SCR%\*.bat %SPN_SCR%\*.xsd %SPN_SCR%\*.manifest %SPN_SCR%\*.nsi %SPN_SCR%\*.settings %SPN_SCR%\*.zargo %SPN_SCR%\*.csproj.user %SPN_SCR%\*.txt

echo done.
pause