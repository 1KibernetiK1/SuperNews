using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SuperNews.DataAccessLayer
{
    public class DataSeeder
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.Users.Count() > 0) return;

            var user1 = new IdentityUser
            {
                UserName = "admin@ya.ru",
                Email = "admin@ya.ru",
                EmailConfirmed = true
            };
            IdentityResult userResult = userManager.CreateAsync(user1, "1Qwerty!").Result;
            if (userResult.Succeeded)
            {
                userResult = userManager.AddToRoleAsync(user1, "Administrator").Result;
            }


        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = new string[]
            {
                "Administrator",
                "Redactor",
                "Moderator",
                "Subscriber",
                "Guest"
            };
            if (roleManager.Roles.Count() > 0) return;

            foreach (var roleName in roleNames)
            {
                var role = new IdentityRole { Name = roleName };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

      
    }
}
