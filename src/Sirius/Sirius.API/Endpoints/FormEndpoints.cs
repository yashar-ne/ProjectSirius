namespace Sirius.API.Endpoints;

public class FormEndpoints : IEndpoint
{
    public void RegisterEndpoints(WebApplication app)
    {
        var posts = app.MapGroup("/api/forms");

        posts.MapGet("/{id:int}", GetFormById).WithName("GetFormById").WithOpenApi();
    }

    private int GetFormById(HttpContext context, int id)
    {
        return id;
    }
}