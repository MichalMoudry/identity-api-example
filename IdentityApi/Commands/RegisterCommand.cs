namespace IdentityApi.Commands;

using System.ComponentModel.DataAnnotations;
using IdentityApi.Commands.Api;

/// <summary>
/// A command for registering a new user.
/// </summary>
public class RegisterCommand : ICommand
{
    [MinLength(3)]
    public string? UserName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [MinLength(7)]
    public string? Password { get; set; }
}