namespace FoodStock.Core.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public Guid UserId { get; }
    
    public UserNotFoundException(Guid userId) : base($"User with id: {userId} not found.")
    {
        UserId = userId;
    }
}
