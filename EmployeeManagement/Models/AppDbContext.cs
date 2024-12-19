using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public class AppDbContext : IdentityDbContext
    {
        // Database
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
            
        }

        // Tables
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // At this point, remember to pass the base class DbContext, which
            // parents IdentityDbContext
            base.OnModelCreating(modelBuilder);

            // Seed it!
            modelBuilder.Seed();
        }
    }
}
