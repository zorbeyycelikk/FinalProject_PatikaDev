using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;
[Route("vk/[controller]")]
[ApiController]

public class OrderController : ControllerBase
{
    private readonly IMediator mediator;
    
    public OrderController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
    {
        var operation = new GetAllOrderQuery();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> Get(string id)
    {
        var operation = new GetOrderById(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("ByParameter")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> ByParameter(
        [FromQuery] string? Id,
        [FromQuery] string? CustomerId,
        [FromQuery] string? OrderNumber,
        [FromQuery] string? Description,
        [FromQuery] string? Address ,
        [FromQuery] string? PaymentMethod,
        [FromQuery] string? PaymentRefCode,
        [FromQuery] decimal? minAmount,
        [FromQuery] decimal? maxAmount,
        [FromQuery] string? Status
    )
    {
        var operation = new GetOrderByParametersQuery(Id, CustomerId, OrderNumber,
            Description, Address, PaymentMethod, PaymentRefCode,minAmount,maxAmount,Status);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost]
    [Authorize(Roles = "Bayi")]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var operation = new CreateOrderCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? Conflict() : BadRequest();
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async  Task<IActionResult> Put(string id, [FromBody] UpdateOrderRequest request)
    {
        var operation = new UpdateOrderCommand(request,id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPut("CancelledWithOrderNumber/{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async  Task<IActionResult> CancelledWithOrderNumber(string id)
    {
        var operation = new CancelledWithOrderNumberCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPut("ConfirmWithOrderNumber/{id}")]
    [Authorize(Roles = "Admin")]
    public async  Task<IActionResult> ConfirmWithOrderNumber(string id)
    {
        var operation = new ConfirmWithOrderNumberCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPut("ConfirmWithId/{id}")]
    [Authorize(Roles = "Admin")]
    public async  Task<IActionResult> ConfirmWithId(string id)
    {
        var operation = new ConfirmWithIdCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteById(string id)
    {
        var operation = new DeleteOrderCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
}