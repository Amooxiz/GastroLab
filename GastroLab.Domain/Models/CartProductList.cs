namespace GastroLab.Domain.Models
{
    public class CartProductList
    {
        public decimal TotalPrice { get; set; }
        public List<CartProduct> Products { get; set; } = new List<CartProduct>();
    }
    public class CartProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid ItemId { get; set; }
        public List<SelectedOption> SelectedOptions { get; set; } = new List<SelectedOption>();
    }

    public class SelectedOption
    {
        public int OptionSetId { get; set; }
        public string OptionSetName { get; set; }
        public int OptionId { get; set; }
        public string OptionName { get; set; }
        public decimal Price { get; set; }
    }
}
