namespace GastroLab.Presentation.RequestModels
{
    public class RegisterWorkingTimeRequest
    {
        public DateTime? StartDate { get; set; }
        public string StartTime { get; set; }
        public DateTime? FinishDate { get; set; }
        public string FinishTime { get; set; }
        public string? Description { get; set; }
        public string UserId { get; set; }
    }
}
