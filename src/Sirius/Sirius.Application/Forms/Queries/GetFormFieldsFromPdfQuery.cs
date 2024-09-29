using MediatR;
using Sirius.Application.Interfaces;

namespace Sirius.Application.Forms.Queries;

public record GetFormFieldsFromPdfQuery(byte[] Pdf): IRequest<IEnumerable<FormFieldDto>>;

public class GetFormFieldsFromPdfQueryHandler(IMlService mlService) : IRequestHandler<GetFormFieldsFromPdfQuery, IEnumerable<FormFieldDto>>
{
    public async Task<IEnumerable<FormFieldDto>> Handle(GetFormFieldsFromPdfQuery request, CancellationToken cancellationToken)
    {
        return await mlService.GetFormFieldsFromPdf(request.Pdf);
    }
}