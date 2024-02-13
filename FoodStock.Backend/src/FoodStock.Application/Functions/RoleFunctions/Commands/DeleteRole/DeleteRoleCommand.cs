using System.Security.AccessControl;
using MediatR;

namespace FoodStock.Application.Functions.RoleFunctions.Commands.DeleteRole;

public sealed class DeleteRoleCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
