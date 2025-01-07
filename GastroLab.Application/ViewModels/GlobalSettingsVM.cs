using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.ViewModels
{
    public class GlobalSettingsVM
    {
        public int Id { get; set; }
        [Display(Name = "Restaurant name")]
        public string RestaurantName { get; set; }
        public virtual AddressVM AddressVM { get; set; }
        [Required]
        [Range(1, 1440, ErrorMessage = "Waiting time must be between 1 and 1440 minutes.")]
        [Display(Name = "Default dineIn waiting time (minutes)")]
        public int DefaultDineInWaitingTimeInMinutes { get; set; }

        [Required]
        [Range(1, 1440, ErrorMessage = "Waiting time must be between 1 and 1440 minutes.")]
        [Display(Name = "Default delivery waiting time (minutes)")]
        public int DefaultDeliveryWaitingTimeInMinutes { get; set; }
    }
}
