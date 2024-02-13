using FoodStock.Core.Entities;
using MediatR;

namespace FoodStock.Application.Functions.CategoryFunctions.Queries.GetCategoriesList;

public class GetCategoryListQuery : IRequest<List<CategoryListViewModel>>
{
}
