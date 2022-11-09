using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Models;

/// <summary>
/// Model class for deleting a user in the system.
/// </summary>
public sealed record class DeleteUserModel(
    [Required, MinLength(length: 3)]
    string? UserName,
    [Required]
    string? Password
);