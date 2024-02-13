using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using MediatR;

namespace FoodStock.Application.Functions.ProductFunctions.Queries.GetProductsList;

public class GetProductListHandler : IRequestHandler<GetProductListQuery, List<ProductListViewModel>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductListHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<List<ProductListViewModel>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllWithIncludesAsync();
        
        switch (request?.OrderBy)
        {
            case "productNameASC":
                var byNameAsc = products.OrderBy(x => x.Name);
                return _mapper.Map<List<ProductListViewModel>>(byNameAsc);
            case "productNameDESC":
                var byNameDesc = products.OrderByDescending(x => x.Name);
                return _mapper.Map<List<ProductListViewModel>>(byNameDesc);
            case "expirationDateASC":
                var byExpirationDate = products.OrderBy(x => x.ExpirationDate);
                return _mapper.Map<List<ProductListViewModel>>(byExpirationDate);
            case "expirationDateDESC":
                var byExpirationDateDesc = products.OrderByDescending(x => x.ExpirationDate);
                return _mapper.Map<List<ProductListViewModel>>(byExpirationDateDesc);
            case "categoryNameASC":
                var byCategoryNameAsc = products.OrderBy(x => x.Category.CategoryName);
                return _mapper.Map<List<ProductListViewModel>>(byCategoryNameAsc);
            case "categoryNameDESC":
                var byCategoryNameDesc = products.OrderByDescending(x => x.Category.CategoryName);
                return _mapper.Map<List<ProductListViewModel>>(byCategoryNameDesc);
            case "producentNameASC":
                var byProducentNameAsc = products.OrderBy(x => x.Producent.Name);
                return _mapper.Map<List<ProductListViewModel>>(byProducentNameAsc);
            case "producentNameDESC":
                var byProducentNameDesc = products.OrderByDescending(x => x.Producent.Name);
                return _mapper.Map<List<ProductListViewModel>>(byProducentNameDesc);
            case "quantityASC":
                var quantityAsc = products.OrderBy(x => x.Quantity);
                return _mapper.Map<List<ProductListViewModel>>(quantityAsc);
            case "quantityDESC":
                var quantityDesc = products.OrderByDescending(x => x.Quantity);
                return _mapper.Map<List<ProductListViewModel>>(quantityDesc);
            default:
                var productsDefault = await _productRepository.GetAllOrderByExpirationDateAscAsync();
                return _mapper.Map<List<ProductListViewModel>>(productsDefault);
        }
    }
}
