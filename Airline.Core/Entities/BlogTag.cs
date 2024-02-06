using Airline.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Core.Entities
{
    public class BlogTag : BaseEntity
    {
        public int? BlogId { get; set; }
        public Blog blog { get; set; }
        public int? TagId { get; set; }
        public Tag tag { get; set; }
    }
}
