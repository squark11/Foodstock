using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.OrderItemFunctions.Commands.CreateOrderItemByProduct;

public class CreateOrderItemByProductResponse : BaseResponse
{
    public Guid OrderItemId { get; set; }

    public CreateOrderItemByProductResponse() : base()
    {
    }

    public CreateOrderItemByProductResponse(string message) : base(message)
    {
    }

    public CreateOrderItemByProductResponse(bool success, string message) : base(success, message)
    {
    }

    public CreateOrderItemByProductResponse(ValidationResult validationResult) : base(validationResult)
    {
    }
    
    public CreateOrderItemByProductResponse(Guid orderItemId)
    {
        OrderItemId = orderItemId;
    }
}
