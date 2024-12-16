namespace EmployeeManagement.Models
{
    // This class implements the IEmployeeRepository interface, providing SQL database operations via Entity Framework Core.
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        // Readonly field to hold the database context.
        private readonly AppDbContext context;
        private readonly ILogger<SQLEmployeeRepository> logger;

        // Constructor to initialize the database context.
        public SQLEmployeeRepository(
            AppDbContext context,
            ILogger<SQLEmployeeRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        // Adds a new employee to the database and saves changes.
        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges(); // Commits the addition to the database.
            return employee;
        }

        // Deletes an employee from the database using their ID.
        public Employee Delete(int id)
        {
            Employee employee = context.Employees.Find(id); // Retrieves the employee with the specified ID.
            if (employee != null)
            {
                context.Employees.Remove(employee); // Marks the employee for deletion.
                context.SaveChanges(); // Commits the deletion to the database.
            }
            return employee;
        }

        // Retrieves all employees from the database.
        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employees; // Returns a collection of all employees.
        }

        // Retrieves a specific employee by their ID.
        public Employee GetEmployee(int id)
        {

            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");

            return context.Employees.Find(id); // Finds and returns the employee with the specified ID.
        }

        // Updates an employee's information in the database.
        public Employee Update(Employee employeeChanges)
        {
            // Attaches the updated employee object to the context.
            var employee = context.Employees.Attach(employeeChanges);
            // Marks the object as modified.
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            // Commits the changes to the database.
            context.SaveChanges();
            return employeeChanges;
        }
    }
}
