using Catalogo.Application.Responses;
using Catalogo.Core.Specification;
using MediatR;

namespace Catalogo.Application.Queries;

public class GetAllProductsQuery: IRequest<Pagination<ProductResponse>>
{
    public CatologSpecParams CatologSpecParams { get; set; }

    public GetAllProductsQuery(CatologSpecParams catologSpecParams)
    {
        CatologSpecParams = catologSpecParams;
    }
}
