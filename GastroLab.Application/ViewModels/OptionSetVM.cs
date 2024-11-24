namespace GastroLab.Application.ViewModels
{
    public class OptionSetVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public bool IsRequired { get; set; } = false;
        public bool IsMultiple { get; set; } = false;
        public bool IsGlobal { get; set; } = false;
        public int OptionCount { get; set; }
        public List<OptionVM> options { get; set; } = new List<OptionVM>();
    }
}