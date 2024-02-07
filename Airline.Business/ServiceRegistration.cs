using Airline.Business.Services.Implementations;
using Airline.Business.Services.Interfaces;
using Airline.DAL.Repositories.Implementations;
using Airline.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            
            services.AddScoped<IBenefitService, BenefitService>();
            services.AddScoped<IPackageService, PackageService>();        
            services.AddScoped<IDealService, DealService>();          
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IBlogService, BlogService>();            
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
