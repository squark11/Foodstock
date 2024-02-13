using AutoMapper;
using FoodStock.Application.Repositories;
using MediatR;

namespace FoodStock.Application.Functions.OrderItemFunctions.Queries.GetOrderItemList;

public class GetOrderItemListHandler : IRequestHandler<GetOrderItemListQuery, List<OrderItemListViewModel>>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public GetOrderItemListHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
    {
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
    }
    
    public async Task<List<OrderItemListViewModel>> Handle(GetOrderItemListQuery request, CancellationToken cancellationToken)
    {
        var orderItems = await _orderItemRepository.GetOrderItemsIncludedAsync();
        return _mapper.Map<List<OrderItemListViewModel>>(orderItems);
    }
}
