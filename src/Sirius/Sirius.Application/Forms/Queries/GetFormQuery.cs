using MediatR;

namespace Sirius.Application.Forms.Queries;

public record GetFormQuery(int FormId) : IRequest<FormDto>;
public class GetFormQueryHandler(IApplicationDbContext context) : IRequestHandler<GetFormQuery, FormDto>
{
    public async Task<FormDto> Handle(GetFormQuery request, CancellationToken cancellationToken)
    {
        var form = await context.Forms.FindAsync(request.FormId, cancellationToken);
        return form is null ? null : new FormDto(form.Id, form.Title, form.Description, form.FormType, form.Url);
    }
}