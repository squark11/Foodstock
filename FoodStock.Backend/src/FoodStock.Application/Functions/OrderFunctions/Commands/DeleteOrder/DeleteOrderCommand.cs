using MediatR;

namespace FoodStock.Application.Functions.OrderFunctions.Commands.DeleteOrder;

public sealed record DeleteOrderCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
