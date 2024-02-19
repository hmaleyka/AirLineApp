using FluentValidation;
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
       
        
        public string? Title { get; set; }
      
        public string? Description { get; set; }
       
        
        public string? Feature { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public string? MainphotoUrl { get; set; }
            
        public double Price { get; set; }
        
        public double? Distance { get; set; }
        
        public double? Range { get; set; }
        
        public double? Speed { get; set; }
        
        public double? Passenger { get; set; }
        
        public List<IFormFile>? dealphotos { get; set; }
        public List<DealImagesVm>? multipledealphotos { get; set; }
        public List<int>? ImageIds { get; set; }
    }
    public class DealImagesVm
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
    }

    public class UpdateDealVMValidator : AbstractValidator<DealUpdateVM>
    {
        public UpdateDealVMValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Please enter the title");
           RuleFor(x => x.Description).NotEmpty().WithMessage("Please Enter the description");
           RuleFor(x => x.Feature).NotEmpty().WithMessage("Please Enter the feature");
           RuleFor(x => x.Distance).NotEmpty().WithMessage("Please Enter the distance");
           RuleFor(x => x.Range).NotEmpty().WithMessage("Please Enter the range");
           RuleFor(x => x.Speed).NotEmpty().WithMessage("Please Enter the speed");
           RuleFor(x => x.Passenger).NotEmpty().WithMessage("Please Enter the passenger");

        }
    }

}
