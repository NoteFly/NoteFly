REM  NoteFly a note application.
REM  Copyright (C) 2010  Tom
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

echo strip reloc info.

REM change this path to where Inno StripReloc tool is installed.
"C:\Program Files\StripReloc_v1.13\StripReloc.exe" .\bin\Release\NoteFly.exe

pause
echo building setup...

REM change this path to where NSIS is installed.
"C:\Program Files\NSIS\makensis.exe" .\bin\Release\create_setup.nsi

echo done.

pause