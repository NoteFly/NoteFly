@echo off
echo Notice: make sure helloworld.dll is compiled as "Any cpu".

copy %cd%\bin\Release\helloworld.dll %cd%\..\..\bin\Release\plugins\helloworld.dll

pause