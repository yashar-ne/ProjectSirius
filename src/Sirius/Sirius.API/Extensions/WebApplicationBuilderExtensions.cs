using Common.Data;
using Microsoft.EntityFrameworkCore;
using Sirius.Scraping.Application.Forms.Queries;

namespace Sirius.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        var cs = builder.Configuration.GetConnectionString("SQLServer");
        builder.Services.AddDbContext<ScrapingDbContext>(options => {options.UseNpgsql(); });
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetFormQuery).Assembly));
    }
}