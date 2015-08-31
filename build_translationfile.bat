@echo off
REM  NoteFly a note application.
REM  Copyright (C) 2012-2013  Tom
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

echo Extracting translations of sourcecode.
REM xgettext needs to be in PATH environment variables.
.\gettext-cs-utils\Gettext.CsUtils\Bin\Gnu.Gettext.Win32\xgettext.exe --from-code=UTF-8 *.cs -k --add-location --keyword=T --language=C# -o .\bin\Strings.pot --copyright-holder="NoteFly"

echo finished
echo.
pause