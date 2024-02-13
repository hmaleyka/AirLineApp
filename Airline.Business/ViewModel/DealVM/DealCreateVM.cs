using Airline.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.ViewModel.DealVM
{
    public class DealCreateVM
    {
        [Required]
        [MaxLength(10)]
        public string Title { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        [Required]
        [MaxLength(20)]
        public string Feature { get; set; }
        public IFormFile MainPhoto { get; set; }
        public double Price { get; set; }
        [Required]
        public double Distance { get; set; }
        [Required]
        public double Range { get; set; }
        [Required]
        public double Speed { get; set; }
        [Required]
        public double Passenger { get; set; }
        public List<IFormFile>? dealphotos { get; set; }
    }
}
