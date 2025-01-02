using System.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    // `ApplicationUser` is passed as a generated parameter
    public class AppDbContext : IdentityDbContext<ApplicationUser>
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

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                Debug.WriteLine($"Entity: {entityType.Name}");
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    Debug.WriteLine($"Foreign Key: {foreignKey.PrincipalEntityType.Name} -> {foreignKey.DeclaringEntityType.Name}");
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }


            // Seed it!
            modelBuilder.Seed();
        }
    }
}
