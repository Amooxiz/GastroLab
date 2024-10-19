using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GastroLab.Application.ViewModels
{
    public class UserVM
    {
        // User Identifier
        public string Id { get; set; }

        // Username (required)
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        // Email Address (required, with email validation)
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // First Name
        [Display(Name = "First Name")]
        public string Name { get; set; }

        // Last Name
        [Display(Name = "Last Name")]
        public string Surname { get; set; }

        // Password (for creating a new user)
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        // Confirm Password (for validation)
        [Required(ErrorMessage = "Please confirm the password.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        // Roles assigned to the user
        public List<string> Roles { get; set; } = new List<string>();

        // All available roles for selection in the UI
        public List<SelectListItem> AllRoles { get; set; } = new List<SelectListItem>();

        // Roles selected in the form (for binding selected roles from the UI)
        public string[] SelectedRoles { get; set; } = new string[] { };
    }
}
