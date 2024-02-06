using Airline.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Core.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; } 
        public DateTime date { get; set; }
        public string? ImgUrl { get; set; }
        public string About { get; set; }
        public List<BlogTag>? blogtags { get; set; }
        public List<BlogPhoto>? blogphotos { get; set; }
    }
}
