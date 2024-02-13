using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.CategoryFunctions.Queries.GetCategoryDetail;

public class GetCategoryDetailHandler : IRequestHandler<GetCategoryDetailQuery, CategoryDetailViewModel>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryDetailHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<CategoryDetailViewModel> Handle(GetCategoryDetailQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category is null)
        {
            throw new CategoryNotFoundException(request.Id);
        }

        var categoryDetail = _mapper.Map<CategoryDetailViewModel>(category);
        return categoryDetail;
    }
}
