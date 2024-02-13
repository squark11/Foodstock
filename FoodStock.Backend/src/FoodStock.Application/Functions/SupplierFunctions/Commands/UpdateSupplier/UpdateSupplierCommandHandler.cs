using AutoMapper;
using FoodStock.Application.Functions.ProducentFunctions.Commands.UpdateProducent;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using MediatR;

namespace FoodStock.Application.Functions.SupplierFunctions.Commands.UpdateSupplier;

public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, UpdateSupplierCommandResponse>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public UpdateSupplierCommandHandler(ISupplierRepository supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }
    
    public async Task<UpdateSupplierCommandResponse> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSupplierCommandValidator(_supplierRepository);
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            return new UpdateSupplierCommandResponse(validatorResult);
        }

        var supplier = _mapper.Map<Supplier>(request);
        await _supplierRepository.UpdateAsync(supplier);
        return new UpdateSupplierCommandResponse(supplier.Id);
    }
}
