for i in `git tag --list --sort=v:refname`; do BUILDTAG=$i; done

for i in `git rev-parse HEAD`; do BUILDCOMMIT=$i; done
BUILDCOMMIT=${BUILDCOMMIT:0:8}

for i in `git rev-parse --abbrev-ref HEAD`; do BUILDBRANCH=$i; done

echo $BUILDTAG $BUILDCOMMIT $BUILDBRANCH

VERSION=${BUILDTAG:1}

echo {  > ../../data/version.json
echo \"tag\":\"$BUILDTAG\", >> ../../data/version.json
echo \"commit\":\"$BUILDCOMMIT\", >> ../../data/version.json
echo \"branch\":\"$BUILDBRANCH\" >> ../../data/version.json
echo }  >> ../../data/version.json

appdir=/tmp/LunaDenyCakesGame.app
mkdir $appdir
mkdir $appdir/Contents
mkdir $appdir/Contents/MacOS
mkdir $appdir/Contents/Frameworks
mkdir $appdir/Contents/Resources

cp Info.plist $appdir/Contents
cp Pkginfo $appdir/Contents

cp LunaDenyCakesGame.icns $appdir/Contents/Resources

cp ../../build-LunaDenyCakesGame/* $appdir/Contents/MacOS

chmod 777 $appdir/Contents/MacOS/LunaDenyCakesGame
cp -r ../../data/* $appdir/Contents/MacOS

cp -R /usr/local/lib/libcsfml*.dylib $appdir/Contents/MacOS
cp -R /usr/local/lib/libsfml*.dylib $appdir/Contents/MacOS

cp -R /Library/Frameworks/FLAC.framework $appdir/Contents/Frameworks
cp -R /Library/Frameworks/freetype.framework $appdir/Contents/Frameworks
cp -R /Library/Frameworks/ogg.framework $appdir/Contents/Frameworks
cp -R /Library/Frameworks/OpenAL.framework $appdir/Contents/Frameworks
cp -R /Library/Frameworks/vorbis.framework $appdir/Contents/Frameworks
cp -R /Library/Frameworks/vorbisenc.framework $appdir/Contents/Frameworks
cp -R /Library/Frameworks/vorbisfile.framework $appdir/Contents/Frameworks

cd /tmp 

echo "en" > $appdir/Contents/MacOS/deflang
zip -r9 LunaDenyCakesGame-EN-$VERSION-MacOS.app.zip LunaDenyCakesGame.app
hdiutil create -srcfolder $appdir -volname "LunaDenyCakesGame" -fs HFS+ -fsargs "-c c=64,a=16,e=16" -format UDZO -size 180000k -imagekey zlib-level=9 LunaDenyCakesGame-EN-$VERSION-MacOS.dmg

echo "ru" > $appdir/Contents/MacOS/deflang
zip -r9 LunaDenyCakesGame-RU-$VERSION-MacOS.app.zip LunaDenyCakesGame.app
hdiutil create -srcfolder $appdir -volname "LunaDenyCakesGame" -fs HFS+ -fsargs "-c c=64,a=16,e=16" -format UDZO -size 180000k -imagekey zlib-level=9 LunaDenyCakesGame-RU-$VERSION-MacOS.dmg
