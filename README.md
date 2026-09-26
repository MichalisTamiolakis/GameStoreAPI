# GameStore API

A RESTful Web API for a video game store, built with **ASP.NET Core** and **Entity Framework Core**. This project is a hands-on exercise in building a properly layered, production-style .NET backend — controllers, services, DTOs with validation, and EF Core migrations against SQL Server.

> 🚧 **Status: In active development.** Core CRUD functionality is working; authentication, orders, and performance improvements are in progress (see [Roadmap](#roadmap) below).

## Tech Stack

- **ASP.NET Core Web API** (C#)
- **Entity Framework Core** — code-first, with migrations
- **SQL Server** — persistence
- **DTOs with validation** — request/response shaping and input validation kept out of the domain models

## Architecture

The project follows a layered structure to keep concerns separated and the codebase testable:

```
GameStoreAPI/
├── Controllers/    # API endpoints (HTTP layer)
├── Services/       # Business logic
├── Data/           # DbContext
├── Models/         # Domain entities
├── Migrations/     # EF Core migrations
└── Program.cs      # App configuration & DI setup
```

Requests flow **Controller → Service → DbContext**, with DTOs used at the API boundary so validation and shaping happen before data ever reaches the domain models.

## Features

- ✅ Full CRUD for **Games**
- ✅ Full CRUD for **Users**
- ✅ Request validation via DTOs
- ✅ EF Core migrations against SQL Server
- 🔜 Orders (linking users to purchased/owned games)
- 🔜 Authentication & authorization
- 🔜 Performance improvements

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (8.0 or later)
- SQL Server (local instance or Docker container)

### Setup

1. Clone the repository
   ```bash
   git clone https://github.com/MichalisTamiolakis/GameStoreAPI.git
   cd GameStoreAPI
   ```

2. Update the connection string in `appsettings.Development.json` to point to your SQL Server instance.

3. Apply the EF Core migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the API:
   ```bash
   dotnet run
   ```

5. Use Postman/curl at the base URL shown in the console output to try out the endpoints.

## Roadmap

- [ ] **Orders** — allow users to purchase/own games, with an Orders entity linking Users and Games
- [ ] **Authentication & Authorization** — JWT-based auth, role-based access control
- [ ] **Performance improvements** — query optimization, caching where appropriate
- [ ] Automated tests
- [ ] Swagger/OpenAPI documentation

## About

This project is part of expanding my skills into .NET backend development, built to apply and practice ASP.NET Core, EF Core, and layered API design in a realistic project — modeling the backend of a game store.
