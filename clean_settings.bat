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
echo This deletes settings.xml in the application data/appdata folder of NoteFly 2.x.
echo.
pause
@echo on

%SYSTEMDRIVE%
cd "%appdata%\.notefly2"
del settings.xml
del skins.xml

@echo off
echo done.
pause