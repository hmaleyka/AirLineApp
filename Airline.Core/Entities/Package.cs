using Airline.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Core.Entities
{
    public class Package : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Feature {  get; set; }
        public string Perk { get; set; }
        public double Price { get; set; }
        public string ImgUrl { get; set; }
        public double? Duration { get; set; }
        public DateTime date { get; set; }
        public int? Person {  get; set; }

    }
}
