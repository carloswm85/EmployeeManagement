using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementCore22.Models
{
    
    // Why use IEmployeeRepository? For using dependency injection, not use concrete implementations
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepository() // ctor
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Mary", Department = "HR", Email = "mary@mail.com" },
                new Employee() { Id = 2, Name = "John", Department = "IT", Email = "john@mail.com" },
                new Employee() { Id = 3, Name = "Sam", Department = "IT", Email = "sam@mail.com" }
            };
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }
    }
}

