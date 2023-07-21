Unicode True
RequestExecutionLevel admin
SetCompressor /SOLID zlib
AutoCloseWindow true
Icon ..\..\graphics\main.ico
XPStyle on

!include StrData.nsi

!include "FileFunc.nsh"
!insertmacro GetTime

!define TEMP1 $R0 

ReserveFile /plugin InstallOptions.dll
ReserveFile "runapp.ini"

OutFile "LunaDenyCakesGame-setup-0.5.0-Win64.exe"

var is_update

Page directory
Page components
Page instfiles
Page custom SetRunApp ValidateRunApp "$(AfterParams)" 

UninstPage uninstConfirm
UninstPage instfiles

Name $(GameGameName)

Function .onInit
  InitPluginsDir
  File /oname=$PLUGINSDIR\runapp.ini "runapp.ini"

  StrCpy $INSTDIR $PROGRAMFILES64\LunaDenyCakesGame

  IfFileExists $INSTDIR\LunaDenyCakesGame.exe +3
  StrCpy $is_update "0"
  Goto +2
  StrCpy $is_update "1"
  
FunctionEnd

Function .onInstSuccess
  StrCmp $is_update "1" SkipAll

  ReadINIStr ${TEMP1} "$PLUGINSDIR\runapp.ini" "Field 1" "State"
  StrCmp ${TEMP1} "0" SkipDesktop

  SetOutPath $INSTDIR
  CreateShortCut "$DESKTOP\$(GameName).lnk" "$INSTDIR\LunaDenyCakesGame.exe" "" 

SkipDesktop:

  ReadINIStr ${TEMP1} "$PLUGINSDIR\runapp.ini" "Field 2" "State"
  StrCmp ${TEMP1} "0" SkipRun

  Exec $INSTDIR\LunaDenyCakesGame.exe

  SkipRun:
  SkipAll:

FunctionEnd

Function un.onUninstSuccess
  MessageBox MB_OK "$(MsgUninstOK)"
FunctionEnd

Function un.onUninstFailed
  MessageBox MB_OK "$(MsgUninstError)"
FunctionEnd

Function .onInstFailed
  MessageBox MB_OK "$(MsgInstError)"
FunctionEnd

Section "$(GameGameName)"
  SectionIn RO

  StrCmp $is_update "0" SkipSleep
  Sleep 3000
  SkipSleep:

  SetOutPath $INSTDIR
  File /r ..\..\build-LunaDenyCakesGame\*
  File ..\..\graphics\main.ico

  SetOutPath $INSTDIR\fonts
  File /r ..\..\data\fonts\*
  SetOutPath $INSTDIR\images
  File /r ..\..\data\images\*
  SetOutPath $INSTDIR\music
  File /r ..\..\data\music\*
  SetOutPath $INSTDIR\sounds
  File /r ..\..\data\sounds\*
  SetOutPath $INSTDIR
  File /r ..\..\data\*

  StrCmp $is_update "1" Skip2
  
  WriteUninstaller $INSTDIR\Uninst.exe

  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LunaDenyCakesGame" \
                 "DisplayName" "$(GameGameName)"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LunaDenyCakesGame" \
                 "UninstallString" "$\"$INSTDIR\Uninst.exe$\""
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LunaDenyCakesGame" \
                 "EstimatedSize" 0x00009200
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LunaDenyCakesGame" \
                 "DisplayIcon" $INSTDIR\main.ico

  ${GetTime} "" "L" $0 $1 $2 $3 $4 $5 $6
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LunaDenyCakesGame" \
                 "InstallDate"  "$2$1$0"

  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LunaDenyCakesGame" \
                 "Publisher"  "Терешенков А.В."
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LunaDenyCakesGame" \
                 "DisplayVersion"  "0.5.0"

  SetOutPath $INSTDIR
  CreateDirectory "$SMPROGRAMS\$(GameName)"
  CreateShortCut "$SMPROGRAMS\$(GameName)\$(GameName).lnk" "$INSTDIR\LunaDenyCakesGame.exe" "" 

Skip2:

SectionEnd

Section "Uninstall"
  RMDir /r $INSTDIR
  RMDir /r "$SMPROGRAMS\$(GameName)"
  Delete "$DESKTOP\$(GameName).lnk"

  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LunaDenyCakesGame"
SectionEnd

Function SetRunApp

  Push ${TEMP1}

  InstallOptions::dialog "$PLUGINSDIR\runapp.ini"
    Pop ${TEMP1}
  
  Pop ${TEMP1}

FunctionEnd

Function ValidateRunApp

FunctionEnd
