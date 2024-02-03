using Airline.Business.ViewModel.DealVM;
using Airline.Business.ViewModel.PackageVM;
using Airline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Interfaces
{
    public interface IDealService
    {
        Task<ICollection<Deal>> GetAllAsync();
        Task<Deal> GetByIdAsync(int id);
        Task<Deal> Create(DealCreateVM deal);
        Task<Deal> Update(DealUpdateVM deal);

         Task<Deal> Delete(int id);
    }
}
