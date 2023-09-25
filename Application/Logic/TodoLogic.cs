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

    public Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters)
    {
        return todoDao.GetAsync(searchParameters);
    }
    
    public async Task<TodoGetDto> GetByIdAsync(int id)
    {
        Todo? existing = await todoDao.GetByIdAsync(id);
        if (existing == null)
            throw new NoTodoByIdException($"Todo with id {id} was not found");
        return await todoDao.GetByIdDtoAsync(id);
    }

    public async Task UpdateAsync(TodoUpdateDto dto)
    {
        Todo? existing = await todoDao.GetByIdAsync(dto.Id);
        
        if (existing == null)
            throw new NoTodoByIdException($"Todo with id {dto.Id} was not found");

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await userDao.GetByUserIdAsync(dto.OwnerId.Value);
            if (user == null)
                throw new NoUserByIdException($"User with id {dto.OwnerId} was not found");
        }
        
        if (dto.IsComplete != null && existing.IsCompleted && !(bool)dto.IsComplete)
            throw new CannotUncompleteTodoException($"Todo with id {dto.Id} is already completed, and cannot be " +
                                                    $"uncompleted");
        
        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        bool isCompleteToUse = dto.IsComplete ?? existing.IsCompleted;

        Todo updated = new(userToUse, titleToUse)
        {
            IsCompleted = isCompleteToUse,
            Id = existing.Id,
        };
        
        ValidateTodo(updated);

        await todoDao.UpdateAsync(updated);
    }
    
    public async Task DeleteAsync(int id)
    {
        Todo? existing = await todoDao.GetByIdAsync(id);
        if (existing == null)
            throw new NoTodoByIdException($"Todo with id {id} was not found");

        if (!existing.IsCompleted)
            throw new CannotDeleteUncompletedTodoException(
                $"Todo with id {id} is not completed, and cannot be deleted");
        
        await todoDao.DeleteAsync(id);
    }
    
    

    private void ValidateTodo(TodoCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title))
            throw new TitleNotFoundException("Title cannot be empty");
        
        //Other validation points.
    }
    
    private void ValidateTodo(Todo dto)
    {
        if (string.IsNullOrEmpty(dto.Title))
            throw new TitleNotFoundException("Title cannot be empty");
        
        //Other validation points.
    }
}