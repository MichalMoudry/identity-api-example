namespace IdentityApi.Queries.Api;

public interface IQueryDispatcher
{
    /// <summary>
    /// Method for sending a query to its handler.
    /// </summary>
    /// <param name="query">An instance of a class that implements the <see cref="IQuery"/>.</param>
    /// <returns>An array of instances of a class implementing <see cref="IResult"/>.</returns>
    IResult[] Send<T>(T query) where T : IQuery;
}