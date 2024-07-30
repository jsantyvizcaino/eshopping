using Catalogo.Application.Responses;
using MediatR;

namespace Catalogo.Application.Queries;

public class GetProductByBrandQuery: IRequest<IList<ProductResponse>>
{
    public string Brandname { get; set; }

    public GetProductByBrandQuery(string brandname)
    {
        Brandname = brandname;
    }
}
