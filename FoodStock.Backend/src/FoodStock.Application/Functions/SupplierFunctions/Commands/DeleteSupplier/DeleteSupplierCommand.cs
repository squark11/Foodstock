using MediatR;

namespace FoodStock.Application.Functions.SupplierFunctions.Commands.DeleteSupplier;

public record DeleteSupplierCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
