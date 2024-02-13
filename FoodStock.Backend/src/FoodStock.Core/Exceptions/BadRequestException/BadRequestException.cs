namespace FoodStock.Core.Exceptions.BadRequesException;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }
}
