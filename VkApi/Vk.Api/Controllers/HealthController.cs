using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation;
using Vk.Schema;

namespace VkApi.Controllers;


[Route("vk/[controller]")]
[ApiController]
public class HealthController : ControllerBase
{
    private readonly IMediator mediator;

    public HealthController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    [Authorize(Roles = "admin")]
    public IActionResult Get(){
        return Ok("Success");
    }
}