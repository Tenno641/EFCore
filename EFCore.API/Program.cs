using System.Text.Json.Serialization;
using EFCore.API.Data;
using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<AgeRating>());
    });

builder.Services.AddOpenApi();

builder.Services.AddDbContext<MoviesContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

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