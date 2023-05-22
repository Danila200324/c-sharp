using Zadanie8.Dto;

namespace Zadanie8.Services;

public interface ILogginService
{
    Task<LoginResult> LoginUser(LogginDtoRequest logginDtoRequest);
}