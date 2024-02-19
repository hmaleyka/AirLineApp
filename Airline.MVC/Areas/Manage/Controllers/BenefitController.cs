using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.BenefitVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [AutoValidateAntiforgeryToken]
    public class BenefitController : Controller
    {
        private readonly IBenefitService _service;

        public BenefitController(IBenefitService service)
        {
            _service = service;
        }
        [Authorize(Roles = "SuperAdmin, Admin , Moderator")]
        public async Task<IActionResult> Index()
        {
            var benefits = await _service.GetAllAsync();
            return View(benefits);
        }
        [Authorize(Roles = "SuperAdmin, Admin , Moderator")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin, Admin , Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(BenefitCreateVM benefitvm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _service.Create(benefitvm);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Update(int id)
        {
            
            var benefit = await _service.GetByIdAsync(id);
            BenefitUpdateVM vm = new BenefitUpdateVM()
            {
                Title = benefit.Title,
                Description = benefit.Description,
                Icon = benefit.Icon,
            };
            return View(vm);

        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public async Task<IActionResult> Update (BenefitUpdateVM benefitvm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var benefits = await _service.Update(benefitvm);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
     }
}
