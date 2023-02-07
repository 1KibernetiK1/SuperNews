using Microsoft.AspNetCore.Identity;
using SuperNews.UsersRoles;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SuperNews.DataAccessLayer
{
    public static class DataSeeder
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
                userResult = userManager.AddToRoleAsync(user1, AppRoles.Administrator).Result;
            }


        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = new string[]
            {
                AppRoles.Administrator,
                AppRoles.Redactor,
                AppRoles.Moderator,
                AppRoles.Subscriber,
                AppRoles.Guest
            };
            if (roleManager.Roles.Count() > 0) return;

            foreach (var roleName in roleNames)
            {
                var role = new IdentityRole { Name = roleName };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static async Task AddPermissionClaim(
             this RoleManager<IdentityRole> roleManager,
             IdentityRole role,
             string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}
