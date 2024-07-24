using Catalogo.Application.Mappers;
using Catalogo.Application.Queries;
using Catalogo.Application.Responses;
using Catalogo.Core.Repositories;
using MediatR;

namespace Catalogo.Application.Handlers;

public class GetAllTypesHandlers : IRequestHandler<GetAllTypesQuery, IList<TypeResponse>>
{
    private readonly ITypesRepository _typesRepository;

    public GetAllTypesHandlers(ITypesRepository typesRepository)
    {
        _typesRepository = typesRepository;
    }
    public async Task<IList<TypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var typeList = await _typesRepository.GetAllTypes();
        var typeResponseList = ProductMapper.Mapper.Map<List<TypeResponse>>(typeList);
        return typeResponseList;
    }
}
