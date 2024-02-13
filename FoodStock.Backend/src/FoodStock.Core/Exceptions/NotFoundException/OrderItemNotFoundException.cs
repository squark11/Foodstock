namespace FoodStock.Core.Exceptions;

public sealed class OrderItemNotFoundException : NotFoundException
{
    public Guid Id { get; set; }
    
    public OrderItemNotFoundException(Guid id) : base($"Order item with id: {id} not found.")
    {
        Id = id;
    }
}
