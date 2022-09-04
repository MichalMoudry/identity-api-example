namespace IdentityApi.Validators;

using FluentValidation;
using IdentityApi.Models;

public class UserModelValidator : AbstractValidator<UserModel>
{
    public UserModelValidator()
    {
        _ = RuleFor(x => x.UserName).NotNull().NotEmpty().MinimumLength(3);
        _ = RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        _ = RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(7);
    }
}