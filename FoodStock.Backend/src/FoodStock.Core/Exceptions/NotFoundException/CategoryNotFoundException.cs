namespace FoodStock.Core.Exceptions;

public sealed class CategoryNotFoundException : NotFoundException
{
    public Guid Id { get; set; }
    
    public CategoryNotFoundException(Guid id) : base($"Category with id: {id} not found.")
    {
        Id = id;
    }
}
