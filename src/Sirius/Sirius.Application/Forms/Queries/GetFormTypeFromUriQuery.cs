using MediatR;
using Sirius.Core.Enums;

namespace Sirius.Application.Forms.Queries;

public record GetFormTypeFromUriQuery(string Uri) : IRequest<FormTypeDto>;

public class GetFormTypeFromUriQueryHandler(IApplicationDbContext context) : IRequestHandler<GetFormTypeFromUriQuery, FormTypeDto>
{
    public Task<FormTypeDto> Handle(GetFormTypeFromUriQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private string GetWebsiteContentFromUri(Uri uri)
    {
        return String.Empty;
    }
}
