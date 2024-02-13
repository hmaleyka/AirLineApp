using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.ViewModel.DealVM
{
    public class DealUpdateVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Title { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        [Required]
        [MaxLength(10)]
        public string Feature { get; set; }
        public string? MainphotoUrl { get; set; }
        public IFormFile MainPhoto { get; set; }
            
        public double Price { get; set; }
        
        public double? Distance { get; set; }
        
        public double? Range { get; set; }
        
        public double? Speed { get; set; }
        [Required]
        public double? Passenger { get; set; }
        
        //public List<string> dealphotosUrl { get; set; }
        public List<IFormFile>? dealphotos { get; set; }
        public List<ProductImagesVm>? alldealphotos { get; set; }
        public List<int> ImageIds { get; set; }
    }
    public class ProductImagesVm
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
    }
}
