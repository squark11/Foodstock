using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using MediatR;

namespace FoodStock.Application.Functions.CategoryFunctions.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateCategoryCommandValidator();
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            return new UpdateCategoryCommandResponse(validatorResult);
        }

        var category = _mapper.Map<Category>(request);
        await _categoryRepository.UpdateAsync(category);
        return new UpdateCategoryCommandResponse(category.Id);
    }
}
