using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using FoodStock.Core.Exceptions;
using FoodStock.Core.Exceptions.BadRequesException;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodStock.Application.Functions.AuthFunctions.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateUserCommandValidator(_userRepository);
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            return new UpdateUserCommandResponse(validatorResult);
        }

        var user = await _userRepository.GetUserDetailAsync(request.Id);
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }
        
        _mapper.Map(request, user);
        await _userRepository.UpdateAsync(user);
        return new UpdateUserCommandResponse(user.Id);

    }
}
