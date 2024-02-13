using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.OrderFunctions.Commands.UpdateOrder;

public class UpdateOrderCommandResponse : BaseResponse
{
    public Guid Id { get; set; }

    public UpdateOrderCommandResponse() : base()
    {
    }

    public UpdateOrderCommandResponse(string message) : base(message)
    {
    }

    public UpdateOrderCommandResponse(bool success, string message) : base(success, message)
    {
    }

    public UpdateOrderCommandResponse(ValidationResult validationResult) : base(validationResult)
    {
    }

    public UpdateOrderCommandResponse(Guid id)
    {
        Id = id;
    }
}
