using Sirius.API.Endpoints;

namespace Sirius.API.Extensions;

public static class WebApplicationExtensions
{
    public static void RegisterEndpointDefinitions(this WebApplication app)
    {
        IEnumerable<IEndpoint> endpointDefinitions = typeof(Program).Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpoint)) && t is { IsAbstract: false, IsInterface: false })
            .Select(Activator.CreateInstance)
            .Cast<IEndpoint>();

        foreach(var endpointDef in endpointDefinitions)
        {
            endpointDef.RegisterEndpoints(app);
        }
    }
}