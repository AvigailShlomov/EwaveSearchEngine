using System.Text.Json.Serialization;

namespace ClientMVC.Models;

public class SearchResult
{
    [JsonPropertyName("searchEngineType")]
    public string SearchEngineType { get; set; }

    [JsonPropertyName("link")]
    public string Link { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("snippet")]
    public string Snippet { get; set; }
}