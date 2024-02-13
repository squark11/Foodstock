namespace FoodStock.Core.Exceptions;

public class RoleNotFoundException : NotFoundException
{
    private Guid Id { get; set; }

    public RoleNotFoundException(Guid id) : base($"Role with id: {id} not found.")
    {
        Id = id;
    }
}
