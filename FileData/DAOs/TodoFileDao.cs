using Application.DaoInterfaces;
using Domain;
using Domain.DTOs;

namespace FileData.DAOs;

public class TodoFileDao : ITodoDao
{
    private readonly FileContext context;

    public TodoFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Todo> CreateAsync(Todo todo)
    {
        int todoId = 1;
        if (context.Todos.Any())
        {
            todoId = context.Todos.Max((t => t.Id));
            todoId++;
        }

        todo.Id = todoId;
        
        context.Todos.Add(todo);
        context.SaveChanges();

        return Task.FromResult(todo);
    }

    public Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters)
    {
        throw new NotImplementedException();
    }
}