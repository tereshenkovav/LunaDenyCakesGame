set ZipName=LunaDenyCakesGame-0.5.0-Win64

rm -f %ZipName%.zip
7z a -mx9 %ZipName%.zip ..\..\build-LunaDenyCakesGame\*
7z a -mx9 %ZipName%.zip ..\..\data\*
