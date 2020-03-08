# Simple-Bank-API
ASP.NET Core Web API 3.0, Entity Framework Core, Swagger

## Dependencies
* Swashbuckle.AspNetCore
* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Tools
* Microsoft.AspNetCore.Authentication.JwtBearer
* Microsoft.AspNetCore.Mvc.Versioning
* Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
* NSubstitute
* Xunit

## API Authorizations
* Get token for authorize in swagger api document
* Request https://localhost:44331/api/v1/users/authenticate with request body 
* Copy token from response body
* Click Authorze button on top right
* Enter Bearer and than space and paste Token
