using Sirius.Core.Enums;

namespace Sirius.Application.Forms;

public record FormDataDto(string? Url, FormType? FormType, string? Content, string ScreenshotAsBase64);