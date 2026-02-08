using System.Text.Json.Serialization;
using EFCore.API.Data;
using EFCore.API.Models;
using EFCore.API.Repositories;
using EFCore.API.Services;
using EFCore.API.Tenants;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<AgeRating>());
    });

builder.Services.AddTenantOpenApi();

builder.Services.AddDbContext<MoviesContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
    options.EnableSensitiveDataLogging();
    options.LogTo(Console.WriteLine);
});

builder.Services.AddScoped<IBatchService, BatchService>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IUnitOfWorkManager, UnitOfWorkManger>();

builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Lazy to load database and run migrations everytime.
using var scope = builder.Services.BuildServiceProvider().CreateScope();
MoviesContext context = scope.ServiceProvider.GetRequiredService<MoviesContext>();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();