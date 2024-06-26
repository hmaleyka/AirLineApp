﻿using Airline.Business.ViewModel.TagVM;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.ViewModel.BlogVM
{
    public class UpdateBlogVM
    {
      
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime date { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
        public IEnumerable<CreateTagVM>? tags { get; set; } = null!;
        public string About { get; set; }
        [Display(Name = "Tag")]
        public List<int> tagIds { get; set; }
        [Required]
        public List<IFormFile>? blogphotos { get; set; }
        public List<ProductImagesVm>? allblogphotos { get; set; }
        [Display(Name = "Image")]
        public List<int>? ImageIds { get; set; }

        public UpdateBlogVM()
        {
            ImageIds=new List<int>();
            blogphotos=new List<IFormFile>();
            tagIds=new List<int>();
            allblogphotos = new List<ProductImagesVm>();
        }

        public class ProductImagesVm
        {
            public int Id { get; set; }
            public string ImgUrl { get; set; }
        }
    }
}
