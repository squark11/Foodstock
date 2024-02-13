using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.AuthFunctions.Commands.UpdatePassword;

public class UpdatePasswordResponse : BaseResponse
{
    public Guid Id { get; set; }

    public UpdatePasswordResponse() : base()
    {
    }

    public UpdatePasswordResponse(string message) : base(message)
    {
    }

    public UpdatePasswordResponse(bool success, string message) : base(success, message)
    {
    }

    public UpdatePasswordResponse(ValidationResult validationResult) : base(validationResult)
    {
    }

    public UpdatePasswordResponse(Guid id)
    {
        Id = id;
    }
}
