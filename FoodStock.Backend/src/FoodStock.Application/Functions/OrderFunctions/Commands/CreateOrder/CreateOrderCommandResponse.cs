using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.OrderFunctions.Commands.CreateOrderCommand;

public class CreateOrderCommandResponse : BaseResponse
{
    public Guid Id { get; set; }
    
    public CreateOrderCommandResponse() : base()
    {
    }

    public CreateOrderCommandResponse(string message) : base(message)
    {
    }

    public CreateOrderCommandResponse(bool success, string message) : base(success, message)
    {
    }

    public CreateOrderCommandResponse(ValidationResult validationResult) : base(validationResult)
    {
    }

    
    public CreateOrderCommandResponse(Guid id)
    {
        Id = id;
    }
}
