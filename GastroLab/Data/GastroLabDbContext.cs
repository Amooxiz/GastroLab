using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GastroLab.Data
{
    public class GastroLabDbContext : IdentityDbContext
    {
        public GastroLabDbContext(DbContextOptions<GastroLabDbContext> options)
            : base(options)
        {
        }
    }
}