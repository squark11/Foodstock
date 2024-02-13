namespace FoodStock.Core.Exceptions.AlreadyExistsException;

public sealed class OrganizationAlreadyExistsException : CustomException
{
    public OrganizationAlreadyExistsException() : base("You have already configured your organization information!")
    {
    }
}
