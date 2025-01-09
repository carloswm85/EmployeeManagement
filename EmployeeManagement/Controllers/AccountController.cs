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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, City = model.City };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // If user is signed in and it is admin
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }

                    // `isPersistent` specifies if we want to create a session cookie
                    // or a permanent cookie
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                // Displayed in asp-validation-summary tag helper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (ModelState.IsValid) // When client-side validation is correctly implemented, this break-point is never hit
            {
                
                var result = await signInManager.PasswordSignInAsync(userName: model.Email, password: model.Password,
                    isPersistent: model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) // Without Url.IsLocalUrl, it will redirect to malicious site example
                    {
                        return Redirect(returnUrl);
                        //return LocalRedirect(returnUrl); // Use without Url.IsLocalUrl(returnUrl)
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }

                ModelState.AddModelError(
                    key: string.Empty,
                    errorMessage: "Invalid Login Attempt"
                 );
            }

            return View();
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
                // Handle the case where the user does not have a local account

                // Get the user's email from the external login claims
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    // Check if a user with this email already exists
                    var user = await userManager.FindByEmailAsync(email);

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
