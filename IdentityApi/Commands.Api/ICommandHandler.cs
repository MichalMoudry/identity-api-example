namespace IdentityApi.Commands.Api;

/// <summary>
/// An interface for a handler used for a specific command.
/// </summary>
public interface ICommandHandler<T> where T : ICommand
{
    /// <summary>
    /// Method for executing a specific command.
    /// </summary>
    Task Handle(T command);
}