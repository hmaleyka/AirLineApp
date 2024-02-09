using Airline.Business.Exceptions;
using Airline.Business.Services.Implementations;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.BlogVM;
using Airline.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using static Airline.Business.ViewModel.BlogVM.UpdateBlogVM;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BlogController : Controller
    {
        private readonly IBlogService _service;
        private readonly ITagService _tagService;
        public BlogController(IBlogService service, ITagService tagService)
        {
            _service = service;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _service.GetAllAsync();
            return View(blogs);
        }
        public async Task<IActionResult> Create()
        {
            
            ViewBag.Tags = await _tagService.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogVM blogVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    ViewBag.Tags = await _tagService.GetAllAsync();
                    return View();
                }
                await _service.Create(blogVM);
                return RedirectToAction(nameof(Index));
            }
            catch (ImageException ex)
            {
                ModelState.AddModelError(ex.name, ex.Message);

                return View();

            }
           
        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Tags = await _tagService.GetAllAsync();
            var blogs = await _service.GetByIdAsync(id);
            UpdateBlogVM blogvm = new UpdateBlogVM()
            {
                Title = blogs.Title,
                Description = blogs.Description,
                About = blogs.About,
                date = blogs.date,
                ImageUrl = blogs.ImgUrl,
                allblogphotos = blogs.blogphotos.Select(item => new ProductImagesVm
                {
                    ImgUrl = item.Imgurl,
                    Id = item.Id,
                }).ToList(),
            };
            return View("Update" ,blogvm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlogVM blogVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                ViewBag.Tags = await _tagService.GetAllAsync();
                await _service.GetByIdAsync(blogVM.Id);

                var blogs = await _service.Update(blogVM);
                return RedirectToAction(nameof(Index));
            }
            catch (ImageException ex)
            {

                ModelState.AddModelError(ex.name, ex.Message);

                return View();
            }
           

            
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }  
    }
}
