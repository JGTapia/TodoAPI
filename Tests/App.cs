global using System.Net;

global using FastEndpoints;
global using FastEndpoints.Testing;

global using Microsoft.Extensions.DependencyInjection;

global using Shouldly;

global using Xunit;

using FastEndpoints.Swagger;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

using Serilog;

using TodoApi.Data;

namespace TodoApi.Tests;

public class App : AppFixture<Program>
{
    protected override void ConfigureApp(IWebHostBuilder a)
    {
        //only needed when tests are not in a separate project
        //a.UseContentRoot(Directory.GetCurrentDirectory());
    }

    protected override void ConfigureServices(IServiceCollection s)
    {
        // Add any additional services needed for testing here
        s.AddDbContext<TodoDbContext>(options =>
            options.UseInMemoryDatabase("TodoDbTest")); // Use a separate in-memory database for tests
    }
}