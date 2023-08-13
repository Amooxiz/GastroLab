using GastroLab.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace GastroLab.Infrastructure.Data
{
    public class GastroLabDbContext : IdentityDbContext<User>
    {
        public GastroLabDbContext(DbContextOptions<GastroLabDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<RegisteredTime> RegisteredTimes { get; set; }
        public DbSet<WorkingTime> WorkingTimes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<OptionSet> OptionSets { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<ProductOptionSet> ProductOptionSets { get; set; }
        public DbSet<ProductPricing> productPricings { get; set; }
        public DbSet<OptionSetOption> OptionSetOptions { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductPricing>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            
            builder.Entity<Supply>()
                .Property(p => p.TotalPrice)
                .HasColumnType("decimal(18,2)");
            
            builder.Entity<Order>()
                .Property(p => p.TotalPrice)
                .HasColumnType("decimal(18,2)");

            // Option N:N OptionSet

            builder.Entity<OptionSetOption>()
                .HasKey(pg => new { pg.OptionId, pg.OptionSetId });

            builder.Entity<OptionSetOption>()
                .HasOne<Option>(pg => pg.Option)
                .WithMany(p => p.OptionSetOptions)
                .HasForeignKey(p => p.OptionId);

            builder.Entity<OptionSetOption>()
                .HasOne<OptionSet>(pg => pg.OptionSet)
                .WithMany(g => g.OptionSetOptions)
                .HasForeignKey(g => g.OptionSetId);

            // Product N:N OptionSet
            
            builder.Entity<ProductOptionSet>()
                .HasKey(pg => new { pg.ProductId, pg.OptionSetId });

            builder.Entity<ProductOptionSet>()
                .HasOne<Product>(pg => pg.Product)
                .WithMany(p => p.ProductOptionSets)
                .HasForeignKey(p => p.ProductId);

            builder.Entity<ProductOptionSet>()
                .HasOne<OptionSet>(pg => pg.OptionSet)
                .WithMany(g => g.ProductOptionSets)
                .HasForeignKey(g => g.OptionSetId);

            // Product N:N Ingredient

            builder.Entity<ProductIngredient>()
                .HasKey(pg => new { pg.ProductId, pg.IngredientId });

            builder.Entity<ProductIngredient>()
                .HasOne<Product>(pg => pg.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(p => p.ProductId);

            builder.Entity<ProductIngredient>()
                .HasOne<Ingredient>(pg => pg.Ingredient)
                .WithMany(g => g.ProductIngredients)
                .HasForeignKey(g => g.IngredientId);
            
            // Product N:N Category

            builder.Entity<ProductCategory>()
                .HasKey(pg => new { pg.ProductId, pg.CategoryId });

            builder.Entity<ProductCategory>()
                .HasOne<Product>(pg => pg.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(p => p.ProductId);

            builder.Entity<ProductCategory>()
                .HasOne<Category>(pg => pg.Category)
                .WithMany(g => g.ProductCategories)
                .HasForeignKey(g => g.CategoryId);

            // Order N:N Product

            builder.Entity<OrderProduct>()
                .HasKey(pg => new { pg.OrderId, pg.ProductId });

            builder.Entity<OrderProduct>()
                .HasOne<Order>(pg => pg.Order)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(p => p.OrderId);

            builder.Entity<OrderProduct>()
                .HasOne<Product>(pg => pg.Product)
                .WithMany(g => g.OrderProducts)
                .HasForeignKey(g => g.ProductId);
            
        }
    }
}