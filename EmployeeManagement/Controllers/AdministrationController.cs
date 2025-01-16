using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace EmployeeManagement.Controllers
{
    //[Authorize(Policy = "AdminRolePolicy")]
    /// <summary>
    /// Controller responsible for administration tasks such as managing roles, users, and claims.
    /// </summary>
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<AdministrationController> logger;

        /// <summary>
        /// Constructor for initializing dependencies for AdministrationController.
        /// </summary>
        /// <param name="roleManager">Role manager service for managing roles.</param>
        /// <param name="userManager">User manager service for managing users.</param>
        /// <param name="logger">Logger service for logging activities and errors.</param>
        public AdministrationController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, ILogger<AdministrationController> logger)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
        }

        /// <summary>
        /// Displays the Manage User Claims view for a specific user.
        /// </summary>
        /// <param name="userId">ID of the user whose claims are being managed.</param>
        /// <returns>A view displaying the user's claims and their selection status.</returns>
        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            // UserManager service GetClaimsAsync method gets all the current claims of the user
            var existingUserClaims = await userManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel
            {
                UserId = userId
            };

            // Loop through each claim we have in our application
            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                // If the user has the claim, set IsSelected property to true, so the checkbox
                // next to the claim is checked on the UI
                if (existingUserClaims.Any(c => c.Type == claim.Type && c.Value == "true"))
                {
                    userClaim.IsSelected = true;
                }

                model.Claims.Add(userClaim);
            }

            return View(model);

        }

        /// <summary>
        /// Handles the update of user claims.
        /// </summary>
        /// <param name="model">ViewModel containing user claims and their selection status.</param>
        /// <returns>Redirects to EditUser view if successful; otherwise returns ManageUserClaims view with errors.</returns>
        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.UserId} cannot be found";
                return View("NotFound");
            }

            // Get all the user existing claims and delete them
            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing claims");
                return View(model);
            }

            // Add all the claims that are selected on the UI
            // https://youtu.be/I2wgxzLbESA?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&t=126
            var newClaims = model.Claims.Select(c => new Claim(c.ClaimType, c.IsSelected ? "true" : "false"));
            result = await userManager.AddClaimsAsync(user, newClaims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected claims to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = model.UserId });

        }

        /// <summary>
        /// Displays the Manage User Roles view for a specific user.
        /// </summary>
        /// <param name="userId">ID of the user whose roles are being managed.</param>
        /// <returns>A view displaying the user's roles and their selection status.</returns>
        [HttpGet]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            // If you want to see ALL user roles, as claims, place a breackpoing here.
            // And execute in the Immediate Window:
            //      User.Claims.Where(c=> c.Type == ClaimTypes.Role).ToList()
            
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }

        /// <summary>
        /// Updates the roles assigned to a user based on the selection made in the UI.
        /// </summary>
        /// <param name="model">A list of roles and their selection status for the user.</param>
        /// <param name="userId">The ID of the user whose roles are being managed.</param>
        /// <returns>
        /// Redirects to the EditUser view if successful. 
        /// Returns the current view with errors if role assignment fails.
        /// </returns>
        [HttpPost]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            // Find the user by ID
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                // Display an error if the user is not found
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            // Retrieve the roles currently assigned to the user
            var roles = await userManager.GetRolesAsync(user);

            // Remove all roles currently assigned to the user
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                // Handle errors in removing roles
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            // Add the roles selected in the UI to the user
            result = await userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                // Handle errors in adding roles
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            // Redirect to the EditUser view to reflect the changes
            return RedirectToAction("EditUser", new { Id = userId });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                try
                {
                    var result = await userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListUsers");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View("ListUsers");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    logger.LogError(ex.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                // Wrap the code in a try/catch block
                try
                {
                    //throw new Exception("Test Exception");

                    var result = await roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View("ListRoles");
                }
                // If the exception is DbUpdateException, we know we NOT able to
                // delete the role as there are users in the role being deleted
                catch (DbUpdateException ex)
                {
                    //Log the exception to a file. We discussed logging to a file
                    // using Nlog in Part 63 of ASP.NET Core tutorial
                    logger.LogError($"Exception Occured : {ex}");
                    // Pass the ErrorTitle and ErrorMessage that you want to show to
                    // the user using ViewBag. The Error view retrieves this data
                    // from the ViewBag and displays to the user.
                    ViewBag.ErrorTitle = $"{role.Name} role is in use";
                    ViewBag.ErrorMessage = $"{role.Name} role cannot be deleted as there are users " +
                        "in this role. If you want to delete this role, please remove the users from " +
                        "the role and then try to delete";
                    return View("ErrorPragim");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            // GetClaimsAsync retunrs the list of role Claims
            var userClaims = await userManager.GetClaimsAsync(user);

            // GetRolesAsync returns the list of role Roles
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                City = user.City,
                Claims = userClaims.Select(c => c.Type + ": " + c.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.City = model.City;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        /// <summary>
        /// Displays the Create Role view
        /// </summary>
        /// <returns>A view to create a new role</returns>
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        /// <summary>
        /// Handles the creation of a new role
        /// </summary>
        /// <param name="model">The model containing the role name</param>
        /// <returns>Redirects to ListRoles view if successful; otherwise returns the CreateRole view with errors</returns>
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new role with the provided name
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Attempt to save the new role to the database
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction(actionName: "ListRoles", controllerName: "Administration");
                }

                // Add error descriptions to the ModelState if creation fails
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        /// <summary>
        /// Displays a list of all roles
        /// </summary>
        /// <returns>A view displaying all roles</returns>
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        /// <summary>
        /// Displays the Edit Role view for a specific role
        /// </summary>
        /// <param name="id">The ID of the role to edit</param>
        /// <returns>A view to edit the role, or NotFound view if the role does not exist</returns>
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            // Find the role by ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            // Initialize the EditRoleViewModel
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            // Add the names of users in this role to the model
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        /// <summary>
        /// Handles updates to an existing role
        /// </summary>
        /// <param name="model">The model containing updated role information</param>
        /// <returns>Redirects to ListRoles view if successful; otherwise returns the EditRole view with errors</returns>
        [HttpPost]        
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                // Update the role's name
                role.Name = model.RoleName;

                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        /// <summary>
        /// Displays a list of users in a specific role
        /// </summary>
        /// <param name="roleId">The ID of the role</param>
        /// <returns>A view displaying users and their selection status for the role</returns>
        [HttpGet]
        public async Task<ActionResult> EditUsersInRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            ViewBag.roleId = role.Id;
            ViewBag.roleName = role.Name;

            var model = new List<UserRoleViewModel>();

            // Create UserRoleViewModel for each role in the database
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = await userManager.IsInRoleAsync(user, role.Name)
                };

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        /// <summary>
        /// Updates the list of users in a specific role
        /// </summary>
        /// <param name="model">The list of users and their selection status for the role</param>
        /// <param name="roleId">The ID of the role</param>
        /// <returns>Redirects to EditRole view if successful</returns>
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                // Add role to role if selected and not already in the role
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                // Remove role from role if not selected but already in the role
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }

                // Skip processing if no changes were made
                if (result != null && !result.Succeeded)
                {
                    continue;
                }

                if (i < model.Count - 1)
                    continue;
                else
                    return RedirectToAction("EditRole", new { Id = roleId });
            }

            return RedirectToAction("EditRole", new { Id = roleId });
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
