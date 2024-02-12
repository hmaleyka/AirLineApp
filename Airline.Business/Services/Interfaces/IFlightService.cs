using Airline.Business.ViewModel.BenefitVM;
using Airline.Business.ViewModel.FlightVM;
using Airline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Interfaces
{
    public interface IFlightService
    {
        Task<ICollection<Flight>> GetAllAsync();
        Task<Flight> GetByIdAsync(int id);
        Task<Flight> Create(CreateFlightVM flightvm);
        Task<Flight> Update(UpdateFlightVM flightvm);

        Task<Flight> Delete(int id);
    }
}
