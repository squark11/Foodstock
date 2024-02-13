using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.SupplierFunctions.Queries.GetSupplierDetail;

public class GetSupplierDetailHandler : IRequestHandler<GetSupplierDetailQuery, SupplierDetailViewModel>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public GetSupplierDetailHandler(ISupplierRepository supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }
    
    public async Task<SupplierDetailViewModel> Handle(GetSupplierDetailQuery request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(request.Id);
        if (supplier is null)
        {
            throw new SupplierNotFoundException(request.Id);
        }
        var supplierDetail = _mapper.Map<SupplierDetailViewModel>(supplier);
        return supplierDetail;
    }
}
