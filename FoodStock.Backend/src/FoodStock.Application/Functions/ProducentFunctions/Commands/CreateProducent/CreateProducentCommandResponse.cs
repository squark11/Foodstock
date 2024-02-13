using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.ProducentFunctions.Commands.CreateProducent;

public class CreateProducentCommandResponse : BaseResponse
{
    public Guid ProducentId { get; set; }

    public CreateProducentCommandResponse() : base()
    {
    }
    
    public CreateProducentCommandResponse(string message) : base(message)
    {
    }
    
    public CreateProducentCommandResponse(ValidationResult validationResult) : base(validationResult)
    {
    }
    
    public CreateProducentCommandResponse(bool success, string message) : base(success, message)
    {
    }
    
    public CreateProducentCommandResponse(Guid producentId)
    {
        ProducentId = producentId;
    }
}
