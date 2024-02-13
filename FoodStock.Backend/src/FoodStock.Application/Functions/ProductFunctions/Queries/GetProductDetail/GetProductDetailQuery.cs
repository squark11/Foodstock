using MediatR;

namespace FoodStock.Application.Functions.ProductFunctions.Queries.GetProductDetail;

public class GetProductDetailQuery : IRequest<ProductDetailViewModel>
{
    public Guid Id { get; set; }
}
