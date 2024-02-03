using Airline.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Core.Entities
{
    public class Team : BaseEntity
    {
        public string Fullname { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string AboutMe { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string Experience { get; set; }
        public string ImgUrl { get; set; }


    }
}
