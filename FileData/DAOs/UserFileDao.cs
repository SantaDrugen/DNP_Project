using Application.DaoInterfaces;
using Domain;
using Domain.DTOs;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max((u => u.Id));
            userId++;
        }

        user.Id = userId;
        
        context.Users.Add((user));
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );

        return Task.FromResult(existing);
    }

    public Task<User?> GetByUserIdAsync(int id)
    {
        User? existing = context.Users.FirstOrDefault((u => u.Id == id));

        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();

        if (searchParameters.UsernameContains != null)
        {
            users = context.Users.Where(u =>
                u.UserName.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }
        
        //Add extra if's to users.Users.Where( some constraint ) if we have more parameters to go through.
        //ie. specific ID or some such.

        return Task.FromResult(users);
    }
}