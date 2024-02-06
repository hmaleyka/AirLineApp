using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.TagVM;
using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TagController : Controller
    {
        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var tags = await _service.GetAllAsync();
            return View(tags);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagVM tagvm)
        {
            await _service.Create(tagvm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var tags = await _service.GetByIdAsync(id);
            UpdateTagVM tagvm = new UpdateTagVM()
            {
                Name = tags.Name,
            };
            return View(tagvm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateTagVM tagvm)
        {
            await _service.Update(tagvm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
