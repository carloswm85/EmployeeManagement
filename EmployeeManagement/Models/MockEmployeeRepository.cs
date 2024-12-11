
namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Mary", Email = "mary@pragimtech.com", Department = Dept.None },
                new Employee() { Id = 2, Name = "John", Email = "john@pragimtech.com", Department = Dept.HR },
                new Employee() { Id = 3, Name = "Sam", Email = "sam@pragimtech.com", Department = Dept.IT },
                new Employee() { Id = 4, Name = "Carlos", Email = "carlos@pragimtech.com", Department = Dept.Payroll },
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(x => x.Id);
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }

        Employee IEmployeeRepository.GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }


    }
}