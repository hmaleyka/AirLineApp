using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.SettingVM;
using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingController : Controller
    {
        private readonly ISettingService _service;

        public SettingController(ISettingService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
           var setting= await _service.GetAllAsync();
            return View(setting);
        }

        public async Task<IActionResult> Update (int id)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
         var setting =   await _service.GetByIdAsync(id);
            return View(setting);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateSettingVM settingvm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var settings = await _service.Update(settingvm);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete (int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        } 
    }
}
