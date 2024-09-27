using MediatR;
using Sirius.Core.Enums;
using OpenQA.Selenium.Chrome;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace Sirius.Application.Forms.Queries;

public record GetFormDataFromUrlQuery(string Url) : IRequest<FormDataDto>;

public class GetFormDataFromUrlQueryHandler(IFormsService formsService) : IRequestHandler<GetFormDataFromUrlQuery, FormDataDto>
{
    public async Task<FormDataDto> Handle(GetFormDataFromUrlQuery request, CancellationToken cancellationToken)
    {
        var content = await formsService.GetWebsiteContentFromUrl(request.Url);
        var formType = await formsService.GetFormTypeFromContent(content, request.Url);
        var screenshot = formsService.GetScreenshotFromUrlAsBase64(request.Url);
       
        return new FormDataDto(request.Url, formType, content, screenshot);
    }
}