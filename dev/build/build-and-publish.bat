@echo off

:: this script requires nuget.exe to be in this folder
:: https://www.nuget.org/downloads

echo.
echo ### DELETING OLD PACKAGES ###
DEL *.nupkg

echo.
echo ### DELETING RELEASE FOLDERS ###
RMDIR ..\..\src\FftSharp\bin\Release /S /Q

echo.
echo ### CLEANING SOLUTION ###
dotnet clean ..\..\src\FftSharp.sln --verbosity quiet --configuration Release

echo.
echo ### REBUILDING SOLUTION ###
dotnet build ..\..\src\FftSharp\FftSharp.csproj --verbosity quiet --configuration Release

echo.
echo ### COPYING PACKAGE HERE ###
copy ..\..\src\FftSharp\bin\Release\*.nupkg .\

echo.
echo WARNING! This script will UPLOAD packages to nuget.org
echo.
echo press ENTER 3 times to proceed...
pause
pause
pause

echo.
echo ### UPDATING NUGET ###
nuget update -self

echo.
echo ### UPLOADING TO NUGET ###
nuget push *.nupkg -Source https://api.nuget.org/v3/index.json

echo.
pause