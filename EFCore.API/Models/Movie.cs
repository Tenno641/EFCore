using System.Text.Json.Serialization;

namespace EFCore.API.Models;

public class Movie
{
    public int Id { get; init; }
    public string? Title { get; set; }    
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
    public AgeRating? AgeRating { get; set; }
    public Person? Director { get; set; }
    public ICollection<Actor> Actors { get; set; }
    // Navigation properties 
    [JsonIgnore]
    public Genre? Genre { get; set; }
    public ExternalInformation? ExternalInformation { get; set; }
    // Foreign keys
    public int? GenreId { get; set; }
}

public enum AgeRating
{
    Adult = 18,
    Kids = 6,
    HighSchool = 16
}

public class MovieTitle
{
    public int Id { get; set; }
    public string? Title { get; set; }
}

public class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}