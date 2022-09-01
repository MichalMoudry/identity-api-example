namespace IdentityApi.Commands.Api;

/// <summary>
/// Handler for a specific command.
/// </summary>
public interface ICommandHandler<T> where T : ICommand
{
    /// <summary>
    /// Method for executing a specific command.
    /// </summary>
    Task Handle(T command);
}