using Domain;
using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface ITodoDao
{
    Task<Todo> CreateAsync(Todo todo);
    
    Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters);
    
    Task UpdateAsync(Todo todo);
    
    Task<Todo?> GetByIdAsync(int id);
    
    Task<TodoGetDto> GetByIdDtoAsync(int id);
    
    Task DeleteAsync(int id);
}