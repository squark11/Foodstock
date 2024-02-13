using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using FoodStock.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodStock.Application.Functions.AuthFunctions.Commands.UpdatePassword;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordCommand, UpdatePasswordResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UpdatePasswordHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<UpdatePasswordResponse> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdatePasswordValidator();
        var validationResult = await validator.ValidateAsync(request);
        
        if (!validationResult.IsValid )
        {
            return new UpdatePasswordResponse(validationResult);
        }

        var user = await _userRepository.GetUserDetailAsync(request.Id);
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        _mapper.Map(request, user);
        var hashedPassword = _passwordHasher.HashPassword(user, request.Password);
        user.Password = hashedPassword;
        await _userRepository.UpdateAsync(user);
        return new UpdatePasswordResponse(user.Id);
    }
}
