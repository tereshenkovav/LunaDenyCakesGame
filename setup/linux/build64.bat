del /q ..\..\build-LunaDenyCakesGame\*
dotnet publish ..\..\LunaDenyCakesGame\LunaDenyCakesGame\LunaDenyCakesGame.csproj -r linux-x64 -c Release --self-contained -o ..\..\build-LunaDenyCakesGame
create_zip64.bat
