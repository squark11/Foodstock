using MediatR;

namespace FoodStock.Application.Functions.ProductFunctions.Queries;

public class GetProductListByCategoryIdQuery : IRequest<List<ProductListByCategoryIdViewModel>>
{
    public Guid CategoryId { get; set; }
    public string? OrderBy { get; set; }
}
