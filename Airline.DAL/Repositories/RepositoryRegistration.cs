using Airline.DAL.Repositories.Implementations;
using Airline.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.DAL.Repositories
{
    public static class RepositoryRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBenefitRepository, BenefitRepository>();
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IDealRepository, DealRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
        }
    }
}
