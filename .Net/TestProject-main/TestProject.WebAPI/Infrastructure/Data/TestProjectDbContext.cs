using Microsoft.EntityFrameworkCore;
using TestProject.WebAPI.Domain.Models;
using TestProject.WebAPI.Infrastructure.Data.Configurations;

namespace TestProject.WebAPI.Infrastructure.Data
{
    public class TestProjectDbContext : DbContext
    {
        public TestProjectDbContext(DbContextOptions<TestProjectDbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

    }
}
