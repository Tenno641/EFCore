using EFCore.API.Data.EntityMappings;
using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Data;

public class MoviesContext : DbContext
{
    public MoviesContext() { } // For Testing Purposes
    public MoviesContext(DbContextOptions<MoviesContext> options) : base(options) { }
    public DbSet<Movie> Movies { get; set; }
    public virtual DbSet<Genre> Genres { get; set; } // Virtual For Testing Purposes

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new MovieMappings());
        modelBuilder.ApplyConfiguration(new GenreMappings());
    }
}