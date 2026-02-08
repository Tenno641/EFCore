using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.API.Data.EntityMappings;

public class ExternalInformationMappings : IEntityTypeConfiguration<ExternalInformation>
{
    public void Configure(EntityTypeBuilder<ExternalInformation> builder)
    {
        builder
            .HasKey(info => info.MovieId);

        builder
            .HasOne(info => info.Movie)
            .WithOne(movie => movie.ExternalInformation)
            .HasForeignKey<ExternalInformation>(info => info.MovieId);
    }
}