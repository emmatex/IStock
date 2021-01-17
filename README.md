# IStock
Inventory application

### Run Migration
  ```
    dotnet ef migrations add InitialIdentity -p Infrastructure -s API/ -o Identity/Migrations -c AppIdentityDbContext (vscode)
    dotnet ef migrations add InitialCreate -p Infrastructure -s API/ -o Data/Migrations -c DataContext (vscode)
    Add-Migration InitialIdentity -c AppIdentityDbContext -o Identity/Migrations -p Infrastructure (visual studio)
    Add-Migration InitialCreate -c DataContext -o Data/Migrations -p Infrastructure (visual studio)
  ```

A sample .netcore 5 project - <b>IStock</b>
