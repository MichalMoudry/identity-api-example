namespace IdentityApi.Helpers;

using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Helper class for Identity API endpoints.
/// </summary>
public class RouteHelper
{
    /// <summary>
    /// Method for creating an error message.
    /// </summary>
    /// <param name="errors">A collection of errors.</param>
    public string CreateErrorMessage<T>(IEnumerable<T>? errors)
    {
        if (errors == null)
        {
            return "";
        }
        var stringBuilder = new StringBuilder().AppendJoin("\n", errors);
        return stringBuilder.ToString();
    }
    
    /// <summary>
    /// Method for creating a new signing key.
    /// </summary>
    /// <param name="securityKey">Security key.</param>
    public SymmetricSecurityKey CreateSigningKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }

    /// <summary>
    /// Method for creating JWT token.
    /// </summary>
    /// <param name="issuer">JWT token issuer.</param>
    /// <param name="audience">JWT token audience.</param>
    /// <param name="claims">User's claims.</param>
    /// <param name="signingKey">Symmetric sercurity key for signing the JWT token.</param>
    /// <returns>JWT token as a string.</returns>
    public string CreateJwtToken(string issuer, string audience, IEnumerable<Claim> claims, SymmetricSecurityKey signingKey)
    {
        var signingCredentials = CreateSigningCredentials(signingKey);
        var token = new JwtSecurityToken(
            issuer, audience, claims, expires: DateTime.Now.AddDays(1), signingCredentials: signingCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Method for creating a new signing credentials.
    /// </summary>
    /// <param name="signingKey">Security key.</param>
    private SigningCredentials CreateSigningCredentials(SymmetricSecurityKey signingKey)
    {
        return new SigningCredentials(
            signingKey,
            SecurityAlgorithms.HmacSha512
        );
    }
}