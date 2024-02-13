namespace FoodStock.Application.Functions.CategoryFunctions.Queries.GetCategoryDetail;

public sealed record CategoryDetailViewModel
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
}
