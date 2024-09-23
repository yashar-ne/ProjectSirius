using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sirius.Application;
using Sirius.Core.Entities;

namespace Sirius.Infrastructure.Data;

public class ApplicationDbContext(IConfiguration configuration) : DbContext, IApplicationDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(configuration.GetConnectionString("WebApiDatabase"));
    }
    
    public DbSet<Form> Forms {get; set;}
}