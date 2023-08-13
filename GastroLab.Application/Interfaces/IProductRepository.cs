using GastroLab.Application.ViewModels;
using GastroLab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface IProductRepository
    {
        public void UpdateProductStatus(int id, bool status);
        public void UpdateProduct(Product product);
        public Product GetProductById(int id);
        public IEnumerable<Product> GetAllProducts();
        public void DeleteProduct(int id);
        public void AddProduct(Product product);
        public IEnumerable<Category> GetAllCategories();
        public IEnumerable<Ingredient> GetAllIngredients();
        void AddCategory(Category category);
        void AddIngredient(Ingredient ingredient);
    }
}
