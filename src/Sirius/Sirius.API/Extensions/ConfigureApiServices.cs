
using Sirius.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Sirius.Application;
using Sirius.Application.Forms.Queries;

namespace Sirius.API.Extensions;

public static class ConfigureApiServices
{
    public static void RegisterApiServices(this WebApplicationBuilder builder)
    {
        builder.Configuration.GetConnectionString("SQLServer");
        builder.Services.AddDbContext<ApplicationDbContext>(options => {options.UseNpgsql(); });
        
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetFormQuery).Assembly));
    }
}