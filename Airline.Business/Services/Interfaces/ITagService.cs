using Airline.Business.ViewModel.PackageVM;
using Airline.Business.ViewModel.TagVM;
using Airline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Interfaces
{
    public interface ITagService
    {
        Task<ICollection<Tag>> GetAllAsync();
        Task<Tag> GetByIdAsync(int id);
        Task<Tag> Create(CreateTagVM tag);
        Task<Tag> Update(UpdateTagVM tag);

        Task<Tag> Delete(int id);
    }
}
