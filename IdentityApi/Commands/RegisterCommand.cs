namespace IdentityApi.Commands;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// A command for registering a new user.
/// </summary>
public class RegisterCommand
{
    [Required]
    public string? UserName { get; set; }

    [Required, EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}