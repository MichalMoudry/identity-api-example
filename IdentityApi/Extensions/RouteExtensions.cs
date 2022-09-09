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
        builder
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status405MethodNotAllowed)
            .ProducesProblem(500);
        return builder;
    }
}