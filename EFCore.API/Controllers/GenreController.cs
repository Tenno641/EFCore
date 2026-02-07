using EFCore.API.Models;
using EFCore.API.Repositories;
using EFCore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController : Controller
{
    private readonly IGenreRepository _genreRepository;
    private readonly IBatchService _batchService;
    
    public GenreController(IGenreRepository genreRepository, IBatchService batchService)
    {
        _genreRepository = genreRepository;
        _batchService = batchService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Genre>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Genre> genres = await _genreRepository.GetAll();

        return Ok(genres);

    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        Genre? genre = await _genreRepository.Get(id);

        if (genre is null)
            return NotFound();

        return Ok(genre);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(Genre genre)
    {
        Genre createdGenre = await _genreRepository.Create(genre);
        
        return CreatedAtAction(nameof(Get), new { createdGenre.Id }, createdGenre);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Genre genre)
    {
        Genre? updatedGenre = await _genreRepository.Update(genre.Id, genre);

        if (updatedGenre is null)
            return NotFound();
        
        return Ok(updatedGenre);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        bool isDeleted = await _genreRepository.Delete(id);
        
        return isDeleted ? Ok() : NotFound();
    }

    [HttpPost("batch")]
    [ProducesResponseType(typeof(List<Genre>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAll([FromBody] List<Genre> genres)
    {
        IEnumerable<Genre> createdGenres = await _batchService.CreateGenres(genres);

        return CreatedAtAction(nameof(GetAll), createdGenres);
    }
}