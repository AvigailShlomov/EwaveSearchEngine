using Microsoft.EntityFrameworkCore;
using SearchEngineServerAPI.Controllers;
using SearchEngineServerAPI.Data;
using SearchEngineServerAPI.Models;

namespace SearchEngineServerAPI.DataAccess
{
    public class SearchDataDA
    {
        private readonly SearchEngineServerAPIContext _context;
        private readonly ILogger<SearchEngineController> _logger;


        public SearchDataDA(SearchEngineServerAPIContext context, ILogger<SearchEngineController> logger)
        {
            _context = context;
            _logger = logger;   
        }

        public IEnumerable<SearchData> Get()
        {
            return _context.SearchData.ToList();
        }

        public void Insert(SearchData data)
        {
            try
            {
                _context.SearchData.Add(data);
            }
            catch (Exception)
            {
                _logger.LogError("Error in Insert to DataBase - SearchDataDA File ");
            }
        }

        public void InsertMany(List<SearchResult> data)
        {
            try
            {
                var convertedData = CreateNewSearchDataList(data);
                _context.SearchData.AddRange(convertedData);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                _logger.LogError("Error in InsertMany to DataBase - SearchDataDA File ");
            }
        }

        private IEnumerable<SearchData> CreateNewSearchDataList(List<SearchResult> searchResultList)
        {
            return searchResultList.Select(searchResult => new SearchData()
            {
                SearchEngine = searchResult.SearchEngineType,
                Title = searchResult.Title,
                EnteredDate = DateTime.Now
            });
        }
    }
}


