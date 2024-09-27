using MediatR;
using Sirius.Application.Interfaces;

namespace Sirius.Application.Forms.Queries;

public record GetFormFieldsFromSourceCodeQuery(string SourceCode): IRequest<IEnumerable<FormFieldDto>>;

public class GetFormFieldsFromSourceCodeQueryHandler(IMlService mlService) : IRequestHandler<GetFormFieldsFromSourceCodeQuery, IEnumerable<FormFieldDto>>
{
    public async Task<IEnumerable<FormFieldDto>> Handle(GetFormFieldsFromSourceCodeQuery request, CancellationToken cancellationToken)
    {
        return await mlService.GetFormFieldsFromSourceCode(request.SourceCode);
    }
}