using Airline.Business.Exceptions.Common;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.FlightVM;
using Airline.Core.Entities;
using Airline.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Implementations
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _repo;

        public FlightService(IFlightRepository repo)
        {
            _repo = repo;
        }

        public async Task<Flight> Create(CreateFlightVM flightvm)
        {
            Flight flight = new Flight()
            {
                From = flightvm.From,
                To = flightvm.To,
                Seat = flightvm.Seat,
                flightdate = flightvm.flightdate,
                Price = flightvm.Price
                
            };
            await _repo.Create(flight);
            await _repo.SaveChangesAsync();
            return flight;
        }

        public async Task<Flight> Delete(int id)
        {
            var flight = await _repo.GetByIdAsync(id);
            flight.IsDeleted= true;
            await _repo.SaveChangesAsync();
            return flight;
        }

        public async Task<ICollection<Flight>> GetAllAsync()
        {
            var flights = await _repo.GetAllAsync();
            return await flights.ToListAsync();
        }

        public async Task<Flight> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException("Id shouldn't be less than or equal zero");
            var flight = await _repo.GetByIdAsync(id);
            if (flight == null) throw new NotFoundException("Id should not be null!");
            return flight;
        }

        public async Task<Flight> Update(UpdateFlightVM flightvm)
        {
            Flight flights = await _repo.GetByIdAsync(flightvm.Id);
            if (flights == null) throw new NotFoundException("Id should not be null!");
            flights.Seat = flightvm.Seat;
            flights.From = flightvm.From;
            flights.To = flightvm.To;
            flights.flightdate = flightvm.flightdate;
            //flights.People = flightvm.People;
            flights.Price = flightvm.Price;
            _repo.Update(flights);
            await _repo.SaveChangesAsync();
            return flights;
        }
    }
}
