using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Security
{
    /// <summary>
    /// Represents a custom authorization requirement for managing admin roles and claims.
    /// This requirement is used in conjunction with one or more custom handlers to enforce specific authorization policies.
    /// </summary>
    public class ManageAdminRolesAndClaimsRequirement : IAuthorizationRequirement
    {
        // This class serves as a marker for the authorization requirement.
        // It does not contain any properties or methods, as the logic is implemented in the corresponding handlers.
    }
}
