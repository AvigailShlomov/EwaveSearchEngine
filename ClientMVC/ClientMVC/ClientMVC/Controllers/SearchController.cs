using ClientMVC.Models;
using ClientMVC.Services;
using ClientMVC.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientMVC.Controllers;

public class SearchController : Controller
{
    private static SearchViewModel searchModel = new SearchViewModel
    {
        SearchQuery = "",
        IsGoogleSelected = false,
        IsBingSelected = false,
        IsCardView = true,
        Results = new List<SearchResult>()
    };

    private readonly SearchService _searchService;

    public SearchController(SearchService searchService)
    {
        _searchService = searchService;
    }

    public ActionResult Index()
    {
        return View(searchModel);
    }

    [HttpPost]
    public async Task<ActionResult> Index(SearchViewModel model)
    {
        if (string.IsNullOrEmpty(model.SearchQuery))
        {
            ViewData["error"] = "Please enter your search query.";
            model.Results = new List<SearchResult>();
            return View(model);
        }

        if (!model.IsGoogleSelected && !model.IsBingSelected)
        {
            ViewData["error"] =  "Please choose serach engine.";
            model.Results = new List<SearchResult>();
            return View(model);
        }

        ViewData["error"] = "";

        var results = new List<SearchResult>();

        var googleList = model.IsGoogleSelected ? await _searchService.GetSearchResultsAsync(model.SearchQuery, "google") :
            new List<SearchResult>();
        var bingList = model.IsBingSelected ? await _searchService.GetSearchResultsAsync(model.SearchQuery, "bing") :
            new List<SearchResult>();

        results.AddRange(googleList);
        results.AddRange(bingList);

        searchModel = new SearchViewModel
        {
            Results = results,
            IsGoogleSelected = model.IsGoogleSelected,
            IsBingSelected = model.IsBingSelected,
            SearchQuery = model.SearchQuery,
            IsCardView = true,
        };
        return View(searchModel);
    }

    [HttpPost]
    public ActionResult ChangeView(SearchViewModel model, string name)
    {
        searchModel = new SearchViewModel
        {
            SearchQuery = model.SearchQuery,
            IsGoogleSelected = model.IsGoogleSelected,
            IsBingSelected = model.IsBingSelected,
            IsCardView = name == "Card",
            Results = searchModel.Results
        };

        return View("Index", searchModel);
    }
}