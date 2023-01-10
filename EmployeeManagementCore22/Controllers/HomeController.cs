
using EmployeeManagementCore22.Models;
using EmployeeManagementCore22.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

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
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(
            IEmployeeRepository employeeRepository,
            IHostingEnvironment hostingEnvironment
            )
        {
            _hostingEnvironment = hostingEnvironment;
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
        // With IActionResult, it is possible to return a ViewResult to RedirectToActionResult, the original return type of Create() post. It accepts any Result type that implements IActionResult.
        public IActionResult Create(EmployeeCreateViewModel model) {
            if(ModelState.IsValid)
            {
                string uniqueFileName = null;

                // Code for single file
                if (model.Photo != null)
                // Code for several files
                /*if (model.Photos != null && model.Photos.Count > 0)*/
                {
                    // Code for single file
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images"); // this line will save the file to wwwroot/images
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName; // Use Guid for avoid file overwriting
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

                    // Code for several files
                    /*
                    foreach (IFormFile photo in model.Photos)
                    {
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images"); // this line will save the file to wwwroot/images
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName; // Use Guid for avoid file overwriting
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    */
                }
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id});
            }

            return View(); 
        }
    } 
}