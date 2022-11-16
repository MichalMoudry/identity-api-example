using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using IdentityApi.Infrastructure;
using IdentityApi.Models;

namespace IdentityApi.IntegrationTests;

public sealed class ApiTests
{
    /// <summary>
    /// Method for testing login API endpoint.
    /// </summary>
    [Fact, Trait("Category", "IntegrationTest")]
    public async Task TestLogin()
    {
        await using var identityApi = new IdentityApi();
        var client = identityApi.CreateClient();
        var payload = new RegisterModel(
            "test@test.com",
            "test_user",
            "Password1."
        );
        using (var scope = identityApi.Services.CreateScope())
        {
            CreateDb(scope);
        }
        var response = await client.PostAsJsonAsync<RegisterModel>("/login", payload);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /// <summary>
    /// Method for testing registration API endpoint.
    /// </summary>
    [Fact, Trait("Category", "IntegrationTest")]
    public async Task TestRegistration()
    {
        await using var identityApi = new IdentityApi();
        var client = identityApi.CreateClient();
        var payload = new RegisterModel(
            "test@test.com",
            "test_user",
            "Password1."
        );
        using (var scope = identityApi.Services.CreateScope())
        {
            CreateDb(scope);
        }
        var response = await client.PostAsJsonAsync<RegisterModel>("/register", payload);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    /// <summary>
    /// Method for testing password reset API endpoint.
    /// </summary>
    [Fact, Trait("Category", "IntegrationTest")]
    public async Task TestPasswordReset()
    {
        await using var identityApi = new IdentityApi();
        var client = identityApi.CreateClient();
    }

    /// <summary>
    /// Method for testing account edit API endpoint.
    /// </summary>
    [Fact, Trait("Category", "IntegrationTest")]
    public async Task TestAccountEdit()
    {
        await using var identityApi = new IdentityApi();
        var client = identityApi.CreateClient();
    }

    private void CreateDb(IServiceScope? serviceScope)
    {
        if (serviceScope == null)
        {
            throw new ArgumentNullException("Service scope is null during DB creation.", nameof(serviceScope));
        }
        var context = serviceScope.ServiceProvider.GetRequiredService<ApiDbContext>();
        context.Database.EnsureCreated();
    }
}