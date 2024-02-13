using FoodStock.Application.Functions.AuthFunctions;
using FoodStock.Application.Functions.AuthFunctions.Commands.DeleteUser;
using FoodStock.Application.Functions.AuthFunctions.Commands.UpdatePassword;
using FoodStock.Application.Functions.AuthFunctions.Commands.UpdateUser;
using FoodStock.Application.Functions.AuthFunctions.Queries.GetInactiveUserList;
using FoodStock.Application.Functions.AuthFunctions.Queries.GetUserDetail;
using FoodStock.Application.Functions.AuthFunctions.Queries.GetUsersList;
using FoodStock.Infrastructure.Authentication.DTO;
using FoodStock.Infrastructure.Authentication.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.Api.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IMediator _mediator;

    public AccountController(IAccountService accountService, IMediator mediator, IUserContextService contextService)
    {
        _accountService = accountService;
        _mediator = mediator;
    }

    [HttpPost("registerUser")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserCommand command)
    {
        var user = await _mediator.Send(command);

        if (!user.Success && user.ValidationErrors != null)
        {
            return BadRequest(user);
        }
        
        return Ok(user);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _accountService.GenerateJwt(dto);
        var user = await _accountService.GetUserByEmail(dto.Email);
        if (!user.IsActive)
        {
            return BadRequest("The user is inactive. Please contact your system administrator.");
        }
        var response = new LoginResponse
        {
            Token = token,
            User = user
        };
        return Ok(response);
    }
    
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Roles = "Admin, Employee, Supplier")]
    public async Task<ActionResult<UpdateUserCommandResponse>> Update([FromBody] UpdateUserCommand command, [FromRoute] Guid id)
    {
        var user = await _mediator.Send(command with { Id = id });

        if (!user.Success && user.ValidationErrors != null)
        {
            return BadRequest(user);
        }
        return NoContent();
    }

    [HttpPatch("changePassword/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Roles = "Admin, Employee, Supplier")]
    public async Task<ActionResult<UpdatePasswordResponse>> UpdatePassword([FromBody] UpdatePasswordCommand command,
        [FromRoute] Guid id)
    {
        var password = await _mediator.Send(command with { Id = id });
        if (!password.Success && password.ValidationErrors != null)
        {
            return BadRequest(password);
        }
        return NoContent();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<UserListViewModel>>> GetAll()
    {
        var users = await _mediator.Send(new GetUserListQuery());
        return Ok(users);
    }
    
    [HttpGet("inactiveUsers")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<UserListViewModel>>> GetAllInactive()
    {
        var users = await _mediator.Send(new GetInactiveUserListQuery());
        return Ok(users);
    }
    
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin, Employee, Supplier")]
    public async Task<ActionResult<UserDetailViewModel>> Get([FromRoute] Guid id)
    {
        var user = await _mediator.Send(new GetUserDetailQuery { Id = id });
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpPatch("deleteUser/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteUserCommand() with { Id = id });
        return NoContent();
    }
}
