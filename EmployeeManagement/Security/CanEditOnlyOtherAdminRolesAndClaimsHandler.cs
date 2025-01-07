using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace EmployeeManagement.Security
{
    /// <summary>
    /// Authorization handler that allows only an admin user to edit roles and claims of other admins, but not their own.
    /// </summary>
    public class CanEditOnlyOtherAdminRolesAndClaimsHandler : AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        /// <summary>
        /// Handles the authorization requirement by checking if the logged-in admin user has the appropriate role and claim.
        /// </summary>
        /// <param name="context">The context for the authorization request.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <returns>A task representing the completion of the requirement handling.</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageAdminRolesAndClaimsRequirement requirement)
        {
            // Cast the Resource to AuthorizationFilterContext to access HTTP context.
            var authFilterContext = context.Resource as AuthorizationFilterContext;
            if (authFilterContext == null)
            {
                // If the context is not valid, complete the task without success.
                return Task.CompletedTask;
            }

            // Retrieve the logged-in admin's user ID from the claims.
            string loggedInAdminId =
                context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            // Retrieve the admin ID being edited from the query string.
            string adminIdBeingEdited = authFilterContext.HttpContext.Request.Query["userId"];

            // Check if the user has the "Admin" role and the "Edit Role" claim, and ensure they are not editing their own account.
            if (context.User.IsInRole("Admin") &&
                context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") &&
                adminIdBeingEdited.ToLower() != loggedInAdminId?.ToLower())
            {
                // Mark the requirement as successfully met.
                context.Succeed(requirement);
            }

            // Complete the task.
            return Task.CompletedTask;
        }
    }
}
