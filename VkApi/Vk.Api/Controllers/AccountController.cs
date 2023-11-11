using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;
[Route("vk/[controller]")]
[ApiController]

public class AccountController : ControllerBase
{
    private readonly IMediator mediator;
    
    public AccountController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> Get()
    {
        var operation = new GetAllAccountQuery();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> Get(string id)
    {
        var operation = new GetAccountById(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("ByParameter")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> ByParameter(
        [FromQuery] string? Name,
        [FromQuery] string? AccountNumber,
        [FromQuery] string? IBAN,
        [FromQuery] int? minBalance,
        [FromQuery] int? maxBalance
    )
    {
        var operation = new GetAccountByParametersQuery(Name, AccountNumber,IBAN, minBalance, maxBalance);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> Create([FromBody] CreateAccountRequest request)
    {
        var operation = new CreateAccountCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? Conflict() : BadRequest();
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async  Task<IActionResult> Put(string id, [FromBody] UpdateAccountRequest request)
    {
        var operation = new UpdateAccountCommand(request,id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> DeleteById(string id)
    {
        var operation = new DeleteAccountCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
}