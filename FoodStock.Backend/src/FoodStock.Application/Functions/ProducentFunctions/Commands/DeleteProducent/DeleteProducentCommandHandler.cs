using FoodStock.Application.Repositories;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.ProducentFunctions.Commands.DeleteProducent;

public class DeleteProducentCommandHandler : IRequestHandler<DeleteProducentCommand>
{
    private readonly IProducentRepository _producentRepository;

    public DeleteProducentCommandHandler(IProducentRepository producentRepository)
    {
        _producentRepository = producentRepository;
    }
    
    public async Task<Unit> Handle(DeleteProducentCommand request, CancellationToken cancellationToken)
    {
        var producent = await _producentRepository.GetByIdAsync(request.Id);
        if (producent is null)
        {
            throw new ProducentNotFoundException(request.Id);
        }
        await _producentRepository.DeleteAsync(producent);
        
        return Unit.Value;
    }
}
