using MediatR;

namespace FoodStock.Application.Functions.ProducentFunctions.Queries.GetProducentDetail;

public class GetProducentDetailQuery : IRequest<ProducentDetailViewModel>
{
    public Guid Id { get; set; }
}
