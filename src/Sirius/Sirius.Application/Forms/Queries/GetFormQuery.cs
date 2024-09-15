using MediatR;

namespace Sirius.Application.Forms.Queries;

public record GetFormQuery(int FormId) : IRequest<FormDto>;
public class GetFormQueryHandler (IFormsRepository repository): IRequestHandler<GetFormQuery, FormDto>
{
    public async Task<FormDto> Handle(GetFormQuery request, CancellationToken cancellationToken)
    {
        var form = await repository.GetByIdAsync(request.FormId);
        return form is null ? null : new FormDto(form.Id, form.Title, form.Description, form.FormType, form.Url);
    }
}