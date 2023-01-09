using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementCore22.Models
{
    public static class ModelBuilderExtensions
    {
        // This is an extension method for Microsoft.EntityFrameworkCore.ModelBuilder
        // It is used in AppDbContext.cs
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Mary",
                    Department = Dept.IT,
                    Email = "mary@pragimtech.com"
                },
                new Employee
                {
                    Id = 2,
                    Name = "John",
                    Department = Dept.HR,
                    Email = "john@pragimtech.com"
                }
            );
        }
    }
}
