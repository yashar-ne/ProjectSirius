using MediatR;
using Sirius.Core.Enums;
using OpenQA.Selenium.Chrome;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace Sirius.Application.Forms.Queries;

public record GetFormDataFromUrlQuery(string Url) : IRequest<FormDataDto>;

public class GetFormDataFromUrlQueryHandler() : IRequestHandler<GetFormDataFromUrlQuery, FormDataDto>
{
    public async Task<FormDataDto> Handle(GetFormDataFromUrlQuery request, CancellationToken cancellationToken)
    {
        var content = await GetWebsiteContentFromUrl(request.Url);
        var formType = await GetFormTypeFromContent(content, request.Url);
        var screenshot = GetScreenshotFromUrlAsBase64(request.Url);
       
        return new FormDataDto(request.Url, formType, content, screenshot);
    }
    
    private async Task<FormType> GetFormTypeFromContent(string content, string url)
    {
        if (content.StartsWith("%pdf-1.", StringComparison.CurrentCultureIgnoreCase))
            return FormType.Pdf;
       
        if (content.Contains("<form", StringComparison.CurrentCultureIgnoreCase) && content.Contains("<input", StringComparison.CurrentCultureIgnoreCase))
            return FormType.Webform;
        
        if (await IsUrlLeadingToImage(url))
            return FormType.Image;
        
        return FormType.Unspecified;
    }

    private async Task<string> GetWebsiteContentFromUrl(string uri)
    {
        var client = GetHttpClient();
        return await client.GetStringAsync(uri);
    }


    private async Task<bool> IsUrlLeadingToImage(string url)
    {
        var client = GetHttpClient();
        var site = await client.GetAsync(url);
        return site.Content.Headers.ContentType is { MediaType: not null } && site.Content.Headers.ContentType.MediaType.Contains("image");
    }
    
    private static HttpClient GetHttpClient()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "other");
        return client;
    }
    
    private string GetScreenshotFromUrlAsBase64(string url)
    {
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArguments("headless");
        
        var vcs = new VerticalCombineDecorator(new ScreenshotMaker());
        
        var driver = new ChromeDriver(chromeOptions);
        driver.Navigate().GoToUrl(url);
        
        var file = driver.TakeScreenshot(vcs);
        return Convert.ToBase64String(file);
    } 
}