using System.ComponentModel.DataAnnotations;
using ClientMVC.Models;

namespace ClientMVC.ViewModel;

public class SearchViewModel
{
    public required string SearchQuery { get; set; } // check if required needed
    public bool IsGoogleSelected { get; set; } 
    public bool IsBingSelected { get; set; }
    public bool IsCardView { get; set; }
    public List<SearchResult> Results { get; set; }
}
