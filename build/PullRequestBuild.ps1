cd $PSScriptRoot/../

<#
   Make sure that you filled in the credentials in the nuget.config file in order to access all the NuGet packages.
#>
dotnet restore

dotnet build --no-restore --configuration Release

dotnet test --no-restore