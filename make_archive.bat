REM  NoteFly a note application.
REM  Copyright (C) 2010-2011  Tom
REM
REM  This program is free software: you can redistribute it and/or modify
REM  it under the terms of the GNU General Public License as published by
REM  the Free Software Foundation, either version 3 of the License, or
REM  (at your option) any later version.
REM
REM  This program is distributed in the hope that it will be useful,
REM  but WITHOUT ANY WARRANTY; without even the implied warranty of
REM  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
REM  GNU General Public License for more details.
REM
REM  You should have received a copy of the GNU General Public License
REM  along with this program.  If not, see <http://www.gnu.org/licenses/>.

@echo off
set path7z="C:\Program Files\7-Zip\7z.exe"
set VERSION="2.0.0"
REM versionstatus: alpha, beta, rc1, rc2 or nothing for final.
set VERSIONSTATUS="beta2" 

echo Making zip archive: src_NoteFly_v%VERSION%%VERSIONSTATUS%.zip
echo 7-zip(7z.exe) should be installed in: %path7z%

pause
IF NOT EXIST %path7z% GOTO NO7ZDIR

%path7z% a -tzip "%cd%\bin\Release\source archives\src_NoteFly_v%VERSION%%VERSIONSTATUS%.zip" -r %cd%\*.cs %cd%\*.resx %cd%\*.config %cd%\*.sh %cd%\deb_control_script %cd%\license.txt %cd%\*.desktop %cd%\*.sln %cd%\*.csproj %cd%\*.eqconfig%cd%\*.png %cd%\*.ico %cd%\*.bat %cd%\*.xsd %cd%\*.manifest %cd%\*.nsi %cd%\*.settings %cd%\*.zargo %cd%\*.uml %cd%\*.csproj.user %cd%\*.pkgxml %cd%\*.in

echo done.
pause
exit

:NO7ZDIR
echo Cannot find 7-zip (7z.exe). Please change the path7z variable in this script.
pause
