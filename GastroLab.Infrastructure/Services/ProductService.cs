using GastroLab.Application.Extensions;
using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public void DeleteIngredient(int id)
        {
            _productRepository.DeleteIngredient(id);
        }
        public void DeleteCategory(int id)
        {
            _productRepository.DeleteCategory(id);
        }

        public IEnumerable<ProductVM> GetProducts(string searchName, int? categoryId)
        {
            return _productRepository.GetProducts(searchName, categoryId).Select(x => x.ToVM());
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
            var existingProduct = _productRepository.GetProductById(product.Id);
            this.UpdateProduct(existingProduct, product);
        }

        private void UpdateProduct(Product existingProduct, ProductVM updatedProduct)
        {
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.ProductPricing.Price = updatedProduct.Price;

            var existingProductIngredients = existingProduct.ProductIngredients != null 
                ? existingProduct.ProductIngredients.ToList() 
                : new List<ProductIngredient>();

            foreach (var updatedIngredientId in updatedProduct.SelectedIngredientIds)
            {
                var existingIngredientExist = false;
                if (existingProduct.ProductIngredients != null)
                {
                    existingIngredientExist = existingProductIngredients.Any(x => x.IngredientId == updatedIngredientId);
                }
                if (!existingIngredientExist)
                {
                    existingProduct.ProductIngredients.Add(new ProductIngredient
                    {
                        IngredientId = updatedIngredientId,
                        ProductId = existingProduct.Id
                    });
                }
            }

            foreach (var existingIngredient in existingProductIngredients)
            {
                var updatedIngredientExist = updatedProduct.SelectedIngredientIds.Any(x => x == existingIngredient.IngredientId);
                if (!updatedIngredientExist)
                {
                    _productRepository.DeleteProductIngredient(existingIngredient);
                }
            }

            var existingProductCategories = existingProduct.ProductCategories != null 
                ? existingProduct.ProductCategories.ToList() 
                : new List<ProductCategory>();

            foreach (var updatedCategoryId in updatedProduct.SelectedCategoryIds)
            {
                var existingCategoryExist = false;
                if (existingProduct.ProductCategories != null)
                {
                    existingCategoryExist = existingProductCategories.Any(x => x.CategoryId == updatedCategoryId);
                }
                if (!existingCategoryExist)
                {
                    existingProduct.ProductCategories.Add(new ProductCategory
                    {
                        CategoryId = updatedCategoryId,
                        ProductId = existingProduct.Id
                    });
                }
            }

            foreach (var existingCategory in existingProductCategories)
            {
                var updatedCategoryExist = updatedProduct.SelectedCategoryIds.Any(x => x == existingCategory.CategoryId);
                if (!updatedCategoryExist)
                {
                    _productRepository.DeleteProductCategory(existingCategory);
                }
            }

            var existingProductOptionSets = existingProduct.ProductOptionSets != null
                ? existingProduct.ProductOptionSets.ToList()
                : new List<ProductOptionSet>();

            var updatedProductOptionSets = updatedProduct.ToModel().ProductOptionSets != null
                ? updatedProduct.ToModel().ProductOptionSets.ToList()
                : new List<ProductOptionSet>();

            foreach (var updatedOptionSet in updatedProductOptionSets)
            {
                var existingOptionSetExist = false;
                if (existingProduct.ProductOptionSets != null)
                {
                    existingOptionSetExist = existingProductOptionSets.Any(x => x.OptionSetId == updatedOptionSet.OptionSetId);
                }
                if (!existingOptionSetExist)
                {
                    if (updatedOptionSet.Id == 0) // nowy optionSet
                    {
                        existingProduct.ProductOptionSets.Add(new ProductOptionSet()
                        {
                            ProductId = existingProduct.Id,
                            Product = existingProduct,
                            OptionSetId = updatedOptionSet.OptionSetId,
                            OptionSet = updatedOptionSet.OptionSet
                        });
                    }
                    else
                    {
                        existingProduct.ProductOptionSets.Add(new ProductOptionSet
                        {
                            ProductId = existingProduct.Id,
                            OptionSetId = updatedOptionSet.OptionSetId
                        });
                    }
                }
            }

            foreach (var existingOptionSet in existingProductOptionSets)
            {
                var updatedOptionSetExist = updatedProductOptionSets.Any(x => x.OptionSetId == existingOptionSet.OptionSetId);
                if (!updatedOptionSetExist)
                {
                    _productRepository.DeleteProductOptionSet(existingOptionSet);
                }
            }

            _productRepository.UpdateProduct(existingProduct);
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
