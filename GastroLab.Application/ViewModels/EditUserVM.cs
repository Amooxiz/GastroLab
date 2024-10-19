using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GastroLab.Application.ViewModels
{
    public class EditUserVM
    {
        // User Identifier (required for editing)
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

        // Roles assigned to the user
        public List<string> Roles { get; set; } = new List<string>();

        // Roles selected in the form
        public string[] SelectedRoles { get; set; } = new string[] { };

        // All available roles for selection in the UI
        public List<SelectListItem> AllRoles { get; set; } = new List<SelectListItem>();
    }
}
