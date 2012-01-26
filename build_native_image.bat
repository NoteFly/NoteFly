@echo off
REM  NoteFly a note application.
REM  Copyright (C) 2012  Tom
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

echo  Start generating a native image of NoteFly.
echo.
set projectfolder = "M:\Public\sourcecode\Projects_Csharp\notefly"
IF NOT EXIST %projectfolder% GOTO PROJECTFOLDERUNSET
"%windir%\Microsoft.NET\Framework\v2.0.50727\ngen.exe" %projectfolder%\bin\release\NoteFly.exe /verbose
echo.
echo  Done generating a native image of NoteFly.
pause
exit

:PROJECTFOLDERUNSET
echo Project folder not found.
pause