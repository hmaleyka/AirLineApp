using Airline.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Deal> deals { get; set; }
        public DbSet<Package> packages { get; set; }
        public DbSet<Benefit> benefits { get; set; }
        public DbSet<DealPhoto> dealPhotos { get; set; }
        public DbSet<Team> teams { get; set; }
        public DbSet<Blog> blogs { get; set; }
        public DbSet<Tag> tags { get; set; }
        public DbSet<BlogTag> blogtags { get; set; }
        public DbSet<Subscribe> subscribe { get; set; }
        public DbSet<Flight> flights { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<Setting> setting { get; set; }
    }
}
