using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Security
{
    /// <summary>
    /// Authorization handler that automatically grants the requirement if the user is in the "Super Admin" role.
    /// This handler allows Super Admin users to manage admin roles and claims without further checks.
    /// </summary>
    public class SuperAdminHandler : AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        /// <summary>
        /// Handles the authorization requirement by checking if the user has the "Super Admin" role.
        /// </summary>
        /// <param name="context">The context for the authorization request.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <returns>A task representing the completion of the requirement handling.</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageAdminRolesAndClaimsRequirement requirement)
        {
            // If the user is in the "Super Admin" role, mark the requirement as successfully met.
            if (context.User.IsInRole("Super Admin"))
            {
                context.Succeed(requirement);
            }

            // Complete the task.
            return Task.CompletedTask;
        }
    }
}
