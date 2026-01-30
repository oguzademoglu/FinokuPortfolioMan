Finoku - Personal Portfolio Management System
Finoku is a comprehensive financial tracking Web API developed with .NET 8. It allows users to manage diverse assets (Stocks, Crypto, Gold) and view their portfolio performance with real-time currency conversions.

Project Structure
The project follows Clean Architecture principles to ensure high maintainability, testability, and scalability.

Finoku/
├── Finoku.Domain          # Entities, Enums, and Core Logic
├── Finoku.Application     # Interfaces, DTOs, Mapping, and Service Logic
├── Finoku.Infrastructure  # DB Context (SQL), NoSQL Logging (MongoDB), External API Clients
└── Finoku.API             # Controllers, and Program.cs


Architectural Decisions & Approach

1. Clean Architecture
I chose this layered approach to decouple the business logic from external concerns (like databases or APIs). This allows the project to adapt to technological changes (e.g., switching from SQL Server to PostgreSQL) without touching the core logic.

2. Hybrid Database Strategy
SQL Server (Relational): Used for transactional data (Users, Assets, Categories) where ACID properties and relationship integrity are critical.

MongoDB (NoSQL): Implemented for high-performance logging. Audit trails and system exceptions are stored here to keep the main database lean and focused on business data.

3. Periodic Background Updates
To avoid hitting external API rate limits and to provide faster response times, a Background Service (PeriodicTimer) updates exchange rates every 5 minutes and caches them in the local SQL database.

4. Security & Role-Based Access (RBAC)
JWT Authentication: Secure stateless communication.

Authorization: Strict role separation where only Admin users can manage AssetCategories.


External API Usage

- The project integrates with ExchangeRate-API (v6) to fetch real-time global exchange rates.

- Provider: ExchangeRate-API

- Version: v6 (Standard/Business)

- Base URL Format: https://v6.exchangerate-api.com/v6/{API_KEY}/latest/{BASE_CURRENCY}

- Implementation: The ExchangeRateService performs an HTTP GET request to fetch parities relative to a base currency (e.g., USD or TRY).

Example Request:
GET https://v6.exchangerate-api.com/v6/12edd8c4224de70eb0d5b647/latest/USD


Operating Instructions & Guidelines

Prerequisites
- .NET 8 SDK

- SQL Server (LocalDB supported)

- MongoDB (Running on localhost:27017)

Step-by-Step Setup
1. Clone the Repository:
git clone https://github.com/yourusername/Finoku.git
cd Finoku
2. Configure Environment: Update the connection strings in Finoku.API/appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FinokuPortfolioDb;..."
},
"MongoDbSettings": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "FinokuLogDb"
}
3. Apply Database Migrations: Open the Package Manager Console and run:
Update-Database -Project Finoku.Infrastructure -StartupProject Finoku.API
4. Run the Project: Press F5 in Visual Studio or run via CLI:
dotnet run --project Finoku.API
5.Access Documentation: Once running, navigate to https://localhost:XXXX/swagger to explore and test endpoints.