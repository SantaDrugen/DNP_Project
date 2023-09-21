using Domain;
using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    public Task<User> CreateAsync(User user);
    
    public Task<User?> GetByUsernameAsync(string userName);

    public Task<User?> GetByUserIdAsync(int id);

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
}