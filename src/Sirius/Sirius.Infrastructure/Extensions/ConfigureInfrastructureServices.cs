using Microsoft.Extensions.DependencyInjection;
using Sirius.Application.Forms;
using Sirius.Infrastructure.Repositories;

namespace Sirius.Infrastructure.Extensions;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IFormsRepository, FormsRepository>();
        return services;
    }
}