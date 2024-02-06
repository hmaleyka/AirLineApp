using Airline.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.ViewModel.BlogVM
{
    public class CreateBlogVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime date { get; set; }
        public IFormFile Image { get; set; }
        public string About { get; set; }
        public List<int>? tagIds { get; set; }
        public List<IFormFile>? blogphotos { get; set; }
    }
}
