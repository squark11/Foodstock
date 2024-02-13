using MediatR;

namespace FoodStock.Application.Functions.CategoryFunctions.Commands.CreateCategory;

public sealed record CreateCategoryCommand : IRequest<CreateCategoryCommandResponse>
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
}
