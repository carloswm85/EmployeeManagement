using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementCore22.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you've requested could not be found";
                    break;
            }
            return View("NotFound");
        }
    }
}
