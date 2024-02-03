using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.ViewModel.TeamVM
{
    public class CreateTeamVM
    {
        public string Fullname { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string AboutMe { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string Experience { get; set; }
        public IFormFile Image { get; set; }
    }
}
