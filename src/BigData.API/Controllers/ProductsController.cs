using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BigData.Application.DTOs;
using BigData.Application.Features.Products.Commands.CreateProduct;
using BigData.Application.Features.Products.Commands.UpdateProduct;
using BigData.Application.Features.Products.Commands.DeleteProduct;
using BigData.Application.Features.Products.Queries.GetProductById;
using BigData.Application.Features.Products.Queries.GetAllProducts;

namespace BigData.API.Controllers;

/// <summary>
/// Products controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all products with pagination
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetAllProductsQuery(pageNumber, pageSize);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    /// <summary>
    /// Get product by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    /// <summary>
    /// Create new product
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductDto productDto)
    {
        var command = new CreateProductCommand(productDto);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        return BadRequest(result.Error);
    }

    /// <summary>
    /// Update existing product
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateProductDto productDto)
    {
        var command = new UpdateProductCommand(id, productDto);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    /// <summary>
    /// Delete product
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteProductCommand(id);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return NotFound(result.Error);
    }
}
