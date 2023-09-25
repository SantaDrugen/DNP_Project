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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId,
        [FromQuery] bool? completedStatus, [FromQuery] string? titleContains)
    {
        try
        {
            SearchTodoParametersDto parameters = new(userName, userId, completedStatus, titleContains);
            var todos = await todoLogic.GetAsync(parameters);
            return Ok(todos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoGetDto>> GetByIdAsync(int id)
    {
        try
        {
            TodoGetDto todo = await todoLogic.GetByIdAsync(id);
            return Ok(todo);
        }
        catch (NoTodoByIdException e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult<Todo>> UpdateAsync([FromBody] TodoUpdateDto dto)
    {
        try
        {
            await todoLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (NoUserByIdException e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e.Message);
        }
        catch (NoTodoByIdException e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e.Message);
        }
        catch (CannotUncompleteTodoException e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            await todoLogic.DeleteAsync(id);
            return Ok();
        }
        catch (NoTodoByIdException e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e.Message);
        }
        catch (CannotDeleteUncompletedTodoException e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}