using Airline.Business.ViewModel.BenefitVM;
using Airline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Interfaces
{
    public interface IBenefitService
    {
        Task<ICollection<Benefit>> GetAllAsync();
        Task<Benefit> GetByIdAsync(int id);
        Task<Benefit> Create(BenefitCreateVM benefit);
        Task<Benefit> Update(BenefitUpdateVM benefit);

        Task<Benefit> Delete(int id);
    }
}
