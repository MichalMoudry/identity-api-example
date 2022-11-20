namespace IdentityApi.Models;

using System.ComponentModel.DataAnnotations;
using IdentityApi.Models.Api;
using IdentityApi.Validators;

/// <summary>
/// A model class for user's registration.
/// </summary>
public sealed record class RegisterModel(
    [Required, MinLength(length: 3)]
    string? UserName,
    [Required, EmailAddress]
    string? Email,
    [Required, MinLength(length: 7), DataType(DataType.Password)]
    string? Password
) : IValidatedModel
{
    /// <inheritdoc />
    public FluentValidation.Results.ValidationResult Validate()
    {
        var validator = new RegisterModelValidator();
        return validator.Validate(this);
    }
}
