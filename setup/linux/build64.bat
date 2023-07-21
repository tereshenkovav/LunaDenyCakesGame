del /q ..\..\build-LunaDenyCakesGame\*
dotnet publish ..\..\LunaDenyCakesGame\LunaDenyCakesGame\LunaDenyCakesGame.csproj -r fedora-x64 -c Release --self-contained -o ..\..\build-LunaDenyCakesGame
create_zip64.bat
