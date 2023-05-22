using Zadanie7.DAL;
using Zadanie7.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zadanie7.Services.IService;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Security.Cryptography.Xml;
using MySqlX.XDevAPI.Relational;

namespace Zadanie7.Services.IServiceImpl
{
    public class ITripServiceImpl : ITripService
    {

        private Apbd7Context _apbd7Context;

        public ITripServiceImpl(Apbd7Context apbd7Context)
        {
            _apbd7Context = apbd7Context;
        }

        public async Task<int> AddClientToTrip(int idTrip, ClientDtoCreate clientDtoCreate)
        {
            var client = await _apbd7Context.Clients.FirstOrDefaultAsync(x => x.Pesel == clientDtoCreate.Pesel);

            if (client == null)
            {
                client = new Client
                {
                    FirstName = clientDtoCreate.FirstName,
                    LastName = clientDtoCreate.LastName,
                    Email = clientDtoCreate.Email,
                    Telephone = clientDtoCreate.Telephone,
                    Pesel = clientDtoCreate.Pesel
                };
                await _apbd7Context.Clients.AddAsync(client);
            }

            var trip = await _apbd7Context.Trips.FirstOrDefaultAsync(x => x.IdTrip == clientDtoCreate.IdTrip && x.Name == clientDtoCreate.TripName) ??
                throw new Exception($"Trip with {clientDtoCreate.IdTrip} not found");


            var clientTrip = await _apbd7Context.ClientTrips
                .FirstOrDefaultAsync(ct => ct.IdClient == client.IdClient && ct.IdTrip == clientDtoCreate.IdTrip);

            if (clientTrip != null)
            {
                throw new Exception();
            }

            var newClientTrip = new ClientTrip
            {
                IdClientNavigation = client,
                IdTrip = clientDtoCreate.IdTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = clientDtoCreate.PaymentDate
            };

            await _apbd7Context.ClientTrips.AddAsync(newClientTrip);

            await _apbd7Context.SaveChangesAsync();

            return client.IdClient;
        }


        public Task<List<TripDto>> GetTripsAsync()
        {
            return _apbd7Context.Trips
            .Select(t => new TripDto
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.Countries.Select(c => new CountryDto
                {
                    Name = c.Name
                }).ToList(),
                Clients = t.Clients.Where(ct => ct.IdTrip == t.IdTrip)
                            .Select(ct => ct.IdClientNavigation)
                            .Distinct()
                            .Select(c => new ClientDto {
                                FirstName = c.FirstName,
                                LastName = c.LastName
                            }).ToList()

            })
               .OrderByDescending(x => x.DateTo).ToListAsync();
        }
    }
}   