dotnet publish ..\..\LunaDenyCakesGame\LunaDenyCakesGame\LunaDenyCakesGame.csproj -r win-x64 -c Release --self-contained -o ..\..\build-LunaDenyCakesGame
"C:\Program Files (x86)\NSIS\makensis.exe" LunaDenyCakesGame.nsi
create_zip64.bat
