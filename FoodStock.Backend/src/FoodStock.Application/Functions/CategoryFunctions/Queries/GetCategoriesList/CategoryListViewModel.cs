namespace FoodStock.Application.Functions.CategoryFunctions.Queries.GetCategoriesList;

public sealed record CategoryListViewModel
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
}
