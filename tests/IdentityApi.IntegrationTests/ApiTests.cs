using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
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
        var payload = new UserModel(
            "test@test.com",
            "test_user",
            "Password1."
        );
        using (var scope = identityApi.Services.CreateScope())
        {
            CreateDb(scope);
        }
        var response = await client.PostAsJsonAsync<UserModel>("/login", payload);
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
        var payload = new UserModel(
            "test@test.com",
            "test_user",
            "Password1."
        );
        using (var scope = identityApi.Services.CreateScope())
        {
            CreateDb(scope);
        }
        var response = await client.PostAsJsonAsync<UserModel>("/register", payload);
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

internal sealed class IdentityApi : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApiDbContext>));
            services.AddDbContext<ApiDbContext>(options =>
                options.UseInMemoryDatabase("TestDb", root));
        });

        return base.CreateHost(builder);
    }
}