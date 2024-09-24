using Sirius.Core.Enums;


namespace Sirius.Application.Forms;

public record FormDto : BaseDto<int>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public FormType? FormType { get; set; }
    public string? Url { get; set; }
}
    