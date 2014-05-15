@echo off

cd /d %~dp0

net session >nul 2>&1
if not %errorLevel% == 0 (
	echo Administrative permissions are required to create symbolic links with mklink command.
	echo The current permissions are inadequate. Please run the script as administrator.
	goto done
)

mklink /D 3rdParty\Bin ..\Base\3rdParty\Bin
mklink /D Config\Base ..\Base\SiteBase\Config
mklink /D Database\Base ..\Base\SiteBase\Database
mklink /D Site\Resources\Base ..\..\Base\SiteBase\Site\Resources\Base

:done

pause