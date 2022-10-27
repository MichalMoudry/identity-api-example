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

namespace IdentityApi.IntegrationTests;

public sealed class ApiTests
{
    [Fact]
    public void Test1()
    {

    }
}

internal sealed class IdentityApi : WebApplicationFactory<Program>
{
    
}