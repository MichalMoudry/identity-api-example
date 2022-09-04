namespace IdentityApi.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// A model class for user's login.
/// </summary>
public class LoginModel
{
    [Required, EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}