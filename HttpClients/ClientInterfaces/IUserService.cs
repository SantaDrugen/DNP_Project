using Domain;
using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(UserCreationDto dto);
}