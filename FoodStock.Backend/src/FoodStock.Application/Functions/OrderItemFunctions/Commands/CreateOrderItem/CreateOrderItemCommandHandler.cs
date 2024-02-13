using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.OrderItemFunctions.Commands.CreateOrderItem;

public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, CreateOrderItemResponse>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IMapper mapper, IOrderRepository orderRepository)
    {
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
        _orderRepository = orderRepository;
    }
    
    public async Task<CreateOrderItemResponse> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateOrderItemCommandValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            return new CreateOrderItemResponse(validatorResult);
        }
        var orderItem = _mapper.Map<OrderItem>(request with {Id = new Guid()});
        
        var order = await GetOrder(orderItem.OrderId);
        orderItem.SupplierId = order.SupplierId;
        
        await _orderItemRepository.AddAsync(orderItem);
        return new CreateOrderItemResponse(orderItem.Id);
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
}
