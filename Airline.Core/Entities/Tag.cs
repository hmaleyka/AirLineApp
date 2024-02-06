using Airline.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Core.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public List<BlogTag>? blogtags { get; set; }
    }
}
