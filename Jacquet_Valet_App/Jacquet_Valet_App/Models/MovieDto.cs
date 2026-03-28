using System.Text.Json.Serialization;

namespace Jacquet_Valet_App.Models;

// Models pour les films correspondants au résultat de l'API
public class MovieDto
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("posterURL")]
    public string? PosterURL { get; set; }
    
    [JsonPropertyName("imdbId")]
    public string? ImdbId { get; set; }
}

