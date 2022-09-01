namespace IdentityApi.Commands.Handlers;

using System.Text;
using System.Threading.Tasks;
using IdentityApi.Commands;
using IdentityApi.Commands.Api;
using IdentityApi.Infrastructure;
using IdentityApi.Validators;

/// <summary>
/// A handler class for a <see cref="RegisterCommand"/>.
/// </summary>
public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    private readonly IApiDbContext _dbContext;

    public RegisterCommandHandler(IApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(RegisterCommand command)
    {
        var validator = new RegisterCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors;
            var message = new StringBuilder().AppendJoin('\n', errors);
            throw new FluentValidation.ValidationException(message.ToString());
        }
        throw new NotImplementedException();
    }
}