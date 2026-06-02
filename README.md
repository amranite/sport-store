# SportStore

SportStore is an ASP.NET Core MVC webshop assignment built with a clean architecture structure.  
I developed it on macOS using JetBrains Rider rather than Visual Studio, mainly because Rider works better for my setup on Mac.

The solution is split into four projects:

- `SportStore.Domain`
- `SportStore.Application`
- `SportStore.Infrastructure`
- `SportStore.WebUI`

It implements:

- product catalog management
- image upload for products
- session-based shopping cart
- ASP.NET Core Identity authentication
- claim-based catalog authorization
- order placement with shipping details

## Architecture

The projects reference each other like this:

- `SportStore.Domain`
  - contains the core entities
  - has no dependency on the other projects
- `SportStore.Application`
  - references `SportStore.Domain`
  - contains repository interfaces and service interfaces/implementations for application logic
- `SportStore.Infrastructure`
  - references `SportStore.Application`
  - contains EF Core, `AppDbContext`, entity configurations, repository implementations, Identity user setup, and seeding
- `SportStore.WebUI`
  - references `SportStore.Infrastructure`
  - contains MVC controllers, Razor views, view models, session/auth wiring, and app startup configuration

This follows the clean-architecture style that was required for the assignment.

## Packages Used

### `SportStore.Infrastructure`

- `Microsoft.EntityFrameworkCore.SqlServer`
  - SQL Server provider for EF Core
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
  - Identity integration with EF Core

### `SportStore.WebUI`

- `Microsoft.EntityFrameworkCore.Tools`
  - used for EF Core migration commands
- `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore`
  - useful for EF Core exception/migrations developer support
- `Microsoft.AspNetCore.Identity.UI`
  - base Identity UI support

## Requirements

- .NET 8 SDK
- Docker Desktop

## Database

This project uses SQL Server in Docker.

I used Docker with Microsoft SQL Server because I am working on macOS. Running SQL Server that way was much more practical than trying to use a Windows-specific local SQL Server setup or a VM.

Example connection string in `SportStore.WebUI/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=<YourDatabaseName>;User Id=sa;Password=<YourStrongPassword>;TrustServerCertificate=True"
}
```

## Running SQL Server With Docker

Example:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrongPassword>" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

The important part is that the password used here must match the password in the connection string. 

## Migrations

Create a migration:

Using `dotnet ef` (cross-platform CLI):

macOS / Linux:

```bash
dotnet ef migrations add <MigrationName> --project SportStore.Infrastructure --startup-project SportStore.WebUI --output-dir Migrations
```

Windows (PowerShell / CMD):

```bash
dotnet ef migrations add <MigrationName> --project .\SportStore.Infrastructure --startup-project .\SportStore.WebUI --output-dir Migrations
```

I used these explicit parameters because during development it was easy to get errors when running commands from the wrong folder or terminal context.  
With these parameters, the command works reliably no matter where it is launched from inside the solution.

Using Visual Studio Package Manager Console on Windows:

```powershell
Add-Migration <MigrationName>
```

For that approach, make sure:

- the **Default project** is `SportStore.Infrastructure`
- the **Startup project** is `SportStore.WebUI`

Update the database:

Using `dotnet ef` (cross-platform CLI):

macOS / Linux:

```bash
dotnet ef database update --project SportStore.Infrastructure --startup-project SportStore.WebUI
```

Windows (PowerShell / CMD):

```bash
dotnet ef database update --project .\SportStore.Infrastructure --startup-project .\SportStore.WebUI
```

Using Visual Studio Package Manager Console on Windows:

```powershell
Update-Database
```

For that approach, make sure:

- the **Default project** is `SportStore.Infrastructure`
- the **Startup project** is `SportStore.WebUI`

Run the application:

macOS / Linux:

```bash
dotnet run --project SportStore.WebUI
```

Windows (PowerShell / CMD):

```bash
dotnet run --project .\SportStore.WebUI
```

You can also simply set `SportStore.WebUI` as the startup project in your IDE and run it from there:

- in Rider: use the normal run button once `SportStore.WebUI` is selected as startup project
- in Visual Studio: set `SportStore.WebUI` as the startup project and use the green run button

I included the CLI command as well because it works regardless of which IDE is being used.

## Seeded Users

Catalog manager:

- Email: `manager@sportstore.com`
- Password: `Manager123!`
- Claim: `CanManageCatalog = true`

Regular customer:

- Email: `customer@sportstore.com`
- Password: `Customer123!`
