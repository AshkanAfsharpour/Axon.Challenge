This is a ASP.NET Core Sample GitHub Interactor Web Service following the Clean Architecture and SOLID Principles alongside with CQRS Pattern.


## Technologies

* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) & [Respawn](https://github.com/jbogard/Respawn)
* [Docker](https://www.docker.com/)
* [Octokit](https://github.com/octokit)
* [Jwt Authentication](https://jwt.io/)


## Getting Started

1. Install  [.NET 3.1 SDK](https://dotnet.microsoft.com/download/dotnet/3.1). 

2. Install SQLServer and Create the database using the Database.sql file placed in documents folder.(Make sure FileName Path is matching your SqlServer installation Path) 

3. Restore nuget packages and build the project using `dotnet restore` and `dotnet build`. 

4. Run Integration and Unit Tests. 

5. Run the project and use the Swagger UI to interact with provided Rest Api's. 


## Note:

* Identity and Users api's are just for demonstration of CRUD process in a CQRS pattern, Validating api's using Fluent Validation, Specification Pattern on Data Querying and simple Usage of JWT Authentication and Authorization. None of the Github api's need Authentication nor Authorization.

* Before you use Github Services make sure you have already created a [Personal GitHub Access Token](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token) with Full or Read permission on user and repository.

* Before running test insert your GitHub Access Token in Application.IntegrationTests/appsettings.json.

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### Presistence

This layer contains all databases configurations and repositories and same as the Infrastructure layer, all implementations Should be based on interfaces defined within the application layer.

### WebApi

This layer is a restful api project managing requests  and responses. This layer depends on the Application ,Infrastructure and Presistence layers, however, the dependency on Infrastructure and Presistence is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

