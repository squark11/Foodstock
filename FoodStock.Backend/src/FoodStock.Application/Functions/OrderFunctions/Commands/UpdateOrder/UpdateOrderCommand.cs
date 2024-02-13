using MediatR;

namespace FoodStock.Application.Functions.OrderFunctions.Commands.UpdateOrder;

public sealed record UpdateOrderCommand: IRequest<UpdateOrderCommandResponse>
{
    public Guid Id { get; set; }
    public string OrderStatus { get; set; }
}
