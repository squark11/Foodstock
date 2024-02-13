using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using MediatR;

namespace FoodStock.Application.Functions.SupplierFunctions.Commands.CreateSupplier;

public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, CreateSupplierCommandResponse>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public CreateSupplierCommandHandler(ISupplierRepository supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }
    
    public async Task<CreateSupplierCommandResponse> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateSupplierCommandValidator(_supplierRepository);
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            return new CreateSupplierCommandResponse(validatorResult);
        }
        
        var supplier = _mapper.Map<Supplier>(request with { Id = new Guid() });
        supplier = await _supplierRepository.AddAsync(supplier);
        return new CreateSupplierCommandResponse(supplier.Id);
    }
}
