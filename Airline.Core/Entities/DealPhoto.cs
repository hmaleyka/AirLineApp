using Airline.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Core.Entities
{
    public class DealPhoto : BaseEntity
    {
        public string ImgUrl { get; set; }
        public int? DealId { get; set; }
        public Deal? deal { get; set; }

    }
}
