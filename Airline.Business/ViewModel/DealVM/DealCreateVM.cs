using Airline.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.ViewModel.DealVM
{
    public class DealCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Feature { get; set; }
        public IFormFile MainPhoto { get; set; }
        public double Price { get; set; }
        public double? Distance { get; set; }
        public double? Range { get; set; }
        public double? Speed { get; set; }
        public double? Passenger { get; set; }
        public List<IFormFile>? dealphotos { get; set; }
    }
}
