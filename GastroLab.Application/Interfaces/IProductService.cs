using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface IProductService
    {
        public void UpdateProductStatus(int id, bool status);
        public void UpdateProduct(ProductVM product);
        public ProductVM GetProductById(int id);
        public IEnumerable<ProductVM> GetAllProducts();
        public void DeleteProduct(int id);
        public void AddProduct(ProductVM product);
        public IEnumerable<CategoryVM> GetAllCategories();
        public IEnumerable<IngredientVM> GetAllIngredients();
        void AddCategory(CategoryVM category);
        void AddIngredient(IngredientVM ingredient);
    }
}
