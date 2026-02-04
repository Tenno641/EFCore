using EFCore.API.Data;
using EFCore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController : Controller
{
    private readonly MoviesContext _moviesContext;
    public GenreController(MoviesContext moviesContext)
    {
        _moviesContext = moviesContext;
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int id)
    {
        var movies = await _moviesContext.Movies
            .Include(movie => movie.Genre)
            .Where(movie => movie.GenreId == id)
            .ToListAsync();

        return Ok(movies);
    }
}