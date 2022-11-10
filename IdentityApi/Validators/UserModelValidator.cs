namespace IdentityApi.Validators;

using FluentValidation;
using IdentityApi.Models;

public sealed class UserModelValidator : AbstractValidator<UserModel>
{
    public UserModelValidator()
    {
        RuleFor(x => x.UserName).NotNull().NotEmpty().MinimumLength(3);
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(7);
    }
}