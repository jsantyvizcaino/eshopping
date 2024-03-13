using Catalogo.Application.Responses;
using MediatR;

namespace Catalogo.Application.Queries;

public class GetAllBrandsQuery: IRequest<IList<BrandResponse>>
{

}
