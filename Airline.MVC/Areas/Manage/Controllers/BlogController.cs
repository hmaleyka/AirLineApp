using Airline.Business.Exceptions;
using Airline.Business.Services.Implementations;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel;
using Airline.Business.ViewModel.BlogVM;
using Airline.Business.ViewModel.DealVM;
using Airline.Core.Entities;
using Airline.DAL.Context;
using Airline.MVC.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static Airline.Business.ViewModel.BlogVM.UpdateBlogVM;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BlogController : Controller
    {
        private readonly IBlogService _service;
        private readonly ITagService _tagService;
        private readonly AppDbContext _context;
        public BlogController(IBlogService service, ITagService tagService, AppDbContext context)
        {
            _service = service;
            _tagService = tagService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _service.GetAllAsync();
            return View(blogs);

            //var query = _context.blogs.AsQueryable().Include(b => b.blogtags).ThenInclude(b => b.tag).Include(b => b.blogphotos);
            //PaginatedList<Blog> paginatedOrders = PaginatedList<Blog>.Create(query, page, 4);
            //return View(paginatedOrders);
        }
        public async Task<IActionResult> Create()
        {
            
            ViewBag.Tags = await _tagService.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogVM blogVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Tags = await _tagService.GetAllAsync();
                return View();
                
            }

            try
            {                              
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
                allblogphotos = new List<ProductImagesVm>()
                //allblogphotos = blogs.blogphotos.Select(item => new ProductImagesVm
                //{
                //    ImgUrl = item.Imgurl,
                //    Id = item.Id,
                //}).ToList(),
            };
            foreach (var item in blogs.blogphotos)
            {
                ProductImagesVm productImageVM = new()
                {
                    Id = item.Id,
                    ImgUrl = item.Imgurl,
                };

                blogvm.allblogphotos.Add(productImageVM);
            }
            return View(blogvm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlogVM blogVM)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Tags = await _tagService.GetAllAsync();
                await _service.GetByIdAsync(blogVM.Id);
                return View(blogVM);
            }
            try
            {              
                var blogs = await _service.Update(blogVM);
                return RedirectToAction(nameof(Index));
            }
            catch (ImageException ex)
            {
                ViewBag.Tags = await _tagService.GetAllAsync();
                await _service.GetByIdAsync(blogVM.Id);
                ModelState.AddModelError(ex.name, ex.Message);

                return View(blogVM);
            }
          
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Details(int id)
        {
            Blog blogs = await _context.blogs.FindAsync(id);
            return View(blogs);
        }
    }
}
