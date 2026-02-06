using EFCore.API.Controllers;
using EFCore.API.Data;
using EFCore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Test;

public class GenreTests : IDisposable
{
    private readonly SqliteConnection _connection;
    public GenreTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        MoviesContext context = CreateInMemoryDatabase();

        context.Database.EnsureCreated();
        
        context.Genres.AddRange(
            new Genre
            {
                Name = "Action"
            },
            new Genre
            {
                Name = "Comedy"
            },
            new Genre
            {
                Name = "Adventure"
            });

        context.SaveChanges();
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
    
    [Fact]
    public async Task Test1()
    {
        // Arrange
        MoviesContext context = CreateInMemoryDatabase();
        GenreController controller = new GenreController(context);
        
        // Act
        IActionResult genre = await controller.GetGenre(2);
        OkObjectResult? result = genre as OkObjectResult;
        
        // Assertion 
        Assert.NotNull(result);
        Assert.Equal(result.StatusCode, 200);
        Assert.Equal("Comedy", (result.Value as Genre)?.Name);
    }

    private MoviesContext CreateInMemoryDatabase()
    {
        DbContextOptions<MoviesContext> dbContextOptions = new DbContextOptionsBuilder<MoviesContext>().UseSqlite(_connection).Options;

        return new MoviesContext(dbContextOptions);
    }
}