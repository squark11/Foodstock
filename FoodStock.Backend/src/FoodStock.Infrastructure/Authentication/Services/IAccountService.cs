using FoodStock.Application.Functions.AuthFunctions.Queries.GetUserDetail;
using FoodStock.Infrastructure.Authentication.DTO;

namespace FoodStock.Infrastructure.Authentication.Services;

public interface IAccountService
{ 
    Task<string> GenerateJwt(LoginDto dto);
    Task<UserDetailViewModelByEmail> GetUserByEmail(string email);
}
