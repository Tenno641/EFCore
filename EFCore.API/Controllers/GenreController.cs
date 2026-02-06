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

    [HttpGet("genres/{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGenre(int id)
    {
        Genre? genre = await _moviesContext.Genres
            .SingleOrDefaultAsync(genre => genre.Id == id);

        if (genre is null)
            return NotFound();

        return Ok(genre);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(Genre genre)
    {
        await _moviesContext.Genres.AddAsync(genre);

        await _moviesContext.SaveChangesAsync();
        
        return CreatedAtAction(nameof(Get), new { genre.Id }, genre);
    }
}