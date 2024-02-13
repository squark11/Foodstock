using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.AuthFunctions.Queries.GetUserDetail;

public class GetUserDetailHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserDetailHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserDetailAsync(request.Id);
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        var userDetail = _mapper.Map<UserDetailViewModel>(user);
        return userDetail;
    }
}
