using Microsoft.EntityFrameworkCore;
using MSJennings.EFCoreDemo.Business.Models;

namespace MSJennings.EFCoreDemo.Business.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<ReportSection> ReportSections { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
