#!/bin/bash

rm -f ../../build-LunaDenyCakesGame/*
dotnet publish ../../LunaDenyCakesGame/LunaDenyCakesGame/LunaDenyCakesGame.csproj -r osx-x64 -c Release --self-contained -o ../../build-LunaDenyCakesGame
