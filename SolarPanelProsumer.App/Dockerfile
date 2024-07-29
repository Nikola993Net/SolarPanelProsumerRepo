#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SolarPanelProsumer.App/SolarPanelProsumer.App.csproj", "SolarPanelProsumer.App/"]
RUN dotnet restore "./SolarPanelProsumer.App/./SolarPanelProsumer.App.csproj"
COPY . .
WORKDIR "/src/SolarPanelProsumer.App"
RUN dotnet build "./SolarPanelProsumer.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SolarPanelProsumer.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SolarPanelProsumer.App.dll"]