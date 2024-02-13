namespace FoodStock.Application.Functions.AuthFunctions.Queries.GetUsersList;

public sealed record UserListViewModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
}
