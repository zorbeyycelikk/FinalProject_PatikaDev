using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;
[Route("vk/[controller]")]
[ApiController]

public class SessionCustomerController : ControllerBase
{
    private readonly IMediator mediator;
    
    public SessionCustomerController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet("GetCustomerInfo")]
    public async Task<IActionResult> CustomerInfo()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerInfoByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("GetCustomerAllAccountInfo")]    
    public async Task<IActionResult> AllAccountInfo()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerAllAccountInfoByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("GetCustomerAllCardInfo")]    
    public async Task<IActionResult> AllCardInfo()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerAllCardInfoByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("GetCustomerAllBasketInfo")] 
    public async Task<IActionResult> BasketInfo()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerAllBasketInfoByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("GetCustomerAllBasketItemInfo")] 
    public async Task<IActionResult> BasketItemInfo()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerAllBasketItemInfoByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    [HttpGet("GetCustomerAllOrderInfo")] 
    public async Task<IActionResult> OrderInfo()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerAllOrderInfoByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    [HttpGet("GetCustomerProductListInfo")] 
    public async Task<IActionResult> ProductListInfo()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerProductListInfoByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
}