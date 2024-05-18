using Catalogo.Application.Queries;
using Catalogo.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Catalogo.Application.Commands;
using Catalogo.Core.Specification;
using Common.Logging.Correlation;

namespace Catalogo.API.Controllers;

public class CatalogController : ApiController
{
    private readonly IMediator _mediator;

    public readonly ILogger<CatalogController> _logger;
    private readonly ICorrelationIdGenerator _correlationIdGenerator;

    public CatalogController(IMediator mediator, ILogger<CatalogController> logger,ICorrelationIdGenerator correlationIdGenerator)
    {
        _mediator = mediator;
        _logger = logger;
        _correlationIdGenerator = correlationIdGenerator;
        _logger.LogInformation("CorrelationId {correlationId}:",_correlationIdGenerator.Get());
    }

    [HttpGet]
    [Route("[action]/{id}",Name ="GetProductById")]
    [ProducesResponseType(typeof(ProductResponse),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponse>> GetProductById(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("[action]/{productName}", Name = "GetProductBysProductName")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductBysProductName(string productName)
    {
        var query = new GetProductByNameQuery(productName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("GetAllProducts")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetAllProducts([FromQuery] CatologSpecParams catologSpecParams)
    {
        try
        {
            var query = new GetAllProductsQuery(catologSpecParams);
            var result = await _mediator.Send(query);
            _logger.LogInformation("All products retrivued");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception has occured: {Exception}");
            throw;
        }
      
    }
    
    [HttpGet]
    [Route("GetAllBrands")]
    [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<BrandResponse>>> GetAllBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("GetAllTypes")]
    [ProducesResponseType(typeof(IList<TypeResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<TypeResponse>>> GetAllTypes()
    {
        var query = new GetAllTypesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }


    [HttpGet]
    [Route("[action]/{brand}", Name = "GetProductsByBrandName")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductsByBrandName(string brand)
    {
        var query = new GetProductByBrandQuery(brand);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Route("CreateProduct")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand productCommand)
    {
        var result = await _mediator.Send(productCommand);
        return Ok(result);
    }
    
    [HttpPut]
    [Route("UpdateProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand productCommand)
    {
        var result = await _mediator.Send(productCommand);
        return Ok(result);
    }
    
    [HttpDelete]
    [Route("{id}",Name = "DeleteProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProduct([FromBody] string id)
    {
        var query = new DeleteProductByIdCommand(id);
        return Ok(await _mediator.Send(query));
    }
}
