using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using MediatR;

namespace FoodStock.Application.Functions.RoleFunctions.Commands.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, UpdateRoleCommandResponse>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public UpdateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
    
    public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateRoleCommandValidator(_roleRepository);
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            return new UpdateRoleCommandResponse(validatorResult);
        }

        var role = _mapper.Map<Role>(request);
        await _roleRepository.UpdateAsync(role);
        return new UpdateRoleCommandResponse(role.Id);
    }
}
