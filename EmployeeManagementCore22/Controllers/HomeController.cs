
using EmployeeManagementCore22.Models;
using EmployeeManagementCore22.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementCore22.Controllers
{
    // ATTRIBUTE ROUTING
    //[Route("Home")] // Used for simplifying routing to controllers
    //[Route("[controller]")] // Use of tokens, replaced with "Home"
    //[Route("[controller]/[action]")] // Use of tokens, replaced with "Home/Action"
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

        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        //[Route("/")]
        //[Route("/Index")]
        //[Route("~/")]
        //[Route("[action]")] // token, replaced with "Index"
        //[Route("~/Home")]
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

        //[Route("Home/Details/{id}")]
        //[Route("[action]/{id}")] // token, replaced with "Details"
        //[Route("{id?}")] // token, replaced with "Details"
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

        public string DetailsString(int? id, string name)
        {
            string result = "id = " + id.Value.ToString() + " | name = " + name;
            return result;
            // Valid requests:
            // Using form values
            // Using route values: https://localhost:44331/home/detailsstring/2?name=carlos&id=23
            // Using query strings: https://localhost:44331/home/detailsstring?name=carlos&id=23
        }

        [HttpGet]
        public ViewResult Create() {
            return View();
        }

        [HttpPost]
        public RedirectToActionResult Create(Employee employee) {
            Employee newEmployee = _employeeRepository.Add(employee);
            return RedirectToAction("details", new { id = newEmployee.Id});
        }
    } 
}