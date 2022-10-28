module RouterHelperTests

open AssertFunctions
open System
open System.Security.Claims
open System.IdentityModel.Tokens.Jwt
open Microsoft.IdentityModel.Tokens
open IdentityApi.Helpers
open Xunit

let routeHelper = new RouteHelper()

/// Test for CreateErrorMessage() method.
[<Fact>]
[<Trait("Category", "UnitTest")>]
let TestCreateErrorMessage () =
    let errors = ["Test"; ""; "Test message"]
    routeHelper.CreateErrorMessage(errors) |> Equal("Test\n\nTest message")

/// Method for testing successful validation of a correct JWT token.
[<Fact>]
[<Trait("Category", "UnitTest")>]
let TestCreateJwtToken () =
    let key = routeHelper.CreateSigningKey("--I3Q5TTGQW!ETG:W!L4(!2::4Q..11111skgpw)))-)�-��")
    let token = routeHelper.CreateJwtToken("test_issuer", "test_audience", [new Claim("id", "idvalue")], key)
    let tokenHandler = new JwtSecurityTokenHandler()
    let validationParameters = new TokenValidationParameters(
        ValidateAudience = true, ValidateIssuer = true, ValidIssuer = "test_issuer", ValidAudience = "test_audience", IssuerSigningKey = key
    )
    token |> NotNullOrEmpty
    let (_, validatedToken) = tokenHandler.ValidateToken(token, validationParameters)
    validatedToken.ValidTo |> Equal (DateTime.Now.AddDays(1).Date)
