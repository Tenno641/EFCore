using EFCore.API.Controllers;
using EFCore.API.Data;
using EFCore.API.Models;
using EFCore.API.Repositories;
using EFCore.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace EFCore.Test;

public class GenreFakeDbSetTests
{
    [Fact]
    public async Task GetGenre_SuccessfulResult_ExistingId()
    {
        // Arrange
        MoviesContext moviesContext = CreateFakeDbContext();
        GenreController controller = CreateMockedController();
        
        // Act
        IActionResult genre = await controller.Get(2);
        OkObjectResult? result = genre as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(result.StatusCode, 200);
        Assert.Equal("Comedy", (result.Value as Genre)?.Name);
    }
    
    private MoviesContext CreateFakeDbContext()
    {
        List<Genre> genres =
        [
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
                Name = "sci-fi"
            }
        ];

        Mock<DbSet<Genre>> genresDbSetMock = genres.BuildMockDbSet();
        genresDbSetMock.Setup(dbSet => dbSet.FindAsync(2))
            .ReturnsAsync(new Genre
            {
                Id = 2,
                Name = "Comedy"
            });
        
        Mock<MoviesContext> moviesContextMock = new Mock<MoviesContext>();
        moviesContextMock.Setup(context => context.Genres)
            .Returns(genresDbSetMock.Object);
    
        return moviesContextMock.Object;
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