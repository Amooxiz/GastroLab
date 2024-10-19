using GastroLab.Domain.DBO;
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

            if (userManager.FindByNameAsync(adminEmail).Result == null)
            {
                var admin = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                
                var userResult = userManager.CreateAsync(admin, adminPassword).Result;
                if (!userResult.Succeeded)
                {
                    throw new InvalidOperationException($"Seeding {adminEmail} user failed.");
                }
                
                var roleResult = userManager.AddToRoleAsync(admin, "Admin").Result;
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Adding {adminEmail} user to AdminRole failed.");
                }
            }
        }

        public static void SeedGlobalSettings(GastroLabDbContext context)
        {
            // Check if GlobalSettings already exists
            if (!context.GlobalSettings.Any())
            {
                // Create default Address
                var address = new Address
                {
                    Street = "Default Street",
                    City = "Default City",
                    HouseNumber = 1,
                    PostCode = "00-000"
                };

                context.Addresses.Add(address);
                context.SaveChanges(); // Save to generate AddressId

                // Create GlobalSettings with the newly created Address
                var globalSettings = new GlobalSettings
                {
                    RestaurantName = "Default Restaurant",
                    AddressId = address.Id,
                    DefaultDineInWaitingTime = TimeSpan.FromMinutes(30),
                    DefaultDeliveryWaitingTime = TimeSpan.FromMinutes(45)
                };

                context.GlobalSettings.Add(globalSettings);
                context.SaveChanges();
            }
        }
    }
}
