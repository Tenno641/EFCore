using EFCore.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.API.Tenants;

public abstract class TenantAwareMapping<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
{
    private const string TenantColumnName = "tenant_id";
    private readonly MoviesContext _context;
    
    protected TenantAwareMapping(MoviesContext context) 
    {
        _context = context;
    }
    
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .Property<string?>(TenantColumnName)
            .HasMaxLength(128);

        builder
            .HasIndex(TenantColumnName);

        builder
            .HasQueryFilter(entity =>
                EF.Property<string>(entity, TenantColumnName)
                == _context.TenantId);
        
        ConfigureEntity(builder);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}