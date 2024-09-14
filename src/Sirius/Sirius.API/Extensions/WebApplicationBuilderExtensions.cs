using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Sirius.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        var cs = builder.Configuration.GetConnectionString("SQLServer");
        builder.Services.AddDbContext<ScrapingDbContext>(options => {options.UseNpgsql(); });
    }
}