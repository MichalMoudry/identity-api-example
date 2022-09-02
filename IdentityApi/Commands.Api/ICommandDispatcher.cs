namespace IdentityApi.Commands.Api;

/// <summary>
/// Dispatcher for commands.
/// </summary>
public interface ICommandDispatcher
{
    /// <summary>
    /// Method for sending a specific command to its handler.
    /// </summary>
    /// <param name="command">A specific command.</param>
    Task Send<T>(T command) where T : ICommand;
}