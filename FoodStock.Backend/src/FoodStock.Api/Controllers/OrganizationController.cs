using FoodStock.Application.Functions.OrganizationFunctions.Commands.CreateOrganization;
using FoodStock.Application.Functions.OrganizationFunctions.Commands.UpdateOrganization;
using FoodStock.Application.Functions.OrganizationFunctions.Queries.GetOrganizationDetail;
using FoodStock.Application.Functions.OrganizationFunctions.Queries.GetOrganizationList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.Api.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/organizations")]
public class OrganizationController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrganizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Employee")]
    public async Task<ActionResult<IEnumerable<GetOrganizationListViewModel>>> GetAll()
    {
        var organizations = await _mediator.Send(new GetOrganizationListQuery());
        return Ok(organizations);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin, Employee")]
    public async Task<ActionResult<GetOrganizationDetailViewModel>> Get([FromRoute] Guid id)
    {
        var organization = await _mediator.Send(new GetOrganizationDetailQuery { Id = id });
        if (organization is null)
        {
            return NotFound();
        }

        return Ok(organization);
    }

    [HttpPost]
    public async Task<ActionResult<CreateOrganizationCommandResponse>> Post([FromBody] CreateOrganizationCommand command)
    {
        var organization = await _mediator.Send(command);
        if (!organization.Success && organization.ValidationErrors != null)
        {
            return BadRequest(organization);
        }
        return Ok(organization);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UpdateOrganizationCommandResponse>> Update([FromBody] UpdateOrganizationCommand command, [FromRoute] Guid id)
    {
        var organization = await _mediator.Send(command with { Id = id });
        if (!organization.Success && organization.ValidationErrors != null)
        {
            return BadRequest(organization);
        }
        return NoContent();
    }
}
