using Ardalis.GuardClauses;
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
        var user = new IdentityUser()
        {
            UserName = userName,
            Email = email
        };
        return (await _userManager.CreateAsync(user, password), user);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> AddUserToRolesAsync(IdentityUser user, params string[] roleNames)
    {
        return await _userManager.AddToRolesAsync(user, roleNames);
    }

    /// <inheritdoc />
    public async Task<(IdentityUser, IList<string>?)> GetUserByEmailAsync(string? email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var roles = await _userManager.GetRolesAsync(user);
        return (user, roles);
    }

    /// <inheritdoc />
    public async Task<(IdentityUser, IList<string>?)> GetUserById(string? id)
    {
        var user = await _userManager.FindByEmailAsync(id);
        var roles = await _userManager.GetRolesAsync(user);
        return (user, roles);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateUser(string? id, string? email, string? password)
    {
        var isEmailNullOrEmpty = string.IsNullOrEmpty(email);
        var isPasswordNullOrEmpty= string.IsNullOrEmpty(password);
        if (id == null || (isEmailNullOrEmpty && isPasswordNullOrEmpty))
        {
            throw new ArgumentNullException();
        }
        var user = await _userManager.FindByIdAsync(id);
        if (!isEmailNullOrEmpty)
        {
            user.Email = email;
            
        }
        if (!isPasswordNullOrEmpty)
        {
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
        }
        return await _userManager.UpdateAsync(user);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> DeleteUser(string? id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return await _userManager.DeleteAsync(user);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> ResetPassword(string? id, string? password)
    {
        var user = await _userManager.FindByIdAsync(id);
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        return await _userManager.ResetPasswordAsync(user, resetToken, password);
    }
}