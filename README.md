# backendTMDB
Install the following NutGet Packages
*Microsoft.EntityFrameworkCore
*Microsoft.IdentityModel.Tokens
*BCrypt.Net-Next
*Microsoft.AspNetCore.Mvc.NewtonsoftJson
*Microsoft.EntityFrameworkCore.SqlServer
*Microsoft.EntityFrameworkCore.Design

Press ctrl ` and do the following commands:

dotnet ef migrations add Init --output-dir Data/Migrations

Check that the migration is not empty

Run : dotnet ef migrations remove if empty 
Run : dotnet build if error
Run : dotnet ef database update

Run Application


