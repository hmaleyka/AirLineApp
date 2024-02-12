using Airline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.ViewModel.FlightVM
{
    public class CreateFlightVM
    {
        public CreateFlightVM()
        {
            Seat = 0;
        }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime flightdate { get; set; }
        public int Seat { get; set; }
        public IEnumerable<Ticket>? People { get; set; }
        public double Price { get; set; }
    }
}
