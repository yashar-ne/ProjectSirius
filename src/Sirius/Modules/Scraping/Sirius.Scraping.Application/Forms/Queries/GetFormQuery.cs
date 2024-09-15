using MediatR;
using Sirius.Scraping.Domain.Enums;

namespace Sirius.Scraping.Application.Forms.Queries;

public record GetFormQuery(int FormId) : IRequest<FormDto>;
public class GetFormQueryHandler : IRequestHandler<GetFormQuery, FormDto>
{
    public async Task<FormDto> Handle(GetFormQuery request, CancellationToken cancellationToken)
    {
        return new FormDto(1, "Some Title", "Some Description", FormType.Webform, "https://www.google.com");
    }
}