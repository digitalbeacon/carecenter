set SERVER=-S localhost
set DB_NAME=CareCenterDemo

sqlcmd %SERVER% -Q "CREATE DATABASE %DB_NAME%"
C:\Windows\Microsoft.NET\Framework\v2.0.50727\aspnet_regsql.exe -U sa -A m %SERVER% -d %DB_NAME%
sqlcmd %SERVER% -d %DB_NAME% -U sa -i base\create-db-user.sql

pause
