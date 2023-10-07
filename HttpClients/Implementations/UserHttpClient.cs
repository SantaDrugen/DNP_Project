using System.Net.Http.Json;
using Domain;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient _client;

    public UserHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<User> CreateUserAsync(UserCreationDto dto)
    {
        var response = await _client.PostAsJsonAsync("/user", dto);
        response.EnsureSuccessStatusCode();
        Task<User> temp = response.Content.ReadFromJsonAsync<User>()!;
        return await temp;
    }
}