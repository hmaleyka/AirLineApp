using Airline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.ViewModel
{
    public class DetailVM
    {
        public List<Deal> deals {  get; set; }
        public Deal deal { get; set; }
        public List<Package> packages { get; set; }
        public Package package { get; set; }
        public List<Team> teams { get; set; }
        public Team team { get; set; }
        public List<Blog> blogs { get; set; }
        public Blog blog { get; set; }
    }
}
