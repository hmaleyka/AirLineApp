using Airline.Business.Exceptions;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.DealVM;
using Airline.Core.Entities;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using static Airline.Business.ViewModel.BlogVM.UpdateBlogVM;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [AutoValidateAntiforgeryToken]
    public class DealController : Controller
    {
        private readonly IDealService _service;
        private readonly AppDbContext _context;

        public DealController(IDealService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }
        [Authorize(Roles = "SuperAdmin, Admin , Moderator")]
        public async Task<IActionResult> Index()
        {
            var deals = await _service.GetAllAsync();
            return View(deals);
        }
        [Authorize(Roles = "SuperAdmin, Admin , Moderator")]
        public IActionResult Create() 
        { 
               return View();
        }
        [Authorize(Roles = "SuperAdmin, Admin , Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(DealCreateVM dealvm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                await _service.Create(dealvm);
                return RedirectToAction(nameof(Index));
            }
            catch (ImageException ex)
            {

                ModelState.AddModelError(ex.name, ex.Message);

                return View();
            }
           
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async  Task<IActionResult> Update(int id)
        {
            Deal existdeals = await _service.GetByIdAsync(id);
            DealUpdateVM dealvm = new DealUpdateVM()
            {
                Title = existdeals.Title,
                Description = existdeals.Description,
                Feature = existdeals.Feature,
                Distance = existdeals.Distance,
                Range = existdeals.Range,
                Speed = existdeals.Speed,
                Passenger = existdeals.Passenger,
                Price = existdeals.Price,
                MainphotoUrl= existdeals.MainPhoto,
                multipledealphotos = new List<DealImagesVm>(),
                //alldealphotos = deals.dealphotos.Select(item => new ProductImagesVm
                //{
                //    ImgUrl = item.ImgUrl,
                //    Id = item.Id,
                //}).ToList(),
            };

            foreach (var item in existdeals.dealphotos)
            {
                DealImagesVm productImageVM = new()
                {
                    Id = item.Id,
                    ImgUrl = item.ImgUrl,
                };

                dealvm.multipledealphotos.Add(productImageVM);
            }

            return View(dealvm);
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public async Task<IActionResult> Update (DealUpdateVM dealvm)
        {
            try
            {
                UpdateDealVMValidator validations = new UpdateDealVMValidator();
                var resultvalidations = await validations.ValidateAsync(dealvm);
                if (!resultvalidations.IsValid)
                {
                    ModelState.Clear();
                    resultvalidations.Errors.ForEach(x => ModelState.AddModelError("",x.ErrorMessage));
                    Deal existdeals = await _service.GetByIdAsync(dealvm.Id);
                    dealvm.multipledealphotos = new List<DealImagesVm>();
                    dealvm.MainphotoUrl = existdeals.MainPhoto;
                    foreach (var item in existdeals.dealphotos)
                    {
                        DealImagesVm productImageVM = new()
                        {
                            Id = item.Id,
                            ImgUrl = item.ImgUrl,
                        };

                        dealvm.multipledealphotos.Add(productImageVM);
                    }
                    return View(dealvm);
                }
                if (!ModelState.IsValid)
                {
                    return View(dealvm);
                }
                var deals = await _service.Update(dealvm);
                return RedirectToAction(nameof(Index));
            }
            catch (ImageException ex)
            {
                ModelState.AddModelError(ex.name, ex.Message);

                return View(dealvm);

            }
            
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            Deal deals = await _context.deals.FindAsync(id);
            return View(deals);
        }
    }
}
