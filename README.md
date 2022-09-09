[![Tests](https://github.com/MichalMoudry/identity-api-example/actions/workflows/dotnet_test.yaml/badge.svg)](https://github.com/MichalMoudry/identity-api-example/actions/workflows/dotnet_test.yaml)
[![CodeQL](https://github.com/MichalMoudry/identity-api-example/actions/workflows/codeql.yml/badge.svg)](https://github.com/MichalMoudry/identity-api-example/actions/workflows/codeql.yml)
[![Publish Docker image](https://github.com/MichalMoudry/identity-api-example/actions/workflows/docker_push.yaml/badge.svg)](https://github.com/MichalMoudry/identity-api-example/actions/workflows/docker_push.yaml)

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
- Tests

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

## How to run
- Docker run command: docker run --rm -it -p 8000:80 -p 8001:443 --name [container_name] [container_image]