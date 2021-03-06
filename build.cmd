@echo off

SET _Configuration=Release
SET _BuildTarget=Rebuild
SET _NoPause=
SET _BuildSiteBase=true
SET _SymLinkMissing=

:setargs
if "%1"=="" goto doneargs
if "%1"=="/release" SET _Configuration=Release
if "%1"=="/debug" SET _Configuration=Debug
if "%1"=="/clean" SET _BuildTarget=Clean
if "%1"=="/nopause" SET _NoPause=nopause
if "%1"=="/sb" SET _BuildSiteBase=true
if "%1"=="/skipbase" SET _BuildSiteBase=
SHIFT
goto setargs

:doneargs

if not exist 3rdParty\Bin SET _SymLinkMissing=true
if not exist Config\Base SET _SymLinkMissing=true
if not exist Database\Base SET _SymLinkMissing=true
if not exist Site\Resources\Base SET _SymLinkMissing=true

if defined _SymLinkMissing echo Symbolic links have not been created. Please run the setup-base.cmd script as administrator before building.
if defined _SymLinkMissing goto exitscript

echo.
echo BuildTarget   : %_BuildTarget%
echo Configuration : %_Configuration%
echo.

if not defined _BuildSiteBase if not exist Base\Bin\*.dll SET _BuildSiteBase=true

if defined _BuildSiteBase (
	echo =========================
	echo Building SiteBase...
	echo =========================
	echo.
	
	cd Base
	if "%_Configuration%"=="Release" call build-sitebase.cmd /nopause
	if "%_Configuration%"=="Debug" call build-sitebase-debug.cmd /nopause
	
	echo =========================
	echo Done Building SiteBase...
	echo =========================
	echo.
	cd ..
)

for %%i in (*.sln) do (
	%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319\msbuild %%i /m /t:%_BuildTarget% /p:Configuration=%_Configuration%
)

:exitscript

if not "%_NoPause%"=="nopause" pause