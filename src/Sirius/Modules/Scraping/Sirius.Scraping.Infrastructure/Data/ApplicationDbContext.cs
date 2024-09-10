using Microsoft.EntityFrameworkCore;
using Sirius.Scraping.Domain.Entities;

namespace Sirius.Scraping.Infrastructure.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Form> Forms {get; set;}
}