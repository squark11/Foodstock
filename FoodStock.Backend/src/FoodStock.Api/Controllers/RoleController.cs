using FoodStock.Application.Functions.RoleFunctions.Commands.CreateRole;
using FoodStock.Application.Functions.RoleFunctions.Commands.DeleteRole;
using FoodStock.Application.Functions.RoleFunctions.Commands.UpdateRole;
using FoodStock.Application.Functions.RoleFunctions.Queries.GetRoleDetail;
using FoodStock.Application.Functions.RoleFunctions.Queries.GetRolesList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.Api.Controllers;

[ApiController]
[Route("api/roles")]
[Authorize(Roles = "Admin")]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleListViewModel>>> GetAll()
    {
        var roles = await _mediator.Send(new GetRoleListQuery());
        return Ok(roles);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RoleDetailViewModel>> Get([FromRoute] Guid id)
    {
        var role = await _mediator.Send(new GetRoleDetailQuery { Id = id });
        if (role is null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    [HttpPost]
    public async Task<ActionResult<CreateRoleCommandResponse>> Post([FromBody] CreateRoleCommand command)
    {
        var role = await _mediator.Send(command);

        if (!role.Success && role.ValidationErrors != null)
        {
            return BadRequest(role);
        }
        return Ok(role);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UpdateRoleCommandResponse>> Update([FromBody] UpdateRoleCommand command,
        [FromRoute] Guid id)
    {
        var role = await _mediator.Send(command with { Id = id });
        if (!role.Success && role.ValidationErrors != null)
        {
            return BadRequest(role);
        }
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteRoleCommand { Id = id });
        return NoContent();
    }
}
