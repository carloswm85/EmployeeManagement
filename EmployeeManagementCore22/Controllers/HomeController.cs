
using EmployeeManagementCore22.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementCore22.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository= employeeRepository;
        }
        public JsonResult JsonTest()
        {
            return Json(new { id = 1, name = "Carlos" });
        }
        public string Index()
        {
            return _employeeRepository.GetEmployee(1).Name;
        }
    }
}

