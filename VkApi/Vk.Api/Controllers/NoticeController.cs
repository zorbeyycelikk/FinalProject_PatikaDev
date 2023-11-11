using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;
[Route("vk/[controller]")]
[ApiController]

public class NoticeController : ControllerBase
{
    private readonly IMediator mediator;
    
    public NoticeController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
    {
        var operation = new GetAllNoticeQuery();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> Get(string id)
    {
        var operation = new GetNoticeById(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("ByParameter")]
    [Authorize(Roles = "Admin,Bayi")]
    public async Task<IActionResult> ByParameter(
        [FromQuery] string? Id,
        [FromQuery] string? ReceiverId,
        [FromQuery] string? Content,
        [FromQuery] bool? ReadStatus
    )
    {
        var operation = new GetNoticeByParametersQuery(Id, ReceiverId, Content, ReadStatus);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateNoticeRequest request)
    {
        var operation = new CreateNoticeCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? Conflict() : BadRequest();
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Bayi")]
    public async  Task<IActionResult> Put(string id, [FromBody] UpdateNoticeRequest request)
    {
        var operation = new UpdateNoticeCommand(request,id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteById(string id)
    {
        var operation = new DeleteNoticeCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
}