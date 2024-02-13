namespace FoodStock.Core.Exceptions;

public sealed class ProducentNotFoundException : NotFoundException
{
    public Guid Id { get; }

    public ProducentNotFoundException(Guid id) : base($"Producent with id: {id} not found.")
    {
        Id = id;
    }
}
