namespace Sirius.Application.Forms;

public record FormFieldDto(string Id, string Label, string InputType, string Required, string Placeholder, string? Value, string Options);