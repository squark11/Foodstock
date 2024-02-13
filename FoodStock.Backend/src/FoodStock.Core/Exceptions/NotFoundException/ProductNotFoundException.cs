namespace FoodStock.Core.Exceptions;

public sealed class ProductNotFoundException : NotFoundException
{
    public Guid Id { get; }
    
    public ProductNotFoundException(Guid id) : base($"Product with id: {id} not found.")
    {
        Id = id;
    }
}
