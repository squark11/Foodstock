using MediatR;

namespace FoodStock.Application.Functions.CategoryFunctions.Commands.DeleteCategory;

public sealed record DeleteCategoryCommand : IRequest
{
    public Guid Id { get; set; }
}
