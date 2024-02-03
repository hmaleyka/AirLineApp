using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.PackageVM;
using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PackageController : Controller
    {
        private readonly IPackageService _service;

        public PackageController(IPackageService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var packages = await _service.GetAllAsync();
            return View(packages);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PackageCreateVM packagevm)
        {
            await _service.Create(packagevm);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var package = await _service.GetByIdAsync(id);
            PackageUpdateVM packagevm = new PackageUpdateVM()
            {
                Title = package.Title,
                Description = package.Description,
                Feature = package.Feature,
                Duration = package.Duration,
                Perk = package.Perk,
                Price = package.Price,
                date = package.date,
                ImgUrl=package.ImgUrl,
                Person=package.Person,
            };
            return View(packagevm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(PackageUpdateVM packagevm)
        {
            var packages = await _service.Update(packagevm);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
