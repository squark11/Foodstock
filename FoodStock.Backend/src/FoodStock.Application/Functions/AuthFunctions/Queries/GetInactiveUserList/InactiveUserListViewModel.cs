namespace FoodStock.Application.Functions.AuthFunctions.Queries.GetInactiveUserList;

public sealed record InactiveUserListViewModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string RoleName { get; set; }
}
