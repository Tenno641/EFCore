using EFCore.API.Data.EntityMappings;
using EFCore.API.Models;
using EFCore.API.Tenants;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Data;

public class MoviesContext : DbContext
{
    private readonly ITenantService _tenantService;
    public MoviesContext() { } // For Testing Purposes
    public MoviesContext(DbContextOptions<MoviesContext> options, ITenantService tenantService) : base(options)
    {
        _tenantService = tenantService;
    }

    public string? TenantId => _tenantService.GetTenantId();
    public DbSet<Movie> Movies { get; init; }
    public DbSet<ExternalInformation> ExternalInformation { get; set; }
    public virtual DbSet<Genre> Genres { get; init; } // Virtual For Testing Purposes
    public DbSet<Actor> Actors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new MovieMappings());
        modelBuilder.ApplyConfiguration(new GenreMappings(this));
        modelBuilder.ApplyConfiguration(new ExternalInformationMappings());
        modelBuilder.ApplyConfiguration(new ActorMappings());
    }
}