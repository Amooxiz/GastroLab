using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using GastroLab.Infrastructure.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<GastroLabDbContext>
{
    public GastroLabDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<GastroLabDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GastroLab;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new GastroLabDbContext(optionsBuilder.Options);
    }
}