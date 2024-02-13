using MediatR;

namespace FoodStock.Application.Functions.SupplierFunctions.Commands.CreateSupplier;

public sealed record CreateSupplierCommand : IRequest<CreateSupplierCommandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
