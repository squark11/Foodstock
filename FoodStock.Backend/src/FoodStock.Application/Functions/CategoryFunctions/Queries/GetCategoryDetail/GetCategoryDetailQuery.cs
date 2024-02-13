using FoodStock.Application.Functions.CategoryFunctions.Queries.GetCategoriesList;
using MediatR;

namespace FoodStock.Application.Functions.CategoryFunctions.Queries.GetCategoryDetail;

public class GetCategoryDetailQuery : IRequest<CategoryDetailViewModel>
{
    public Guid Id { get; set; }
}
