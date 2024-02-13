using MediatR;

namespace FoodStock.Application.Functions.OrderFunctions.Commands.CreateOrderCommand;

public sealed record CreateOrderCommand : IRequest<CreateOrderCommandResponse>
{
    public Guid Id { get; set; }
    public Guid SupplierId { get; set; }
    
}
