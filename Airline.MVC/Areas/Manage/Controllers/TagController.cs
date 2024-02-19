using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.TagVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Area("Manage")]
    public class TagController : Controller
    {
        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }
        [Authorize(Roles = "SuperAdmin, Admin , Moderator")]
        public async Task<IActionResult> Index()
        {
            var tags = await _service.GetAllAsync();
            return View(tags);
        }
        [Authorize(Roles = "SuperAdmin, Admin , Moderator")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin, Admin , Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagVM tagvm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _service.Create(tagvm);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var tags = await _service.GetByIdAsync(id);
            UpdateTagVM tagvm = new UpdateTagVM()
            {
                Name = tags.Name,
            };
            return View(tagvm);
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateTagVM tagvm)
        {
            await _service.Update(tagvm);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
