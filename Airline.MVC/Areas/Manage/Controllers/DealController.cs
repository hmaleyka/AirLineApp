﻿using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.DealVM;
using Airline.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DealController : Controller
    {
        private readonly IDealService _service;

        public DealController(IDealService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var deals = await _service.GetAllAsync();
            return View(deals);
        }

        public IActionResult Create() 
        { 
               return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DealCreateVM dealvm)
        {
            await _service.Create(dealvm);
            return RedirectToAction(nameof(Index));
        }
        public async  Task<IActionResult> Update(int id)
        {
            var deals = await _service.GetByIdAsync(id);
            DealUpdateVM dealvm = new DealUpdateVM()
            {
                Title = deals.Title,
                Description = deals.Description,
                Feature = deals.Feature,
                Distance = deals.Distance,
                Range = deals.Range,
                Speed = deals.Speed,
                Passenger = deals.Passenger,
                MainphotoUrl=deals.MainPhoto,
            };
            foreach (var item in dealvm.alldealphotos)
            {
                ProductImagesVm productImages = new ProductImagesVm()
                {
                    ImgUrl = item.ImgUrl,
                    Id = item.Id,
                };

                dealvm.alldealphotos?.Add(productImages);
            }

            return View(dealvm);
        }
        [HttpPost]
        public async Task<IActionResult> Update (DealUpdateVM dealvm)
        {
            var deals = await _service.Update(dealvm);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        
        }
    }
}
