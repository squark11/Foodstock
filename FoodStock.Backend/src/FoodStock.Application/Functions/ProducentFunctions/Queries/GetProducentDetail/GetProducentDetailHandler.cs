using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.ProducentFunctions.Queries.GetProducentDetail;

public class GetProducentDetailHandler : IRequestHandler<GetProducentDetailQuery, ProducentDetailViewModel>
{
    private readonly IProducentRepository _producentRepository;
    private readonly IMapper _mapper;

    public GetProducentDetailHandler(IProducentRepository producentRepository, IMapper mapper)
    {
        _producentRepository = producentRepository;
        _mapper = mapper;
    }
    
    public async Task<ProducentDetailViewModel> Handle(GetProducentDetailQuery request, CancellationToken cancellationToken)
    {
        var producent = await _producentRepository.GetByIdAsync(request.Id);
        if (producent is null)
        {
            throw new ProducentNotFoundException(request.Id);
        }

        var producentDetail = _mapper.Map<ProducentDetailViewModel>(producent);

        return producentDetail;
    }
}
