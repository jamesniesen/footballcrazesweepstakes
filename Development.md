# SFX Football Craze Sweepstakes

## Description

## Functionality
### Uploads E-Tickets
### Handles E-Ticket purchases

## App Info:
FootballCrazeSweepstakes
Windows Forms App
.NET 9
Entity Framework 
SQL Lite

https://sqlitebrowser.org/dl/

# Initial Setup 
If you do not have ef installed globally, install.....S
1. install Microsoft.EntityFrameworkCore 9.0 Nuget package
2. install Microsoft.EntityFrameworkCore.Tools 9.0 Nuget packagae
3. install Microsoft.EntityFrameworkCore.SqlLite 9.0 Nuget package
1. install Microsoft.Extensions.Configuration.Json
1. Microsoft.Extensions.Configuration.Binder
1. DotNetCore.NPOI
1. install Microsoft.Extensions.Hosting
1. install Microsoft.Extensions.Hosting.Abstractions
1. create Models folder
1. create Models/ApplicationDbContext.cs
1. create the database
	Create App_Data folder in FootballCrazeSweepstakes\FootballCrazeSweepstakes\bin\Debug\net9.0-windows
	Create ETicketsToUpload folder inside App_Data
1. Open the Developer Powershell and navigate to the project's root.From the project's root, run:
	dotnet ef migrations add InitialCreate
dotnet ef database update
1. rename Form1.cs to Dashboard.cs
1. Adjust Program.cs so you can use dependency injection for repository and service.
1. Add xUnit test project


## Create model/entity/table example: Eticket
Create Models/ETicket.cs
1. Add public DbSet<ETicket> ETicket { get; set; }in ApplicationDbContext.cs
1. Open the Developer Powershell and navigate to the project's root.From the project's root, run:
	dotnet ef migrations add AddEticketEntity
dotnet ef database update
