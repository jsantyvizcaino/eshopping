
using Catalogo.Application.Mappers;
using Catalogo.Application.Queries;
using Catalogo.Application.Responses;
using Catalogo.Core.Repositories;
using MediatR;

namespace Catalogo.Application.Handlers;

public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
{
    private readonly IBrandRepository _brandRepository;

    public GetAllBrandsHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }
    public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await _brandRepository.GetAllBrands();
        var brandResponse = ProductMapper.Mapper.Map<IList<BrandResponse>>(brandList);
        return brandResponse;
    }
}
