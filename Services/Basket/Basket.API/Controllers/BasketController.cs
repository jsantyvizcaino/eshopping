﻿using Basket.Application.Command;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Common.Logging.Correlation;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers;

public class BasketController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<BasketController> _logger;
    private readonly ICorrelationIdGenerator _correlationIdGenerator;

    public BasketController(IMediator mediator, IPublishEndpoint publishEndpoint,ILogger<BasketController> logger,ICorrelationIdGenerator correlationIdGenerator)
    {
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
        _correlationIdGenerator = correlationIdGenerator;
        _logger = logger;
        _logger.LogInformation("CorrelationId {correlationId}:", _correlationIdGenerator.Get());
    }

    [HttpGet]
    [Route("[action]/{userName}", Name = "GetBasketByUserName")]
    [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
    {
        var query = new GetBasketByUserNameQuery(userName);
        var basket = await _mediator.Send(query);
        return Ok(basket);
    }

    [HttpPost("CreateBasket")]
    [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
    {

        var basket = await _mediator.Send(createShoppingCartCommand);
        return Ok(basket);
    }

    [HttpDelete]
    [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> DeleteBasket(string userName)
    {
        var command = new DeleteBasketByUserNameCommand(userName);
        return Ok(await _mediator.Send(command));
    }

    [Route("[action]")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
        //obtener basket existente del usuario
        var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
        var basket = await _mediator.Send(query);
        if (basket is null) return BadRequest();
        var eventMesg = BasketMapper.Mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMesg.TotalPrice = basket.TotalPrice;
        eventMesg.CorrelationId = _correlationIdGenerator.Get();
        await _publishEndpoint.Publish(eventMesg);
        //eliminar the basket
        var deleteQuery = new DeleteBasketByUserNameCommand(basketCheckout.UserName);
        await _mediator.Send(deleteQuery);
        return Accepted();
    }
    
   
}
