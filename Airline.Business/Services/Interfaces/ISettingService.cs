using Airline.Business.ViewModel.SettingVM;
using Airline.Business.ViewModel.TagVM;
using Airline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Interfaces
{
    public interface ISettingService
    {
        Task<ICollection<Setting>> GetAllAsync();
        Task<Setting> GetByIdAsync(int id);
        Task<Setting> Update(UpdateSettingVM setting);

        Task<Setting> Delete(int id);
    }
}
