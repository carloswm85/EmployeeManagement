using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementCore22.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    // This status code works with: https://localhost:44331/foo/bar?abc=xyz
                    ViewBag.ErrorMessage = "Sorry, the resource you've requested could not be found";
                    ViewBag.Path = statusCodeResult.OriginalPath; // /foo/bar
                    ViewBag.QS = statusCodeResult.OriginalQueryString; // ?abc=xyz
                    break;
            }
            return View("NotFound");
        }
    }
}
