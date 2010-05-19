@echo off
echo This deletes settings.xml in the application data/appdata folder of NoteFly.
echo.
pause
@echo on

%SYSTEMDRIVE%
cd "%appdata%\.notefly"
del settings.xml

@echo off
echo done.
pause