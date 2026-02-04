using EFCore.API.Data;
using EFCore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : Controller
{
    private readonly MoviesContext _moviesContext;
    public MoviesController(MoviesContext moviesContext)
    {
        _moviesContext = moviesContext;
    } 
    
    [HttpGet]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _moviesContext.Movies
            .Include(movie => movie.Genre)
            .ToListAsync();

        return Ok(movies);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        Movie? movie = await _moviesContext.Movies
            .Include(movie => movie.Genre)
            .FirstOrDefaultAsync(movie => movie.Id == id);
        
        if (movie is null)
            return NotFound();

        return Ok(movie);
    }

    [HttpGet("until-age{ageRating}")]
    public async Task<IActionResult> GetUntilAge(AgeRating ageRating)
    {
        var movies = await _moviesContext.Movies
            .Where(movie => movie.AgeRating <= ageRating)
            .Include(movie => movie.Genre)
            .Select(movie => new MovieTitle() { Title = movie.Title, Id = movie.Id })
            .ToListAsync();

        return Ok(movies);
    }

    [HttpGet("on-year/{year:int}")]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetYear(int year)
    {
        List<MovieTitle> movieTitles = await _moviesContext.Movies
            .Include(movie => movie.Genre)
            .Where(movie => movie.ReleaseDate.Year == year)
            .Select(movie => new MovieTitle() { Id = movie.Id, Title = movie.Title })
            .ToListAsync();

        return Ok(movieTitles);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Movie movie)
    {
        await _moviesContext.AddAsync(movie);
        
        await _moviesContext.SaveChangesAsync();
        
        return CreatedAtAction(nameof(Get), new { Id = movie.Id }, movie);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Movie movie)
    {
        Movie? fetchedMovie = await _moviesContext.Movies.FirstOrDefaultAsync(movie => movie.Id == id);
        
        if (fetchedMovie is null)
            return NotFound();

        fetchedMovie.Title = movie.Title;
        fetchedMovie.ReleaseDate = movie.ReleaseDate;
        fetchedMovie.Synopsis = movie.Synopsis;

        await _moviesContext.SaveChangesAsync();

        return Ok(fetchedMovie);
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        Movie? movie = await _moviesContext.Movies.FirstOrDefaultAsync(movie => movie.Id == id);

        if (movie is null)
            return NotFound();

        _moviesContext.Remove(movie);

        await _moviesContext.SaveChangesAsync();

        return Ok();
    }
}