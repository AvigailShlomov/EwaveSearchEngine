using Microsoft.EntityFrameworkCore;
using SearchEngineServerAPI.Data;
using SearchEngineServerAPI.DataAccess;
using SearchEngineServerAPI.API;
using Serilog;
using Microsoft.AspNetCore.Mvc;



namespace SearchEngineServerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SearchEngineServerAPIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SearchEngineServerAPIContext") ?? throw new InvalidOperationException("Connection string 'SearchEngineServerAPIContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<SearchDataDA>();
            builder.Services.AddScoped<SearchEnginesApi>();

            builder.Services.AddHttpClient();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddResponseCaching();
            builder.Services.AddControllers(options =>
            {
                options.CacheProfiles.Add("Default30",
                    new CacheProfile()
                    {
                        Duration = 30
                    });
            });
            builder.Host.UseSerilog((context, services, loggerConfiguration) =>
             loggerConfiguration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day));

            var app = builder.Build();

            app.MapGet("/", () =>
            {
                Log.Information("This is a test log!");
                return "Hello World!";
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseResponseCaching();



            app.MapControllers();


            app.Run();
        }
    }
}
