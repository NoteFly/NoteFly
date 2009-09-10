@echo off

echo building setup...

REM change this path to where NSIS is installed.
"C:\Program Files\NSIS\makensis.exe" bin\Release\create_setup.nsi

echo done.

pause