using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation;
using Vk.Operation.Command.CompleteOrderWithPaymentMethodsCommandHandlers;
using Vk.Operation.Cqrs;
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
            
    [HttpPost("CompleteOrderWithHavale")]
    public async Task<IActionResult> CompleteOrderWithHavale([FromBody] CreateCompleteOrderWithHavaleRequest request)
    {
        var operation = new CompleteOrderWithHavaleTransfer(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
}
    