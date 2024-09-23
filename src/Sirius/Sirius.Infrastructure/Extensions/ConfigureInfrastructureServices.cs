using Microsoft.Extensions.DependencyInjection;
using Sirius.Application;
using Sirius.Infrastructure.Data;

namespace Sirius.Infrastructure.Extensions;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        return services;
    }
}