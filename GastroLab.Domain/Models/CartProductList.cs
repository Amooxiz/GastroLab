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
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public List<SelectedOption> SelectedOptions { get; set; } = new List<SelectedOption>();
    }

    public class SelectedOption
    {
        public int OptionSetId { get; set; }
        public int OptionId { get; set; }
        public decimal Price { get; set; }
    }
}
