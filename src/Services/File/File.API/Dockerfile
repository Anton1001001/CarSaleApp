FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/File/File.API/File.API.csproj", "src/Services/File/File.API/"]
COPY ["src/Services/File/File.Core/File.Core.csproj", "src/Services/File/File.Core/"]
COPY ["src/Services/File/File.DataAccess/File.DataAccess.csproj", "src/Services/File/File.DataAccess/"]
RUN dotnet restore "src/Services/File/File.API/File.API.csproj"
COPY . .
WORKDIR "/src/src/Services/File/File.API"
RUN dotnet build "File.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "File.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "File.API.dll"]
