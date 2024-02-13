namespace FoodStock.Core.Exceptions;

public class SupplierNotFoundException : NotFoundException
{
    public Guid Id { get; }
    
    public SupplierNotFoundException(Guid id) : base($"Supplier with id: {id} not found.")
    {
        Id = id;
    }
}
