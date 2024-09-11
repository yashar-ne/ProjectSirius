using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sirius.Scraping.Domain.Entities;

namespace Common.Data;

public class ScrapingDbContext: DbContext
{
    protected readonly IConfiguration Configuration;

    public ScrapingDbContext(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
    }
    
    public DbSet<Form> Forms {get; set;}
}