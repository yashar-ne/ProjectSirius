using Sirius.Core.Enums;


namespace Sirius.Application.Forms;

public record FormDto(int Id, string? Title, string? Description, FormType? FormType, string? Url);