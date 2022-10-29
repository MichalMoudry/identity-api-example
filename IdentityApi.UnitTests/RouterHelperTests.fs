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
    routeHelper.CreateErrorMessage(errors) |> equal ("Test\n\nTest message")

/// Method for testing successful validation of a correct JWT token.
[<Fact>]
[<Trait("Category", "UnitTest")>]
let TestCreateJwtToken () =
    let key = routeHelper.CreateSigningKey("--I3Q5TTGQW!ETG:W!L4(!2::4Q..11111skgpw)))-)----")
    let token = routeHelper.CreateJwtToken("test_issuer", "test_audience", [new Claim("id", "idvalue")], key)
    let tokenHandler = new JwtSecurityTokenHandler()
    let validationParameters = new TokenValidationParameters(
        ValidateAudience = true, ValidateIssuer = true, ValidIssuer = "test_issuer", ValidAudience = "test_audience", IssuerSigningKey = key
    )
    token |> notNullOrEmpty
    let (_, validatedToken) = tokenHandler.ValidateToken(token, validationParameters)
    validatedToken.ValidTo.Date |> equal (DateTime.Now.AddDays(1).Date)

/// Method for testing creation of claims for a default user.
[<Fact>]
[<Trait("Category", "UnitTest")>]
let TestCreateClaimsForDefaultUser () =
    let claims =
        routeHelper.CreateClaimsForDefaultUser("id", "user@user.com", "userName", ["userRole"])
        |> Seq.map (fun i -> i.Type, i.Value)
        |> dict
    claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] |> equal "user@user.com"
    claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] |> equal "userName"
    claims["UserId"] |> equal "id"
    claims["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] |> equal "userRole"
