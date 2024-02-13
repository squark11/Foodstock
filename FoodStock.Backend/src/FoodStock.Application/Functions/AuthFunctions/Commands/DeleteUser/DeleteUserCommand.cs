using MediatR;

namespace FoodStock.Application.Functions.AuthFunctions.Commands.DeleteUser;

public record DeleteUserCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
