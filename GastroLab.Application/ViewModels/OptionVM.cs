namespace GastroLab.Application.ViewModels
{
    public class OptionVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public bool IsSelected { get; set; }
        public decimal? SelectedPrice { get; set; }
    }
}