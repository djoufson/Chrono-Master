using Application.Features.Authentication;
using Application.Features.Authentication.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Login.Request request)
    {
        var result = await _sender.Send(request);
        if(result.IsSuccess)
            return Ok(result.Value);

        var error = result.Errors.Select(e => e.Message);
        return result.Errors.First() switch
        {
            BadCredentialsError or PasswordRequirementsError => BadRequest(error),
            UserNotFoundError => NotFound(error),
            _ => Problem()
        };
    }
}
