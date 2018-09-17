using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using API.Models.Entities;

namespace API.Data
{
    public class SeedData
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<AppUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            //context.Database.EnsureCreated();
            string[] roleNames = { "Admin", "Manager", "Member" };
            context.Database.Migrate();
            context.Database.EnsureCreated();
            foreach (var roleName in roleNames)
            {
                var roleExist = await rolemanager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    await rolemanager.CreateAsync(new IdentityRole(roleName));
                }
            }
            var admin = await usermanager.FindByNameAsync("admin");
            if(admin == null)
            {
                var _admin = new AppUser
                {
                    UserName = "admin", Email = "admin@fler.com", FirstName="admin", LastName=""
                };
                var result = await usermanager.CreateAsync(_admin, "1234567890");
                if (result.Succeeded)
                {
                    await usermanager.AddToRoleAsync(_admin, "Admin");
                }
            }
            else
            {
                if (! await usermanager.IsInRoleAsync(admin, "Admin"))
                {
                    await usermanager.AddToRoleAsync(admin, "Admin");
                }
            }

            //if (!context.Сurrencies.Any())
            //{
            //    context.Сurrencies.Add(new Models.Currency { Name = "Bitcoin", ShortName = "BTC" });

            //    context.Сurrencies.Add(new Models.Currency { Name = "Litecoin", ShortName = "LTC" });
            //    context.Сurrencies.Add(new Models.Currency { Name = "Ethereum", ShortName = "ETH" });
            //    context.Сurrencies.Add(new Models.Currency { Name = "Ripple", ShortName = "XPR" });
            //    context.Сurrencies.Add(new Models.Currency { Name = "Cardano", ShortName = "ADA" });
            //    context.Сurrencies.Add(new Models.Currency { Name = "Monero", ShortName = "XMR" });
            //    context.Сurrencies.Add(new Models.Currency { Name = "Ethereum Classic", ShortName = "ETC" });
            //    context.Сurrencies.Add(new Models.Currency { Name = "Bitcoin Gold", ShortName = "BTG" });

            //    context.SaveChanges();
            //}

        }
    }
}
