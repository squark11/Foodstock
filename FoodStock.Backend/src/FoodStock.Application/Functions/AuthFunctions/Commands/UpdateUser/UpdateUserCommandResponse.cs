using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.AuthFunctions.Commands.UpdateUser;

public class UpdateUserCommandResponse : BaseResponse
{
    public Guid UserId { get; set; }

    public UpdateUserCommandResponse() : base()
    {
    }

    public UpdateUserCommandResponse(ValidationResult validationResult) : base(validationResult)
    {
    }
    
    public UpdateUserCommandResponse(string message) : base(message)
    {
    }
    
    public UpdateUserCommandResponse(bool sucess, string message)
        : base(sucess, message)
    {
    }
    
    public UpdateUserCommandResponse(Guid userId)
    {
        UserId = userId;
    }
}
