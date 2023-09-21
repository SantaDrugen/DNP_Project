using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Domain.Exceptions;

namespace Application.Logic;

public class TodoLogic : ITodoLogic
{
    private readonly ITodoDao todoDao;
    private readonly IUserDao userDao;

    public TodoLogic(ITodoDao todoDao, IUserDao userDao)
    {
        this.todoDao = todoDao;
        this.userDao = userDao;
    }

    public async Task<Todo> CreateAsync(TodoCreationDto dto)
    {
        User? user = await userDao.GetByUserIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new NoUserByIdException($"User with id {dto.OwnerId} was not found");
        }

        ValidateTodo(dto);
        Todo todo = new Todo(user, dto.Title);
        Todo created = await todoDao.CreateAsync((todo));
        
        return created;
    }

    private void ValidateTodo(TodoCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title))
            throw new TitleNotFoundException("Title cannot be empty");
        
        //Other validation points.
    }
}