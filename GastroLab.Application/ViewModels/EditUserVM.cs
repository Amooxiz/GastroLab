using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GastroLab.Application.ViewModels
{
    public class EditUserVM
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Display(Name = "Last Name")]
        public string Surname { get; set; }

        public List<string> Roles { get; set; } = new List<string>();

        public string[] SelectedRoles { get; set; } = new string[] { };

        public List<SelectListItem> AllRoles { get; set; } = new List<SelectListItem>();
    }
}
