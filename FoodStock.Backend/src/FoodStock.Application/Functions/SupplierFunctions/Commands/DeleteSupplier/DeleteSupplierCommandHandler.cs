using FoodStock.Application.Repositories;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.SupplierFunctions.Commands.DeleteSupplier;

public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand>
{
    private readonly ISupplierRepository _supplierRepository;

    public DeleteSupplierCommandHandler(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }
    
    public async Task<Unit> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(request.Id);
        if (supplier is null)
        {
            throw new SupplierNotFoundException(request.Id);
        }
        await _supplierRepository.DeleteAsync(supplier);
        return Unit.Value;
    }
}
