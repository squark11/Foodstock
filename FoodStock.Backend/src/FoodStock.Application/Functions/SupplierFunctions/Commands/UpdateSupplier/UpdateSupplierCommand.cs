using MediatR;

namespace FoodStock.Application.Functions.SupplierFunctions.Commands.UpdateSupplier;

public sealed record UpdateSupplierCommand : IRequest<UpdateSupplierCommandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
