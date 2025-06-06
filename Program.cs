global using FluentValidation;

using FastEndpoints;
using FastEndpoints.Swagger;

using Microsoft.EntityFrameworkCore; // This is crucial

using Serilog;

using TodoApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseInMemoryDatabase("TodoDb")); // Now this will work

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProperty("Application", "TodoAPI")
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();


// Configure Serilog
// builder.Host.UseSerilog((ctx, lc) => lc
//     .WriteTo.Console()
//     .ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseFastEndpoints();
app.UseSwaggerGen();


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
