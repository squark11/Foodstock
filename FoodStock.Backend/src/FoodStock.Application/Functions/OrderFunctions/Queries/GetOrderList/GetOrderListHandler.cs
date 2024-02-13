using AutoMapper;
using FoodStock.Application.Repositories;
using MediatR;

namespace FoodStock.Application.Functions.OrderFunctions.Queries.GetOrderList;

public class GetOrderListHandler : IRequestHandler<GetOrderListQuery, List<GetOrderListViewModel>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderListHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    
    public async Task<List<GetOrderListViewModel>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllIncludedAsync();
        return _mapper.Map<List<GetOrderListViewModel>>(orders);
    }
}
