namespace IdentityApi.Helpers;

using System.Text;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Helper class for Identity API endpoints.
/// </summary>
public class EndpointHelper
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
    /// Method for creating a new signing credentials.
    /// </summary>
    /// <param name="signingKey">Security key.</param>
    public SigningCredentials CreateSigningCredentials(SymmetricSecurityKey signingKey)
    {
        return new SigningCredentials(
            signingKey,
            SecurityAlgorithms.HmacSha512
        );
    }
}