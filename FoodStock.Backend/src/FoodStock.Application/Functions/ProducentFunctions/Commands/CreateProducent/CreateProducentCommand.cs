using MediatR;

namespace FoodStock.Application.Functions.ProducentFunctions.Commands.CreateProducent;

public record CreateProducentCommand : IRequest<CreateProducentCommandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
