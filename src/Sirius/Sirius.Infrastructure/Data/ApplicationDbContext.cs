using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sirius.Application;
using Sirius.Application.Interfaces;
using Sirius.Core.Entities;

namespace Sirius.Infrastructure.Data;

public class ApplicationDbContext(IConfiguration configuration) : DbContext, IApplicationDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(configuration.GetConnectionString("WebApiDatabase"));
    }
    
    public DbSet<Form> Forms {get; set;}
}