using GastroLab.Application.Extensions;
using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public void AddProduct(ProductVM product)
        {
            _productRepository.AddProduct(product.ToModel());
        }

        public void DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
        }

        public IEnumerable<ProductVM> GetAllProducts()
        {
            return _productRepository.GetAllProducts().Select(x => x.ToVM());
            
        }

        public ProductVM GetProductById(int id)
        {
            return _productRepository.GetProductById(id).ToVM();
        }

        public void UpdateProduct(ProductVM product)
        {
            _productRepository.UpdateProduct(product.ToModel());
        }

        public void UpdateProductStatus(int id, bool status)
        {
            _productRepository.UpdateProductStatus(id, status);
        }

        public IEnumerable<CategoryVM> GetAllCategories()
        {
            return _productRepository.GetAllCategories().Select(x => x.ToVM());
        }

        public IEnumerable<IngredientVM> GetAllIngredients()
        {
            return _productRepository.GetAllIngredients().Select(x => x.ToVM());
        }

        public void AddCategory(CategoryVM category)
        {
            _productRepository.AddCategory(category.ToModel());
        }
        public void AddIngredient(IngredientVM ingredient)
        {
            _productRepository.AddIngredient(ingredient.ToModel());
        }
    }
}
