namespace EFCore.API.Data;

public interface IUnitOfWorkManager
{
    void StartUnitOfWork();
    bool IsUnitOfWorkStarted { get; }
    Task<int> SaveChangesAsync();
}

public class UnitOfWorkManger : IUnitOfWorkManager
{
    private readonly MoviesContext _moviesContext;
    
    public UnitOfWorkManger(MoviesContext moviesContext)
    {
        _moviesContext = moviesContext;

    }
    
    public bool IsUnitOfWorkStarted { get; private set; }
    
    public void StartUnitOfWork() => IsUnitOfWorkStarted = true;
    
    public async Task<int> SaveChangesAsync() => await _moviesContext.SaveChangesAsync();
}