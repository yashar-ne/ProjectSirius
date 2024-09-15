using Microsoft.Extensions.DependencyInjection;
using Sirius.Application.Forms;
using Sirius.Infrastructure.Repositories;

namespace Sirius.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IFormsRepository, FormsRepository>();
        return services;
    }
}