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

    public Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParams)
    {
        IEnumerable<Todo> result = context.Todos.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParams.Username))
        {
            // we know username is unique, so just fetch the first
            result = context.Todos.Where(todo =>
                todo.Owner.UserName.Equals(searchParams.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParams.UserId != null)
        {
            result = result.Where(t => t.Owner.Id == searchParams.UserId);
        }

        if (searchParams.CompletedStatus != null)
        {
            result = result.Where(t => t.IsCompleted == searchParams.CompletedStatus);
        }

        if (!string.IsNullOrEmpty(searchParams.TitleContains))
        {
            result = result.Where(t =>
                t.Title.Contains(searchParams.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }

    public Task UpdateAsync(Todo todo)
    {
        Todo? existing = context.Todos.FirstOrDefault(t => t.Id == todo.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Todo with id {todo.Id} not found");
        }

        context.Todos.Remove(existing);
        context.Todos.Add(todo);
        
        context.SaveChanges();
        
        return Task.CompletedTask;
    }

    public Task<Todo?> GetByIdAsync(int id)
    {
        Todo? existing = context.Todos.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(existing);
    }
}