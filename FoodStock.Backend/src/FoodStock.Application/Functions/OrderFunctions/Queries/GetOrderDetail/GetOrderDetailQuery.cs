using MediatR;

namespace FoodStock.Application.Functions.OrderFunctions.Queries.GetOrderDetail;

public class GetOrderDetailQuery : IRequest<OrderDetailViewModel>
{
    public Guid Id { get; set; }
}
