namespace IdentityApi.Validators;

using FluentValidation;
using IdentityApi.Commands;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserName).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty();
    }
}