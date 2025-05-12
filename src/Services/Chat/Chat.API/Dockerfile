FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Chat/Chat.API/Chat.API.csproj", "src/Services/Chat/Chat.API/"]
COPY ["Services/Car/Car.Infrastructure/Car.Infrastructure.csproj", "src/Services/Car/Car.Infrastructure/"]
COPY ["Services/Car/Car.Domain/Car.Domain.csproj", "src/Services/Car/Car.Domain/"]
COPY ["Services/Car/Car.GrpcService/Car.GrpcService.csproj", "src/Services/Car/Car.GrpcService/"]
COPY ["Services/Car/Car.Application/Car.Application.csproj", "src/Services/Car/Car.Application/"]
COPY ["Services/Advert/Advert.Infrastructure/Advert.Infrastructure.csproj", "src/Services/Advert/Advert.Infrastructure/"]
COPY ["Services/Advert/Advert.Application/Advert.Application.csproj", "src/Services/Advert/Advert.Application/"]
COPY ["Services/Advert/Advert.Domain/Advert.Domain.csproj", "src/Services/Advert/Advert.Domain/"]
COPY ["Shared/Auth.Shared/Auth.Shared.csproj", "src/Shared/Auth.Shared/"]
COPY ["Services/Advert/Advert.GrpcService/Advert.GrpcService.csproj", "src/Services/Advert/Advert.GrpcService/"]
COPY ["Services/Chat/Chat.DataAccess/Chat.DataAccess.csproj", "src/Services/Chat/Chat.DataAccess/"]
COPY ["Services/Chat/Chat.Core/Chat.Core.csproj", "src/Services/Chat/Chat.Core/"]
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
