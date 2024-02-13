using AutoMapper;
using FoodStock.Application.Functions.ProductFunctions.Queries.GetProductsList;
using FoodStock.Application.Repositories;
using MediatR;

namespace FoodStock.Application.Functions.AuthFunctions.Queries.GetUsersList;

public class GetUserListHandler : IRequestHandler<GetUserListQuery, List<UserListViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserListHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<List<UserListViewModel>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllWithIncludedAsync();
        return _mapper.Map<List<UserListViewModel>>(users);
    }
}
