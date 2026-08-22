
using FoodFlow.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class AppController : ControllerBase
{
    private IMediator mediator = null!;

    protected IMediator Mediator
    {
        get
        {
            if (mediator == null)
                mediator = this.HttpContext.RequestServices.GetService<IMediator>()!;
            return mediator;
        }
    }

    protected IActionResult ReturnResult<T>(Result<T> result)
    {
        if (result.Status)
            return StatusCode(result.StatusCode, result.Data);
        else
            return StatusCode(result.StatusCode, new { error = result.Message });
    }

    [HttpHead]
    public IActionResult EndpointActive()
    {
        return Ok();
    }

}
