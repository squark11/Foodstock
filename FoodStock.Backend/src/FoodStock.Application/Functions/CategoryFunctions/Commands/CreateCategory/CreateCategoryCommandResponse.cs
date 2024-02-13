using FoodStock.Aplication.Responses;
using FluentValidation.Results;
using FoodStock.Application.Functions.CategoryFunctions.Queries.GetCategoriesList;


namespace FoodStock.Application.Functions.CategoryFunctions.Commands.CreateCategory;

public class CreateCategoryCommandResponse : BaseResponse
{
    public Guid? CategoryId { get; set; }

    public CreateCategoryCommandResponse() : base()
    {}

    public CreateCategoryCommandResponse(ValidationResult validationResult)
        : base(validationResult)
    {}

    public CreateCategoryCommandResponse(string message)
    : base(message)
    {}

    public CreateCategoryCommandResponse(bool sucess, string message)
    : base(sucess, message)
    {}

    public CreateCategoryCommandResponse(Guid categoryId)
    {
        CategoryId = categoryId;
    }
    
}
