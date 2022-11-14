using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Models;

/// <summary>
/// Model class for reseting user's password.
/// </summary>
public sealed record class PasswordResetModel(
    [Required, MinLength(36), MaxLength(36)]
    string? Id,
    [Required, DataType(DataType.Password)]
    string? NewPassword
);