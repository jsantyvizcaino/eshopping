using Catalogo.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.Queries;

public class GetProductByBrandQuery: IRequest<IList<ProductResponse>>
{
    public string Brandname { get; set; }

    public GetProductByBrandQuery(string brandname)
    {
        Brandname = brandname;
    }
}
