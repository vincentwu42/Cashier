using Cashier.Data;
using Cashier.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cashier.Services
{
    public class RoleInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public static async Task Initialize(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //if (await roleManager.FindByNameAsync("Admin") == null)
            //{
            //    await roleManager.CreateAsync(new IdentityRole("Admin"));
            //}

            //if (await roleManager.FindByNameAsync("Cashier") == null)
            //{
            //    await roleManager.CreateAsync(new IdentityRole("Cashier"));
            //}

            //string username = "admin";
            //string password = "admin123.";
            //if (await userManager.FindByIdAsync(username) == null)
            //{
            //    IdentityUser admin = new IdentityUser { UserName = username };
            //    IdentityResult result = await userManager.CreateAsync(admin, password);
            //    if (result.Succeeded)
            //    {
            //        await userManager.AddToRoleAsync(admin, "Admin");
            //    }
            //}
        }
    }
}
