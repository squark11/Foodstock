using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.OrderFunctions.Queries.GetOrderDetail;

public class GetOrderDetailHandler : IRequestHandler<GetOrderDetailQuery, OrderDetailViewModel>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderDetailHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    
    public async Task<OrderDetailViewModel> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderDetailAsync(request.Id);
        if (order is null)
        {
            throw new OrderNotFoundException(request.Id);
        }
        return _mapper.Map<OrderDetailViewModel>(order);
    }
}
