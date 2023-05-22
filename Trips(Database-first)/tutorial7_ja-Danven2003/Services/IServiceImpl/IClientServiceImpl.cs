using Microsoft.EntityFrameworkCore;
using Zadanie7.DAL;
using Zadanie7.DTO;
using Zadanie7.Services.IService;

namespace Zadanie7.Services.IServiceImpl
{
    public class IClientServiceImpl : IClientService
    {

        private Apbd7Context _apbd7Context;

        public IClientServiceImpl(Apbd7Context apbd7Context)
        {
            _apbd7Context = apbd7Context;
        }

        public async Task DeleteClient(int id)
        {
            var client1 = await _apbd7Context.Clients.FindAsync(id) ?? throw new InvalidOperationException("No such client");

            if (_apbd7Context.ClientTrips.Any(ct => ct.IdClient == id))
            {
                throw new InvalidOperationException("The client has efforts to a trip");
            }

            _apbd7Context.Clients.Remove(client1);
            await _apbd7Context.SaveChangesAsync();
        }

    }
}