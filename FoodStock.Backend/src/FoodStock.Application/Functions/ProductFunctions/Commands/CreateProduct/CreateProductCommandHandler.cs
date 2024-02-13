using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using FoodStock.Infrastructure.Authentication.Services;
using MediatR;

namespace FoodStock.Application.Functions.ProductFunctions.Commands.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContextService;

    public CreateProductHandler(IProductRepository productRepository, IMapper mapper, IUserContextService userContextService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _userContextService = userContextService;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator();
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            return new CreateProductCommandResponse(validatorResult);
        }

        var product = _mapper.Map<Product>(request with {Id = Guid.NewGuid()});

        product.UserId = _userContextService.GetUserId;

        product = await _productRepository.AddAsync(product);

        return new CreateProductCommandResponse(product.Id);
    }
}