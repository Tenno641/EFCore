using System.Globalization;
using EFCore.API.Data.ValueConvertors;
using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.API.Data.EntityMappings;

public class MovieMappings : IEntityTypeConfiguration<Movie>
{
    private const string DateTimeFormat = "yyyyMMdd";
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

        builder
            .Property(movie => movie.ReleaseDate)
            .HasColumnType("char(8)")
            .HasConversion(dateTimeValue => dateTimeValue.ToString(DateTimeFormat, CultureInfo.InvariantCulture),
                dateTimeValue => DateTime.ParseExact(dateTimeValue, DateTimeFormat, CultureInfo.InvariantCulture));
        
        Seed(builder);

        /* Custom Convertor
        builder
            .Property(movie => movie.ReleaseDate)
            .HasColumnType("char(8)")
            .HasConversion(new DateTimeConvertor());
        */
    }
    private void Seed(EntityTypeBuilder<Movie> builder)
    {
        builder.HasData(new Movie()
        {
            Id = 1,
            ReleaseDate = new DateTime(1999, 5, 25),
            Synopsis = "Defined-Data",
            Title = "Defined-Title",
            GenreId = 1,
            AgeRating = AgeRating.Adult
        });

        builder.HasData(new Movie()
        {
            Id = 2,
            ReleaseDate = new DateTime(1999, 5, 25),
            Synopsis = "Defined-Data",
            Title = "Defined-Title",
            GenreId = 2,
            AgeRating = AgeRating.Kids
        });
    }
}