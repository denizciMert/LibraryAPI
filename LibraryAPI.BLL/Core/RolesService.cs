using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryAPI.BLL.Core
{
    public class RolesService
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>();

            foreach (var role in roles)
            {
                var roleName = role.ToString();
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            await CreateAdminUser(userManager);
        }

        private static async Task CreateAdminUser(UserManager<ApplicationUser> userManager)
        {
            const string adminUserName = "admin";
            const string adminEmail = "admin@poweruser.com";
            const string adminPassword = "Admin123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    UserRole = UserRole.Yönetici,
                    FirstName = "Admin",
                    LastName = "Poweruser",
                    PhoneNumber = "1234567890",
                    IdentityNo = "00000000000",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    DateOfRegister = DateTime.UtcNow,
                    Gender = Gender.Belirtilmedi,
                    Banned = false,
                    State = State.Eklendi,
                    UserImagePath = string.Empty
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, UserRole.Yönetici.ToString());
                }
            }
        }

    }
}
