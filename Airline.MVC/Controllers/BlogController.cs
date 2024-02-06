using Airline.Business.ViewModel;
using Airline.Core.Entities;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Blog> blog = _context.blogs.Where(b => b.IsDeleted == false).ToList();
            return View(blog);
        }
        public IActionResult Details(int ? id)
        {
            if (id == null) return BadRequest();
            Blog blog = _context.blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null) return NotFound();
            DetailVM detailVM = new DetailVM()
            {
                blog = blog,
                blogs = _context.blogs.Where(t => t.Id != blog.Id).ToList()
            };
            return View(blog);
        }
    }
}
