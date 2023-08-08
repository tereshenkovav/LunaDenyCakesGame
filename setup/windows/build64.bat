del /q ..\..\build-LunaDenyCakesGame\*
dotnet publish ..\..\LunaDenyCakesGame\LunaDenyCakesGame\LunaDenyCakesGame.csproj -r win-x64 -c Release --self-contained -o ..\..\build-LunaDenyCakesGame

"C:\Program Files (x86)\NSIS\makensis.exe" /DGAMELANG=ru /DUPPERLANG=RU LunaDenyCakesGame.nsi
"C:\Program Files (x86)\NSIS\makensis.exe" /DGAMELANG=en /DUPPERLANG=EN LunaDenyCakesGame.nsi

call create_zip64.bat ru RU
call create_zip64.bat en EN
