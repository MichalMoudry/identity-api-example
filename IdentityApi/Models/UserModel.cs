namespace IdentityApi.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// A model class for user's login.
/// </summary>
public class UserModel
{
    [Required, MinLength(length: 3)]
    public string? UserName { get; set; }

    [Required, EmailAddress]
    public string? Email { get; set; }

    [Required, MinLength(length: 7)]
    public string? Password { get; set; }
}