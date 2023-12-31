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
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> AllCardInfo()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerAllCardInfoByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("GetCustomerAllBasketInfo")] 
    [Authorize(Roles = "Bayi")]
    public async Task<IActionResult> BasketInfo()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerAllBasketInfoByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("GetCustomerActiveBasketInfo")] 
    [Authorize(Roles = "Bayi")]
    public async Task<IActionResult> ActiveBasketInfo()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerActiveBasketInfoByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    [HttpGet("GetCustomerAllBasketItemInfo")] 
    [Authorize(Roles = "Bayi")]
    public async Task<IActionResult> BasketItemInfo()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerAllBasketItemInfoByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("GetCustomerBasketItemInfoForActiveBasketByCustomerNumber")] 
    [Authorize(Roles = "Bayi")]
    public async Task<IActionResult> BasketItemInfoForActiveBasket()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new     GetSessionCustomerBasketItemInfoForActiveBasketByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("GetCustomerAllOrderInfo")] 
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> OrderInfo()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerAllOrderInfoByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    [HttpGet("GetCustomerProductListInfo")] 
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> ProductListInfo()
    {
        var number = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetSessionCustomerProductListInfoByCustomerNumber(number);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
}