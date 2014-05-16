@echo off

SET Configuration=Release
SET BuildTarget=_CopyWebApplication
SET NoPause=
SET Version=

for %%a in (.) do set Project=%%~na

SETLOCAL EnableDelayedExpansion
FOR /F "tokens=2" %%i IN (Src\CommonAssemblyInfo.cs) DO (
	SET token=%%i
	SET token=!token:")]=!
	IF "!token:~0,19!"=="AssemblyFileVersion" SET Version=-!token:~21!
)

:setargs
if "%1"=="" goto doneargs
if "%1"=="/release" SET Configuration=Release
if "%1"=="/debug" SET Configuration=Debug
if "%1"=="/clean" SET BuildTarget=Clean
if "%1"=="/compress" SET BuildTarget=CompressWebAssets
if "%1"=="/nopause" SET NoPause=nopause
if "%1"=="/version" (
	SET Version=-%2
	SHIFT
)
if "%1"=="/project" (
	SET Project=%2
	SHIFT
)
SHIFT
goto setargs

:doneargs

rmdir /s /q Publish

for %%i in (Site\*.csproj) do (
	%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319\msbuild %%i /t:%BuildTarget% /p:Configuration=%Configuration%;WebProjectOutputDir=%cd%\Publish\Site\;OutDir=%cd%\Publish\Site\Bin\
)

copy /y Site\Bin\*.dll Publish\Site\Bin
copy /y Site\Bin\*.pdb Publish\Site\Bin

mkdir Publish\Config\Config
xcopy /Y Site\web.config Publish\Config
xcopy Config\Base\*.config Publish\Config\Config
xcopy /Y Config\*.config Publish\Config\Config
xcopy /Y Config\Live\web.config Publish\Config
xcopy /Y Config\Live\*.config Publish\Config\Config
del Publish\Config\Config\web.config

if exist "%ProgramFiles%\7-Zip\7z.exe" set zipexe=%ProgramFiles%\7-Zip\7z.exe
if not defined zipexe if exist "%PROGRAMW6432%\7-Zip\7z.exe" set zipexe=%PROGRAMW6432%\7-Zip\7z.exe

if defined zipexe (
	cd Publish\Site
	"%zipexe%" a -r -xr^^!.git* ..\%Project%%Version%.zip *
	cd ..\..\Site
	"%zipexe%" a -r -xr^^!.git* ..\Publish\SiteBaseResources%Version%.zip Resources\Base\*
	cd ..\Publish\Config
	"%zipexe%" a -r -xr^^!.git* ..\Config%Version%.zip *
	cd ..\..
)

mkdir Publish\Site\Resources\Base
xcopy Site\Resources\Base Publish\Site\Resources\Base /E

xcopy Publish\Config\* Publish\Site /E
rmdir /s /q Publish\Config

cd Publish\Site
del /S /Q .git*
cd ..\..

if not defined zipexe (
	echo.
	echo Could not find 7-zip.
	echo.
)

echo.
echo Deployment files are located in the Publish folder.
echo.

if not "%NoPause%"=="nopause" pause