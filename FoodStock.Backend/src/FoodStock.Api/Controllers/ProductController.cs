using FoodStock.Application.Functions.CategoryFunctions.Queries.GetCategoryDetail;
using FoodStock.Application.Functions.ProductFunctions.Commands.CreateProduct;
using FoodStock.Application.Functions.ProductFunctions.Commands.DeleteProduct;
using FoodStock.Application.Functions.ProductFunctions.Commands.UpdateProduct;
using FoodStock.Application.Functions.ProductFunctions.Queries;
using FoodStock.Application.Functions.ProductFunctions.Queries.GetProductDetail;
using FoodStock.Application.Functions.ProductFunctions.Queries.GetProductsList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductListViewModel>>> GetAll([FromQuery] string? orderBy)
    {
        var products = await _mediator.Send(new GetProductListQuery {OrderBy = orderBy});
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDetailViewModel>> GetProduct([FromRoute] Guid id)
    {
        var product = await _mediator.Send(new GetProductDetailQuery { Id = id });
        if (product is null)
        {
            return NotFound();
        }
        return Ok(product);
    }


    [HttpGet("byCategoryId")]
    public async Task<ActionResult<List<ProductListByCategoryIdViewModel>>> GetAllByCategoryId(
        [FromQuery] Guid categoryId, [FromQuery] string? orderBy)
    {
        var category = await _mediator.Send(new GetCategoryDetailQuery { Id = categoryId });
        if (category is null)
        {
            return NotFound();
        }

        var products = await _mediator.Send(new GetProductListByCategoryIdQuery { CategoryId = category.Id, OrderBy = orderBy});
        return Ok(products);
    }

    [HttpPost]
    [Authorize(Roles = "Employee, Admin")]
    public async Task<ActionResult<CreateProductCommandResponse>> Post([FromBody] CreateProductCommand command)
    {
        var product = await _mediator.Send(command);

        if (!product.Success && product.ValidationErrors != null)
        {
            return BadRequest(product);
        }
        return Ok(product);
    }

    [HttpPost("byCategoryId")]
    [Authorize(Roles = "Employee, Admin")]
    public async Task<ActionResult<CreateProductCommandResponse>> CreateByCategoryId([FromBody] CreateProductCommand command, [FromQuery] Guid categoryId)
    {
        var category = await _mediator.Send(new GetCategoryDetailQuery{ Id = categoryId });
        
        if (category is null)
        {
            return NotFound();
        }
        
        command.CategoryId = category.Id;
        var product = await _mediator.Send(command);
        if (!product.Success && product.ValidationErrors != null)
        {
            return BadRequest(product);
        }

        return Ok(product);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UpdateProductCommandResponse>> Update([FromBody] UpdateProductCommand command,
        [FromRoute] Guid id)
    {
        var product = await _mediator.Send(command with { Id = id });
        if (!product.Success && product.ValidationErrors != null)
        {
            return BadRequest(product);
        }
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
         await _mediator.Send(new DeleteProductCommand { Id = id });
        return NoContent();
    }
}
