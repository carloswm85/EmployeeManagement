
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementCore22.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult Index()
        {
            return Json(new { id = 1, name = "Carlos" });
        }
    }
}

