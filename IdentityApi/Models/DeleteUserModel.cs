using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Models;

public sealed record class DeleteUserModel(
    [Required, MinLength(length: 3)]
    string? UserName,
    [Required]
    string? Password
);