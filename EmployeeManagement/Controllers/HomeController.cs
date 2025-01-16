using System.Diagnostics;
using EmployeeManagement.Models;
using EmployeeManagement.Security;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    /// <summary>
    /// The HomeController class manages requests related to employee management, 
    /// including displaying employee lists, creating, editing, and viewing employee details. 
    /// It also handles privacy and error views and ensures secure handling of sensitive data using data protection.
    /// </summary>

    // [Route("[controller]/[action]")]
    [Authorize]
    public class HomeController : Controller
    {
        #region Constructor Injection

        private readonly ILogger<HomeController> _logger; // Logger for logging information, warnings, errors, etc.
        private readonly IEmployeeRepository _employeeRepository; // Repository for accessing employee data.
        private readonly IWebHostEnvironment _hostingEnvironment; // Provides information about the web hosting environment.

        // IDataProtector interface provides methods to encrypt and decrypt data.
        // Protect() method is used to encrypt data, and Unprotect() method is used to decrypt it.
        private readonly IDataProtector protector;

        // IDataProtectionProvider interface is used to create an IDataProtector instance.
        // The CreateProtector() method requires a purpose string, which is used to define 
        // the scope and intent of the encryption. The purpose strings are managed through 
        // the DataProtectionPurposeStrings class.

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class. 
        /// Injects dependencies for logging, employee repository, hosting environment, 
        /// and data protection services.
        /// </summary>
        /// <param name="logger">Logger for tracking and debugging application behavior.</param>
        /// <param name="employeeRepository">Repository for managing employee data.</param>
        /// <param name="hostingEnvironment">Provides access to the application's web root path.</param>
        /// <param name="dataProtectionProvider">Provides data protection services for encrypting and decrypting data.</param>
        /// <param name="dataProtectionPurposeStrings">Purpose strings used to define the scope of encryption.</param>
        public HomeController(
            ILogger<HomeController> logger,
            IEmployeeRepository employeeRepository,
            IWebHostEnvironment hostingEnvironment,
            IDataProtectionProvider dataProtectionProvider,
            DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            // Initialize logger for capturing application logs.
            _logger = logger;

            // Initialize the employee repository for database operations.
            _employeeRepository = employeeRepository;

            // Initialize the hosting environment to access application-specific paths.
            _hostingEnvironment = hostingEnvironment;

            // Create an IDataProtector instance using the provided purpose string.
            // The purpose string ensures the encrypted data can only be decrypted in the intended context.
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.EmployeeIdRouteValue);

            // Reference: https://youtu.be/HlHDTQhVYoI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&t=339
        }


        #endregion

        /// <summary>
        /// Displays the list of all employees. The employee IDs are encrypted before being passed to the view.
        /// </summary>
        /// <returns>A view displaying the list of employees with their encrypted IDs.</returns>
        [AllowAnonymous]
        public ViewResult Index()
        {
            // Retrieve all employees from the repository and encrypt their IDs for secure usage in the view.
            var model = _employeeRepository.GetAllEmployee()
                .Select(e =>
                {
                    e.EncryptedId = protector.Protect(e.Id.ToString()); // Encrypt the employee's ID.
                    return e;
                });

            // Return the view displaying the list of employees.
            return View(model);
        }

        /// <summary>
        /// Displays the details of a specific employee, using an encrypted ID to ensure security.
        /// </summary>
        /// <param name="id">The encrypted ID of the employee whose details are to be displayed.</param>
        /// <returns>
        /// A view displaying the details of the employee. 
        /// If the employee is not found, returns a "EmployeeNotFound" view.
        /// </returns>
        [AllowAnonymous]
        public ViewResult Details(string id)
        {
            // Decrypt the encrypted ID and convert it to an integer.
            string decryptedId = protector.Unprotect(id);
            int decryptedIntId = Convert.ToInt32(decryptedId);

            // Log various messages for debugging and monitoring purposes.
            _logger.LogTrace("Trace Log");
            _logger.LogDebug("Debug Log");
            _logger.LogInformation("Information Log");
            _logger.LogWarning("Warning Log");
            _logger.LogError("Error Log");
            _logger.LogCritical("Critical Log");

            // Retrieve the employee using the decrypted ID.
            Employee employee = _employeeRepository.GetEmployee(decryptedIntId);

            // If the employee does not exist, return a "404 Not Found" response with a custom view.
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }

            // Create a view model to pass the employee details and page title to the view.
            var homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };

            // Return the view displaying the employee details.
            return View(homeDetailsViewModel);
        }

        /// <summary>
        /// Displays the Create view for adding a new employee.
        /// </summary>
        /// <returns>A view for creating a new employee.</returns>
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        /// <summary>
        /// Displays the Edit view for updating the details of a specific employee.
        /// </summary>
        /// <param name="id">The ID of the employee whose details are to be edited.</param>
        /// <returns>A view populated with the existing details of the employee.</returns>
        [HttpGet]
        public ViewResult Edit(int id)
        {
            // Retrieve the existing employee details using the provided ID.
            Employee employee = _employeeRepository.GetEmployee(id);

            // Create a view model populated with the employee's current details.
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath // Include the existing photo path for reference.
            };

            // Return the view for editing the employee's details.
            return View(employeeEditViewModel);
        }

        /// <summary>
        /// Handles the editing of an existing employee by updating their details, processing uploaded photos, 
        /// and managing the replacement of an existing photo if applicable.
        /// </summary>
        /// <param name="model">The view model containing the updated employee details and uploaded photo(s).</param>
        /// <returns>
        /// Redirects to the "Index" action if the update is successful. 
        /// Otherwise, returns the view to allow corrections.
        /// </returns>
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            // Validate the incoming model. If the model state is valid, proceed with the update.
            if (ModelState.IsValid)
            {
                // Retrieve the existing employee from the repository by their ID.
                Employee employee = _employeeRepository.GetEmployee(model.Id);

                // Update the employee's properties with the new values from the model.
                employee.Name = model.Name;           // Update the employee's name.
                employee.Email = model.Email;         // Update the employee's email address.
                employee.Department = model.Department; // Update the employee's department.

                // Check if a new photo has been uploaded.
                if (model.Photos != null)
                {
                    // If there is an existing photo, delete it from the file system.
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath); // Remove the old photo from the "images" folder.
                    }

                    // Process the uploaded photo(s) and assign the new unique file name to the employee.
                    employee.PhotoPath = ProcessUploadedFile(model);
                }

                // Update the employee's details in the repository (database).
                _employeeRepository.Update(employee);

                // Redirect to the Index page to show the updated list of employees.
                return RedirectToAction("index");
            }

            // If the model state is invalid, return the same view for corrections.
            return View();
        }


        /// <summary>
        /// Handles the creation of a new employee by processing the provided form data, 
        /// saving the uploaded photo, and adding the employee to the database.
        /// </summary>
        /// <param name="model">The view model containing the employee details and uploaded photo(s).</param>
        /// <returns>
        /// Redirects to the "Details" action for the newly created employee if successful. 
        /// Otherwise, returns the view to allow corrections.
        /// </returns>
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? uniqueFileName = ProcessUploadedFile(model);

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }

        /// <summary>
        /// Processes the uploaded files from the given model, saves them to the "images" folder in the wwwroot directory, 
        /// and generates a unique file name for each uploaded file.
        /// </summary>
        /// <param name="model">The view model containing the list of uploaded photos to be processed.</param>
        /// <returns>The unique file name of the last uploaded photo, or null if no photos were uploaded.</returns>
        private string? ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string? uniqueFileName = null;

            // Check if the model contains any uploaded photos.
            if (model.Photos != null && model.Photos.Count > 0)
            {
                // Log the web root path for debugging purposes.
                Console.WriteLine($"_hostingEnvironment.WebRootPath: {_hostingEnvironment.WebRootPath}");

                // Define the folder path where the uploaded images will be stored.
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

                // Process each photo in the uploaded photos collection.
                foreach (IFormFile photo in model.Photos)
                {
                    // Generate a unique file name using a GUID to avoid conflicts.
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                    // Combine the upload folder path and the unique file name to get the full file path.
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the file to the specified location using a FileStream.
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                    /*
                     * IMPORTANT:
                     * Always dispose of the FileStream to release system resources.
                     * Failure to do so may cause the following error:
                     * IOException: The process cannot access the file because it is being used by another process.
                     * This issue could occur when trying to access or overwrite the file in subsequent operations.
                     * Implementing 'using' ensures proper disposal of the FileStream.
                     */
                }
            }

            // Return the unique file name of the last processed photo or null if no photos were uploaded.
            return uniqueFileName;
        }

        /// <summary>
        /// Handles the Privacy view, which is typically used to display information about the application's privacy policy.
        /// </summary>
        /// <returns>A view that renders the Privacy page.</returns>
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Handles the Error view and provides details about an error that has occurred during the application's execution.
        /// </summary>
        /// <returns>A view that renders the Error page, including the error details encapsulated in the <see cref="ErrorViewModel"/>.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
