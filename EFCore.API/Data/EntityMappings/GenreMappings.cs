using EFCore.API.Data.ValueGenerators;
using EFCore.API.Models;
using EFCore.API.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.API.Data.EntityMappings;

public class GenreMappings : TenantAwareMapping<Genre>
{
    public GenreMappings(MoviesContext context) : base(context) { }
    
    protected override void ConfigureEntity(EntityTypeBuilder<Genre> builder)
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
        
        builder
            .Property<bool>("Deleted")
            .HasDefaultValue(false);

        builder
            .HasQueryFilter(genre => EF.Property<bool>(genre, "Deleted") == false)
            .HasAlternateKey(genre => genre.Name);
        
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