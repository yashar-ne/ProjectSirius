using Microsoft.Extensions.DependencyInjection;
using Sirius.Application;
using Sirius.Application.Forms;
using Sirius.Application.Interfaces;
using Sirius.Infrastructure.Data;
using Sirius.Infrastructure.Services;

namespace Sirius.Infrastructure.Extensions;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection RegisterConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IFormsService, FormsService>();
        services.AddScoped<IMlService, MlService>();
        return services;
    }
}