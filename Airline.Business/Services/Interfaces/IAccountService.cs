using Airline.Business.ViewModel.AccountVM;
using Airline.Core.Entities;
using Microsoft.AspNetCore.Identity;
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
        Task Subscription(SubscribeVM vm);

       
    }
}
