#!/bin/bash

for i in `git tag --list --sort=committerdate`; do BUILDTAG=$i; done

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

appdir=/tmp/LunaDenyCakesGame.AppDir

rm -rf $appdir

mkdir $appdir
cp appruns/AppRun-x86_64 $appdir/AppRun
chmod 777 $appdir/AppRun
cp ../../graphics/main.png $appdir/LunaDenyCakesGame.png
pushd $appdir
ln -s LunaDenyCakesGame.png .DirIcon
popd

cp LunaDenyCakesGame.desktop $appdir
mkdir $appdir/usr
mkdir $appdir/usr/bin
mkdir $appdir/usr/lib

cp ../../build-LunaDenyCakesGame/* $appdir/usr/bin

cp /usr/lib64/libsfml* $appdir/usr/bin
cp /usr/local/lib/libcsfml* $appdir/usr/bin
cp /usr/lib64/libvorbis* $appdir/usr/lib
cp /usr/lib64/libasound.so* $appdir/usr/lib
cp /usr/lib64/libfreetype.so* $appdir/usr/lib
cp /usr/lib64/libjpeg.so* $appdir/usr/lib
cp /usr/lib64/libopenal.so* $appdir/usr/lib
cp /usr/lib64/libatomic.so* $appdir/usr/lib
cp /usr/lib64/libGLU.so* $appdir/usr/lib
cp /usr/lib64/libharfbuzz.so* $appdir/usr/lib
cp /usr/lib64/libpng16.so* $appdir/usr/lib
cp /usr/lib64/libogg.so* $appdir/usr/lib
cp /usr/lib64/libFLAC.so* $appdir/usr/lib

chmod 777 $appdir/usr/bin/LunaDenyCakesGame
cp -r ../../data/* $appdir/usr/bin

export ARCH=x86_64

echo "en" > $appdir/usr/bin/deflang
appimagetool-x86_64.AppImage $appdir /tmp/LunaDenyCakesGame-EN-$VERSION-x86_64.AppImage

echo "ru" > $appdir/usr/bin/deflang
appimagetool-x86_64.AppImage $appdir /tmp/LunaDenyCakesGame-RU-$VERSION-x86_64.AppImage
