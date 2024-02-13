namespace FoodStock.Core.Exceptions;

public sealed class OrderNotFoundException : NotFoundException
{

    public Guid Id { get; set; }

    public OrderNotFoundException(Guid id) : base($"Order with id: {id} not found.")
    {
        Id = id;
    }
}
