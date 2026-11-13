@ECHO OFF

REM ----------------------------
REM TEST CONFIG
REM ----------------------------

REM APP INDICATES THE PATH THE THE EXE
SET APP=C:\BLUE\Sandbox\C#\RegularPolygons\bin\Debug\RegularPolygons.exe

REM D INDICATES THE PATH TO THE TEST FILES
SET D=C:\BLUE\Sandbox\C#\RegularPolygons\test\

REM TEST FILES
SET T0=T0.txt
SET T1=T1.txt
SET T2=T2.txt

REM ----------------------------
REM TEST CALLS
REM ----------------------------

CALL "%APP%" "%D%%T2%"
PAUSE
CALL "%APP%" "%D%%T0%"
PAUSE
CALL "%APP%" "%D%%T1%" 
PAUSE

