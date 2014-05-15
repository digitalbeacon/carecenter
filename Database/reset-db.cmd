@echo off

set SERVER=-S localhost
set DB_NAME=CareCenterDemo
set USER=-U web
set PASS=-P Password1
set NO_PAUSE=
set INSERT_POSTAL_CODES=

:setargs
if "%1"=="" goto doneargs
if "%1"=="/nopause" SET NO_PAUSE=nopause
if "%1"=="/initpostalcodes" SET INSERT_POSTAL_CODES=true
SHIFT
goto setargs

:doneargs

sqlcmd %SERVER% -d %DB_NAME% %USER% %PASS% -i base\truncate-aspnet-membership.sql
sqlcmd %SERVER% -d %DB_NAME% %USER% %PASS% -i base\drop-all-tables.sql
sqlcmd %SERVER% -d %DB_NAME% %USER% %PASS% -i base\common-drop-all-tables.sql
sqlcmd %SERVER% -d %DB_NAME% %USER% %PASS% -i base\common-data-model.sql
sqlcmd %SERVER% -d %DB_NAME% %USER% %PASS% -i base\common-seed-data.sql
sqlcmd %SERVER% -d %DB_NAME% %USER% %PASS% -i data-model.sql
sqlcmd %SERVER% -d %DB_NAME% %USER% %PASS% -i seed-data.sql
sqlcmd %SERVER% -d %DB_NAME% %USER% %PASS% -i test-data.sql

if defined INSERT_POSTAL_CODES sqlcmd %SERVER% -d %DB_NAME% %USER% %PASS% -i base\postal-codes.sql

echo touching web.config to trigger app pool recycle
pushd .
cd ..\Site
copy /b web.config +,,
popd

echo Done

if not "%NO_PAUSE%"=="nopause" pause