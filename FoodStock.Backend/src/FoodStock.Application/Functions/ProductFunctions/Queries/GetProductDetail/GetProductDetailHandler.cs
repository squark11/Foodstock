using System.Net;
using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.ProductFunctions.Queries.GetProductDetail;

public class GetProductDetailHandler : IRequestHandler<GetProductDetailQuery, ProductDetailViewModel>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductDetailHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDetailViewModel> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    { 
        var product = await _productRepository.GetProductDetailAsync(request.Id);
        if (product is null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        var productDetail = _mapper.Map<ProductDetailViewModel>(product);
        return productDetail;
    }
}
