using AutoMapper;
using FoodStock.Application.Repositories;
using MediatR;

namespace FoodStock.Application.Functions.ProducentFunctions.Queries.GetProducentListQuery;

public class GetProducentListHandler : IRequestHandler<GetProducentListQuery, List<ProducentListViewModel>>
{
    private readonly IProducentRepository _producentRepository;
    private readonly IMapper _mapper;

    public GetProducentListHandler(IProducentRepository producentRepository, IMapper mapper)
    {
        _producentRepository = producentRepository;
        _mapper = mapper;
    }
    
    public async Task<List<ProducentListViewModel>> Handle(GetProducentListQuery request, CancellationToken cancellationToken)
    {
        var producents = await _producentRepository.GetAllAsync();
        return _mapper.Map<List<ProducentListViewModel>>(producents);
    }
}
