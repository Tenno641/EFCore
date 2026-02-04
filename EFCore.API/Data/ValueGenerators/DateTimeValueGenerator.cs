using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace EFCore.API.Data.ValueGenerators;

public class DateTimeValueGenerator : ValueGenerator<DateTime>
{
    // can inspect the entire entity 
    public override DateTime Next(EntityEntry entry)
    {
        return DateTime.UtcNow;
    }
    public override bool GeneratesTemporaryValues => false;
}