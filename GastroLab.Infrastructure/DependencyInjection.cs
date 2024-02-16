using GastroLab.Application.Interfaces;
using GastroLab.Domain.DBO;
using GastroLab.Infrastructure.Data;
using GastroLab.Infrastructure.Repositories;
using GastroLab.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectService(this IServiceCollection services, IConfiguration conf)
        {
            var connectionString = conf.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<GastroLabDbContext>(options =>
                options.UseSqlServer(connectionString));
            //services.AddDbContext<GrownOverDbContext>(options =>
            //    options.UseSqlServer(conf.GetConnectionString("DefaultConnection")));

            //services.AddDefaultIdentity<User>(options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = false;
            //    options.SignIn.RequireConfirmedEmail = false;
            //    options.Password.RequiredLength = 8;
            //}).AddRoles<IdentityRole>()
            //.AddEntityFrameworkStores<GrownOverDbContext>()
            //.AddDefaultTokenProviders();

            //services.AddAuthentication().AddJwtBearer
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOptionSetService, OptionSetService>();
            services.AddTransient<IOptionSetRepository, OptionSetRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICookieService, CookieService>();
            return services;
        }
    }
}
