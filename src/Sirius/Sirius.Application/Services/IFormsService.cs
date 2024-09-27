using Sirius.Core.Enums;

namespace Sirius.Application.Forms;

public interface IFormsService
{
    Task<string> GetWebsiteContentFromUrl(string uri);
    Task<FormType> GetFormTypeFromContent(string content, string url);
    string GetScreenshotFromUrlAsBase64(string url);
}