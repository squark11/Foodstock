using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodStock.Application.Functions.AuthFunctions;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new RegisterUserCommandValidator(_userRepository);
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            return new RegisterUserCommandResponse(validatorResult);
        }
        
        var user = _mapper.Map<User>(request with { Id = Guid.NewGuid() });
        var hashedPassword = _passwordHasher.HashPassword(user, request.Password);
        user.Password = hashedPassword;
        user = await _userRepository.AddAsync(user);

        return new RegisterUserCommandResponse(user.Id);
    }
}
