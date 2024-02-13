using MediatR;

namespace FoodStock.Application.Functions.AuthFunctions.Commands.UpdateUser;

public sealed record UpdateUserCommand : IRequest<UpdateUserCommandResponse>
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public Guid? RoleId { get; set; }
    public Guid? SupplierId { get; set; }
}

