using Airline.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.ViewModel.BlogVM
{
    public class CreateBlogVM
    {
        [Required]
        [MaxLength(10)]
        public string Title { get; set; }
        [Required]
        [MaxLength(20)]
        public string Description { get; set; }
        public DateTime date { get; set; }
        public IFormFile Image { get; set; }
        public string About { get; set; }
        [Display(Name ="Tag")]
        public List<int> tagIds { get; set; }
        [Required]
        public List<IFormFile>? blogphotos { get; set; }
    }
}
