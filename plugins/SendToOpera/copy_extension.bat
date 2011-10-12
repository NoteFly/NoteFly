@echo off
echo Notice: make sure SendToOpera.dll is compiled as "Any cpu".

copy %cd%\bin\Release\SendToOpera.dll %cd%\..\..\bin\Release\plugins\SendToOpera.dll

pause