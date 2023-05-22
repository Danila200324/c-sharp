using Zadanie7.DAL;
using Zadanie7.DTO;

namespace Zadanie7.Services.IService
{
    public interface IClientService
    {
        public Task DeleteClient(int id);
    }
}