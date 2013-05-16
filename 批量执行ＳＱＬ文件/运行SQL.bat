@echo off
chcp 936

cd.>result.txt

REM * 
SET counter=0 

for /F "tokens=*" %%a in ('dir /B /A:-D *.sql') do (
echo %%a >> result.txt
SQLCMD -S 192.168.1.51 -U sa -P sa123456. -f 65001 -i  %%a >> result.txt
echo.>> result.txt
set /A Counter += 1 
)

echo press any key to exit
pause >nul
exit