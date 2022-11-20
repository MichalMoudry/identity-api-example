using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Models;

public sealed record class LoginModel(
    [Required, MinLength(length: 3)]
    string? UserName,
    [Required, MinLength(length: 7), DataType(DataType.Password)]
    string? Password
);
