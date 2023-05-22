using Zadanie8.Dto;
using Zadanie8.Models;

namespace Zadanie8.Services;

public interface IUserService
{
    Task<string> CreateUser(UserDtoRequest userDtoRequest);

    Task<User?> GetUserByEmail(string email);
}