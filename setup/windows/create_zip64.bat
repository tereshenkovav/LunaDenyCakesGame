if NOT "%~1" == "" goto mainproc

echo "Argument - lang code" 
exit

:mainproc

set ZipName=LunaDenyCakesGame-%2-%VERSION%-Win64

echo %1 > ..\..\data\deflang

rm -f %ZipName%.zip
7z a -mx9 %ZipName%.zip ..\..\build-LunaDenyCakesGame\*
7z a -mx9 %ZipName%.zip ..\..\data\*
