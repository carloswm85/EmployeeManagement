using System.Collections.Generic;

namespace EmployeeManagementCore22.Models
{
    /* The repository interface only contains what operations are supported
     * by the current repository. It does not contain the details of how
     * that operation is performed.
     */
    public interface IEmployeeRepository
    {
        // ALL CRUD OPERATIONS ARE DESCRIBED
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetAllEmployee();
        Employee Add(Employee employee);
        Employee Update(Employee employee);
        Employee Delete(int id);
    }
}

/* WHY USE REPOSITORY PATTERN?
 * - Benefits: Cleaner code, easy to reuse and maintain.
 * - Enables the use of loosed coupled systems.
 */
