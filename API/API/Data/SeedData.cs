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
            if (admin == null)
            {
                var _admin = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@fler.com",
                    FirstName = "admin",
                    LastName = ""
                };
                var result = await usermanager.CreateAsync(_admin, "1234567890");
                if (result.Succeeded)
                {
                    await usermanager.AddToRoleAsync(_admin, "Admin");
                }
            }
            else
            {
                if (!await usermanager.IsInRoleAsync(admin, "Admin"))
                {
                    await usermanager.AddToRoleAsync(admin, "Admin");
                }
            }

            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category { Name = "Fashion & Accessories", Route = "fashion", parentId = null });
                context.Categories.Add(new Category { Name = "Jewelry", Route = "jewelry", parentId = null });
                context.Categories.Add(new Category { Name = "Home & Living", Route = "home", parentId = null });
                context.Categories.Add(new Category { Name = "Health & Beauty", Route = "health", parentId = null });
                context.Categories.Add(new Category { Name = "Special occasion", Route = "occasion", parentId = null });
                context.Categories.Add(new Category { Name = "Material for creation", Route = "material", parentId = null });

                context.SaveChanges();

                //Fashion & Accessories
                context.Categories.Add(new Category { Name = "For her", Route = "forher", parentId = context.Categories.Where(c => c.Route == "fashion").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "For him", Route = "forhim", parentId = context.Categories.Where(c => c.Route == "fashion").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Baby & kids", Route = "baby", parentId = context.Categories.Where(c => c.Route == "fashion").SingleOrDefault().Id });

                //Jewelry
                context.Categories.Add(new Category { Name = "Necklaces", Route = "necklaces", parentId = context.Categories.Where(c => c.Route == "jewelry").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Bracelets", Route = "bracelets", parentId = context.Categories.Where(c => c.Route == "jewelry").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Rings", Route = "rings", parentId = context.Categories.Where(c => c.Route == "jewelry").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Earrings", Route = "earrings", parentId = context.Categories.Where(c => c.Route == "jewelry").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Pendants", Route = "pendants", parentId = context.Categories.Where(c => c.Route == "jewelry").SingleOrDefault().Id });

                context.SaveChanges();

                //For her;
                context.Categories.Add(new Category { Name = "Clothing", Route = "womenclothing", parentId = context.Categories.Where(c => c.Route == "forher").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Bags", Route = "womenbags", parentId = context.Categories.Where(c => c.Route == "forher").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Purses", Route = "womenpurses", parentId = context.Categories.Where(c => c.Route == "forher").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Shoes", Route = "womenshoes", parentId = context.Categories.Where(c => c.Route == "forher").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Cases", Route = "womencases", parentId = context.Categories.Where(c => c.Route == "forher").SingleOrDefault().Id });
                context.SaveChanges();

                //Clothing
                context.Categories.Add(new Category { Name = "Dresses", Route = "dresses", parentId = context.Categories.Where(c => c.Route == "womenclothing").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Skirts", Route = "skirts", parentId = context.Categories.Where(c => c.Route == "womenclothing").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Tops", Route = "tops", parentId = context.Categories.Where(c => c.Route == "womenclothing").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Trousers & Shorts", Route = "trousers", parentId = context.Categories.Where(c => c.Route == "womenclothing").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Jackets & Coats", Route = "jackets", parentId = context.Categories.Where(c => c.Route == "womenclothing").SingleOrDefault().Id });

                //Bags
                context.Categories.Add(new Category { Name = "Handbags", Route = "womanhandbags", parentId = context.Categories.Where(c => c.Route == "womenbags").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Cross body & shoulder Bags", Route = "womancrossbody", parentId = context.Categories.Where(c => c.Route == "womenbags").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Cosmetic bags & pouches", Route = "womancosmeticbags", parentId = context.Categories.Where(c => c.Route == "womenbags").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Clutches", Route = "womanclutches", parentId = context.Categories.Where(c => c.Route == "womenbags").SingleOrDefault().Id });

                //Purses
                context.Categories.Add(new Category { Name = "Leather purses", Route = "womanleatherpurses", parentId = context.Categories.Where(c => c.Route == "womenpurses").SingleOrDefault().Id });
                context.Categories.Add(new Category { Name = "Coin purses", Route = "womancoinpurses", parentId = context.Categories.Where(c => c.Route == "womenpurses").SingleOrDefault().Id });



                context.SaveChanges();


            }
            if (!context.Products.Any())
            {
                var _category = context.Categories.Where(c => c.Route == "dresses").SingleOrDefault();
                if (_category != null)
                {
                    var _product = new Product { Name = "Вариант коктейльного платья для коктейльного вечера", Category = _category };
                    _product.Images = new List<Image>();
                    _product.Images.Add(new Image { Url = "1.jpg" });
                    context.Products.Add(_product);
                    context.SaveChanges();
                }
            }

        }
    }
}
