using FoodStock.Application.Repositories;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.OrderItemFunctions.Commands.DeleteOrderItem;

public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, Unit>
{
    private readonly IOrderItemRepository _orderItemRepository;

    public DeleteOrderItemCommandHandler(IOrderItemRepository orderItemRepository)
    {
        _orderItemRepository = orderItemRepository;
    }

    public async Task<Unit> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItem = await _orderItemRepository.GetByIdAsync(request.Id);
        if (orderItem is null)
        {
            throw new OrderItemNotFoundException(request.Id);
        }

        await _orderItemRepository.DeleteAsync(orderItem);
        return Unit.Value;
    }
}
