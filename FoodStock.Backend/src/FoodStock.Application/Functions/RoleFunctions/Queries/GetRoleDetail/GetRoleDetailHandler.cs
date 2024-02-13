using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.RoleFunctions.Queries.GetRoleDetail;

public class GetRoleDetailHandler : IRequestHandler<GetRoleDetailQuery, RoleDetailViewModel>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public GetRoleDetailHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
    
    public async Task<RoleDetailViewModel> Handle(GetRoleDetailQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id);
        if (role is null)
        {
            throw new RoleNotFoundException(request.Id);
        }

        var roleDetail = _mapper.Map<RoleDetailViewModel>(role);
        return roleDetail;
    }
}
