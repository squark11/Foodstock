namespace FoodStock.Core.Exceptions;

public sealed class OrganizationNotFoundException : NotFoundException
{
    public Guid Id { get; set; }
    
    public OrganizationNotFoundException(Guid id) : base($"Organization with id: {id} not found.")
    {
        Id = id;
    }
}
