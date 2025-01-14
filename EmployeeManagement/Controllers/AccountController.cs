using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.Models;
using System.Security.Claims;

namespace EmployeeManagement.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<AccountController> logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        //[HttpPost, HttpGet]
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use.");
            }
        }

        /// <summary>
        /// Handles the user registration process.
        /// </summary>
        /// <param name="model">The registration data provided by the user.</param>
        /// <returns>Returns an IActionResult based on the success or failure of the registration process.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Check if the model state is valid before processing registration
            if (ModelState.IsValid)
            {
                // Create a new ApplicationUser instance with the provided registration details
                // ApplicationUser inherits from IdentityUser in ASP.NET Identity framework
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, City = model.City };

                // Attempt to create the user in the identity system using the provided password
                var result = await userManager.CreateAsync(user, model.Password);

                // If the user creation was successful
                if (result.Succeeded)
                {
                    // Generate an email confirmation token for the newly created user
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    // Create a confirmation link to be sent to the user's email
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                    // Log the confirmation link for debugging purposes
                    logger.Log(LogLevel.Warning, confirmationLink);

                    // Check if the current user is signed in and has an admin role
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        // Redirect the admin user to the list of users
                        return RedirectToAction("ListUsers", "Administration");
                    }

                    // Set success message details for the view
                    ViewBag.SuccessTitle = "Registration successful";
                    ViewBag.SuccessMessage = "Before you can Login, please confirm your email, by clicking on the confirmation link we have emailed you";

                    // Return the Success view to the user
                    return View("Success");
                }

                // If user creation failed, iterate through the errors and add them to the ModelState
                // These errors will be displayed using the asp-validation-summary tag helper in the view
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If model state is invalid or registration fails, return the view with the model data
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string? returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
            };

            return View(model);
        }

        /// <summary>
        /// Confirms the email address of a user based on the provided user ID and confirmation token.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="token">The email confirmation token.</param>
        /// <returns>Returns a view based on the success or failure of the email confirmation process.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            // Check if the userId or token is null, and redirect to the home page if either is missing
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            // Attempt to find the user in the identity system using the provided userId
            var user = await userManager.FindByIdAsync(userId);

            // If the user is not found, display an error message and return the NotFound view
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            // Attempt to confirm the user's email using the provided token
            // If successful, the EmailConfirmed field in the AspNetUsers table is set to True
            var result = await userManager.ConfirmEmailAsync(user, token);

            // If email confirmation is successful, return the default confirmation view
            if (result.Succeeded)
            {
                return View();
            }

            // If email confirmation fails, set an error title and return the ErrorPragim view
            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("ErrorPragim");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null && !user.EmailConfirmed &&
                            (await userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View(model);
                }

                var result = await signInManager.PasswordSignInAsync(model.Email,
                                        model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        /// <summary>
        /// Initiates the external login process by redirecting the user to the specified external provider's login page.
        /// Every call handles one single provider at the time.
        /// This works the same for Google, Facebook.
        /// </summary>
        /// <param name="provider">The name of the external authentication provider (e.g., Google, Facebook).</param>
        /// <param name="returnUrl">The URL to which the user should be redirected after a successful login.</param>
        /// <returns>
        /// A ChallengeResult that triggers the external authentication process with the specified provider.
        /// Notice the resulting callback is set in it ("ExternalLoginCallback").
        /// </returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string? returnUrl)
        {
            // Build the URL for the external login callback, including the return URL parameter
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl })
                  ?? throw new InvalidOperationException("Callback URL generation failed.");

            // Configure the properties for the external authentication process, including the redirect URL
            var properties = signInManager?.ConfigureExternalAuthenticationProperties(provider, redirectUrl)
                  ?? throw new NullReferenceException("SignInManager is not initialized.");

            // Return a ChallengeResult to trigger the external login with the specified provider
            return new ChallengeResult(provider, properties);
        }

        /// <summary>
        /// Handles the callback after an external login attempt.
        /// This works the same for Google, Facebook.
        /// </summary>
        /// <param name="returnUrl">The URL to return to after a successful login.</param>
        /// <param name="error">An error message returned by the external login provider, if any.</param>
        /// <returns>An IActionResult that redirects the user or shows an error page.</returns>
        [AllowAnonymous]
        public async Task<IActionResult>
            ExternalLoginCallback(string? returnUrl = null, string? error = null)
        {
            // If returnUrl is null, default to the home page
            returnUrl = returnUrl ?? Url.Content("~/");

            // Prepare the LoginViewModel with the return URL and external login schemes
            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins =
                        (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            // ERROR #1: Handle any errors from the external login provider
            if (error != null)
            {
                // Add an error to the model state to be displayed on the login page
                ModelState
                    .AddModelError(string.Empty, $"Error from external provider: {error}");

                // Return the login view with the error message
                return View("Login", loginViewModel);
            }

            // Get the login information about the user from the external login provider
            // User claims from the provider are contained here
            var info = await signInManager.GetExternalLoginInfoAsync();

            // ERROR #2: Handle cases where external login information is not available
            if (info == null)
            {
                // Add an error to the model state and return the login view
                ModelState
                    .AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginViewModel);
            }

            // Handle the case where the user does not have a local account
            // Get the user's email from the external login claims
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            ApplicationUser user = null;

            if (email != null)
            {
                // Find the user
                user = await userManager.FindByEmailAsync(email);

                // If email is not confirmed, display login view with validation error
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View("Login", loginViewModel);
                }
            }

            // Check if the user already has a login associated with this external provider
            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            // `isPersistent` determines whether the session is maintained across browser restarts
            // `bypassTwoFactor` allows bypassing two-factor authentication during this login attempt

            if (signInResult.Succeeded)
            {
                // Redirect the user to the specified return URL upon successful sign-in
                return LocalRedirect(returnUrl);
            }
            else
            {
                if (email != null)
                {
                    // If the user does not exist, create a new one without a password
                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };

                        // CREATE THE NEW USER IN THE DATABASE IF IT DOES NOT EXIST
                        await userManager.CreateAsync(user);

                        // After a local user account is created, generate and log the
                        // email confirmation link
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                        var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                        new { userId = user.Id, token = token }, Request.Scheme);

                        logger.Log(LogLevel.Warning, confirmationLink);

                        ViewBag.ErrorTitle = "Registration successful";
                        ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
                            "email, by clicking on the confirmation link we have emailed you";
                        return View("ErrorPragim");
                    }

                    // Link the external login to the newly created or existing user
                    // This means that one single user may have Google, Facebook, etc accounts
                    // link together, as long as they have the same email.
                    await userManager.AddLoginAsync(user, info);

                    // Sign in the user with the new login credentials
                    await signInManager.SignInAsync(user, isPersistent: false);

                    // Redirect the user to the specified return URL
                    return LocalRedirect(returnUrl);
                }

                // If no email claim is received, display an error page
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on Pragim@PragimTech.com";

                return View("Error");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
