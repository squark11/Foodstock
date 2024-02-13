using FluentValidation.Results;

namespace FoodStock.Aplication.Responses;

public abstract class BaseResponse
{
    public ResponseStatus Status { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public List<string> ValidationErrors { get; set; }

    public BaseResponse()
    {
        ValidationErrors = new List<string>();
        Success = true;
    }

    public BaseResponse(string message = null)
    {
        ValidationErrors = new List<string>();
        Success = true;
        Message = message;
    }

    public BaseResponse(bool success, string message)
    {
        Success = success;
        Message = message;
        ValidationErrors = new List<string>();
    }
    
    public BaseResponse(ValidationResult validationResult)
    {
        ValidationErrors = new List<String>();
        Success = validationResult.Errors.Count < 0;
        foreach (var item in validationResult.Errors)
        {
            ValidationErrors.Add(item.ErrorMessage);
        }

    }

    public enum ResponseStatus
    {
        Success = 0,
        NotFound = 1,
        BadQuery = 2,
        ValidationError = 3
    }
}
