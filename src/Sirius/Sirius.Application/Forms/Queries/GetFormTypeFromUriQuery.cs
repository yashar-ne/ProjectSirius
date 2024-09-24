using System.Net;
using System.Text;
using MediatR;
using Sirius.Core.Enums;

namespace Sirius.Application.Forms.Queries;

public record GetFormTypeFromUriQuery(string Uri) : IRequest<FormTypeDto>;

public class GetFormTypeFromUriQueryHandler(IApplicationDbContext context) : IRequestHandler<GetFormTypeFromUriQuery, FormTypeDto>
{
    public async Task<FormTypeDto> Handle(GetFormTypeFromUriQuery request, CancellationToken cancellationToken)
    {
        var content = await GetWebsiteContentFromUri(request.Uri);

        if (content.ToLower().StartsWith("%pdf"))
            return new FormTypeDto(FormType.Pdf);
       
        if (content.ToLower().Contains("<input"))
            return new FormTypeDto(FormType.Webform);
        
        return new FormTypeDto(FormType.Unspecified);
    }

    private async Task<string> GetWebsiteContentFromUri(string uri)
    {
        HttpClient client = new HttpClient();
        return await client.GetStringAsync(uri);
    }
}