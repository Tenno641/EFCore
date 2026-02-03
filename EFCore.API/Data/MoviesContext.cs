using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.API.Data;

public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
}