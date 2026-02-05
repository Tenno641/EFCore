using EFCore.API.Data.ValueGenerators;
using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.API.Data.EntityMappings;

public class GenreMappings : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder
            .Property(genre => genre.Name)
            .HasMaxLength(128);
        
        /*builder
            .Property(genre => genre.CreatedAt)
            .HasDefaultValueSql("now()");*/

        builder
            .Property<DateTime>("CreatedAt")
            .HasColumnName("CreatedAt")
            .HasValueGenerator<DateTimeValueGenerator>();
        
        // Seed(builder); Causing Infinity Time!
    }
    private void Seed(EntityTypeBuilder<Genre> builder)
    {
        builder.HasData(new Genre()
        {
            Id = 1,
            Name = "Defined-Name"
        });

        builder.HasData(new Genre()
        {
            Id = 2,
            Name = "Defined-Name"
        });
    }
}