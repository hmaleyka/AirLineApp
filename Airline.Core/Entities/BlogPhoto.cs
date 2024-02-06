using Airline.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Core.Entities
{
    public class BlogPhoto : BaseEntity
    {
        public string Imgurl { get; set; }
        public int? BlogId { get; set; }
        public Blog? blog { get; set; }
    }
}
