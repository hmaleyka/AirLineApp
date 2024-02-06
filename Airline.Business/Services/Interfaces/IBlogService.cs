using Airline.Business.ViewModel.BlogVM;
using Airline.Business.ViewModel.TagVM;
using Airline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Interfaces
{
    public interface IBlogService
    {
        Task<ICollection<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task<Blog> Create(CreateBlogVM blog);
        Task<Blog> Update(UpdateBlogVM blog);

        Task<Blog> Delete(int id);
    }
}
