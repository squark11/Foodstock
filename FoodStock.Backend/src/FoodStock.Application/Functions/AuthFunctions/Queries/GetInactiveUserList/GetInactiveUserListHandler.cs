using AutoMapper;
using FoodStock.Application.Repositories;
using MediatR;

namespace FoodStock.Application.Functions.AuthFunctions.Queries.GetInactiveUserList;

public class GetInactiveUserListHandler : IRequestHandler<GetInactiveUserListQuery, List<InactiveUserListViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetInactiveUserListHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<List<InactiveUserListViewModel>> Handle(GetInactiveUserListQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllInactiveWithIncludeAsync();
        return _mapper.Map<List<InactiveUserListViewModel>>(users);
    }
}
