using FoodStock.Application.Functions.ProducentFunctions.Commands.CreateProducent;
using FoodStock.Application.Functions.ProducentFunctions.Commands.DeleteProducent;
using FoodStock.Application.Functions.ProducentFunctions.Commands.UpdateProducent;
using FoodStock.Application.Functions.ProducentFunctions.Queries.GetProducentDetail;
using FoodStock.Application.Functions.ProducentFunctions.Queries.GetProducentListQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.Api.Controllers;

[ApiController]
[Route("api/producents")]
public class ProducentController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ProducentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProducentListViewModel>>> GetAll()
    {
        var producents = await _mediator.Send(new GetProducentListQuery());
        return Ok(producents);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProducentDetailViewModel>> Get([FromRoute] Guid id)
    {
        var producent = await _mediator.Send(new GetProducentDetailQuery {Id = id});
        if (producent is null)
        {
            return NotFound();
        }
        return Ok(producent);
    }

    [HttpPost]
    public async Task<ActionResult<CreateProducentCommandResponse>> Post([FromBody] CreateProducentCommand command)
    {
        var producent = await _mediator.Send(command);
        if (!producent.Success && producent.ValidationErrors != null)
        {
            return BadRequest(producent);
        }

        return Ok(producent);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UpdateProducentCommandResponse>> Update([FromBody] UpdateProducentCommand command,
        [FromRoute] Guid id)
    {
        var producent = await _mediator.Send(command with { Id = id });
        if (!producent.Success && producent.ValidationErrors != null)
        {
            return BadRequest(producent);
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteProducentCommand() with { Id = id });
        return NoContent();
    }
}
