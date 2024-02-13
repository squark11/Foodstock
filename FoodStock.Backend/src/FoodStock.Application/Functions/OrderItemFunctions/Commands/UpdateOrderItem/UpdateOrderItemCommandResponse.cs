using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.OrderItemFunctions.Commands.UpdateOrderItem;

public class UpdateOrderItemCommandResponse : BaseResponse
{
    public Guid Id { get; set; }
    
    public UpdateOrderItemCommandResponse() : base()
    {
    }

    public UpdateOrderItemCommandResponse(string message) : base(message)
    {
    }

    public UpdateOrderItemCommandResponse(bool success, string message) : base(success, message)
    {
    }

    public UpdateOrderItemCommandResponse(ValidationResult validationResult) : base(validationResult)
    {
    }

    public UpdateOrderItemCommandResponse(Guid id)
    {
        Id = id;
    }
}
