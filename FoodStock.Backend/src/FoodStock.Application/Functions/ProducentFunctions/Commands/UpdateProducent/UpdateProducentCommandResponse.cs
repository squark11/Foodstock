using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.ProducentFunctions.Commands.UpdateProducent;

public class UpdateProducentCommandResponse : BaseResponse
{
    public Guid ProducentId { get; set; }
    
    public UpdateProducentCommandResponse() : base()
    {
    }
    
    public UpdateProducentCommandResponse(string message) : base(message)
    {
    }
    
    public UpdateProducentCommandResponse(bool success, string message) : base(success, message)
    {
    }
    
    public UpdateProducentCommandResponse(ValidationResult validationResult) : base(validationResult)
    {
    }
    
    public UpdateProducentCommandResponse(Guid producentId)
    {
        ProducentId = producentId;
    }
}
