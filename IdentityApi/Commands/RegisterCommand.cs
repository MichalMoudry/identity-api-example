namespace IdentityApi.Commands;

using IdentityApi.Commands.Api;

/// <summary>
/// A command for registering a new user.
/// </summary>
public class RegisterCommand : ICommand
{
    public string? UserName { get; set; }

    public string? Email { get; set; }
}