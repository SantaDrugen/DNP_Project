using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDto userToCreate);
    
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
}