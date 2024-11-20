using System.Text;
using System.Text.Json;
using Azure.Core;
using ClientMVC.Models;
using ClientMVC.ViewModel;

namespace ClientMVC.Services;

public class SearchService
{
    private readonly HttpClient _client;

    public SearchService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<SearchResult>> GetSearchResultsAsync(string searchQuery, string engineName)
    {
        string requestUri = "https://localhost:7297/api/SearchEngine"; //TODO: place as constant


        var body = new BodyForServer
        {
            SearchQuery = searchQuery,
            EngineName = engineName,
            Results = new List<SearchResult>() // empty list
        };

        var jsonContent = JsonSerializer.Serialize(body);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(requestUri, httpContent);

        if (response.IsSuccessStatusCode)
        {
            var resData = await response.Content.ReadAsStringAsync();
            var results = JsonSerializer.Deserialize<List<SearchResult>>(resData);
            return results ?? new List<SearchResult>();
        }

        return new List<SearchResult>();
    }
}
