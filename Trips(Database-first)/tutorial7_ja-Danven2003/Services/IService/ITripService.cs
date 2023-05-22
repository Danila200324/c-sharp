using Zadanie7.DAL;
using Zadanie7.DTO;

namespace Zadanie7.Services.IService
{
    public interface ITripService
    {
        public Task<List<TripDto>> GetTripsAsync();

        public Task<int> AddClientToTrip(int IdTrip, ClientDtoCreate clientDtoCreate);
    }
}