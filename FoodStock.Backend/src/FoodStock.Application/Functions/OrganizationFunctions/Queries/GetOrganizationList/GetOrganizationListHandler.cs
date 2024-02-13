using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using MediatR;

namespace FoodStock.Application.Functions.OrganizationFunctions.Queries.GetOrganizationList;

public class GetOrganizationListHandler : IRequestHandler<GetOrganizationListQuery, List<GetOrganizationListViewModel>>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;

    public GetOrganizationListHandler(IOrganizationRepository organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
    }
    
    public async Task<List<GetOrganizationListViewModel>> Handle(GetOrganizationListQuery request, CancellationToken cancellationToken)
    {
        var organizations = await _organizationRepository.GetAllAsync();
        return _mapper.Map<List<GetOrganizationListViewModel>>(organizations);
    }
}
