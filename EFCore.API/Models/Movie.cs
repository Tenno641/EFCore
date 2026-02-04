using System.Text.Json.Serialization;

namespace EFCore.API.Models;

public class Movie
{
    public int Id { get; init; }
    public string? Title { get; set; }    
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
    
    [JsonIgnore]
    public Genre Genre { get; set; }
    public int GenreId { get; set; }
}

public class MovieTitle
{
    public int Id { get; set; }
    public string? Title { get; set; }
}