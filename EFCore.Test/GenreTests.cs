using EFCore.API.Controllers;
using EFCore.API.Data;
using EFCore.API.Models;
using EFCore.API.Repositories;
using EFCore.API.Tenants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EFCore.Test;

public class GenreInMemoryTests : IDisposable
{
    private readonly SqliteConnection _connection;
    public GenreInMemoryTests()
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
    public async Task GetGenre_SuccessfulResult_ExistingId()
    {
        // Arrange
        MoviesContext context = CreateInMemoryDatabase();
        GenreController controller = CreateMockedController();
        
        // Act
        IActionResult genre = await controller.Get(2);
        OkObjectResult? result = genre as OkObjectResult;
        
        // Assertion 
        Assert.NotNull(result);
        Assert.Equal(result.StatusCode, 200);
        Assert.Equal("Comedy", (result.Value as Genre)?.Name);
    }

    private MoviesContext CreateInMemoryDatabase()
    {
        DbContextOptions<MoviesContext> dbContextOptions = new DbContextOptionsBuilder<MoviesContext>()
            .UseSqlite(_connection)
            .Options;

        Mock<ITenantService> tenantServiceMock = new Mock<ITenantService>();
        tenantServiceMock.Setup(service => service.GetTenantId())
            .Returns("Defined-Tenant");

        return new MoviesContext(dbContextOptions, tenantServiceMock.Object);
    }

    private GenreController CreateMockedController()
    {
        Mock<IGenreRepository> genreRepositoryMock = new Mock<IGenreRepository>();
        genreRepositoryMock.Setup(repo => repo.Get(2))
            .ReturnsAsync(new Genre
            {
                Id = 2,
                Name = "Comedy"
            });
        
        return new GenreController(genreRepositoryMock.Object, null);
    }
}