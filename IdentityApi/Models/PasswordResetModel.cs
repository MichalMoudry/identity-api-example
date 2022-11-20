using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Models;

/// <summary>
/// Model class for reseting user's password.
/// </summary>
public sealed record class PasswordResetModel(
    [Required, DataType(DataType.Password)]
    string? NewPassword
);