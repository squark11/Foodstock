using MediatR;

namespace FoodStock.Application.Functions.OrderItemFunctions.Commands.DeleteOrderItem;

public class DeleteOrderItemCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
