using Domain;
using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface ITodoDao
{
    Task<Todo> CreateAsync(Todo todo);
    
    Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters);
}