namespace IdentityApi.Validators;

using FluentValidation;
using IdentityApi.Models;

public sealed class RegisterModelValidator : AbstractValidator<RegisterModel>
{
    public RegisterModelValidator()
    {
        RuleFor(x => x.UserName).NotNull().NotEmpty().MinimumLength(3);
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(7);
    }
}