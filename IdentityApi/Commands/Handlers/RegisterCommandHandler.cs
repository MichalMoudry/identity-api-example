namespace IdentityApi.Commands.Handlers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityApi.Commands;
using IdentityApi.Commands.Api;

/// <summary>
/// A handler class for a <see cref="RegisterCommand"/>.
/// </summary>
public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    /// <inheritdoc />
    public async Task Handle(RegisterCommand command)
    {
        var user = new IdentityUser()
        {
            UserName = command.UserName,
            Email = command.Email
        };
        await _userManager.CreateAsync(user, command.Password);
        var result = await _userManager.AddToRoleAsync(user, "user");
    }
}