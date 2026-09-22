# Library Hub - Assignment 2

ASP.NET Core MVC application for the Library Management System topic. This phase implements catalogue search, book/category maintenance, stock inquiry, borrowing, renewal and return handling. Registration/login/permissions and online fine payment are intentionally reserved for the final phase.

## Run in Visual Studio 2022

1. Open `LibraryManagementSystem.sln` or `LibraryManagementSystem.csproj`.
2. Restore NuGet packages and run the project with HTTPS.
3. The default connection uses SQL Server LocalDB. `EnsureCreated` creates and seeds `LibraryManagementSystemDb` on first run.

If LocalDB is not installed or running, open Visual Studio Installer and add SQL Server Express LocalDB, or change `DefaultConnection` in `appsettings.json` to an available SQL Server instance before running.

## Architecture

The project uses MVC controllers and Razor views, EF Core SQL Server through `LibraryDbContext`, dependency injection in `Program.cs`, and a generic Repository plus Unit of Work for persistence. Bootstrap and jQuery unobtrusive validation are loaded in the shared layout.
