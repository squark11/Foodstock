using MediatR;

namespace FoodStock.Application.Functions.OrderFunctions.Queries.GetOrderList;

public class GetOrderListQuery : IRequest<List<GetOrderListViewModel>>
{
}
