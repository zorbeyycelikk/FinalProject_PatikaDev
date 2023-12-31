using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;
[Route("vk/[controller]")]
[ApiController]

public class PaymentMethodController : ControllerBase
{
    private readonly IMediator mediator;
    
    public PaymentMethodController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("EftPayment")]  
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> CreateEftTransfer([FromBody] CreatePaymentByEftRequest request)
    {
        var operation = new CreatePaymentByEftTransferCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost("HavalePayment")]  
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> CreateHavaleTransfer([FromBody] CreatePaymentByHavaleRequest request)
    {
        var operation = new CreatePaymentByHavaleTransferCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost("CardPayment")] 
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> CreateCardTransfer([FromBody] CreatePaymentByCardRequest request)
    {
        var operation = new CreatePaymentCardTransferCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost("OpenAccountPayment")] 
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> CreateOpenAccountTransfer([FromBody] CreatePaymentByOpenAccountRequest request)
    {
        var operation = new CreatePaymentOpenAccountTransferCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    
}