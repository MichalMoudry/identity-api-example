using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Infrastructure.Repositories.Api;

/// <summary>
/// Interface for a user repository.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Method for adding a new user to the database.
    /// </summary>
    /// <param name="userName">User's name.</param>
    /// <param name="email">User's email.</param>
    /// <returns>Result of creating a new user operation.</returns>
    public Task<IdentityResult> CreateUserAsync(string userName, string email);

    /// <summary>
    /// Method for user data from the database.
    /// </summary>
    /// <param name="email">User's email.</param>
    /// <returns>User data (including roles).</returns>
    public Task<(IdentityUser, IReadOnlyList<string>?)> GetUserByEmailAsync(string email);
}