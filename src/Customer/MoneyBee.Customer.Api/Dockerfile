FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Customer/MoneyBee.Customer.Api/MoneyBee.Customer.Api.csproj", "src/Customer/MoneyBee.Customer.Api/"]
COPY ["src/Customer/MoneyBee.Customer.Application/MoneyBee.Customer.Application.csproj", "src/Customer/MoneyBee.Customer.Application/"]
COPY ["src/Customer/MoneyBee.Customer.Domain/MoneyBee.Customer.Domain.csproj", "src/Customer/MoneyBee.Customer.Domain/"]
COPY ["src/Common/MoneyBee.Common/MoneyBee.Common.csproj", "src/Common/MoneyBee.Common/"]
COPY ["src/Customer/MoneyBee.Customer.Infrastructure/MoneyBee.Customer.Infrastructure.csproj", "src/Customer/MoneyBee.Customer.Infrastructure/"]
COPY ["src/Infrastructure/MoneyBee.External.Api.Client/MoneyBee.External.Api.Client.csproj", "src/Infrastructure/MoneyBee.External.Api.Client/"]

RUN dotnet restore "src/Customer/MoneyBee.Customer.Api/MoneyBee.Customer.Api.csproj"

COPY . .

WORKDIR "/src/src/Customer/MoneyBee.Customer.Api"
RUN dotnet build "MoneyBee.Customer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MoneyBee.Customer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MoneyBee.Customer.Api.dll"]