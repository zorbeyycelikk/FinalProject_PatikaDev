using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;
[Route("vk/[controller]")]
[ApiController]

public class CardController : ControllerBase
{
    private readonly IMediator mediator;
    
    public CardController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> Get()
    {
        var operation = new GetAllCardQuery();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> Get(string id)
    {
        var operation = new GetCardById(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("ByAccount/{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> GetByAccount(string id)
    {
        var operation = new GetCardByAccountNumber(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("ByParameter")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> ByParameter(
        [FromQuery] DateTime? ExpiryDate
    )
    {
        var operation = new GetCardByParametersQuery(ExpiryDate);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> Create([FromBody] CreateCardRequest request)
    {
        var operation = new CreateCardCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? Conflict() : BadRequest();
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async  Task<IActionResult> Put(string id, [FromBody] UpdateCardRequest request)
    {
        var operation = new UpdateCardCommand(request,id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> DeleteById(string id)
    {
        var operation = new DeleteCardCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
}