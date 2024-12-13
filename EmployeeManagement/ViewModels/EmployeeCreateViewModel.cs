using EmployeeManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public required string Name { get; set; }
        [Required]
        [Display(Name = "Office Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid email format")]
        public required string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }

        // https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iformfile?view=aspnetcore-8.0
        // For multiple files, watch: https://youtu.be/14ZqBoQIW-Q?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU
        public List<IFormFile>? Photos { get; set; }

        // The model Employee contains:
        // public string PhotoPath { get; set; }
    }
}
