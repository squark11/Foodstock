using AutoMapper;
using FoodStock.Application.Repositories;
using MediatR;

namespace FoodStock.Application.Functions.SupplierFunctions.Queries.GetSupplierList;

public class GetSupplierListHandler : IRequestHandler<GetSupplierListQuery, List<SupplierListViewModel>>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;
    
    public GetSupplierListHandler(ISupplierRepository supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }

    public async Task<List<SupplierListViewModel>> Handle(GetSupplierListQuery request, CancellationToken cancellationToken)
    {
        var suppliers = await _supplierRepository.GetAllAsync();
        return _mapper.Map<List<SupplierListViewModel>>(suppliers);
    }
}
