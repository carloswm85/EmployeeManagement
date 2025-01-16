using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    /// <summary>
    /// Only used for testing purposes at the beginning of the course..
    /// </summary>
    public class DepartmentsController : Controller
    {
        /// <summary>
        /// Displays a list of departments, as a string.
        /// </summary>
        /// <returns>A string message indicating the List method of the DepartmentsController is invoked.</returns>
        public string List()
        {
            return "List() of DepartmentsController";
        }

        /// <summary>
        /// Displays the details of a department, as a string.
        /// </summary>
        /// <returns>A string message indicating the Details method of the DepartmentsController is invoked.</returns>
        public string Details()
        {
            return "Details() of DepartmentsController";
        }
    }
}
