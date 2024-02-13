using FoodStock.Application.Functions.OrderFunctions.Commands.CreateOrderCommand;
using FoodStock.Application.Functions.OrderFunctions.Commands.DeleteOrder;
using FoodStock.Application.Functions.OrderFunctions.Commands.UpdateOrder;
using FoodStock.Application.Functions.OrderFunctions.Queries.GetOrderDetail;
using FoodStock.Application.Functions.OrderFunctions.Queries.GetOrderList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize(Roles = "Admin, Employee")]
    [HttpPost]
    public async Task<ActionResult<CreateOrderCommandResponse>> Post([FromBody] CreateOrderCommand command)
    {
        var order = await _mediator.Send(command);
        if (!order.Success && order.ValidationErrors != null)
        {
            return BadRequest(order);
        }
        return Ok(order);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetOrderListViewModel>>> GetAll()
    {
        var orders = await _mediator.Send(new GetOrderListQuery());
        return orders;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDetailViewModel>> Get([FromRoute] Guid id)
    {
        var order = await _mediator.Send(new GetOrderDetailQuery { Id = id });
        if (order is null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UpdateOrderCommandResponse>> Update([FromBody] UpdateOrderCommand command,
        [FromRoute] Guid id)
    {
        var order = await _mediator.Send(command with { Id = id });
        if (order.ValidationErrors != null && !order.Success)
        {
            return BadRequest(order);
        }
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteOrderCommand { Id = id });
        return NoContent();
    }
}
