using OpenQA.Selenium.Chrome;
using Sirius.Application.Forms;
using Sirius.Application.Interfaces;
using Sirius.Core.Enums;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace Sirius.Infrastructure.Services;

public class FormsService: IFormsService
{
    public async Task<string> GetWebsiteContentFromUrl(string uri)
    {
        var client = GetHttpClient();
        return await client.GetStringAsync(uri);
    }
    
    public async Task<FormType> GetFormTypeFromContent(string content, string url)
    {
        if (content.StartsWith("%pdf-1.", StringComparison.CurrentCultureIgnoreCase))
            return FormType.Pdf;
       
        if (content.Contains("<form", StringComparison.CurrentCultureIgnoreCase) && content.Contains("<input", StringComparison.CurrentCultureIgnoreCase))
            return FormType.Webform;
        
        if (await IsUrlLeadingToImage(url))
            return FormType.Image;
        
        return FormType.Unspecified;
    }

    public string GetScreenshotFromUrlAsBase64(string url)
    {
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArguments("headless");
        
        var vcs = new VerticalCombineDecorator(new ScreenshotMaker());
        
        var driver = new ChromeDriver(chromeOptions);
        driver.Navigate().GoToUrl(url);
        
        var file = driver.TakeScreenshot(vcs);
        return Convert.ToBase64String(file);
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
    
}