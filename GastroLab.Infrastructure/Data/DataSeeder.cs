using GastroLab.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void SeedUsersAndRoles(GastroLabDbContext context, IConfiguration conf,
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();
            
            var adminUsername = conf.GetSection("Users")["Admin:UserName"];
            var adminPassword = conf.GetSection("Users")["Admin:Password"];
            var adminEmail = conf.GetSection("Users")["Admin:Email"];

            string[] roles = conf.GetSection("Roles").GetChildren().Select(x => x.Value).ToArray();
            
            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                {
                    var result = roleManager.CreateAsync(new IdentityRole(role)).Result;
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException($"Seeding {role} role failed. Errors: {result.Errors.Select(e => e.Description)}");
                    }
                }
            }

            if (userManager.FindByNameAsync(adminUsername).Result == null)
            {
                var admin = new User
                {
                    UserName = adminUsername,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                
                var userResult = userManager.CreateAsync(admin, adminPassword).Result;
                if (!userResult.Succeeded)
                {
                    throw new InvalidOperationException($"Seeding {adminUsername} user failed.");
                }
                
                var roleResult = userManager.AddToRoleAsync(admin, "Admin").Result;
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Adding {adminUsername} user to AdminRole failed.");
                }
            }
        }
    }
}
