using MediatR;

namespace FoodStock.Application.Functions.CategoryFunctions.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand : IRequest<UpdateCategoryCommandResponse>
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
}
