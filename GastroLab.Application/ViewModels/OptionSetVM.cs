namespace GastroLab.Application.ViewModels
{
    public class OptionSetVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public List<OptionVM> options { get; set; } = new List<OptionVM>();
    }
}