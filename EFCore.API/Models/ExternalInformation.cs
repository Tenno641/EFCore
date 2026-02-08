using System.Text.Json.Serialization;

namespace EFCore.API.Models;

public class ExternalInformation
{
    public int MovieId { get; set; }
    public string? Imdb { get; set; }
    
    public string? RottenTomatoes { get; set; }
    
    [JsonIgnore]
    public Movie Movie { get; set; }
}