using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.CategoryFunctions.Commands.UpdateCategory;

public class UpdateCategoryCommandResponse : BaseResponse
{
    public Guid? CategoryId { get; set; }

    public UpdateCategoryCommandResponse() : base()
    { }
    
    public UpdateCategoryCommandResponse(ValidationResult validationResult) 
        : base(validationResult)
    {}
    
    public UpdateCategoryCommandResponse(string message) 
        : base(message)
    {}

    public UpdateCategoryCommandResponse(bool success, string message)
        : base(success, message)
    {}
    
    public UpdateCategoryCommandResponse(Guid categoryId)
    {
        CategoryId = categoryId;
    }
}

