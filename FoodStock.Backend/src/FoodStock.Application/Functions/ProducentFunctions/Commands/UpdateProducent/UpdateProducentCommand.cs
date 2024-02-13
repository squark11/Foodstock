using MediatR;

namespace FoodStock.Application.Functions.ProducentFunctions.Commands.UpdateProducent;

public record UpdateProducentCommand : IRequest<UpdateProducentCommandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
