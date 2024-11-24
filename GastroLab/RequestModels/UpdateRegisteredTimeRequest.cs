using System.ComponentModel.DataAnnotations;

namespace GastroLab.Presentation.RequestModels
{
    public class UpdateRegisteredTimeRequest
    {
        public int TimeslotId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime FinishDate { get; set; }

        public string Description { get; set; }

    }
}
