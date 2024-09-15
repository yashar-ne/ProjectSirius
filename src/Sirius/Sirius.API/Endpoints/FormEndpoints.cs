using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sirius.Scraping.Application.Forms;
using Sirius.Scraping.Application.Forms.Queries;

namespace Sirius.API.Endpoints;

public class FormEndpoints : IEndpoint
{
    public void RegisterEndpoints(WebApplication app)
    {
        var posts = app.MapGroup("/forms");
        
        posts.MapGet("/{id:int}", GetFormById)
            .WithName("GetFormById")
            .WithOpenApi();
    }

    private Task<FormDto> GetFormById([FromServices] IMediator mediator, [FromRoute] int id)
    {
        return mediator.Send(new GetFormQuery(id));
    }
}