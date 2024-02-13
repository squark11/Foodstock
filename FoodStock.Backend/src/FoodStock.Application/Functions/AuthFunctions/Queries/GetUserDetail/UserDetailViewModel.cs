namespace FoodStock.Application.Functions.AuthFunctions.Queries.GetUserDetail;

public sealed record UserDetailViewModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
}
