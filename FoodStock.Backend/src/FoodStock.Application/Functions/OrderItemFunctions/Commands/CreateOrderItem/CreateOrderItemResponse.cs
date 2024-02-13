using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.OrderItemFunctions.Commands.CreateOrderItem;

public class CreateOrderItemResponse : BaseResponse
{
    public Guid Id { get; set; }
    
    public CreateOrderItemResponse() : base()
    {
    }

    public CreateOrderItemResponse(string message) : base(message)
    {
    }

    public CreateOrderItemResponse(bool success, string message) : base(success, message)
    {
    }

    public CreateOrderItemResponse(ValidationResult validationResult) : base(validationResult)
    {
    }

    public CreateOrderItemResponse(Guid id)
    {
        Id = id;
    }
}
