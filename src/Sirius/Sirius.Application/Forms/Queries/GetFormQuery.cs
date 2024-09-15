using MediatR;
using Sirius.Core.Enums;

namespace Sirius.Application.Forms.Queries;

public record GetFormQuery(int FormId) : IRequest<FormDto>;
public class GetFormQueryHandler (IFormsRepository repository): IRequestHandler<GetFormQuery, FormDto>
{
    public async Task<FormDto> Handle(GetFormQuery request, CancellationToken cancellationToken)
    {
        var form = await repository.GetByIdAsync(request.FormId);
        if (form is null)
        {
            return null;
        }
        return new FormDto(form.Id, form.Title, form.Description, form.FormType, form.Url);
        // return new FormDto(1, "Some Title", "Some Description", FormType.Webform, "https://www.google.com");
        
    }
}