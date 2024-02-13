using AutoMapper;
using FluentValidation.Results;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using MediatR;

namespace FoodStock.Application.Functions.RoleFunctions.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreateRoleCommandResponse>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
    
    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateRoleCommandValidator(_roleRepository);
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            return new CreateRoleCommandResponse(validatorResult);
        }

        var role = _mapper.Map<Role>(request with { Id = Guid.NewGuid() });
        role = await _roleRepository.AddAsync(role);

        return new CreateRoleCommandResponse(role.Id);
    }
}
