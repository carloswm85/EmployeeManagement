using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementCore22.Models
{
    public class Employee
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }
        
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format.")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }

        [Required]
        // Dept only, displays validation error: The value '' is invalid, because value for "Please Select" in option-select is empty.
        // Dept Required only, displays validation error: The value '' is invalid. n fact, required has no effect, since Enum data type are inherently required.
        // Dept ? only, displays validation error: None, and the Enum data type is made OPTIONAL
        // Dept ? and Required, displays CORRECT validation error: The Department field is required.
        public Dept? Department { get; set; } 
    }
}
