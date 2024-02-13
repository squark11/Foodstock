using MediatR;

namespace FoodStock.Application.Functions.OrderItemFunctions.Commands.UpdateOrderItem;

public sealed record UpdateOrderItemCommand : IRequest<UpdateOrderItemCommandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ProducentId { get; set; }
    public string? BarCode { get; set; }
    public DateTime? ExpirationDate { get; set; }
}
