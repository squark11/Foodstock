using AutoMapper;
using FoodStock.Application.Repositories;
using MediatR;

namespace FoodStock.Application.Functions.ProductFunctions.Queries;

public class GetProductListByCategoryIdHandler : IRequestHandler<GetProductListByCategoryIdQuery, List<ProductListByCategoryIdViewModel>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductListByCategoryIdHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<List<ProductListByCategoryIdViewModel>> Handle(GetProductListByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllWithIncludesAsync();
        var productsByCategoryId = products.Where(x => x.CategoryId == request.CategoryId);
        
        switch (request?.OrderBy)
        {
            case "productNameASC":
                var byNameAsc = productsByCategoryId.OrderBy(x => x.Name);
                return _mapper.Map<List<ProductListByCategoryIdViewModel>>(byNameAsc);
            case "productNameDESC":
                var byNameDesc = productsByCategoryId.OrderByDescending(x => x.Name);
                return _mapper.Map<List<ProductListByCategoryIdViewModel>>(byNameDesc);
            case "expirationDateASC":
                var byExpirationDate = productsByCategoryId.OrderBy(x => x.ExpirationDate);
                return _mapper.Map<List<ProductListByCategoryIdViewModel>>(byExpirationDate);
            case "expirationDateDESC":
                var byExpirationDateDesc = productsByCategoryId.OrderByDescending(x => x.ExpirationDate);
                return _mapper.Map<List<ProductListByCategoryIdViewModel>>(byExpirationDateDesc);
            case "producentNameASC":
                var byProducentNameAsc = products.OrderBy(x => x.Producent.Name);
                return _mapper.Map<List<ProductListByCategoryIdViewModel>>(byProducentNameAsc);
            case "producentNameDESC":
                var byProducentNameDesc = products.OrderByDescending(x => x.Producent.Name);
                return _mapper.Map<List<ProductListByCategoryIdViewModel>>(byProducentNameDesc);
            case "quantityASC":
                var quantityAsc = products.OrderBy(x => x.Quantity);
                return _mapper.Map<List<ProductListByCategoryIdViewModel>>(quantityAsc);
            case "quantityDESC":
                var quantityDesc = products.OrderByDescending(x => x.Quantity);
                return _mapper.Map<List<ProductListByCategoryIdViewModel>>(quantityDesc);
            default:
                var defaultSort = productsByCategoryId.OrderBy(x => x.ExpirationDate);
                return _mapper.Map<List<ProductListByCategoryIdViewModel>>(defaultSort);
        }
    }
}
