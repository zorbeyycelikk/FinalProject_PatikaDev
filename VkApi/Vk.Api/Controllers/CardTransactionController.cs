using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;
[Route("vk/[controller]")]
[ApiController]

public class CardTransactionController : ControllerBase
{
    private readonly IMediator mediator;
    
    public CardTransactionController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> Get()
    {
        var operation = new GetAllCardTransactionQuery();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> Get(string id)
    {
        var operation = new GetCardTransactionById(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("ByParameter")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> ByParameter(
        [FromQuery] string? transactionRefNumber,
        [FromQuery] string? CardId,
        [FromQuery] string? receiverAccountNumber,
        [FromQuery] string? CardNumber,
        [FromQuery] string? Status,
        [FromQuery] decimal? minAmount,
        [FromQuery] decimal? maxAmount
    )
    {
        var operation = new GetCardTransactionByParametersQuery(transactionRefNumber, CardId,receiverAccountNumber, 
            CardNumber, Status , minAmount , maxAmount);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> Create([FromBody] CreateCardTransactionRequest request)
    {
        var operation = new CreateCardTransactionCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? Conflict() : BadRequest();
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async  Task<IActionResult> Put(string id, [FromBody] UpdateCardTransactionRequest request)
    {
        var operation = new UpdateCardTransactionCommand(request,id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> DeleteById(string id)
    {
        var operation = new DeleteCardTransactionCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
}