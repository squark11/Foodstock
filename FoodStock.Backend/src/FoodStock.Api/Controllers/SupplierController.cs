using FoodStock.Application.Functions.SupplierFunctions.Commands.CreateSupplier;
using FoodStock.Application.Functions.SupplierFunctions.Commands.DeleteSupplier;
using FoodStock.Application.Functions.SupplierFunctions.Commands.UpdateSupplier;
using FoodStock.Application.Functions.SupplierFunctions.Queries.GetSupplierDetail;
using FoodStock.Application.Functions.SupplierFunctions.Queries.GetSupplierList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.Api.Controllers;

[ApiController]
[Route("api/suppliers")]
public class SupplierController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public SupplierController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierListViewModel>>> GetAll()
    {
        var suppliers = await _mediator.Send(new GetSupplierListQuery());
        return Ok(suppliers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SupplierDetailViewModel>> Get([FromRoute] Guid id)
    {
        var supplier = await _mediator.Send(new GetSupplierDetailQuery { Id = id });
        if (supplier is null)
        {
            return NotFound();
        }

        return Ok(supplier);
    }

    [HttpPost]
    public async Task<ActionResult<CreateSupplierCommandResponse>> Post([FromBody] CreateSupplierCommand command)
    {
        var supplier = await _mediator.Send(command);
        if (!supplier.Success && supplier.ValidationErrors != null)
        {
            return BadRequest(supplier);
        }
        return Ok(supplier);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UpdateSupplierCommandResponse>> Update([FromBody] UpdateSupplierCommand command,
        [FromRoute] Guid id)
    {
        var supplier = await _mediator.Send(command with { Id = id });
        if (!supplier.Success && supplier.ValidationErrors != null)
        {
            return BadRequest(supplier);
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteSupplierCommand { Id = id });
        return NoContent();
    }
}
