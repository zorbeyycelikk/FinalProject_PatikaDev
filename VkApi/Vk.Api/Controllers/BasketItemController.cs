using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;
[Route("vk/[controller]")]
[ApiController]

public class BasketItemController : ControllerBase
{
    private readonly IMediator mediator;
    
    public BasketItemController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var operation = new GetAllBasketItemQuery();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var operation = new GetBasketItemById(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("ByParameter")]
    public async Task<IActionResult> ByParameter(
        [FromQuery] int? minQuantity,
        [FromQuery] int? maxQuantity
    )
    {
        var operation = new GetBasketItemByParametersQuery(minQuantity, maxQuantity);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBasketItemRequest request)
    {
        var operation = new CreateBasketItemCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? Conflict() : BadRequest();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(string id)
    {
        var operation = new DeleteBasketItemCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    [HttpDelete("HardDelete/{id}")]
    public async Task<IActionResult> HardDeleteById(string id)
    {
        var operation = new HardDeleteBasketItemCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("HardDeleteByProductId/{id}")]
    public async Task<IActionResult> DeleteByProductId(string id)
    {
        var operation = new HardDeleteBasketItemByProductNumberCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
}