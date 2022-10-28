FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "Cifra.Api/Cifra.Api.csproj" "Cifra.Api/"
COPY "Cifra.Api.UnitTests/Cifra.Api.UnitTests.csproj" "Cifra.Api.UnitTests/"
COPY "Cifra.Api.Client/Cifra.Api.Client.csproj" "Cifra.Api.Client/"
COPY "Cifra.Api.IntegrationTests/Cifra.Api.IntegrationTests.csproj" "Cifra.Api.IntegrationTests/"
COPY "Cifra.Application/Cifra.Application.csproj" "Cifra.Application/"
COPY "Cifra.Application.UnitTests/Cifra.Application.UnitTests.csproj" "Cifra.Application.UnitTests/"
COPY "Cifra.Core/Cifra.Core.csproj" "Cifra.Core/"
COPY "Cifra.ConsoleHost/Cifra.ConsoleHost.csproj" "Cifra.ConsoleHost/"
COPY "Cifra.ConsoleHost.UnitTests/Cifra.ConsoleHost.UnitTests.csproj" "Cifra.ConsoleHost.UnitTests/"
COPY "Cifra.FileSystem/Cifra.FileSystem.csproj" "Cifra.FileSystem/"
COPY "Cifra.FileSystem.UnitTests/Cifra.FileSystem.UnitTests.csproj" "Cifra.FileSystem.UnitTests/"
COPY "Cifra.TestUtilities/Cifra.TestUtilities.csproj" "Cifra.TestUtilities/"
COPY "Cifra.Database/Cifra.Database.csproj" "Cifra.Database/"
COPY "Cifra.Database.UnitTests/Cifra.Database.UnitTests.csproj" "Cifra.Database.UnitTests/"
COPY "Cifra.Frontend/Cifra.Frontend.csproj" "Cifra.Frontend/"
COPY Cifra.sln .
COPY NuGet.Config .
RUN dotnet restore
COPY . .
RUN dotnet build -c Debug -o /app/build --no-restore

FROM build AS Test
RUN dotnet test --no-restore --no-build

FROM build AS publish
RUN dotnet publish "Cifra.Api/Cifra.Api.csproj" -c Debug -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cifra.Api.dll"]