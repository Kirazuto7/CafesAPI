using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using CafesAPI.Models;

namespace CafesAPI.Models
{
    public class CafesAPIDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public CafesAPIDBContext(DbContextOptions<CafesAPIDBContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("cafesdb");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Cafe> Cafe { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Location> Location { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
    }
}
