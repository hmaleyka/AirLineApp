using Airline.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.ViewModel
{
    public class HomeVM
    {
        public List<Benefit> benefits {  get; set; }
        public List<Package> packages { get; set; }
        public List<Deal> deals { get; set; }
        public List<Team> teams { get; set; }
        public List<Blog> blogs { get; set; }
    }
}
