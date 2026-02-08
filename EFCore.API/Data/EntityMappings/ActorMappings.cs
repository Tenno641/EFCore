using EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.API.Data.EntityMappings;

public class ActorMappings : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder
            .HasMany(actor => actor.Movies)
            .WithMany(movie => movie.Actors)
            .UsingEntity("Movie_Actor",
                left => left // mapping from the link table to which table
                    .HasOne(typeof(Movie))
                    .WithMany()
                    .HasForeignKey("MovieId") // foreign key in the intermediate/link table
                    .HasPrincipalKey(nameof(Movie.Id)) // key which the foreign key referencing 
                    .HasConstraintName("FK_MovieActor_Movie") // foreign ket constrain name
                    .OnDelete(DeleteBehavior.Cascade),
                right => right
                    .HasOne(typeof(Actor))
                    .WithMany()
                    .HasForeignKey("ActorId")
                    .HasPrincipalKey(nameof(Actor.Id))
                    .HasConstraintName("FK_MovieActor_Actor")
                    .OnDelete(DeleteBehavior.Cascade), 
                linkBuilder => linkBuilder.HasKey("MovieId", "ActorId")); // link entity builder
    }
}