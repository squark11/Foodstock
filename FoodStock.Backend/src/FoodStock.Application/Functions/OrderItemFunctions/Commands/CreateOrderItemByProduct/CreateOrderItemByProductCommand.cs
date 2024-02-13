using MediatR;

namespace FoodStock.Application.Functions.OrderItemFunctions.Commands.CreateOrderItemByProduct;

public sealed record CreateOrderItemByProductCommand : IRequest<CreateOrderItemByProductResponse>
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid OrderId { get; set; }
}
