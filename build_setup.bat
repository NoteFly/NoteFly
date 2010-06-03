REM Copyright (C) 2009-2010
REM This program is free software; you can redistribute it and/or modify it
REM Free Software Foundation; either version 2, or (at your option) any
REM later version.
REM
REM This program is distributed in the hope that it will be useful,
REM but WITHOUT ANY WARRANTY; without even the implied warranty of
REM MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
REM GNU General Public License for more details.
REM
REM You should have received a copy of the GNU General Public License
REM along with this program; if not, write to the Free Software
REM Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA

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