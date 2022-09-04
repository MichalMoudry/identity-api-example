namespace IdentityApi.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// A model class for registering a new user.
/// </summary>
public class RegisterModel
{
    [Required]
    public string? UserName { get; set; }

    [Required, EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}