FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/File/File.GrpcService/File.GrpcService.csproj", "src/Services/File/File.GrpcService/"]
COPY ["src/Services/File/File.Core/File.Core.csproj", "src/Services/File/File.Core/"]
COPY ["src/Services/File/File.DataAccess/File.DataAccess.csproj", "src/Services/File/File.DataAccess/"]
RUN dotnet restore "src/Services/File/File.GrpcService/File.GrpcService.csproj"
COPY . .
WORKDIR "/src/src/Services/File/File.GrpcService"
RUN dotnet build "File.GrpcService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "File.GrpcService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "File.GrpcService.dll"]
