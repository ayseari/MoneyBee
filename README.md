# MoneyBee
MoneyBee, mikroservis mimarisi ile geliştirilmiş, güvenli ve ölçeklenebilir bir para transferi platformudur.

## Features
Customer & Identity Management
Money Transfer (P2P)
Transaction Lifecycle & Status Logs
Business Rule & Validation Engine
Cache Support (Redis)

##  Requirements
.NET SDK 8.0+
Docker & Docker Compose
SQL Server
Redis

##  Configuration
All application settings are managed via appsettings.json and Options Pattern
Example
{
  "MoneyTransferSettings": {
    "DailyLimit": 50000,
    "MaxTransactionAmount": 10000
  }
}

Registered in DI container:
 services.Configure<MoneyTransferSettings>(
    configuration.GetSection(MoneyTransferSettings.SectionName));
