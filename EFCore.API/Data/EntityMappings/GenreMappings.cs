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
    }
}