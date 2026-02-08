using System.Text.Json.Serialization;
using EFCore.API.Data;
using EFCore.API.Models;
using EFCore.API.Repositories;
using EFCore.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<AgeRating>());
    });

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, _, _) =>
    {
        foreach (var path in document.Paths.Values)
        {
            if (path.Operations is null) continue;
            foreach (var operation in path.Operations.Values)
            {
                if (operation.Parameters is null)
                    operation.Parameters = new List<IOpenApiParameter>();
                
                operation.Parameters.Add(new OpenApiParameter
                {
                    In = ParameterLocation.Header,
                    Name = "X-Tenant",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = JsonSchemaType.String
                    },
                });        
            }
        }
         
        return Task.CompletedTask;
    });
});

builder.Services.AddDbContext<MoviesContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

builder.Services.AddScoped<IBatchService, BatchService>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IUnitOfWorkManager, UnitOfWorkManger>();

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