namespace IdentityApi.Dispatchers;

using System.Threading.Tasks;
using IdentityApi.Commands.Api;

/// <summary>
/// Dispatcher class for commands.
/// </summary>
public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public async Task Send<T>(T command) where T : ICommand
    {
        var handler = _serviceProvider.GetService(typeof(ICommandHandler<T>));
        if (handler != null)
        {
            await ((ICommandHandler<T>)handler).Handle(command);
        }
        else
        {
            throw new Exception("No command handler has been found.");
        }
    }
}