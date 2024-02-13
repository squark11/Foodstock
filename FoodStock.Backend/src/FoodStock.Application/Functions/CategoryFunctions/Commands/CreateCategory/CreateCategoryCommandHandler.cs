using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using MediatR;

namespace FoodStock.Application.Functions.CategoryFunctions.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateCategoryCommandValidator();
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            return new CreateCategoryCommandResponse(validatorResult);
        }

        var category = _mapper.Map<Category>(request with { Id = Guid.NewGuid() });

        category = await _categoryRepository.AddAsync(category);
        
        return new CreateCategoryCommandResponse(category.Id);
    }
}
