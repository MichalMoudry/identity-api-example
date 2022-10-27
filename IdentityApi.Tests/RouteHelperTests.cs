namespace IdentityApi.Tests;

using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using IdentityApi.Helpers;

/// <summary>
/// Class containing methods for testing <seealso cref="RouteHelper" />.
/// </summary>
public sealed class RouteHelperTests
{
    private readonly RouteHelper _routeHelper;

    public RouteHelperTests() => _routeHelper = new RouteHelper();

    /// <summary>
    /// Test method for testing CreateErrorMessage() method.
    /// </summary>
    [Fact]
    public void TestCreateErrorMessage()
    {
        var errors = new string[]
        {
            "Test",
            "",
            "Test message"
        };
        _ = _routeHelper.CreateErrorMessage(errors).Should().Be("Test\n\nTest message");
    }

    /// <summary>
    /// Test method for testing successful validation of a correct JWT token.
    /// </summary>
    [Fact]
    public void TestCreateJwtToken()
    {
        var signingKey = _routeHelper.CreateSigningKey("--I3Q5TTGQW!ETG:W!L4(!2::4Q..11111skgpw)))-)ů-§§");
        var token = _routeHelper.CreateJwtToken(
            "test_issuer",
            "test_audience",
            new Claim[]
            {
                new Claim("id", "idvalue")
            },
            signingKey
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = "test_issuer",
            ValidAudience = "test_audience",
            IssuerSigningKey = signingKey
        };
        _ = token.Should().NotBeNullOrEmpty();
        SecurityToken validatedToken;
        tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
        _ = validatedToken.ValidTo.Date.Should().Be(DateTime.Now.AddDays(1).Date);
    }
}