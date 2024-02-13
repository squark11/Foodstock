using MediatR;

namespace FoodStock.Application.Functions.ProductFunctions.Queries.GetProductsList;

public class GetProductListQuery : IRequest<List<ProductListViewModel>>
{
    public string? OrderBy { get; set; }    
}
