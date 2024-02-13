using FoodStock.Application.Functions.AuthFunctions.Queries.GetUserDetail;

namespace FoodStock.Infrastructure.Authentication.DTO;

public class LoginResponse
{
    public string Token { get; set; }
    public UserDetailViewModelByEmail User { get; set; }
}
