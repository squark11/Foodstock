using FoodStock.Application.Functions.OrderItemFunctions.Commands.CreateOrderItem;
using FoodStock.Application.Functions.OrderItemFunctions.Commands.CreateOrderItemByProduct;
using FoodStock.Application.Functions.OrderItemFunctions.Commands.DeleteOrderItem;
using FoodStock.Application.Functions.OrderItemFunctions.Commands.UpdateOrderItem;
using FoodStock.Application.Functions.OrderItemFunctions.Queries.GetOrderItemList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.Api.Controllers;

[ApiController]
[Route("api/orderItems")]
public class OrderItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItemListViewModel>>> GetAll()
    {
        var orderItems = await _mediator.Send(new GetOrderItemListQuery());
        return Ok(orderItems);
    }

    [HttpPost]
    public async Task<ActionResult<CreateOrderItemResponse>> Post([FromBody] CreateOrderItemCommand command)
    {
        var orderItem = await _mediator.Send(command);
        if (!orderItem.Success && orderItem.ValidationErrors != null)
        {
            return BadRequest(orderItem);
        }
        return Ok(orderItem);
    }

    [HttpPost("byProduct/{productId:guid}")]
    public async Task<ActionResult<CreateOrderItemByProductResponse>> PostByProduct(
        [FromBody] CreateOrderItemByProductCommand command, Guid productId)
    {
        var orderItem = await _mediator.Send(command with { ProductId = productId });
        if (!orderItem.Success && orderItem.ValidationErrors != null)
        {
            return BadRequest(orderItem);
        }
        return Ok(orderItem);
    }

    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UpdateOrderItemCommandResponse>> Update([FromRoute] Guid id, [FromBody] UpdateOrderItemCommand command)
    {
        var orderItem = await _mediator.Send(command with { Id = id });
        if (!orderItem.Success && orderItem.ValidationErrors != null)
        {
            return BadRequest(orderItem);
        }
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteOrderItemCommand { Id = id });
        return NoContent();
    }
}
