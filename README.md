## DemoApp-NetCoreWebAPI
This is a simple web api application using .Net Core 3.1

### Pre-requisites
- MS Visual Studio 2019
- MS SQL Server 2017

### CLI command to import database objects to the project
Scaffold-DbContext "data source=<your_server_name>;initial catalog=<your_database_name>;user id=<database_username>;password=<database_password>;MultipleActiveResultSets=True" Microsoft.EntityFrameworkCore.SqlServer -Force
