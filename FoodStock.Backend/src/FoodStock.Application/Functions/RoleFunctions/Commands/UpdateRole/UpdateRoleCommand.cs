using System.Reflection.Metadata.Ecma335;
using MediatR;

namespace FoodStock.Application.Functions.RoleFunctions.Commands.UpdateRole;

public sealed record UpdateRoleCommand : IRequest<UpdateRoleCommandResponse>
{    
    public Guid Id { get; set; }
    public string Name { get; set; }
}
