using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using IdentityApi.Infrastructure;

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