using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.OrderItemFunctions.Commands.CreateOrderItemByProduct;

public class CreateOrderByProductHandler : IRequestHandler<CreateOrderItemByProductCommand, CreateOrderItemByProductResponse>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateOrderByProductHandler(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
    {
        _orderItemRepository = orderItemRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<CreateOrderItemByProductResponse> Handle(CreateOrderItemByProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateOrderItemByProductValidator();
        var validationResult = await validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            return new CreateOrderItemByProductResponse(validationResult);
        }
        var orderItem = _mapper.Map<OrderItem>(request with {Id = new Guid()});
        var order = await GetOrder(request.OrderId);
        var product = await GetProduct(request.ProductId);

        orderItem.Name = product.Name;
        orderItem.BarCode = product.BarCode;
        orderItem.CategoryId = product.CategoryId;
        orderItem.ProducentId = product.ProducentId;
        orderItem.SupplierId = order.SupplierId;

        await _orderItemRepository.AddAsync(orderItem);
        return new CreateOrderItemByProductResponse(orderItem.Id);
    }
    
    private async Task<Order> GetOrder(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order is null)
        {
            throw new OrderNotFoundException(orderId);
        }
        return order;
    }

    private async Task<Product> GetProduct(Guid productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null)
        {
            throw new ProducentNotFoundException(productId);
        }
        return product;
    }
}
