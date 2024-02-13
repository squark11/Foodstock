using AutoMapper;
using FluentValidation;
using FoodStock.Application.Repositories;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.OrderItemFunctions.Commands.UpdateOrderItem;

public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, UpdateOrderItemCommandResponse>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public UpdateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
    {
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
    }
    
    public async Task<UpdateOrderItemCommandResponse> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateOrderItemCommandValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid && validatorResult.Errors != null)
        {
            return new UpdateOrderItemCommandResponse(validatorResult);
        }
        var orderItem = await _orderItemRepository.GetByIdAsync(request.Id);
        if (orderItem is null)
        {
            throw new OrderItemNotFoundException(request.Id);
        }
        _mapper.Map(request, orderItem);
        await _orderItemRepository.UpdateAsync(orderItem);
        return new UpdateOrderItemCommandResponse(orderItem.Id);
    }
}
