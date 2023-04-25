using GastroLab.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GastroLab.Infrastructure.Data
{
    public class GastroLabDbContext : IdentityDbContext<User>
    {
        public GastroLabDbContext(DbContextOptions<GastroLabDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}