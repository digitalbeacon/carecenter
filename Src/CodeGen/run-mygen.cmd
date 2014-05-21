@echo off

cd MyGeneration\Projects

for %%i in (*.zprj) do (
	..\..\..\..\Base\CodeGen\MyGeneration\ZeusCmd.exe -p %%i
)

cd ..\..

echo Done

if not "%1"=="nopause" pause