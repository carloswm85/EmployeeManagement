using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    /// <summary>
    /// Controller responsible for handling error scenarios in the application.
    /// </summary>
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance to log error details.</param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Handles HTTP status code errors such as 404 and 405.
        /// </summary>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <returns>A view displaying error details.</returns>
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found.";
                    logger.LogWarning($"{statusCode} Error Ocurred. Path = {statusCodeResult.OriginalPath}" + 
                        $" and QueryString = {statusCodeResult.OriginalQueryString ?? "no-query-string"}");
                    break;
                case 405:
                    // A 405 status code, also known as "Method Not Allowed", is an HTTP response code that a server
                    // sends when a client requests a method that the resource doesn't support.
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found.";
                    logger.LogWarning($"405 Error Ocurred. Path = {statusCodeResult.OriginalPath}" +
                        $" and QueryString = {statusCodeResult.OriginalQueryString ?? "no-query-string"}");
                    break;
                default:
                    ViewBag.ErrorMessage = $"Status code has occured: {statusCode}";
                    break;
            }

            return View("NotFound");
        }

        /// <summary>
        /// Handles application-level exceptions by logging error details and displaying a custom error page.
        /// </summary>
        /// <returns>A view displaying error details, using a custom "ErrorPragim" view.</returns>
        [Route("Error")]
        [AllowAnonymous]
        public ActionResult Error()
        {
            // Retrieve exception details from the HttpContext features.
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            // Log the path that caused the exception and the exception message.
            logger.LogError($"The path {exceptionDetails.Path} threw an exception " +
                $" {exceptionDetails.Error}");

            // Return a view showing a custom error page (ErrorPragim).
            return View("ErrorPragim");
        }

    }
}
