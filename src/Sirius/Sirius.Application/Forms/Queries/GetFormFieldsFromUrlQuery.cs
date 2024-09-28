using MediatR;
using Sirius.Application.Interfaces;

namespace Sirius.Application.Forms.Queries;

public record GetFormFieldsFromUrlQuery(string Url): IRequest<IEnumerable<FormFieldDto>>;

public class GetFormFieldsFromUrlQueryHandler(IMlService mlService) : IRequestHandler<GetFormFieldsFromUrlQuery, IEnumerable<FormFieldDto>>
{
    public async Task<IEnumerable<FormFieldDto>> Handle(GetFormFieldsFromUrlQuery request, CancellationToken cancellationToken)
    {
        return await mlService.GetFormFieldsFromSourceCode(request.Url);
    }
}