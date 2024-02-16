using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using GastroLab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly GastroLabDbContext _context;
        public ProductRepository(GastroLabDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var productToDelete = _context.Products.Find(id);
            if (productToDelete == null)
            {
                throw new ArgumentNullException(nameof(productToDelete));
            }
            _context.Products.Remove(productToDelete);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products
                .Include(x => x.ProductPricing)
                .Include(x => x.ProductIngredients)
                .ThenInclude(x => x.Ingredient)
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Order)
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .Include(x => x.ProductOptionSets)
                .ThenInclude(x => x.OptionSet);
        }

        public Product GetProductById(int id)
        {
            var product = _context.Products
                .Where(x => x.Id == id)
                .Include(x => x.ProductPricing)
                .Include(x => x.ProductIngredients)
                .ThenInclude(x => x.Ingredient)
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Order)
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .Include(x => x.ProductOptionSets)
                .ThenInclude(x => x.OptionSet)
                .ThenInclude(x => x.OptionSetOptions)
                .ThenInclude(x => x.Option)
                .FirstOrDefault();
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            return product;
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void UpdateProductStatus(int id, bool status)
        {
            var productToUpdate = _context.Products.Find(id);
            if (productToUpdate == null)
            {
                throw new ArgumentNullException(nameof(productToUpdate));
            }

            productToUpdate.productStatus = status ? ProductStatus.Available : ProductStatus.Unavailable;
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public IEnumerable<Ingredient> GetAllIngredients()
        {
            return _context.Ingredients;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void AddIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();
        }
    }
}
