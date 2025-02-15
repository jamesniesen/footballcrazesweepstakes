# EFCore Notes

## Entity Framework Core Migrations (Code-First)
- [Entity Framework Core tools reference - .NET Core CLI](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)
- [Entity Framework Core Migrations Overview](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
- [Create a New Table with EF Code-First](./CreateNewTable.md)

### Install the EF Core tool
The CLI tools for EF Core are not part of .NET Core, and need to be installed separately. The tools should be installed globally:
- \>`dotnet tool install --global dotnet-ef`

To update the tools:
- \>`dotnet tool update --global dotnet-ef`