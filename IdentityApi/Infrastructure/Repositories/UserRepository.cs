using IdentityApi.Infrastructure.Repositories.Api;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserRepository(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    /// <inheritdoc />
    public async Task<(IdentityResult, IdentityUser)> CreateUserAsync(string? userName, string? email, string? password)
    {
        if (userName == null || email == null || password == null)
        {
            throw new ArgumentNullException();
        }
        var user = new IdentityUser()
        {
            UserName = userName,
            Email = email
        };
        return (await _userManager.CreateAsync(user, password), user);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> AddUserToRolesAsync(IdentityUser? user, params string[] roleNames)
    {
        if (user == null)
        {
            throw new ArgumentNullException("Attempted to add a null user to a role.", nameof(user));
        }
        return await _userManager.AddToRolesAsync(user, roleNames);
    }

    /// <inheritdoc />
    public async Task<(IdentityUser, IList<string>?)> GetUserByEmailAsync(string? email)
    {
        if (email == null)
        {
            throw new ArgumentNullException("Attempted to get user with null email.", nameof(email));
        }
        var user = await _userManager.FindByEmailAsync(email);
        var roles = await _userManager.GetRolesAsync(user);
        return (user, roles);
    }
}