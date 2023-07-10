using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShowcaseProject.Products.Application.Commands;
using ShowcaseProject.Products.Application.Queries.Products;

namespace ShowcaseProject.Products.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductQueries _productQueries;
    private readonly IMediator _mediator;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductQueries productQueries, IMediator mediator, ILogger<ProductsController> logger)
    {
        _productQueries = productQueries.ThrowIfNull();
        _mediator = mediator.ThrowIfNull();
        _logger = logger;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var result = await _productQueries.GetProductAsync(id);

        if(result is null) return NotFound("");

        return result;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts(bool availableOnly = false)
    {
        return await _productQueries.GetAllProductsAsync(availableOnly);
    }

    [Route("Create")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> CreateProduct([FromBody]CreateProductCommand command)
    {
        try
        {
            return await _mediator.Send(command);
        }
        catch (Exception ex)
        {
            _logger.LogError("ProductsController.CreateProduct caught an exception.", ex);
            return BadRequest(ex.Message);
        }
    }

    [Route("AddStock")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddToStock([FromBody] AddToStockCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("ProductsController.AddToStock caught an exception.", ex);
            return BadRequest(ex.Message);
        }
    }

    [Route("RemoveStock")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RemoveFromStock([FromBody] RemoveFromStockCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("ProductsController.RemoveFromStock caught an exception.", ex);
            return BadRequest(ex.Message);
        }
    }

    [Route("SetAvailable")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SetAvailableStatus([FromBody] SetAvailableStatusCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("ProductsController.SetAvailableStatus caught an exception.", ex);
            return BadRequest(ex.Message);
        }
    }

    [Route("SetUnavailable")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SetUnavailableStatus([FromBody] SetUnavailableStatusCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("ProductsController.SetUnavailableStatus caught an exception.", ex);
            return BadRequest(ex.Message);
        }
    }

    [Route("SetDiscontinued")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SetDiscontinuedStatus([FromBody] SetDiscontinuedStatusCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("ProductsController.SetDiscontinuedStatus caught an exception.", ex);
            return BadRequest(ex.Message);
        }
    }
}
