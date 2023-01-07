using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementCore22.Models
{
    public class AppDbContext : DbContext
    {
        // Configure: (1) DB Provider, and (2) connection string
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        // Include a DB set property for each type in the project (Employee.cs)
        public DbSet<Employee> Employees { get; set; }
    }
}
