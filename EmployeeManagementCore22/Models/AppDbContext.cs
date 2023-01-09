using Microsoft.EntityFrameworkCore;
// DB Providers: https://learn.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli

namespace EmployeeManagementCore22.Models
{
    public class AppDbContext : DbContext
    {
        // Configure: (1) DB Provider, and (2) connection string
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            // Nothing
        }

        // Include a DB set property for each type in the project (Employee.cs)
        public DbSet<Employee> Employees { get; set; }

        // Seeding the Database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}

// EFC MIGRATIONS (also works with dot-net core cli)
/*
 * Package Manager Console:
 * - get-help about_entityframeworkcore
 * - add-migration NameOfTheMigration
 * - update-database
 * - remove-migration
 * - update-database NameOfMigration // This one is used when reverting changes to a previous migration
 */