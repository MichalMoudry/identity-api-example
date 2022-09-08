namespace IdentityApi.Extensions;

/// <summary>
/// Extension class for HTTP routes.
/// </summary>
public static class RouteExtensions
{
    /// <summary>
    /// Adds default status codes metadata to a route handler builder.
    /// </summary>
    public static RouteHandlerBuilder AddDefaultStatusCodes(this RouteHandlerBuilder builder)
    {
        _ = builder
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .ProducesProblem(500);
        return builder;
    }
}