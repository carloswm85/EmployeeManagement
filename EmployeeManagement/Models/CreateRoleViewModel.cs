using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
