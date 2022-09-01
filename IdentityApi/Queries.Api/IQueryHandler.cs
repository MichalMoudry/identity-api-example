namespace IdentityApi.Queries.Api;

public interface IQueryHandler<T> where T : IQuery
{
    /// <summary>
    /// Method that handles a specific query.
    /// </summary>
    /// <param name="query">An instance of a class that implements the <see cref="IQuery"/>.</param>
    /// <returns>An array of instances of a class implementing <see cref="IResult"/>.</returns>
    IResult[] Handle(T query);
}