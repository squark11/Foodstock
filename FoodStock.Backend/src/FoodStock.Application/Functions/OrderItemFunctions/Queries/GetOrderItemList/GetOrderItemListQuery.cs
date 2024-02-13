using MediatR;

namespace FoodStock.Application.Functions.OrderItemFunctions.Queries.GetOrderItemList;

public class GetOrderItemListQuery : IRequest<List<OrderItemListViewModel>>
{
}
