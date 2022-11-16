using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Infrastructure.Repositories;

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
    /// <param name="password">User's password.</param>
    /// <returns>Result of creating a new user operation.</returns>
    Task<(IdentityResult, IdentityUser)> CreateUserAsync(string? userName, string? email, string? password);

     /// <summary>
    /// Method for adding user to roles.
    /// </summary>
    /// <param name="user">An instance of <seealso cref="IdentityUser" /> class.</param>
    /// <param name="roleNames">Roles that will be added to a user.</param>
    /// <returns>Result of adding a user to a role.</returns>
    Task<IdentityResult> AddUserToRolesAsync(IdentityUser user, params string[] roleNames);

    /// <summary>
    /// Method for obtaining user data from the database.
    /// </summary>
    /// <param name="email">User's email.</param>
    /// <returns>User data (including roles).</returns>
    Task<(IdentityUser, IList<string>?)> GetUserByEmailAsync(string? email);

    /// <summary>
    /// Method for obtaining user data from the database.
    /// </summary>
    /// <param name="userName">User's name.</param>
    /// <returns>User data (including roles).</returns>
    Task<(IdentityUser, IList<string>?)> GetUserByUserName(string? userName);

    /// <summary>
    /// Method for obtaining user data from the database.
    /// </summary>
    /// <param name="email">User's ID.</param>
    /// <returns>User data (including roles).</returns>
    Task<(IdentityUser, IList<string>?)> GetUserById(string? id);

    /// <summary>
    /// Method for deleting a user from the database.
    /// </summary>
    /// <param name="email">User's email.</param>
    /// <returns>Operation (deletion of a user) result.</returns>
    Task<IdentityResult> DeleteUser(string? email);

    /// <summary>
    /// Method for reseting user's password.
    /// </summary>
    /// <param name="id">User's unique ID.</param>
    /// <param name="newPassword">User's new password.</param>
    /// <returns>Operation (password reset) result.</returns>
    Task<IdentityResult> ResetPassword(string? id, string? newPassword);
}