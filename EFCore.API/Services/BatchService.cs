using EFCore.API.Data;
using EFCore.API.Models;
using EFCore.API.Repositories;

namespace EFCore.API.Services;

public interface IBatchService
{
    Task<IEnumerable<Genre>> CreateGenres(List<Genre> genres);
}

public class BatchService : IBatchService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IUnitOfWorkManager _unitOfWork;
    
    public BatchService(IUnitOfWorkManager unitOfWork, IGenreRepository genreRepository)
    {
        _unitOfWork = unitOfWork;
        _genreRepository = genreRepository;

    }
    
    public async Task<IEnumerable<Genre>> CreateGenres(List<Genre> genres)
    {
        _unitOfWork.StartUnitOfWork();

        foreach (Genre genre in genres)
        {
            await _genreRepository.Create(genre);
        }

        await _unitOfWork.SaveChangesAsync();
        
        return genres;
    }
}