using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Domain.Exceptions;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto userToCreate)
    {
        User? existing = await userDao.GetByUsernameAsync(userToCreate.UserName);
        if (existing != null)
            throw new UnavailableUsernameException("Username is already taken :(");

        ValidateData(userToCreate);
        User toCreate = new User
        {
            UserName = userToCreate.UserName
        };

        User created = await userDao.CreateAsync(toCreate);

        return created;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }
    
    
    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.UserName;

        if (userName.Length < 3)
            throw new InvalidUsernameLengthException("Username sadly has to be atleast 3 characters long :(");

        if (userName.Length > 15)
            throw new InvalidUsernameLengthException("Username sadly has to be 15 characters or shorter :(");
    }
}