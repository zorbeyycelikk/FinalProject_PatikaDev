using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vk.Operation.Command.CompleteOrderWithPaymentMethodsCommandHandlers;
using Vk.Operation.Cqrs.CompleteOrderWithPaymentMethodsCqrs;
using Vk.Schema;

namespace VkApi.Controllers;


[Route("vk/[controller]")]
[ApiController]
public class CompleteOrderWithPaymentMethods : ControllerBase
{
    private readonly IMediator mediator;

    public CompleteOrderWithPaymentMethods(IMediator mediator)
    {
        this.mediator = mediator;
    }
            
    // Havale yöntemi ile sipariş başarılı , başarısız şekilde oluşturulur
    [HttpPost("CompleteOrderWithHavale")]
    public async Task<IActionResult> CompleteOrderWithHavale([FromBody] CreateCompleteOrderWithHavaleRequest request)
    {
        var operation = new CompleteOrderWithHavaleTransfer(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    // Havale yöntemi ile sipariş başarılı , başarısız şekilde oluşturulur
    [HttpPost("CompleteOrderWithEft")]
    public async Task<IActionResult> CompleteOrderWithEft([FromBody] CreateCompleteOrderWithEftRequest request)
    {
        var operation = new CompleteOrderWithEftTransfer(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    // Card yöntemi ile sipariş başarılı , başarısız şekilde oluşturulur
    [HttpPost("CompleteOrderWithCard")]
    public async Task<IActionResult> CompleteOrderWithEft([FromBody] CreateCompleteOrderWithCardRequest request)
    {
        var operation = new CompleteOrderWithCardTransfer(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    // Card yöntemi ile sipariş başarılı , başarısız şekilde oluşturulur
    [HttpPost("CompleteOrderWithOpenAccount")]
    public async Task<IActionResult> CompleteOrderWithOpenAccount([FromBody] CreateCompleteOrderWithOpenAccountRequest request)
    {
        var operation = new CompleteOrderWithOpenAccountTransfer(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
}
    