Write-Output "Publishing Cifra as a Main build"

cd $PSScriptRoot/../

<#
   Make sure that you filled in the credentials in the nuget.config file in order to access all the NuGet packages.
#>
dotnet restore ./src/Cifra.Api.Client/Cifra.Api.Client.csproj

dotnet build ./src/Cifra.Api.Client/Cifra.Api.Client.csproj --no-restore --configuration Release

dotnet pack ./src/Cifra.Api.Client/Cifra.Api.Client.csproj --no-restore --configuration Release