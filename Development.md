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
4. install Microsoft.Extensions.Configuration.Json
5. Microsoft.Extensions.Configuration.Binder
6. DotNetCore.NPOI
7. install Microsoft.Extensions.Hosting
8. install Microsoft.Extensions.Hosting.Abstractions
9. create Models folder
10. create Models/ApplicationDbContext.cs
11. create the database
	Create App_Data folder in FootballCrazeSweepstakes\FootballCrazeSweepstakes\bin\Debug\net9.0-windows
	Create ETicketsToUpload folder inside App_Data
12. Open the Developer Powershell and navigate to the project's root (not the solution's root).
	From the project's root, run:
	dotnet ef database update
13. rename Form1.cs to Dashboard.cs
14. Adjust Program.cs so you can use dependency injection for repository and service.

## Create Initial/New Database
1. To remove the old database, Delete the following 3 files from 'C:\Users\james\source\repos\footballcrazesweepstakes\FootballCrazeSweepstakes\bin\Debug\net9.0-windows\App_Data':
-  FootballCrazeSweepstakes.db
-  FootballCrazeSweepstakes.db-shm
-  FootballCrazeSweepstakes.db-wal
2. Open the Developer Powershell and navigate to the project's root (not the solution's root).From the project's root, run:
dotnet ef database update

## Create model/entity/table example: Eticket
Create Models/ETicket.cs
1. Add public DbSet<ETicket> ETicket { get; set; }in ApplicationDbContext.cs
2. Open the Developer Powershell and navigate to the project's root (not the solution's root).From the project's root, run:
dotnet ef database update
