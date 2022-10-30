[![Tests](https://github.com/MichalMoudry/identity-api-example/actions/workflows/dotnet_test.yaml/badge.svg)](https://github.com/MichalMoudry/identity-api-example/actions/workflows/dotnet_test.yaml)
[![CodeQL](https://github.com/MichalMoudry/identity-api-example/actions/workflows/codeql.yml/badge.svg)](https://github.com/MichalMoudry/identity-api-example/actions/workflows/codeql.yml)
[![Publish Docker image](https://github.com/MichalMoudry/identity-api-example/actions/workflows/docker_publish.yaml/badge.svg)](https://github.com/MichalMoudry/identity-api-example/actions/workflows/docker_publish.yaml)
[![Integration tests](https://github.com/MichalMoudry/identity-api-example/actions/workflows/integration_tests.yaml/badge.svg)](https://github.com/MichalMoudry/identity-api-example/actions/workflows/integration_tests.yaml)

# Identity microservice example
Repository with an example of an identity API application, using minimal APIs structure.

## Solution structure
- Infrastructure
    - Database context and possible repositories.
- Models
- Helpers
- Extensions
- Validators
    - Validators for model or other classes.
- Migrations
    - EF Core database migrations.
- UnitTests
- IntegrationTests

## Used packages
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.EntityFrameworkCore.Tools
- FluentValidation
- FluentAssertion
- Swashbuckle.AspNetCore

## Useful links
- https://github.com/davidfowl/CommunityStandUpMinimalAPI
- https://abdelmajid-baco.medium.com/cqrs-pattern-with-c-a9aff05aae3f
- https://umayangag.medium.com/jwt-authentication-with-asp-net-core-3-1-identity-for-web-apis-fe36d4bb6630
- https://medium.com/front-end-weekly/net-core-web-api-with-docker-compose-postgresql-and-ef-core-21f47351224f

## How to run
- **Docker run command**: docker run -d -p 443:443 --name [container_name] [container_image]