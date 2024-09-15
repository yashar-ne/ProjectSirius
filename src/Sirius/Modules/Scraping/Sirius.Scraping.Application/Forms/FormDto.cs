using Sirius.Scraping.Domain.Enums;

namespace Sirius.Scraping.Application.Forms;

public record FormDto(int Id, string Title, string Description, FormType FormType, string Url);