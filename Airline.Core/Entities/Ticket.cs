using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Core.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
       
        public AppUser user { get; set; }
        public string userId { get; set; }
        public int seat {  get; set; }
        public Flight flight { get; set; }
        public int FlightId { get; set; }

    }
}
