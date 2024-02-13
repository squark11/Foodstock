using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.AuthFunctions;

public class RegisterUserCommandResponse : BaseResponse
{
    public Guid UserId { get; set; }

    public RegisterUserCommandResponse() : base()
    {
    }
    
    
    public RegisterUserCommandResponse(ValidationResult validationResult) : base(validationResult)
    {
    }
    
    public RegisterUserCommandResponse(string message) : base(message)
    {
    }

    public RegisterUserCommandResponse(bool sucess, string message)
        : base(sucess, message)
    {
    }

    public RegisterUserCommandResponse(Guid userId)
    {
        UserId = userId;
    }
}
