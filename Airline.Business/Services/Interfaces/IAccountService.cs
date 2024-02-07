using Airline.Business.ViewModel.AccountVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task Register(RegisterVM vm);
        Task Login(LoginVM vm);
        Task Logout();
        Task CreateRoles();
    }
}
