using DTH.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DTH.API.Data
{
    public class HomeProjectDbContext : DbContext
    {
        public HomeProjectDbContext(DbContextOptions<HomeProjectDbContext> options)
            : base(options)
        {
        }

        public DbSet<HomeProject>? HomeProjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=HomeProjects.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HomeProject>()
                .Property(p => p.ProjectStatus)
                .HasConversion(new EnumToStringConverter<ProjectStatus>());
            modelBuilder.Entity<HomeProject>()
                .Property(p => p.ClientStanding)
                .HasConversion(new EnumToStringConverter<ClientStanding>());
        }
    }
}
