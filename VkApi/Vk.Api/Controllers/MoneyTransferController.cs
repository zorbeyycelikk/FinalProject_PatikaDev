using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;
[Route("vk/[controller]")]
[ApiController]

public class MoneyTransferController : ControllerBase
{
    private readonly IMediator mediator;
    
    public MoneyTransferController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    // [HttpGet]
    // public async Task<IActionResult> Get()
    // {
    //     var operation = new GetAllMoneyTransferQuery();
    //     var result = await mediator.Send(operation);
    //     return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    // }
    //
    // [HttpGet("{id}")]
    // public async Task<IActionResult> Get(string id)
    // {
    //     var operation = new GetMoneyTransferById(id);
    //     var result = await mediator.Send(operation);
    //     return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    // }
    //
    // [HttpGet("ByAccount/{id}")]
    // public async Task<IActionResult> GetByAccount(string id)
    // {
    //     var operation = new GetMoneyTransferByAccountNumber(id);
    //     var result = await mediator.Send(operation);
    //     return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    // }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentByEftRequest request)
    {
        var operation = new CreatePaymentByEftTransferCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    // [HttpPut("{id}")]
    // public async  Task<IActionResult> Put(string id, [FromBody] UpdateMoneyTransferRequest request)
    // {
    //     var operation = new UpdateMoneyTransferCommand(request,id);
    //     var result = await mediator.Send(operation);
    //     return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteById(string id)
    // {
    //     var operation = new DeleteMoneyTransferCommand(id);
    //     var result = await mediator.Send(operation);
    //     return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    // }
}