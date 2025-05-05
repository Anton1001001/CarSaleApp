FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Chat/Chat.API/Chat.API.csproj", "src/Services/Chat/Chat.API/"]
COPY ["src/Services/Chat/Chat.Core/Chat.Core.csproj", "src/Services/Chat/Chat.Core/"]
COPY ["src/Services/Chat/Chat.DataAccess/Chat.DataAccess.csproj", "src/Services/Chat/Chat.DataAccess/"]
COPY ["src/Services/Advert/Advert.GrpcService/Advert.GrpcService.csproj", "src/Services/Advert/Advert.GrpcService/"]
COPY ["src/Services/Advert/Advert.Application/Advert.Application.csproj", "src/Services/Advert/Advert.Application/"]
COPY ["src/Services/Advert/Advert.Domain/Advert.Domain.csproj", "src/Services/Advert/Advert.Domain/"]
COPY ["src/Services/Advert/Advert.Infrastructure/Advert.Infrastructure.csproj", "src/Services/Advert/Advert.Infrastructure/"]
COPY ["src/Services/Car/Car.GrpcService/Car.GrpcService.csproj", "src/Services/Car/Car.GrpcService/"]
COPY ["src/Services/Car/Car.Application/Car.Application.csproj", "src/Services/Car/Car.Application/"]
COPY ["src/Services/Car/Car.Domain/Car.Domain.csproj", "src/Services/Car/Car.Domain/"]
COPY ["src/Services/Car/Car.Infrastructure/Car.Infrastructure.csproj", "src/Services/Car/Car.Infrastructure/"]
COPY ["src/Shared/Auth.Shared/Auth.Shared.csproj", "src/Shared/Auth.Shared/"]
RUN dotnet restore "src/Services/Chat/Chat.API/Chat.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Chat/Chat.API"
RUN dotnet build "Chat.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Chat.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chat.API.dll"]
