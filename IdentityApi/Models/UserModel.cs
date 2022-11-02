namespace IdentityApi.Models;

using System.ComponentModel.DataAnnotations;
using IdentityApi.Models.Api;
using IdentityApi.Validators;

/// <summary>
/// A model class for user's login.
/// </summary>
public sealed class UserModel : IValidatedModel
{
    [Required, MinLength(length: 3)]
    public string? UserName { get; set; }

    [Required, EmailAddress]
    public string? Email { get; set; }

    [Required, MinLength(length: 7)]
    public string? Password { get; set; }

    public FluentValidation.Results.ValidationResult Validate()
    {
        var validator = new UserModelValidator();
        return validator.Validate(this);
    }
}