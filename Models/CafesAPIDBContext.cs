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
        public DbSet<Menu> Menu { get; set; } = null!;
        public DbSet<Item> Item { get; set; } = null!;
        public DbSet<Location> Location { get; set; } = null!;
        public DbSet<Schedule> Schedule { get; set; } = null!;
    }
}
