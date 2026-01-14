
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Transfer/MoneyBee.Transfer.Api/MoneyBee.Transfer.Api.csproj", "src/Transfer/MoneyBee.Transfer.Api/"]
COPY ["src/Customer/MoneyBee.Customer.Api.Client/MoneyBee.Customer.Api.Client.csproj", "src/Customer/MoneyBee.Customer.Api.Client/"]
COPY ["src/Common/MoneyBee.Common/MoneyBee.Common.csproj", "src/Common/MoneyBee.Common/"]
COPY ["src/Infrastructure/MoneyBee.External.Api.Client/MoneyBee.External.Api.Client.csproj", "src/Infrastructure/MoneyBee.External.Api.Client/"]
COPY ["src/Transfer/MoneyBee.Transfer.Application/MoneyBee.Transfer.Application.csproj", "src/Transfer/MoneyBee.Transfer.Application/"]
COPY ["src/Transfer/MoneyBee.Transfer.Domain/MoneyBee.Transfer.Domain.csproj", "src/Transfer/MoneyBee.Transfer.Domain/"]
COPY ["src/Transfer/MoneyBee.Transfer.Infrastructure/MoneyBee.Transfer.Infrastructure.csproj", "src/Transfer/MoneyBee.Transfer.Infrastructure/"]

RUN dotnet restore "src/Transfer/MoneyBee.Transfer.Api/MoneyBee.Transfer.Api.csproj"

COPY . .

WORKDIR "/src/src/Transfer/MoneyBee.Transfer.Api"
RUN dotnet build "MoneyBee.Transfer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MoneyBee.Transfer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MoneyBee.Transfer.Api.dll"]
