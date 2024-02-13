using AutoMapper;
using FoodStock.Application.Repositories;
using MediatR;

namespace FoodStock.Application.Functions.RoleFunctions.Queries.GetRolesList;

public class GetRoleListHandler : IRequestHandler<GetRoleListQuery, List<RoleListViewModel>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public GetRoleListHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
    
    public async Task<List<RoleListViewModel>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllAsync();
        return _mapper.Map<List<RoleListViewModel>>(roles);
    }
}
