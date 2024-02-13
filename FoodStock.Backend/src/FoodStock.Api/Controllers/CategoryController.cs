using FoodStock.Application.Functions.CategoryFunctions.Commands.CreateCategory;
using FoodStock.Application.Functions.CategoryFunctions.Commands.DeleteCategory;
using FoodStock.Application.Functions.CategoryFunctions.Commands.UpdateCategory;
using FoodStock.Application.Functions.CategoryFunctions.Queries.GetCategoriesList;
using FoodStock.Application.Functions.CategoryFunctions.Queries.GetCategoryDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryListViewModel>>> GetAll()
    {
        var categories = await _mediator.Send(new GetCategoryListQuery());
        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryDetailViewModel>> GetCategory([FromRoute] Guid id)
    {
        var category = await _mediator.Send(new GetCategoryDetailQuery { Id = id });
        if (category is null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CreateCategoryCommandResponse>> Post([FromBody] CreateCategoryCommand command)
    {
        var category = await _mediator.Send(command);

        if (!category.Success && category.ValidationErrors != null)
        {
            return BadRequest(category);
        }

        return Ok(category);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UpdateCategoryCommandResponse>> Update([FromBody] UpdateCategoryCommand command,
        [FromRoute] Guid id)
    {
        var category = await _mediator.Send(command with { Id = id });

        if (!category.Success && category.ValidationErrors != null)
        {
            return BadRequest(category);
        }
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteCategoryCommand() with { Id = id });
        return NoContent();
    }
}
