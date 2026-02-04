using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.API.Data.EntityMappings;

public class MovieMappings : IEntityTypeConfiguration<Movie>
{

    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder
            .Property(movie => movie.Title)
            .HasMaxLength(128);

        builder
            .Property(movie => movie.Synopsis)
            .HasMaxLength(256);

        builder
            .Property(movie => movie.ReleaseDate)
            .HasColumnType("date");

        builder
            .HasOne(movie => movie.Genre)
            .WithMany(genre => genre.Movies)
            .HasForeignKey(movie => movie.GenreId);
    }
}