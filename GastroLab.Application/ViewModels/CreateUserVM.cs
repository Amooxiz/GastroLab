﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GastroLab.Application.ViewModels
{
    public class CreateUserVM
    {
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

        // Password (required for creation)
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        // Confirm Password (required for creation)
        [Required(ErrorMessage = "Please confirm the password.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        // Roles selected in the form
        public string[] SelectedRoles { get; set; } = new string[] { };

        // All available roles for selection in the UI
        public List<SelectListItem> AllRoles { get; set; } = new List<SelectListItem>();
    }
}
