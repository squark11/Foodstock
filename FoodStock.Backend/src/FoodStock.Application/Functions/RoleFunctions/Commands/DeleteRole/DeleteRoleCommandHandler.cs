using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.RoleFunctions.Commands.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public DeleteRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
    }
    
    public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id);
        if (role is null)
        {
            throw new RoleNotFoundException(request.Id);
        }
        await _roleRepository.DeleteAsync(role);
        
        return Unit.Value;
    }
}
