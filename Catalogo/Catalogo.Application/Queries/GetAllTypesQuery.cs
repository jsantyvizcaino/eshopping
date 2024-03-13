using Catalogo.Application.Responses;
using MediatR;

namespace Catalogo.Application.Queries;

public class GetAllTypesQuery : IRequest<IList<TypeResponse>>
{
}
