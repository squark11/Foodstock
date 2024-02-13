using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using MediatR;

namespace FoodStock.Application.Functions.ProducentFunctions.Commands.UpdateProducent;

public class UpdateProducentCommandHandler : IRequestHandler<UpdateProducentCommand, UpdateProducentCommandResponse>
{
    private readonly IProducentRepository _producentRepository;
    private readonly IMapper _mapper;

    public UpdateProducentCommandHandler(IProducentRepository producentRepository, IMapper mapper)
    {
        _producentRepository = producentRepository;
        _mapper = mapper;
    }

    public async Task<UpdateProducentCommandResponse> Handle(UpdateProducentCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateProducentCommandValidator(_producentRepository);
        var validatorResult = await validator.ValidateAsync(request);
        
        if (!validatorResult.IsValid)
        {
            return new UpdateProducentCommandResponse(validatorResult);
        }

        var producent = _mapper.Map<Producent>(request);
        await _producentRepository.UpdateAsync(producent);
        return new UpdateProducentCommandResponse(producent.Id);
    }
}
