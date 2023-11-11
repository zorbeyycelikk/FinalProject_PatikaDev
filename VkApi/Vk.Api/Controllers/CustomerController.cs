using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;
[Route("vk/[controller]")]
[ApiController]

public class CustomerController : ControllerBase
{
    private readonly IMediator mediator;
    
    public CustomerController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    // [Authorize(Roles = "admin")]
    public async Task<IActionResult> Get()
    {
        var operation = new GetAllCustomerQuery();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var operation = new GetCustomerById(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("ByParameter")]
    public async Task<IActionResult> ByParameter(
        [FromQuery] string? Id,
        [FromQuery] string? Name,
        [FromQuery] string? Email,
        [FromQuery] string? Phone,
        [FromQuery] string? Role ,
        [FromQuery] decimal? minProfit,
        [FromQuery] decimal? maxProfit,
        [FromQuery] decimal? minopenAccountLimit,
        [FromQuery] decimal? maxopenAccountLimit
    )
    {
        var operation = new GetCustomerByParametersQuery(Id, Name, Email,
            Phone, Role, minProfit, maxProfit,minopenAccountLimit,maxopenAccountLimit);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
    {
        var operation = new CreateCustomerCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? Conflict() : BadRequest();
    }
    
    [HttpPut("{id}")]
    public async  Task<IActionResult> Put(string id, [FromBody] UpdateCustomerRequest request)
    {
        var operation = new UpdateCustomerCommand(request,id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(string id)
    {
        var operation = new DeleteCustomerCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
}