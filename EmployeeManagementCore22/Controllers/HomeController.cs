
using EmployeeManagementCore22.Models;
using EmployeeManagementCore22.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementCore22.Controllers
{
    //[Route("Home")] // Used for simplifing routing to controllers
    public class HomeController : Controller
    {
        #region Constructor injection

        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #endregion
        
        public JsonResult JsonTest()
        {
            return Json(new { id = 1, name = "Carlos" });
        }

        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View("~/Views/Home/Index.cshtml", model);
        }

        public JsonResult DetailsJson()
        {
            Employee model = _employeeRepository.GetEmployee(1);
            return Json(model);
        }
        
        public ObjectResult DetailsObject()
        {
            Employee model = _employeeRepository.GetEmployee(1);
            return new ObjectResult(model);
        }

        [Route("Home/Details/{id}")]
        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id ?? 1), // If id is null, use 1
                //Employee = _employeeRepository.GetEmployee(id), // regular
                PageTitle = "Employee Details"

            };
            return View(homeDetailsViewModel);
        }
    }
}