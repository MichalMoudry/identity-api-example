namespace IdentityApi.Validators;

using FluentValidation;
using IdentityApi.Models;

public class RegisterCommandValidator : AbstractValidator<RegisterModel>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserName).NotNull().NotEmpty().MinimumLength(3);
        RuleFor(x => x.Email).NotNull().NotEmpty().MinimumLength(5);
        RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(7);
    }
}