namespace GastroLab.Presentation.RequestModels
{
    public class UpdateWorkingTimeRequest
    {
        public int TimeslotId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string UserId { get; set; }
    }
}
