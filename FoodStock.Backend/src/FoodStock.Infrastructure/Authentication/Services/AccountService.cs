using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FoodStock.Application.Functions.AuthFunctions.Queries.GetUserDetail;
using FoodStock.Core.Entities;
using FoodStock.Core.Exceptions.BadRequesException;
using FoodStock.Infrastructure.Authentication.DTO;
using FoodStock.Infrastructure.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FoodStock.Infrastructure.Authentication.Services;

internal class AccountService : IAccountService
{
    private readonly FoodStockDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;
    private readonly IMapper _mapper;

    public AccountService(FoodStockDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IMapper mapper)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
        _mapper = mapper;
    }
    public async Task<string> GenerateJwt(LoginDto dto)
    {
        var user = await _dbContext.Users
            .Include(r => r.Role)
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user is null)
        {
            throw new BadRequestException("Invalid username or password");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new BadRequestException("Invalid username or password");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, $"{user.FirstName} {user.Surname}"),
            new(ClaimTypes.Role, $"{user.RoleId}"),
            new(ClaimTypes.Role, $"{user.Role.Name}")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        
        return tokenHandler.WriteToken(token);
    }

    public async Task<UserDetailViewModelByEmail> GetUserByEmail(string email)
    {
        var user = await _dbContext.Users
            .Include(r => r.Role)
            .FirstOrDefaultAsync(e => e.Email == email);
        var userDetail = _mapper.Map<UserDetailViewModelByEmail>(user);
        return userDetail;
    }
}
