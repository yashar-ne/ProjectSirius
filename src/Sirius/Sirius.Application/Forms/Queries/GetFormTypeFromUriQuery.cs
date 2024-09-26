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

        if (content.ToLower().StartsWith("%pdf-1."))
            return new FormTypeDto(FormType.Pdf);
       
        if (content.ToLower().Contains("<form") && content.ToLower().Contains("<input"))
            return new FormTypeDto(FormType.Webform);
        
        if (await IsUrlLeadingToImage(request.Uri))
            return new FormTypeDto(FormType.Image);
        
        return new FormTypeDto(FormType.Unspecified);
    }

    private async Task<string> GetWebsiteContentFromUri(string uri)
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "other");
        return await client.GetStringAsync(uri);
    }
    
    private async Task<bool> IsUrlLeadingToImage(string url)
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "other");
        var site = await client.GetAsync(url);
        if (site.Content.Headers.ContentType.MediaType.Contains("image"))
            return true;
        
        return false;
    }
}