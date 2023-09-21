using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DNP_Project.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoLogic todoLogic;

    public TodoController(ITodoLogic todoLogic)
    {
        this.todoLogic = todoLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Todo>> CreateAsync([FromBody]TodoCreationDto dto)
    {
        try
        {
            Todo todo = await todoLogic.CreateAsync(dto);
            return Created($"/todos/{todo.Id}", todo);
        }
        catch (TitleNotFoundException e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e.Message);
        }
        catch (NoUserByIdException e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e.Message);
        }
        
    }
}