FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Auth/MoneyBee.Auth.Api/MoneyBee.Auth.Api.csproj", "src/Auth/MoneyBee.Auth.Api/"]
COPY ["src/Auth/MoneyBee.Auth.Application/MoneyBee.Auth.Application.csproj", "src/Auth/MoneyBee.Auth.Application/"]
COPY ["src/Auth/MoneyBee.Auth.Domain/MoneyBee.Auth.Domain.csproj", "src/Auth/MoneyBee.Auth.Domain/"]
COPY ["src/Common/MoneyBee.Common/MoneyBee.Common.csproj", "src/Common/MoneyBee.Common/"]
COPY ["src/Auth/MoneyBee.Auth.Infrastructure/MoneyBee.Auth.Infrastructure.csproj", "src/Auth/MoneyBee.Auth.Infrastructure/"]

RUN dotnet restore "src/Auth/MoneyBee.Auth.Api/MoneyBee.Auth.Api.csproj"

COPY . .

WORKDIR "/src/src/Auth/MoneyBee.Auth.Api"
RUN dotnet build "MoneyBee.Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MoneyBee.Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MoneyBee.Auth.Api.dll"]