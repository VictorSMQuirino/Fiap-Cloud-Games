FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FIAP_CloudGames.API/FIAP_CloudGames.API.csproj", "src/FIAP_CloudGames.API/"]
COPY ["src/FIAP_CloudGames.Application/FIAP_CloudGames.Application.csproj", "src/FIAP_CloudGames.Application/"]
COPY ["src/FIAP_CloudGames.Domain/FIAP_CloudGames.Domain.csproj", "src/FIAP_CloudGames.Domain/"]
COPY ["src/FIAP_CloudGames.Infrastructure/FIAP_CloudGames.Infrastructure.csproj", "src/FIAP_CloudGames.Infrastructure/"]
RUN dotnet restore "src/FIAP_CloudGames.API/FIAP_CloudGames.API.csproj"
COPY . .
WORKDIR "/src/src/FIAP_CloudGames.API"
RUN dotnet build "./FIAP_CloudGames.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FIAP_CloudGames.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FIAP_CloudGames.API.dll"]
