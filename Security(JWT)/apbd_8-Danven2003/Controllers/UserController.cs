using Microsoft.AspNetCore.Mvc;
using Zadanie8.Dto;
using Zadanie8.Services;

namespace Zadanie8.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly IUserService _userService;

    private readonly ILogginService _logginService;

    public UserController(IUserService userService, ILogginService logginService)
    {
        _userService = userService;
        _logginService = logginService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateUser(UserDtoRequest userDtoRequest)
    {
        try
        {
            var user = await _userService.CreateUser(userDtoRequest);
            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new JsonResult(
                new { message = e.Message, Date = DateTime.Now }));
        }
    }
    
    [HttpPost("/login")]
    
    public async Task<ActionResult<LoginResult>> Login(LogginDtoRequest logginDtoRequest)
    {
        try
        {
            return Ok(await _logginService.LoginUser(logginDtoRequest));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new JsonResult(
                new { message = e.Message, Date = DateTime.Now }));
        }
    }
    
    
    [HttpGet("/tryMiddleware")]
    public void CheckExceptionHandler( )
    {
        throw new AggregateException("error testing");
    }
    


}