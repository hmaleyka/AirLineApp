using Airline.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Core.Entities
{
    public class Flight : BaseEntity
    {
        public Flight()
        {
            Seat = 0;
        }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime flightdate { get; set; }
        public int Seat { get; set; }
        public double Price { get; set; }
        public List<Ticket>? People { get; set; }

    }
}
