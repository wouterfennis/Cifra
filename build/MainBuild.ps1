Write-Output "Publishing Cifra as a Main build"

cd $PSScriptRoot/../

<#
   Make sure that you filled in the credentials in the nuget.config file in order to access all the NuGet packages.
#>
dotnet restore

dotnet build --no-restore --configuration Release

dotnet test --no-restore

<#
Publish project for Linux x64
#>
dotnet publish ./src/Cifra.ConsoleHost/Cifra.ConsoleHost.csproj --configuration Release --no-restore -p:PublishProfile=Linux64

<#
Publish project for Mac OSX x64
#>
dotnet publish ./src/Cifra.ConsoleHost/Cifra.ConsoleHost.csproj --configuration Release --no-restore -p:PublishProfile=Osx64

<#
Publish project for Windows x64
#>
dotnet publish ./src/Cifra.ConsoleHost/Cifra.ConsoleHost.csproj --configuration Release --no-restore -p:PublishProfile=Windows64 -p:IncludeNativeLibrariesForSelfExtract=true

Write-Output "Done publishing Cifra as a Main build"