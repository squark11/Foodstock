using MediatR;

namespace FoodStock.Application.Functions.RoleFunctions.Commands.CreateRole;

public sealed record CreateRoleCommand : IRequest<CreateRoleCommandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
