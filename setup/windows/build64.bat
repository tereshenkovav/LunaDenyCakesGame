@echo off

for /F %%i in ('git tag --list --sort=committerdate') do set BUILDTAG=%%i
for /F %%i in ('git rev-parse HEAD') do set BUILDCOMMIT=%%i
set BUILDCOMMIT=%BUILDCOMMIT:~0,8%
for /F %%i in ('git branch --show-current') do set BUILDBRANCH=%%i

echo %BUILDTAG% %BUILDCOMMIT% %BUILDBRANCH%

echo {  > ..\..\data\version.json
echo "tag":"%BUILDTAG%", >> ..\..\data\version.json
echo "commit":"%BUILDCOMMIT%", >> ..\..\data\version.json
echo "branch":"%BUILDBRANCH%" >> ..\..\data\version.json
echo }  >> ..\..\data\version.json

SET VERSION=%BUILDTAG:~1%

del /q ..\..\build-LunaDenyCakesGame\*
dotnet publish ..\..\LunaDenyCakesGame\LunaDenyCakesGame\LunaDenyCakesGame.csproj -r win-x64 -c Release --self-contained -o ..\..\build-LunaDenyCakesGame

"C:\Program Files (x86)\NSIS\makensis.exe" /DVERSION=%VERSION% /DGAMELANG=ru /DUPPERLANG=RU LunaDenyCakesGame.nsi
"C:\Program Files (x86)\NSIS\makensis.exe" /DVERSION=%VERSION% /DGAMELANG=en /DUPPERLANG=EN LunaDenyCakesGame.nsi

del ..\..\data\deflang

SmartZipBuilder.exe script.szb /LANGL=ru /LANGH=RU
SmartZipBuilder.exe script.szb /LANGL=en /LANGH=EN
