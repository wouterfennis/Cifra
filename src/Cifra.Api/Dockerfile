#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Cifra.Api/Cifra.Api.csproj", "src/Cifra.Api/"]
RUN dotnet restore "src/Cifra.Api/Cifra.Api.csproj"
COPY . .
WORKDIR "/src/src/Cifra.Api"
RUN dotnet build "Cifra.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cifra.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cifra.Api.dll"]