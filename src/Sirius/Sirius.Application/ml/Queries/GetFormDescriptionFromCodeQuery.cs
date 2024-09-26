using MediatR;

namespace Sirius.Application.ml.Queries;

public record GetFormDescriptionFromCodeQuery (string Code) : IRequest<FormDescriptionDto>;