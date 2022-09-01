namespace IdentityApi.Commands.Api;

/// <summary>
/// Dispatcher for commands.
/// </summary>
public interface ICommandDispatcher
{
    //https://abdelmajid-baco.medium.com/cqrs-pattern-with-c-a9aff05aae3f
    /// <summary>
    /// Method for sending a specific command to its handler.
    /// </summary>
    /// <param name="command">A specific command.</param>
    Task Send<T>(T command) where T : ICommand;
}