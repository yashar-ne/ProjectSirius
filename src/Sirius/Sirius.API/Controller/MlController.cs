using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sirius.Application.Forms.Queries;

namespace Sirius.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class MlController : Microsoft.AspNetCore.Mvc.Controller
{
    [HttpPost]
    [Route("GetFormFieldsFromSourceCode")]
    public async Task<IResult> GetFormFieldsFromSourceCode([FromServices] IMediator mediator, [FromBody] string sourceCode)
    {
        return Results.Ok(await mediator.Send(new GetFormFieldsFromSourceCodeQuery(sourceCode)));
    }
    
    [HttpPost]
    [Route("GetFormFieldsFromUrl")]
    public async Task<IResult> GetFormFieldsFromUrl([FromServices] IMediator mediator, [FromBody] string url)
    {
        return Results.Ok(await mediator.Send(new GetFormFieldsFromUrlQuery(url)));
    }
}