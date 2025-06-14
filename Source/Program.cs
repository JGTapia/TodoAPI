global using FluentValidation;

using System.Runtime.CompilerServices;

using FastEndpoints;
using FastEndpoints.Swagger;

using Microsoft.EntityFrameworkCore; // This is crucial

using Serilog;

using TodoApi.Data;
using TodoApi.Middleware;

[assembly: InternalsVisibleTo("Tests")]

// Log.Logger = new LoggerConfiguration()
//     .MinimumLevel.Information() // Set the minimum logging level
//     .Enrich.FromLogContext()
//     .Enrich.WithMachineName()
//     .Enrich.WithEnvironmentName()
//     .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
//     //.WriteTo.GrafanaLoki("http://localhost:3100") // Loki's default port
//     .WriteTo.File(new Serilog.Formatting.Json.JsonFormatter(), "logs/log.txt", rollingInterval: RollingInterval.Day)
//     .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Add services to the container
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

// builder.Services.SwaggerDocument(options =>
// {
//     options.DocumentSettings = s =>
//     {
//         s.AddSecurity("Negotiate", new NSwag.OpenApiSecurityScheme
//         {
//             Name = "Authorization",
//             Type = NSwag.OpenApiSecuritySchemeType.Http,
//             Scheme = "Negotiate",
//             In = NSwag.OpenApiSecurityApiKeyLocation.Header,
//             Description = "Windows Authentication using Negotiate scheme"
//         });

//         s.OperationProcessors.Add(new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("Negotiate"));
//     };
// });
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseInMemoryDatabase("TodoDb")); // Now this will work
builder.Services.AddHttpClient();

// builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//     .AddNegotiate(); // Enables Windows Authentication

// builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);

// builder.Services.AddAuthorization();



//builder.Host.UseSerilog();

// Configure Serilog

// Log.Logger = new LoggerConfiguration()
//     .MinimumLevel.Information()
//     .Enrich.FromLogContext()
//     .Enrich.WithMachineName()
//     .Enrich.WithThreadId()
//     .Enrich.WithProperty("Application", "TodoAPI")
//     .WriteTo.Console()
//     .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
//     .CreateLogger();

// builder.Host.UseSerilog();

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

app.UseSerilogRequestLogging(); // Logs request start/stop, status code, duration

app.UseCors("AllowFrontend");
// app.UseAuthentication();
// app.UseAuthorization();

// Configure the HTTP request pipeline
app.UseFastEndpoints(
       c =>
       {
           c.Errors.UseProblemDetails();
       });
app.UseSwaggerGen();

app.UseMiddleware<LoggingMiddleware>();


// Seed mock data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    db.Database.EnsureCreated();
    SeedData.Initialize(db);
}

// Log app start
Log.Information("Todo API is starting...");

app.Run();

public partial class Program;