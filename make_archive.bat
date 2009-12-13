@echo off
set path7z="C:\Libs\mozilla-build\7zip\7z.exe"
set VERSION=1.0.0

echo Making zip archive: src_NoteFly_v%VERSION%.zip
echo 7-zip(7z.exe) should be installed in: %path7z%

pause
IF NOT EXIST %path7z% GOTO NO7ZDIR

%path7z% a -tzip "%cd%\bin\Release\source archives\src_NoteFly_v%VERSION%.zip" -r %cd%\*.cs %cd%\*.resx %cd%\*.config %cd%\*.sln %cd%\*.csproj %cd%\*.eqconfig%cd%\*.png %cd%\*.ico %cd%\*.bat %cd%\*.xsd %cd%\*.manifest %cd%\*.nsi %cd%\*.settings %cd%\*.zargo %cd%\*.uml %cd%\*.csproj.user %cd%\*.pkgxml

echo done.
pause
exit

:NO7ZDIR
echo Cannot find 7-zip (7z.exe). Please change the path7z variable in this script.
pause
