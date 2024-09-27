using Sirius.Core.Enums;

namespace Sirius.Application.Forms;

public record FormDataDto(string? Url, FormType? FormType, string? SourceCode, string ScreenshotAsBase64);