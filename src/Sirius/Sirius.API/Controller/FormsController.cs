using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sirius.Application.Forms.Queries;

namespace Sirius.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class FormsController : Microsoft.AspNetCore.Mvc.Controller
{
    [HttpGet("{id:int}")]
    public async Task<IResult> GetFormById([FromServices] IMediator mediator, [FromRoute] int id)
    {
        return Results.Ok(await mediator.Send(new GetFormQuery(id)));
    }
    
    [HttpPost]
    public async Task<IResult> GetDataFromUrl([FromServices] IMediator mediator, [FromBody] string url)
    {
        return Results.Ok(await mediator.Send(new GetFormDataFromUrlQuery(url)));
    }
    
    [HttpGet]
    public async Task<IResult> GetChatCompletionTest([FromServices] IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetFormFieldsFromSourceCodeQuery("<html></html>")));
    }
}