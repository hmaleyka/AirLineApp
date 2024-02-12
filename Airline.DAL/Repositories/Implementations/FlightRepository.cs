using Airline.Core.Entities;
using Airline.DAL.Context;
using Airline.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.DAL.Repositories.Implementations
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        public FlightRepository(AppDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
