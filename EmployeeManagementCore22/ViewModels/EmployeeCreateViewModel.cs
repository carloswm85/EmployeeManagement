using EmployeeManagementCore22.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EmployeeManagementCore22.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format.")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }

        [Required]
        public Dept? Department { get; set; }

        // Code for single file
        public IFormFile Photo { get; set; }

        // Code for several files
        /* For adding several files for one single employee , in the DB,
         * split the Employee table into 2 tables. Add a one to many relationship,
         * with Employee table and EmployeePhotos table
         */
        //public List<IFormFile> Photos { get; set; }
    }
}
