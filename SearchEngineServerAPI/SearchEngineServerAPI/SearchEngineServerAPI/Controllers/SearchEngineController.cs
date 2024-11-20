using Microsoft.AspNetCore.Mvc;
using SearchEngineServerAPI.API;
using SearchEngineServerAPI.Data;
using SearchEngineServerAPI.DataAccess;
using SearchEngineServerAPI.Models;
using System.Collections.Generic;

namespace SearchEngineServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchEngineController : ControllerBase
    {

        private readonly SearchEngineServerAPIContext _context;
        private readonly SearchDataDA _searchData;
        private readonly SearchEnginesApi _searchEnginesApi;
        private readonly ILogger<SearchEngineController> _logger;



        public SearchEngineController(SearchEngineServerAPIContext context,
                                    SearchDataDA searchData,
                                    SearchEnginesApi searchEnginesApi,
                                    ILogger<SearchEngineController> logger)
        {
            _context = context;
            _searchData = searchData;
            _searchEnginesApi = searchEnginesApi;
            _logger = logger;
        }

        [HttpPost]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<List<SearchResult>>> PostDataFromSearchEngine([FromBody] SearchRequest request)
        {
            _logger.LogInformation("SearchEngineController Post Method start excuting..");
            if (string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                _logger.LogError("MISSING SEARCH QUERY in post method - Required ! ");
                return BadRequest("Search Query Is Required !");
            }
            if (!(request.EngineName == "google" || request.EngineName == "bing"))
            {
                _logger.LogError("Incorrect engine name entered");
                return BadRequest("Engine Name invalid !");

            }

            var searchResultList = await _searchEnginesApi
                .PostDataFromSearchEngineExternal(request.EngineName, request.SearchQuery);
            _searchData.InsertMany(searchResultList);

            return searchResultList;
        }
    }
}

