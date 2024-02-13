using FoodStock.Application.Repositories;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.AuthFunctions.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }
        
        user.IsActive = false;
        await _userRepository.UpdateAsync(user);
        return Unit.Value;
    }
}
