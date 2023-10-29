using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vk.Operation.Cqrs;

namespace Vk.Api.Controllers;


[Route("vk/api/v1/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IMediator mediator;
    
    public TestController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
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
    
    
}