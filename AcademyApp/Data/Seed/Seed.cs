using AcademyApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AcademyApp.Data.Seed
{
    public static class Seed
    {
        public static async Task AddRoles(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        public static async Task AddAdmin(UserManager<UserIdentity> userManager)
        {
            var admin = new UserIdentity
            {
                Name = "admin",
                LastName = "admin",
                Email = "admin@admin.1",
                UserName = "autoAdmin",
            };
            var check = await userManager.FindByEmailAsync(admin.Email);
            if (check == null)
            {
                await userManager.CreateAsync(admin, "test123");

                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
