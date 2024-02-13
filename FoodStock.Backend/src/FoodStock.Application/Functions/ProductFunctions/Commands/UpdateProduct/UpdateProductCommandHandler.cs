using AutoMapper;
using FoodStock.Aplication.Responses;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.ProductFunctions.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    
    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator();
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid )
        {
            return new UpdateProductCommandResponse(validatorResult);
        }

        var product = await _productRepository.GetProductDetailAsync(request.Id);
        if (product is null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        _mapper.Map(request, product);
        await _productRepository.UpdateAsync(product);
        return new UpdateProductCommandResponse(product.Id); 
    }
}
