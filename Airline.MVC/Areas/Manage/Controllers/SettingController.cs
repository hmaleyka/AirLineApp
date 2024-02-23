using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.SettingVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [AutoValidateAntiforgeryToken]
    public class SettingController : Controller
    {
        private readonly ISettingService _service;

        public SettingController(ISettingService service)
        {
            _service = service;
        }
        [Authorize(Roles = "SuperAdmin, Admin , Moderator")]
        public async Task<IActionResult> Index()
        {
           var setting= await _service.GetAllAsync();
            return View(setting);
        }
        [Authorize(Roles = "SuperAdmin, Admin ")]
        public async Task<IActionResult> Update (int id)
        {
           
         var setting =   await _service.GetByIdAsync(id);
            UpdateSettingVM settingvm = new UpdateSettingVM()
            {
                Key = setting.Key,
                Value = setting.Value,
            };
            return View(settingvm);
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
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

        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Delete (int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        } 
    }
}
