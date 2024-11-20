using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SearchEngineServerAPI.Models;

namespace SearchEngineServerAPI.Data
{
    public class SearchEngineServerAPIContext : DbContext
    {
        public SearchEngineServerAPIContext (DbContextOptions<SearchEngineServerAPIContext> options)
            : base(options)
        {
        }

        public DbSet<SearchEngineServerAPI.Models.SearchData> SearchData { get; set; } = default!;
    }
}
