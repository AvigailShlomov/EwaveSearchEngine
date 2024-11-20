namespace SearchEngineServerAPI.Models
{
    public class SearchRequest// from mvc
    {
        public string SearchQuery { get; set; }
        public string EngineName { get; set; }
        public List<SearchResult> Results { get; set; }
    }
}



