namespace ClientMVC.Models
{
    public class BodyForServer
    {
        public string SearchQuery { get; set; }
        public string EngineName { get; set; }
        public List<SearchResult> Results { get; set; }
    }
}
