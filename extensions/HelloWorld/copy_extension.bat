@echo off
echo Notice: make sure helloworld.dll is compiled as "Any cpu".

copy %cd%\bin\Release\helloworld.dll %cd%\..\..\bin\Release\helloworld.dll

REM copy symbols for debugging
copy %cd%\bin\Release\helloworld.pdb %cd%\..\..\bin\Release\helloworld.pdb
pause