using Airline.Business.Exceptions;
using Airline.Business.Exceptions.Common;
using Airline.Business.Helpers;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel;
using Airline.Business.ViewModel.BlogVM;
using Airline.Core.Entities;
using Airline.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repo;
        private readonly IWebHostEnvironment _env;



        public BlogService(IBlogRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public async Task<Blog> Create(CreateBlogVM blogvm)
        {
            if (blogvm == null) throw new NotFoundException("It should not be null");
            Blog blogs = new Blog()
            {
                Title = blogvm.Title,
                Description = blogvm.Description,
                date = blogvm.date,
                About = blogvm.About,
                blogtags = new List<BlogTag>(),
                blogphotos = new List<BlogPhoto>()
            };
            if (blogvm.Image != null)
            {

                if (!blogvm.Image.CheckType("image/"))
                {
                    throw new ImageException("Image type should be img", nameof(blogvm.Image));
                }
                if (!blogvm.Image.CheckLong(2097152))
                {
                    throw new ImageException("Image size should not be large than 2mb", nameof(blogvm.Image));
                }
                blogs.ImgUrl = blogvm.Image.Upload(_env.WebRootPath, @"\Upload\Blog\");
            }
            else
            {
                throw new ImageException("Please enter the image", nameof(blogvm.Image));
            }

            if (blogvm.blogphotos != null)
            {
                foreach (var photo in blogvm.blogphotos)
                {
                    if (!photo.CheckType("image/"))
                    {
                        throw new Exception("Image type should be img");
                    }
                    if (!photo.CheckLong(2097152))
                    {
                        throw new Exception("Image size should not be large than 2mb");
                    }
                    BlogPhoto blogphoto = new BlogPhoto()
                    {
                        Imgurl = photo.Upload(_env.WebRootPath, @"\Upload\Deal\"),
                        blog = blogs
                    };
                    blogs.blogphotos.Add(blogphoto);

                }
            }
            else
            {
                throw new ImageException("Image should not be null", nameof(blogvm.Image));
            }
            foreach (var item in blogvm.tagIds)
            {
                blogs.blogtags.Add(new BlogTag
                {
                    blog = blogs,
                    TagId = item,
                });
            }
            await _repo.Create(blogs);
            await _repo.SaveChangesAsync();
            return blogs;

        }

        public async Task<Blog> Delete(int id)
        {
            var blog = await _repo.GetByIdAsync(id);
            if (blog == null) throw new NotFoundException("Blog should not be null");
            blog.IsDeleted = true;
            await _repo.SaveChangesAsync();
            return blog;
        }

        public async Task<ICollection<Blog>> GetAllAsync()
        {
            var blogs = await _repo.GetQuery(x => x.IsDeleted == false)
                 .Include(x => x.blogtags)
                 .ThenInclude(x => x.tag).Include(x => x.blogphotos)
                 .ToListAsync();
            return blogs;
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException("Id should not be less than zero");
            var blogs = await _repo.GetQuery(x => x.IsDeleted == false && x.Id == id)
                .Include(x => x.blogtags).ThenInclude(x => x.tag).Include(x => x.blogphotos).FirstOrDefaultAsync();
            if (blogs == null) throw new NotFoundException("Id should not be null");
            return blogs;
        }

        public async Task<Blog> Update(UpdateBlogVM blogvm)
        {
            //Blog existblog = await _repo.GetQuery(x => x.IsDeleted == false && x.Id == blogvm.Id).Include(x=> x.blogtags)
            //    .ThenInclude(x=>x.tag)
            //    .Include(x=>x.blogphotos)
            //    .FirstOrDefaultAsync();
            //ViewBag.tags = await .tag.ToListAsync();
            Blog? existblog = await _repo.GetAsync(x => !x.IsDeleted && x.Id == blogvm.Id, "blogtags.tag");
            if (existblog == null) throw new NotFoundException("Blog should not be null");
            existblog.Title = blogvm.Title;
            existblog.Description = blogvm.Description;
            existblog.About = blogvm.About;
            existblog.date = blogvm.date;
            if (blogvm.Image != null)
            {
                if (!blogvm.Image.CheckType("image/"))
                {
                    throw new ImageException("image type should be img", nameof(blogvm.Image));
                }
                if (!blogvm.Image.CheckLong(2097152))
                {
                    throw new ImageException("the long should not be large than 2mb", nameof(blogvm.Image));
                }
                existblog.ImgUrl = blogvm.Image.Upload(_env.WebRootPath, @"\Upload\Blog\");
            }

            if (blogvm.ImageIds == null)
            {
                existblog.blogphotos.RemoveAll(d => d.IsDeleted = false);
            }
            else
            {
                var removeListImage = existblog.blogphotos?.Where(p => !blogvm.ImageIds.Contains(p.Id)).ToList();
                if (removeListImage != null)
                {
                    foreach (var image in removeListImage)
                    {
                        existblog.blogphotos.Remove(image);
                        FileManager.DeleteFile(image.Imgurl, _env.WebRootPath, @"\Upload\Blog\");
                    }
                }
                //else
                //{
                //    existblog.blogphotos.RemoveAll(p => p.IsDeleted = false);
                //}

            }

            if (blogvm.blogphotos != null)
            {
                foreach (var photo in blogvm.blogphotos)
                {
                    if (!photo.CheckType("image/"))
                    {
                        throw new Exception("Image type should be img");
                    }
                    if (!photo.CheckLong(2097152))
                    {
                        throw new Exception("Image size should not be large than 2mb");
                    }
                    BlogPhoto blogPhoto = new BlogPhoto()
                    {
                        Imgurl = photo.Upload(_env.WebRootPath, @"\Upload\Blog\"),
                        blog = existblog
                    };
                    existblog.blogphotos.Add(blogPhoto);
                }
            }
            else
            {
                throw new ImageException("Please Enter the email" , nameof(blogvm.blogphotos));
            }

            existblog.blogtags.Clear();
            foreach (var item in blogvm.tagIds)
            {
                existblog.blogtags.Add(new BlogTag
                {
                    blog = existblog,
                    TagId = item,
                });
            }

            _repo.Update(existblog);
            await _repo.SaveChangesAsync();
            return existblog;
        }
    }
}
