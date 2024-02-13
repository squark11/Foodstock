using MediatR;

namespace FoodStock.Application.Functions.ProducentFunctions.Commands.DeleteProducent;

public record DeleteProducentCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
