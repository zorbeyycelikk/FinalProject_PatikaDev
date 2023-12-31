using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;
[Route("vk/[controller]")]
[ApiController]

public class ProductController : ControllerBase
{
    private readonly IMediator mediator;
    
    public ProductController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> Get()
    {
        var operation = new GetAllProductQuery();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> Get(string id)
    {
        var operation = new GetProductById(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("GetAllUniqueProductCategoryNamesQuery")] 
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> GetCategories()
    {
        var operation = new GetAllUniqueProductCategoryNamesQuery();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("ByParameter")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> ByParameter(
        [FromQuery] string? Id,
        [FromQuery] string? Name,
        [FromQuery] string? Category,
        [FromQuery] int? minStock,
        [FromQuery] int? maxStock,
        [FromQuery] int? minPrice,
        [FromQuery] int? maxPrice
    )
    {
        var operation = new GetProductByParametersQuery(Id, Name, Category,
            minStock, maxStock, minPrice, maxPrice);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var operation = new CreateProductCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? Conflict() : BadRequest();
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async  Task<IActionResult> Put(string id, [FromBody] UpdateProductRequest request)
    {
        var operation = new UpdateProductCommand(request,id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPut("ProductStockAfterCreateOrder")]
    [Authorize(Roles = "Admin,Bayi")]
    public async  Task<IActionResult> UpdateProductStockAfterCreateOrder(string basketId)
    {
        var operation = new UpdateProductStockAfterCreateOrderCommand(basketId);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPut("ProductStockAfterCancelledOrder")]
    [Authorize(Roles = "Admin,Bayi")]
    public async  Task<IActionResult> UpdateProductStockAfterCancelledOrder(string basketId)
    {
        var operation = new UpdateProductStockAfterCancelledOrderCommand(basketId);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteById(string id)
    {
        var operation = new DeleteProductCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
}