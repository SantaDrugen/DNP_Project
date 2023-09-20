using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DNP_Project.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic userLogic;

    public UserController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }


    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)
    {
        try
        {
            User user = await userLogic.CreateAsync(dto);
            return Created($"/users/{user.Id}", user);
        }
        catch (UnavailableUsernameException e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e.Message);
        }
        catch (InvalidUsernameLengthException e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e.Message);
        }
    }
}