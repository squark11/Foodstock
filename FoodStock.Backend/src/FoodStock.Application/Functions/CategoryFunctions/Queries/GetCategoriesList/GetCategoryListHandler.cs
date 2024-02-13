using AutoMapper;
using FoodStock.Application.Repositories;
using MediatR;

namespace FoodStock.Application.Functions.CategoryFunctions.Queries.GetCategoriesList;

public class GetCategoryListHandler : IRequestHandler<GetCategoryListQuery, List<CategoryListViewModel>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryListHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoryListViewModel>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync();
        var sorted = categories.OrderBy(x => x.CategoryName);
        return _mapper.Map<List<CategoryListViewModel>>(sorted);
    }
}
