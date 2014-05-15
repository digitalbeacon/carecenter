@echo off

cd MyGeneration\Projects

..\..\..\..\Base\CodeGen\MyGeneration\ZeusCmd.exe -p CareCenter.zprj

cd ..\..

echo Done

if not "%1"=="nopause" pause