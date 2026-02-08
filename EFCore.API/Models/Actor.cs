using System.Text.Json.Serialization;

namespace EFCore.API.Models;

public class Actor
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    [JsonIgnore]
    public ICollection<Movie> Movies { get; set; }
}